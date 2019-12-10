using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    Player player;
    TextMeshProUGUI playerHealth;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        player = FindObjectOfType<Player>();
        playerHealth = GetComponent<TextMeshProUGUI>();
    }

    public void healthOpacity()
    {
        image.color = new Color (image.color.r, image.color.g, image.color.b, 0.5f);
    }
}
