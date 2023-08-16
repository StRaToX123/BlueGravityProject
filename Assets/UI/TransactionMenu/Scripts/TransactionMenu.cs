using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.UI;

public class TransactionMenu : MonoBehaviour
{
    [HideInInspector]
    public delegate void OnCloseAction();
    [HideInInspector]
    public event OnCloseAction OnClose;
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpSellerName;
    public TextMeshProUGUI tmpControl01;
    public TextMeshProUGUI tmpControl02;
    public TextMeshProUGUI tmpPlayerAvailableCoins;
    public RectTransform upScrollArrow;
    public RectTransform downScrollArrow;
    public RectTransform background;
    // The amount of items in the array is equal to the numberOfItemsOnScreen variable
    public List<TransactionMenuItem> transactionMenuItems;
    private GameObject merchantTransactionMenuViewPrefab;
    private ItemInventory buyerInventory;
    private ItemInventory sellerInventory;
    private int selectedTransactionMenuItemIndex;
    private int selectedSellerItemIndex;
    // These two variables keep track of the indexes of the items
    // which are at the top and bottom of the viewable list.
    // Once the selectedSellerItemIndex crosses one of these bounds
    // we will have to scroll the list
    private int transactionMenuItemsWindowLowerBoundIndex;
    private int transactionMenuItemsWindowUpperBoundIndex;
    private bool scrollingEnabled;

    // Must be called before the TransactionMenu is enabled i.e. the Start method runs
    public void ShowTransactionMenu(string title,
        string sellerName,
        GameObject sellerTransactionMenuViewPrefab,
        string textControl01,
        ItemInventory sellerInventory,
        ItemInventory buyerInventory)
    {
        // Setup the text mesh pro displays
        tmpTitle.text = title;
        tmpSellerName.text = sellerName;
        tmpControl01.text = textControl01;
        this.sellerInventory = sellerInventory;
        this.buyerInventory = buyerInventory;
        UpdatePlayerAvailableCoinsText();
        // Setup the items
        int excessTransactionMenuItemCount = transactionMenuItems.Count - sellerInventory.items.Count;
        if (excessTransactionMenuItemCount > 0)
        {
            downScrollArrow.gameObject.SetActive(false);
            scrollingEnabled = false;
            for (int i = 0; i < excessTransactionMenuItemCount; i++)
            {
                GameObject.Destroy(transactionMenuItems[transactionMenuItems.Count - 1].gameObject);
                transactionMenuItems.RemoveAt(transactionMenuItems.Count - 1);
            }
        }
        else
        {
            scrollingEnabled = true;
        }

        // Setup the sellerTransactionMenuViewPrefab
        RectTransform newSellerTransactionMenuViewPrefab = GameObject.Instantiate(sellerTransactionMenuViewPrefab).GetComponent<RectTransform>();
        Vector3 previousNewSellerTransactionMenuViewPrefabPosition = newSellerTransactionMenuViewPrefab.position;
        Vector3 previousNewSellerTransactionMenuViewPrefabScale = newSellerTransactionMenuViewPrefab.localScale;
        newSellerTransactionMenuViewPrefab.gameObject.transform.SetParent(background.transform, false);
        newSellerTransactionMenuViewPrefab.localPosition = previousNewSellerTransactionMenuViewPrefabPosition;
        newSellerTransactionMenuViewPrefab.localScale = previousNewSellerTransactionMenuViewPrefabScale;

        // Setup the indexes
        selectedTransactionMenuItemIndex = 0;
        selectedSellerItemIndex = 0;
        transactionMenuItemsWindowUpperBoundIndex = 0;
        transactionMenuItemsWindowLowerBoundIndex = transactionMenuItems.Count - 1;
        upScrollArrow.gameObject.SetActive(false);
        UpdateItems();

        // Turn on the gameobject
        this.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sellerInventory.items.Count > 0)
        {
            transactionMenuItems[selectedTransactionMenuItemIndex].background.color = new Color(255.0f, 255.0f, 255.0f);
            // Update the index of the selected item
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedTransactionMenuItemIndex++;
                selectedSellerItemIndex++;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedTransactionMenuItemIndex--;
                selectedSellerItemIndex--;
            }

            ClampIndexes();
            // Color the currenly selected transaction menu item
            transactionMenuItems[selectedTransactionMenuItemIndex].GetComponent<Image>().color = Color.grey;

