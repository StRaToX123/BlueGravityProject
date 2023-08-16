using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    public string merchantName = "DefaultName";
    // Which prompt to spawn when the player is near this merchant
    public GameObject interactionPromptPrefab;
    public GameObject interactionOptionsPrefab;
    // List of items this merchant has
    public List<ItemDataSO> items;
    // Insert custom options logic here. For example, merchants
    // can show different dialogue optioons depending on if the player has completed
    // scertain goals.
    public virtual void StartInteraction()
    {
        
    }

    private Button[] interactionOptionsButtons;
    private int selectedInteractionOptionIndex;

    public virtual void Start()
    {
        selectedInteractionOptionIndex = 0;
        interactionOptionsButtons = interactionOptionsPrefab.GetComponentsInChildren<Button>();
        interactionOptionsButtons[0].Select();
    }


    // Update is called once per frame
    public virtual void Update()
    {
        // Navigate the interaction options
        // but only if the player has interacted with the merchant
        if (interactionOptionsPrefab.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedInteractionOptionIndex++;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedInteractionOptionIndex--;
            }

            if (selectedInteractionOptionIndex == interactionOptionsButtons.Length)
            {
                selectedInteractionOptionIndex = 0;
            }

            if (selectedInteractionOptionIndex < 0)
            {
                selectedInteractionOptionIndex = interactionOptionsButtons.Length - 1;
            }

            interactionOptionsButtons[selectedInteractionOptionIndex].Select();
            if (Input.GetButtonDown("Interact"))
            {
                interactionOptionsButtons[selectedInteractionOptionIndex].onClick.Invoke();
            }
        }

        // Check if the player can interact with this merchant
        if (interactionPromptPrefab.activeSelf)
        {
            if (Input.GetButtonDown("Interact"))
            {
                interactionPromptPrefab.SetActive(false);
                StartInteraction();
                selectedInteractionOptionIndex = 0;
                interactionOptionsPrefab.SetActive(true);
                // Freeze the player
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionPromptPrefab.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionPromptPrefab.gameObject.SetActive(false);
        }
    }

    public void InteractionOptionsExitCallback()
    {
        // Unfreeze the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
        interactionOptionsPrefab.SetActive(false);
    }
}
