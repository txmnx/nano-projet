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
    public RectTransform rectTransformChoices;
    public RectTransform rectTransformWrapper;

    public Animator validateAnimator;
    public TextMeshProUGUI[] choicesTexts;

    private Choice selection = Choice.REPLAY;
    private bool isMoving = false;
    private bool isPausing = true;

    public void Display()
    {
        isMoving = true;
        StartCoroutine(DisplayAnimation(0.7f, -800f));
    }
    
    private IEnumerator DisplayAnimation(float duration, float offset)
    {
        float timer = 0.0f;
        float initX = rectTransformWrapper.anchoredPosition.x;
        float x = initX;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = Mathf.Clamp(timer / duration, 0f, 1f);

            x = selectionCurve.Evaluate(progress) * offset;

            rectTransformWrapper.anchoredPosition = new Vector2(initX + x, rectTransformWrapper.anchoredPosition.y);

            timer += Time.deltaTime;

            yield return null;
        }

        rectTransformWrapper.anchoredPosition = new Vector2(initX + offset, rectTransformWrapper.anchoredPosition.y);
        isMoving = false;
        isPausing = false;
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        if (isPausing) return;

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
                    AkSoundEngine.PostEvent("UI_Menu_Clic_Start", gameObject);
                    AkSoundEngine.PostEvent("UI_Back_Main_Menu", gameObject);
                    SceneManager.LoadScene("SampleScene");
                   
                    break;
                case Choice.RETURN:
                    AkSoundEngine.PostEvent("UI_Menu_Clic_Option", gameObject);
                    AkSoundEngine.PostEvent("UI_Back_Main_Menu", gameObject);
                    SceneManager.LoadScene("MainMenuScene");
               
                    break;
                default:
                    break;
            }
        }
    }

    public void Reset()
    {
        rectTransformChoices.anchoredPosition = new Vector2(rectTransformChoices.anchoredPosition.x, 0f);
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
        float initY = rectTransformChoices.anchoredPosition.y;
        float y = initY;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = Mathf.Clamp(timer / duration, 0f, 1f);

            y = selectionCurve.Evaluate(progress) * offset;

            rectTransformChoices.anchoredPosition = new Vector2(rectTransformChoices.anchoredPosition.x, initY + y);

            timer += Time.deltaTime;

            yield return null;
        }

        rectTransformChoices.anchoredPosition = new Vector2(rectTransformChoices.anchoredPosition.x, initY + offset);
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
