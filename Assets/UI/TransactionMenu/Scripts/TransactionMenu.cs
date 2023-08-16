using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionMenu : MonoBehaviour
{
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpSellerName;
    public TextMeshProUGUI tmpControl01;
    public TextMeshProUGUI tmpControl02;
    public TextMeshProUGUI tmpAvailableCoins;
    public RectTransform upArrow;
    public RectTransform downArrow;
    // The amount of items in the array is equal to the numberOfItemsOnScreen variable
    public List<TransactionMenuItem> transactionMenuItems;
    private GameObject merchantTransactionMenuViewPrefab;
    private PlayerData playerData;
    private List<ItemDataSO> buyerInventory;
    private List<ItemDataSO> sellerInventory;
    private int selectedTransactionMenuItemIndex;
    private int selectedMerchantItemIndex;

    // Must be called before the TransactionMenu is enabled i.e. the Start method runs
    public void ShowTransactionMenu(string title,
        string sellerName, 
        string textControl01, 
        List<ItemDataSO> sellerInventory, 
        List<ItemDataSO> buyerInventory)
    {
        // Setup the text mesh pro displays
        tmpTitle.text = title;
        tmpSellerName.text = sellerName;
        tmpControl01.text = textControl01;
        this.sellerInventory = sellerInventory;
        this.buyerInventory = buyerInventory;
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        tmpAvailableCoins.text = "Available    : " + playerData.numberOfCoins.ToString();
        // Setup the items
        int excessTransactionMenuItemCount = transactionMenuItems.Count - sellerInventory.Count;
        if (excessTransactionMenuItemCount > 0)
        {
            for (int i = 0; i < excessTransactionMenuItemCount; i++)
            {
                GameObject.Destroy(transactionMenuItems[transactionMenuItems.Count - 1].gameObject);
                transactionMenuItems.RemoveAt(transactionMenuItems.Count - 1);
            }
        }

        for (int i = 0; i < transactionMenuItems.Count; i++)
        {
            transactionMenuItems[i].SetupItem(sellerInventory[i].itemName, sellerInventory[i].price.ToString(), sellerInventory[i].sprite);
        }

        selectedTransactionMenuItemIndex = 0;
        // Turn on the gameobject
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sellerInventory.Count > 0)
        {
            transactionMenuItems[selectedTransactionMenuItemIndex].background.color = new Color(255.0f, 255.0f, 255.0f);
            // Update the index of the selected item
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedTransactionMenuItemIndex++;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedTransactionMenuItemIndex--;
            }

            if (selectedTransactionMenuItemIndex == transactionMenuItems.Count)
            {
                selectedTransactionMenuItemIndex = transactionMenuItems.Count - 1;
            }

            if (selectedTransactionMenuItemIndex < 0)
            {
                selectedTransactionMenuItemIndex = 0;
            }

            transactionMenuItems[selectedTransactionMenuItemIndex].GetComponent<Image>().color = Color.grey;
            if (Input.GetButtonDown("Interact"))
            {
                //interactionOptionsButtons[selectedInteractionOptionIndex].onClick.Invoke();
            }

            Debug.Log("selectedTransactionMenuItemIndex : " + selectedTransactionMenuItemIndex.ToString());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //interactionOptionsButtons[selectedInteractionOptionIndex].onClick.Invoke();
        }

        

    }



}
