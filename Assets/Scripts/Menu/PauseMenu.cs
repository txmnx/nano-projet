using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public MatchManager matchManager;

    public Animator player1Animator;
    public Animator player2Animator;
    public Animator cameraAnimator;

    public AnimationCurve popCurve;
    public Animator exitKeyAnimator;

    private RectTransform rectTransform;

    private bool isPaused = false;
    private bool isMoving = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (isMoving) return;

        if (isPaused) {
            if (Input.GetButtonDown("Exit")) {
                exitKeyAnimator.Play("Press", 0, 0);
                ReturnToMainMenu();
                return;
            }
            if (Input.GetButtonDown("Pause")) {
                Hide();
                isPaused = false;
            }
        }
        else if (Input.GetButtonDown("Pause")) {
            Display();
            isPaused = true;
        }
    }

    public void Hide()
    {
        player1Animator.enabled = true;
        player2Animator.enabled = true;
        cameraAnimator.enabled = true;
        Pop(true);

        AkSoundEngine.PostEvent("UI_Menu_UnPauseGame", gameObject);
        InputTranslator.sequence = matchManager.currentSequence;
    }

    public void Display()
    {
        player1Animator.enabled = false;
        player2Animator.enabled = false;
        cameraAnimator.enabled = false;
        Pop(false);

        AkSoundEngine.PostEvent("UI_Menu_PauseGame", gameObject);
        matchManager.currentSequence = InputTranslator.sequence;
        InputTranslator.sequence = Sequence.IDLE;
    }

    void Pop(bool up)
    {
        isMoving = true;
        StartCoroutine(PopAnimation(up, 0.2f));
    }

    private IEnumerator PopAnimation(bool up, float duration)
    {
        float timer = 0.0f;
        float scale = 0f;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = ((up) ? 1 - Mathf.Clamp(timer / duration, 0f, 1f) : Mathf.Clamp(timer / duration, 0f, 1f));

            scale = popCurve.Evaluate(progress);

            rectTransform.localScale = new Vector2(scale, scale);

            timer += Time.deltaTime;

            yield return null;
        }

        rectTransform.localScale = ((up) ? new Vector2(0, 0) : new Vector2(1, 1));
        isMoving = false;
    }

    public void ReturnToMainMenu()
    {
        AkSoundEngine.PostEvent("UI_Option_Back",gameObject);
        AkSoundEngine.PostEvent("UI_Back_Main_Menu", gameObject);
        SceneManager.LoadScene("MainMenuScene");
    }
}
