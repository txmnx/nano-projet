using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public Animator player1Animator;
    public Animator player2Animator;
    public Animator cameraAnimator;

    public void Hide()
    {
        //anim resume
        player1Animator.enabled = true;
        player2Animator.enabled = true;
        cameraAnimator.enabled = true;
    }

    public void Display()
    {
        //anim pause
        player1Animator.enabled = false;
        player2Animator.enabled = false;
        cameraAnimator.enabled = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
