using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    public MainMenu mainMenu;

    private bool isPausing = false;

    // Update is called once per frame
    void Update()
    {
        if (isPausing) return;

        if (Input.GetButtonDown("Exit")) {
            mainMenu.ReturnToMenuChoices();
        }
    }

    public void Pause(bool pause)
    {
        isPausing = pause;
    }
}
