using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantRogue : Merchant
{
    public GameObject transactionMenuPrefab;
    // UI renderable version of the merchant
    public GameObject merchantTransactionMenuViewPrefab;
    public GameObject dialogueBox;
    private bool skipAFrame;
    private bool isTransactionMenuActive;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isTransactionMenuActive = false;
        skipAFrame = false;
    }

    // Update is called once per frame
    public override void Update()
    {
        if ((isTransactionMenuActive == false) && (dialogueBox.activeSelf == false))
        {
            base.Update();
        }

        if(dialogueBox.activeSelf == true) 
        {
            if (skipAFrame == true)
            {
                skipAFrame = false;
            }
            else
            {
                if (Input.GetButtonDown("Interact"))
                {
                    dialogueBox.SetActive(false);
                }
            }
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
        // We can create custom dialogue logic here.
        // But for now we just spawn in the dialogue box.
        // The update function will fire of an interact action in the same frame 
        // in which we activated the dialogue box, the skipAFrame prevents that
        skipAFrame = true;
        dialogueBox.SetActive(true);
    }

    void OnTransactionMenuCloseCallback()
    {
        isTransactionMenuActive = false;
    }
}
