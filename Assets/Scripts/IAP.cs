using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OnePF;

public class IAP : MonoBehaviour
{
    public const string SKU_10 = "buy10k_coin";
    public const string SKU_50 = "buy50k_coin";
    public const string SKU_100 = "buy100k_coin";
    public const string SKU_Life = "buylife";

    public const string googleKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAql2H3kRqgqZJLfqXklQYhZJYJ+EBbgBwB0tLJeEB2mJbUWtMo8ASXR3kami1Opl2CtWkQEPa3DNNBxBUwZldWzvC9q++YYmy6XRqnoupvgUsMpn/aA4DStn0UbL12wjKi7ZHXqgMiMMSfidFGIGXuM0H4eLVZ7/Hj7ZrDSjBpHrFMEv2kdJTtQHr+Yn0Ek0gDnkDvXxx4IvyaDk3Z+VV+Dg29CkS4VSNLYxYLqUw1oLPqSmPpppirlM5ErUNqjr0KOhN93P7+KNuSa0HPmOte/rXBrwch3LEOcUifhhsYjft4z8FKELDlhFvMPrpqaayxrDDK94omuL/LCUedjh8cQIDAQAB";

    private void Awake()
    {
        // Subscribe to all billing events
        OpenIABEventManager.billingSupportedEvent += OnBillingSupported;
        OpenIABEventManager.billingNotSupportedEvent += OnBillingNotSupported;
        OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceded;
        OpenIABEventManager.purchaseFailedEvent += OnPurchaseFailed;
        OpenIABEventManager.consumePurchaseSucceededEvent += OnConsumePurchaseSucceeded;
        OpenIABEventManager.consumePurchaseFailedEvent += OnConsumePurchaseFailed;
        OpenIABEventManager.transactionRestoredEvent += OnTransactionRestored;
        OpenIABEventManager.restoreSucceededEvent += OnRestoreSucceeded;
        OpenIABEventManager.restoreFailedEvent += OnRestoreFailed;
    }

    private void OnDestroy()
    {
        // Unsubscribe to avoid nasty leaks
        OpenIABEventManager.billingSupportedEvent -= OnBillingSupported;
        OpenIABEventManager.billingNotSupportedEvent -= OnBillingNotSupported;
        OpenIABEventManager.purchaseSucceededEvent -= OnPurchaseSucceded;
        OpenIABEventManager.purchaseFailedEvent -= OnPurchaseFailed;
        OpenIABEventManager.consumePurchaseSucceededEvent -= OnConsumePurchaseSucceeded;
        OpenIABEventManager.consumePurchaseFailedEvent -= OnConsumePurchaseFailed;
        OpenIABEventManager.transactionRestoredEvent -= OnTransactionRestored;
        OpenIABEventManager.restoreSucceededEvent -= OnRestoreSucceeded;
        OpenIABEventManager.restoreFailedEvent -= OnRestoreFailed;
    }

    void Start()
    {

        OpenIAB.mapSku(SKU_10, OpenIAB_Android.STORE_GOOGLE, "buy10k_coin");
        OpenIAB.mapSku(SKU_50, OpenIAB_Android.STORE_GOOGLE, "buy50k_coin");
        OpenIAB.mapSku(SKU_100, OpenIAB_Android.STORE_GOOGLE, "buy100k_coin");
        OpenIAB.mapSku(SKU_Life, OpenIAB_Android.STORE_GOOGLE, "buylife");
        var options = new OnePF.Options();
        options.checkInventory = false;
        options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
        options.storeKeys.Add(OpenIAB_Android.STORE_GOOGLE, googleKey);
        OpenIAB.init(options);

    }

    public void BuyCoin10k()
    {
        OpenIAB.purchaseProduct(SKU_10);
    }

    public void BuyCoin50k()
    {
        OpenIAB.purchaseProduct(SKU_50);
    }

    public void BuyCoin100k()
    {
        OpenIAB.purchaseProduct(SKU_100);
    }

    public void buylife()
    {
        OpenIAB.purchaseProduct(SKU_Life);
    }

    private void OnBillingSupported()
    {
        Debug.Log("Billing is supported");
        OpenIAB.queryInventory(new string[] { SKU_10, SKU_50, SKU_100, SKU_Life });
    }

    private void OnBillingNotSupported(string error)
    {
        Debug.Log("Billing not supported: " + error);
    }

    private void OnQueryInventorySucceeded(Inventory inventory)
    {
        Debug.Log("Query inventory succeeded: " + inventory);
    }

    private void OnQueryInventoryFailed(string error)
    {
        Debug.Log("Query inventory failed: " + error);
    }

    private void OnPurchaseSucceded(Purchase purchase)
    {
        Debug.Log("Purchase succeded: " + purchase.Sku + "; Payload: " + purchase.DeveloperPayload);

        switch (purchase.Sku)
        {
            case SKU_10:
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 10000);
                break;
            case SKU_50:
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 50000);
                break;
            case SKU_100:
                ProtectedPrefs.SetInt("Coins", ProtectedPrefs.GetInt("Coins") + 100000);
                break;
            case SKU_Life:
                ProtectedPrefs.SetInt("mLamp", ProtectedPrefs.GetInt("mLamp") + 10);
                break;
            default:
                Debug.LogWarning("Unknown SKU: " + purchase.Sku);
                break;
        }
    }

    private void OnPurchaseFailed(int errorCode, string error)
    {
        Debug.Log("Purchase failed: " + error);
    }

    private void OnConsumePurchaseSucceeded(Purchase purchase)
    {
        Debug.Log("Consume purchase succeded: " + purchase.ToString());
    }

    private void OnConsumePurchaseFailed(string error)
    {
        Debug.Log("Consume purchase failed: " + error);
    }

    private void OnTransactionRestored(string sku)
    {
        Debug.Log("Transaction restored: " + sku);
    }

    private void OnRestoreSucceeded()
    {
        Debug.Log("Transactions restored successfully");
    }

    private void OnRestoreFailed(string error)
    {
        Debug.Log("Transaction restore failed: " + error);
    }

}