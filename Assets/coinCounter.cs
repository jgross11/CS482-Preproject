using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinCounter : MonoBehaviour
{


    public Text text;

    public playMenuHandler menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("tower-menu-background").GetComponent<playMenuHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Number of coins: " + menu.GetCoins();
    }
}
