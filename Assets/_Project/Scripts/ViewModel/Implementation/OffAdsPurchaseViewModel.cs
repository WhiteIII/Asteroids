using _Project.Scripts.Common.Services.InAppPurchase.Base;
using _Project.Scripts.Common.Services.InAppPurchase.Data;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class OffAdsPurchaseViewModel : InAppPurchaseViewModel
    {
        public OffAdsPurchaseViewModel( IInApp inApp) : base(inApp, "off_ads") { }
    }
}