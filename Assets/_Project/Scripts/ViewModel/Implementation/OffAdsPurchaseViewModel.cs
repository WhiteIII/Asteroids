using _Project.Scripts.Common.Services.InAppPurchase.Base;

namespace _Project.Scripts.ViewModel.Implementation
{
    public class OffAdsPurchaseViewModel : InAppPurchaseViewModel
    {
        public OffAdsPurchaseViewModel( IInApp inApp) : base(inApp, "off_ads") { }
    }
}