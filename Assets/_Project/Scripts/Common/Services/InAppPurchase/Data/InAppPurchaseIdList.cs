using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.Common.Services.InAppPurchase.Data
{
    [CreateAssetMenu(fileName = "InAppPurchaseIdList", menuName = "_Project/InAppPurchaseIdList")]
    public class InAppPurchaseIdList : ScriptableObject
    {
        [SerializeField] private PurchaseProductData[] _products;

        internal IEnumerable<PurchaseProductData> Products => _products;
    }

    [Serializable]
    internal class PurchaseProductData
    {
        public string ProductId;
        public ProductType ProductType;
        public string GoogleStoreSpecificId;
        public string MacStoreSpecificId;
        public string AppleStoreSpecificId;
    }
}