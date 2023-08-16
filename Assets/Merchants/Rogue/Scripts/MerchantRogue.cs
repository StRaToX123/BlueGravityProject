using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantRogue : Merchant
{
    public GameObject transactionMenuPrefab;
    // UI renderable version of the merchant
    public GameObject merchantTransactionMenuViewPrefab;
    private bool isTransactionMenuActive;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isTransactionMenuActive = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isTransactionMenuActive == false)
        {
            base.Update();
        }
    }

    // Setup the transaction menu so that the player is the buyer, and this merchant is the seller
    public void InteractionOptionBuyCallback()
    {
        TransactionMenu transactionMenu = GameObject.Instantiate(transactionMenuPrefab).GetComponent<TransactionMenu>();
        transactionMenu.OnClose += OnTransactionMenuCloseCallback;
        transactionMenu.ShowTransactionMenu("BUY MENU",
            merchantName,
            merchantTransactionMenuViewPrefab,
            "E: Purchase",
            itemInventory,
            GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInventory>());
        isTransactionMenuActive = true;
    }

    public void InteractionOptionTalkCallback()
    {
        Debug.Log("WOOAH");
    }

    void OnTransactionMenuCloseCallback()
    {
        isTransactionMenuActive = false;
    }
}
