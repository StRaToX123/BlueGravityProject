using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantCat : Merchant
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

    public override void StartInteraction()
    {
        // This merchant has no custom logic for it's interaction options
        // For example, merchants can show different dialogue options depending
        // on if the player has completed scertain goals.
        // This is where we can load a different interactionOptions GameObject or alter the
        // already existing one
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

    // Setup the transaction menu so that the player is the seller, and this merchant is the buyer
    public void InteractionOptionSellCallback()
    {
        TransactionMenu transactionMenu = GameObject.Instantiate(transactionMenuPrefab).GetComponent<TransactionMenu>();
        transactionMenu.OnClose += OnTransactionMenuCloseCallback;
        transactionMenu.ShowTransactionMenu("SELL ITEMS",
            merchantName,
            merchantTransactionMenuViewPrefab,
            "E: Sell Item",
            GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInventory>(),
            itemInventory);
        isTransactionMenuActive = true;
    }

    void OnTransactionMenuCloseCallback()
    {
        isTransactionMenuActive = false;
    }
}
