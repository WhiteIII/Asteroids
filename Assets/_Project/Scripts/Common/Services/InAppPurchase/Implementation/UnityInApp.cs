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
    public partial class UnityInApp : IInApp, IInitializable, IDisposable
    {
        private readonly InAppPurchaseIdList _inAppPurchaseIdList;
        private readonly StoreController _storeController = UnityIAPServices.StoreController();

        private bool _isPurchaseInProgress;
        private PurchaseCompleteStatus _currentPurchaseCompleteStatus = PurchaseCompleteStatus.NotDefined;

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

            _isPurchaseInProgress = true;
            _storeController.PurchaseProduct(productId);
            bool completeStatus;
            await UniTask.WaitWhile(() => _isPurchaseInProgress);

            if (_currentPurchaseCompleteStatus == PurchaseCompleteStatus.Success)
                completeStatus = true;
            else if (_currentPurchaseCompleteStatus == PurchaseCompleteStatus.Failed)
                completeStatus = false;
            else
                throw new Exception($"Failed to buy product {productId}");

            _currentPurchaseCompleteStatus = PurchaseCompleteStatus.NotDefined;
            return completeStatus;
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
            CatalogProvider catalogProvider = new();
            foreach (PurchaseProductData productData in _inAppPurchaseIdList.Products)
            {
                catalogProvider.AddProduct(
                    productData.ProductId,
                    productData.ProductType,
                    new StoreSpecificIds
                    {
                        { productData.GoogleStoreSpecificId, GooglePlay.Name },
                        { productData.MacStoreSpecificId, MacAppStore.Name },
                        { productData.AppleStoreSpecificId, AppleAppStore.Name }
                    });
            }

            return catalogProvider;
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
            Product product = _storeController.GetProductById(productId);
            try
            {
                Debug.Log($"Pending purchase: {product.definition.id}");

                _storeController.ConfirmPurchase(pending);

                Debug.Log($"Confirmed purchase: {product.definition.id}");
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error processing pending order: {exception.Message}");
                _currentPurchaseCompleteStatus = PurchaseCompleteStatus.Failed;
                _isPurchaseInProgress = false;
            }
        }

        private void OnPurchaseFailed(FailedOrder order)
        {
            _currentPurchaseCompleteStatus = PurchaseCompleteStatus.Failed;
            _isPurchaseInProgress = false;
        }

        private void OnPurchaseConfirmed(Order order)
        {
            if (order is FailedOrder failedOrder)
            {
                Debug.LogWarning($"Confirmation failed: {failedOrder.FailureReason}");
                return;
            }

            Product purchasedProduct = order.CartOrdered.Items().First().Product;
            string productId = purchasedProduct.definition.id;

            Debug.Log($"Purchase confirmed: {productId} | Tx: {order.Info.TransactionID}");
            _currentPurchaseCompleteStatus = PurchaseCompleteStatus.Success;
            _isPurchaseInProgress = false;
        }
    }

    public partial class UnityInApp
    {
        private enum PurchaseCompleteStatus
        {
            Success,
            Failed,
            NotDefined
        }
    }
}