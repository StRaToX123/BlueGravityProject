using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantCat : Merchant
{
    
    public GameObject transcationMenu;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
        Debug.Log("KUPUJEMO");
        //GameObject newTransactionMenu = GameObject.Instantiate(transcationMenu);
        //newTransactionMenu.GetComponent<TransactionMenu>().Setup(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>(), this);
    }

    // Setup the transaction menu so that the player is the seller, and this merchant is the buyer
    public void InteractionOptionSellCallback()
    {
        Debug.Log("Prodajemo");
    }
}
