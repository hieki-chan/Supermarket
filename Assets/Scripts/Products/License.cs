﻿using Supermarket.Pricing;
using System.Collections.Generic;
using UnityEngine;

namespace Supermarket.Products
{
    [CreateAssetMenu(fileName = "License", menuName = "Licenses")]
    public class License : ScriptableObject
    {
        public string LicenseName => m_LicenseName;
        [SerializeField, TextArea] private string m_LicenseName;

        public int LicenseId => m_LicenseId;
        [SerializeField] private int m_LicenseId;

        public string Description=> m_Description;
        [SerializeField, TextArea] private string m_Description;

        public unit Cost => m_Cost;
        [SerializeField] private unit m_Cost;

        public int StoreLeveRequired => m_RequireStoreLevel;
        [SerializeField, Min(0)] private int m_RequireStoreLevel;



        public List<ProductInfo> AllowedPurchaseAndSellProducts => m_AllowedPurchaseAndSellProducts;
        [SerializeField] private List<ProductInfo> m_AllowedPurchaseAndSellProducts = new List<ProductInfo>();
    }
}