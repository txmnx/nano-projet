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

        if (Input.GetButtonDown("Pause")) {
            if (!isPaused) {
                Display();
                isPaused = true;
            }
            else if (isPaused) {
                Hide();
                isPaused = false;
            }
        }
    }

    public void Hide()
    {
        //anim resume
        player1Animator.enabled = true;
        player2Animator.enabled = true;
        cameraAnimator.enabled = true;
        Pop(true);

        AkSoundEngine.PostEvent("UI_Menu_UnPauseGame", gameObject);
        InputTranslator.sequence = matchManager.currentSequence;
    }

    public void Display()
    {
        //anim pause
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
        SceneManager.LoadScene("MainMenuScene");
    }
}
