using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _Project.Scripts.Common.Services.InAppPurchase.Data
{
    [CreateAssetMenu(fileName = "InAppPurchaseIdList", menuName = "_Project/InAppPurchaseIdList")]
    public partial class InAppPurchaseIdList : ScriptableObject
    {
        [SerializeField] private PurchaseProductData[] _ids;
        
        internal IEnumerable<PurchaseProductData> Ids => _ids;

        public string GetIdByProduct(InAppPurchaseProduct product)
        {
            /*foreach (PurchaseProductData productData in _ids)
            {
                if (product == productData.Product)
                    return productData.ProductId;
            }*/

            throw new Exception("Product id not found!");
        }
    }

    public partial class InAppPurchaseIdList
    {
        [Serializable]
        internal class PurchaseProductData
        {
            public string ProductId;
            //public  
        }
    }

    public class UnityIAPIdList
    {
        
    }
}