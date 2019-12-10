using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text[] texts;
    public int[] pif;
    public Color baseColor;
    public Color selectedColor;

    private int selectedText;

    void Start()
    {
        selectedText = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Up"))
        {
            if (selectedText != 0)
                selectedText--;
            else
                selectedText = texts.Length - 1;

            for(int i = 0; i<texts.Length; i++)
            {
                if(i == selectedText)
                    texts[i].color = selectedColor;
                else
                    texts[i].color = baseColor;
            }
        }

        if (Input.GetButtonDown("Down"))
        {
            if (selectedText != texts.Length - 1)
                selectedText++;
            else
                selectedText = 0;

            for (int i = 0; i < texts.Length; i++)
            {
                if (i == selectedText)
                    texts[i].color = selectedColor;
                else
                    texts[i].color = baseColor;
            }
        }

        if (Input.GetButtonDown("Enter"))
        {
            
        }
    }
}
