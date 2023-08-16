using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionMenuItem : MonoBehaviour
{
    public TextMeshProUGUI tmpName;
    public TextMeshProUGUI tmpPrice;
    public Image image;
    public Image background;

    void Start()
    {
        background = GetComponent<Image>();
    }

    public void SetupItem(string name, string price, Sprite sprite)
    {
        tmpName.text = name;
        tmpPrice.text = price;
        image.sprite = sprite;
    }
}
