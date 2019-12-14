using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    enum Choice
    {
        REPLAY,
        RETURN
    }
    int choicesLength = 2;

    private float distanceBetweenChoices = 60f;

    public AnimationCurve selectionCurve;
    public RectTransform rectTransform;

    public Animator validateAnimator;
    public TextMeshProUGUI[] choicesTexts;

    private Choice selection = Choice.REPLAY;
    private bool isMoving = false;

    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        //Lancer les events WWISE
        if (!isMoving) {
            if (Input.GetAxisRaw("SelectionVerticalButton") > 0f || Input.GetAxisRaw("SelectionVerticalJoystick") > 0f) {
                validateAnimator.Play("Giggle Up", 0, 0);
                AkSoundEngine.PostEvent("UI_Menu_Hovered_Main", gameObject);
                if (selection - 1 < 0) {
                    selection = (Choice)choicesLength - 1;
                    MoveBottom();
                }
                else {
                    selection--;
                    MoveUp();
                }

                HighlightText(selection);
            }
            else if (Input.GetAxisRaw("SelectionVerticalButton") < 0f || Input.GetAxisRaw("SelectionVerticalJoystick") < 0f) {
                validateAnimator.Play("Giggle Down", 0, 0);
                AkSoundEngine.PostEvent("UI_Menu_Hovered_Main", gameObject);
                if (selection + 1 >= (Choice)choicesLength) {
                    selection = 0;
                    MoveTop();
                }
                else {
                    selection++;
                    MoveDown();
                }

                HighlightText(selection);
            }
        }

        if (Input.GetButtonDown("Validate")) {
            validateAnimator.Play("Press", 0, 0);
            switch (selection) {
                case Choice.REPLAY:
                    SceneManager.LoadScene("SampleScene");
                    AkSoundEngine.PostEvent("UI_Menu_Clic_Start", gameObject);
                    break;
                case Choice.RETURN:
                    SceneManager.LoadScene("MainMenuScene");
                    AkSoundEngine.PostEvent("UI_Menu_Clic_Option", gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    public void Reset()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
        selection = Choice.REPLAY;
        HighlightText(selection);
    }

    private void MoveUp()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.15f, -distanceBetweenChoices));
    }

    private void MoveDown()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.15f, distanceBetweenChoices));
    }

    private void MoveTop()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.2f, -distanceBetweenChoices));
    }

    private void MoveBottom()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.2f, distanceBetweenChoices));
    }

    private IEnumerator MoveAnimation(float duration, float offset)
    {
        float timer = 0.0f;
        float initY = rectTransform.anchoredPosition.y;
        float y = initY;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = Mathf.Clamp(timer / duration, 0f, 1f);

            y = selectionCurve.Evaluate(progress) * offset;

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initY + y);

            timer += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initY + offset);
        isMoving = false;
    }

    private void HighlightText(Choice choice)
    {
        for (int i = 0; i < choicesTexts.Length; ++i) {
            if (i == (int)choice) {
                choicesTexts[i].fontSize = 33;
                choicesTexts[i].color = Color.white;
            }
            else {
                choicesTexts[i].fontSize = 30;
                choicesTexts[i].color = Color.gray;
            }
        }
    }
}
