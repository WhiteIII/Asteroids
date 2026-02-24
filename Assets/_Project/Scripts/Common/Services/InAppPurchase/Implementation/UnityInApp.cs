using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.Common.Services.InAppPurchase.Base;
using _Project.Scripts.Common.Services.InAppPurchase.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Common.Services.InAppPurchase.Implementation
{
    public class UnityInApp : IInApp, IInitializable, IDisposable
    {
        private Action<string> _onPurchaseSucceed;
        private Action<string> _onPurchaseCancelled;

        private readonly InAppPurchaseIdList _inAppPurchaseIdList;
        private readonly StoreController _storeController = UnityIAPServices.StoreController();
        
        private bool _isPurchaseInProgress;

        public UnityInApp(InAppPurchaseIdList inAppPurchaseIdList) => 
            _inAppPurchaseIdList = inAppPurchaseIdList;

        public async void Initialize()
        {
            SubscribeToAllStoreControllerEvents();
            
            await InitializeStoreControllerAsync();
        }
        
        public void Dispose() => 
            UnSubscribeToAllStoreControllerEvents();

        public async UniTask<bool> TryBuyProductAsync(string productId)
        {
            if (_isPurchaseInProgress)
                return false;
            
            _storeController.PurchaseProduct(productId);
            using CancellationTokenSource cancellationTokenSource = new();
            (int winArgumentIndex, string result1, string result2) completePurchaseData = await UniTask.WhenAny(
                WaitEventAsync(_onPurchaseSucceed, cancellationTokenSource.Token), 
                WaitEventAsync(_onPurchaseCancelled, cancellationTokenSource.Token));
            cancellationTokenSource.Cancel();
            if (completePurchaseData.winArgumentIndex == 0 && completePurchaseData.result1 == productId)
                return true;
            return false;
        }

        private async UniTask InitializeStoreControllerAsync()
        {
            try
            {
                await _storeController.Connect().ContinueWith(_ => 
                    GetCatalogProvider().FetchProducts(list => _storeController.FetchProducts(list)));
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        private void SubscribeToAllStoreControllerEvents()
        {
            _storeController.OnProductsFetched += OnProductsFetched;
            _storeController.OnProductsFetchFailed += OnProductsFetchFailed;
            
            _storeController.OnPurchasesFetched += OnPurchasesFetched;
            _storeController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
            
            _storeController.OnPurchasePending += OnPurchasesPending;
            _storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;
            _storeController.OnPurchaseFailed += OnPurchaseFailed;

            _storeController.OnStoreDisconnected += OnStoreDisconnected;
        }

        private void UnSubscribeToAllStoreControllerEvents()
        {
            _storeController.OnProductsFetched -= OnProductsFetched;
            _storeController.OnProductsFetchFailed -= OnProductsFetchFailed;
            
            _storeController.OnPurchasesFetched -= OnPurchasesFetched;
            _storeController.OnPurchasesFetchFailed -= OnPurchasesFetchFailed;
            
            _storeController.OnPurchasePending -= OnPurchasesPending;
            _storeController.OnPurchaseConfirmed -= OnPurchaseConfirmed;
            _storeController.OnPurchaseFailed -= OnPurchaseFailed;

            _storeController.OnStoreDisconnected -= OnStoreDisconnected;
        }

        private CatalogProvider GetCatalogProvider()
        {
            throw new NotImplementedException();
        }

        private void OnProductsFetched(List<Product> products)
        {
            LogProductsFetched(products);
            _storeController.FetchPurchases();
        } 

        private void LogProductsFetched(List<Product> products)
        {
            Debug.Log($"Products fetched: {products.Count}");
            foreach (var p in products)
                Debug.Log($"{p.definition.id} | {p.metadata.localizedTitle} | {p.metadata.localizedPriceString}");
        }
        
        private void OnProductsFetchFailed(ProductFetchFailed failure) => 
            Debug.LogError($"Product fetch failed: {failure.FailureReason}");

        private void OnPurchasesFetched(Orders orders) { }

        private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure) => 
            Debug.LogError($"Purchases fetch failed: {failure.FailureReason}");

        private void OnStoreDisconnected(StoreConnectionFailureDescription desc) => 
            Debug.LogError($"Store disconnected: {desc.Message}");

        private void OnPurchasesPending(PendingOrder pending)
        {
            Debug.Log($"Full receipt JSON: {pending.Info.Receipt}");

            CartItem firstItem = pending.CartOrdered.Items().First();
            string productId = firstItem.Product.definition.id;
    
            if (string.IsNullOrEmpty(productId))
            {
                Debug.LogError("Pending order has no product id.");
                _onPurchaseCancelled?.Invoke("No product id in pending order");
                return;
            }

            Product product = _storeController.GetProductById(productId);
            try
            {
                if (product == null)
                {
                    Debug.LogError($"Product not found in controller: {productId}");
                    _onPurchaseCancelled?.Invoke($"Product not found: {productId}");
                    return;
                }

                Debug.Log($"Pending purchase: {product.definition.id}");

                _storeController.ConfirmPurchase(pending);
                _onPurchaseSucceed?.Invoke(product.definition.id);
                
                Debug.Log($"Confirmed purchase: {product.definition.id}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Error processing pending order: {e.Message}");
                _onPurchaseCancelled?.Invoke(product.definition.id);
            }
            _isPurchaseInProgress = false;
        }

        private void OnPurchaseFailed(FailedOrder order) =>
            _onPurchaseCancelled?.Invoke(order.Info.PurchasedProductInfo.First().productId);

        private void OnPurchaseConfirmed(Order order)
        {
            _isPurchaseInProgress = false;

            if (order is FailedOrder failedOrder)
            {
                Debug.LogWarning($"Confirmation failed: {failedOrder.FailureReason}");
                return;
            }

            var purchasedProduct = order.CartOrdered.Items().First().Product;

            Debug.Log($"Purchase confirmed: {purchasedProduct?.definition.id} | Tx: {order.Info?.TransactionID}");
            _onPurchaseSucceed?.Invoke($"Purchase confirmed: { purchasedProduct?.definition.id}");
        }

        private UniTask<string> WaitEventAsync(Action<string> action, CancellationToken cancellationToken = default)
        {
            UniTaskCompletionSource<string> completionSource = new();

            Action<string> handler = null;
            handler = (value) =>
            {
                action -= handler;
                completionSource.TrySetResult(value);
            };

            action += handler;

            cancellationToken.Register(() =>
            {
                action -= handler;
                completionSource.TrySetCanceled(cancellationToken);
            });

            return completionSource.Task;
        }
    }
}