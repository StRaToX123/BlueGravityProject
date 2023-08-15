using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{
    public string merchantName = "DefaultName";
    // Which prompt to spawn when the player is near this merchant
    public GameObject interactionPrompt;
    public GameObject interactionOptions;
    // Insert custom options logic here. For example, merchants
    // can show different dialogue optioons depending on if the player has completed
    // scertain goals.
    public virtual void StartInteraction()
    {
        
    }

    // List of items this merchant has
    protected List<Item> items;
    private Button[] interactionOptionsButtons;
    private int selectedInteractionOptionIndex;

    public virtual void Start()
    {
        selectedInteractionOptionIndex = 0;
        interactionOptionsButtons = interactionOptions.GetComponentsInChildren<Button>();
        interactionOptionsButtons[0].Select();
    }


    // Update is called once per frame
    public virtual void Update()
    {
        // Check if the player can interact with this merchant
        if (interactionPrompt.activeSelf)
        {
            if (Input.GetButtonDown("Interact"))
            {
                interactionPrompt.SetActive(false);
                StartInteraction();
                selectedInteractionOptionIndex = 0;
                interactionOptions.SetActive(true);
                // Freeze the player
                GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;

            }
        }

        // Navigate the interaction options
        // but only if the player has interacted with the merchant
        if (interactionOptions.activeSelf)
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    public void InteractionOptionsExitCallback()
    {
        // Unfreeze the player
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;
        interactionOptions.SetActive(false);
    }
}
