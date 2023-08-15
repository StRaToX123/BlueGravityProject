using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    // Which prompt to spawn when the player is near this merchant
    public GameObject interactionPrompt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionPrompt.activeSelf)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("WOOAH");


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
}