            #region Scrolling
            if (scrollingEnabled == true)
            {
                // Update the transaction menu item window indexes
                // These values are used to check if we need to scroll the item list
                if (selectedSellerItemIndex > transactionMenuItemsWindowLowerBoundIndex)
                {
                    // We need to scroll the items list down
                    transactionMenuItemsWindowLowerBoundIndex = selectedSellerItemIndex;
                    transactionMenuItemsWindowUpperBoundIndex = transactionMenuItemsWindowLowerBoundIndex - (transactionMenuItems.Count - 1);
                }

                if (selectedSellerItemIndex < transactionMenuItemsWindowUpperBoundIndex)
                {
                    // We need to scroll the items list up
                    transactionMenuItemsWindowUpperBoundIndex = selectedSellerItemIndex;
                    transactionMenuItemsWindowLowerBoundIndex = transactionMenuItemsWindowUpperBoundIndex + (transactionMenuItems.Count - 1);
                }

                // Check to see if the down scroll arrow should be displayed
                if (transactionMenuItemsWindowLowerBoundIndex == (sellerInventory.items.Count - 1))
                {
                    downScrollArrow.gameObject.SetActive(false);
                }
                else
                {
                    downScrollArrow.gameObject.SetActive(true);
                }

                // Check to see if the up scroll arrow should be displayed
                if (transactionMenuItemsWindowUpperBoundIndex == 0)
                {
                    upScrollArrow.gameObject.SetActive(false);
                }
                else
                {
                    upScrollArrow.gameObject.SetActive(true);
                }
            }
            #endregion

            // Sell an item
            if (Input.GetButtonDown("Interact"))
            {
                // We can only purchase items which we can afford
                if ((buyerInventory.hasInfiniteCoins == true)
                    ||
                    (buyerInventory.numberOfCoins >= sellerInventory.items[selectedSellerItemIndex].price))
                {
                    // If there are about to be less than max drawable items on screen,
                    // we need to start destroying transaction menu item game objects
                    if (sellerInventory.items.Count <= transactionMenuItems.Count)
                    {
                        scrollingEnabled = false;
                        Destroy(transactionMenuItems[transactionMenuItems.Count - 1].gameObject);
                        transactionMenuItems.RemoveAt(transactionMenuItems.Count - 1);
                    }

                    buyerInventory.numberOfCoins -= sellerInventory.items[selectedSellerItemIndex].price;
                    sellerInventory.numberOfCoins += sellerInventory.items[selectedSellerItemIndex].price;
                    buyerInventory.items.Add(sellerInventory.items[selectedSellerItemIndex]);
                    sellerInventory.items.RemoveAt(selectedSellerItemIndex);
                    UpdatePlayerAvailableCoinsText();
                    ClampIndexes();
                }
            }

            UpdateItems();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Close the transaction menu and return control back to the merchant
            if (OnClose != null)
            {
                OnClose();
            }

            Destroy(this.gameObject);
        }
    }

    private void UpdateItems()
    {
        int itemIndex = transactionMenuItemsWindowUpperBoundIndex;
        for (int i = 0; i < transactionMenuItems.Count; i++)
        {
            transactionMenuItems[i].SetupItem(sellerInventory.items[itemIndex].itemName, 
                sellerInventory.items[itemIndex].price.ToString(), 
                sellerInventory.items[itemIndex].sprite);
            itemIndex++;
        }
    }

    private void UpdatePlayerAvailableCoinsText()
    {
        tmpPlayerAvailableCoins.text = "Available    : " + GameObject.FindGameObjectWithTag("Player").GetComponent<ItemInventory>().numberOfCoins.ToString();
    }

    private void ClampIndexes()
    {
        // Clamp the selectedTransactionMenuItemIndex
        if (selectedTransactionMenuItemIndex >= transactionMenuItems.Count)
        {
            selectedTransactionMenuItemIndex = transactionMenuItems.Count - 1;
        }

        if (selectedTransactionMenuItemIndex < 0)
        {
            selectedTransactionMenuItemIndex = 0;
        }

        // Clamp the selectedSellerItemIndex
        if (selectedSellerItemIndex >= sellerInventory.items.Count)
        {
            selectedSellerItemIndex = sellerInventory.items.Count - 1;
        }

        if (selectedSellerItemIndex < 0)
        {
            selectedSellerItemIndex = 0;
        }

        // Clamp the window bound indexes
        if (transactionMenuItemsWindowLowerBoundIndex >= sellerInventory.items.Count)
        {
            transactionMenuItemsWindowLowerBoundIndex = sellerInventory.items.Count - 1;
            if (transactionMenuItemsWindowLowerBoundIndex < 0)
            {
                transactionMenuItemsWindowLowerBoundIndex = 0;
            }

            transactionMenuItemsWindowUpperBoundIndex -= 1;
            if (transactionMenuItemsWindowUpperBoundIndex < 0)
            {
                transactionMenuItemsWindowUpperBoundIndex = 0;
            }
        }
    }
}
