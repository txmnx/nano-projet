using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesPanel : MonoBehaviour
{
    public MainMenu mainMenu;
    public Animator exitAnimator;

    private bool isPausing = false;

    // Update is called once per frame
    void Update()
    {
        if (isPausing) return;

        if (Input.GetButtonDown("Exit")) {
            exitAnimator.Play("Press", 0, 0);
            mainMenu.ReturnToMenuChoices(GetComponent<RectTransform>());
        }
    }

    public void Pause(bool pause)
    {
        isPausing = pause;
    }
}
