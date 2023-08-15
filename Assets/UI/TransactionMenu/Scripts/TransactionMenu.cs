using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionMenu : MonoBehaviour
{
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpMerchantName;
    public TextMeshProUGUI tmpControl01;
    public TextMeshProUGUI tmpControl02;
    public TextMeshProUGUI tmpAvailableCoins;
    public RectTransform upArrow;
    public RectTransform downArrow;
    public GameObject itemPrefab;
    public uint numberOfItemsOnScreen;
    public GameObject background;
    private GameObject merchantTransactionMenuViewPrefab;
    private PlayerData playerData;
    // The amount of items in the array is equal to the numberOfItemsOnScreen variable
    private List<TransactionMenuItem> transactionMenuItems;
    private Merchant merchant;
    private bool isMerchantSelling;
    private int selectedTransactionMenuItemIndex;

    // Must be called before the TransactionMenu is enabled i.e. the Start method runs
    public void ShowTransactionMenu(Merchant merchant, bool isMerchantSelling)
    {
        this.merchant = merchant;
        this.isMerchantSelling = isMerchantSelling;
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        // Spawn in the appropriate amount of transaction menu item prefabs
        transactionMenuItems.Capacity = (int)numberOfItemsOnScreen;
        for (int i = 0; i < numberOfItemsOnScreen; i++)
        {
            GameObject newItem = GameObject.Instantiate(itemPrefab);
            RectTransform newItemRectTransform = newItem.GetComponent<RectTransform>();
            newItem.transform.parent = background.transform;
            Vector3 newItemPosition = newItemRectTransform.position;
            newItemPosition.y -= i * 17.43f;
            newItemRectTransform.position = newItemPosition;
            transactionMenuItems.Add(newItem.GetComponent<TransactionMenuItem>());
        }

        // Setup the text
        if (isMerchantSelling == true)
        {
            tmpTitle.text = "BUY MENU";
            tmpControl01.text = "E : Purchase";
        }
        else
        {
            tmpTitle.text = "SELL ITEMS";
            tmpControl01.text = "E : Sell Item";
        }

        tmpMerchantName.text = merchant.merchantName;
        tmpAvailableCoins.text = "Available    : " + playerData.numberOfCoins.ToString();

        // Setup the items
        for (int i = 0; i < numberOfItemsOnScreen; i++)
        {
            transactionMenuItems[i].SetupItem(merchant.items[i].itemName, merchant.items[i].price.ToString(), merchant.items[i].sprite);
        }

        selectedTransactionMenuItemIndex = 0;
        // Turn on the gameobject
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
