using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChoices : MonoBehaviour
{
    enum Choice
    {
        VERSUS,
        AI,
        CONTROLS,
        OPTIONS
    }
   

    Choice selection = Choice.VERSUS;
    int choicesLength = 4;

    private RectTransform rectTransform;

    public AnimationCurve selectionCurve;
    public float distanceBetweenChoices = 60f;

    private bool isMoving = false;
    private bool isPausing = false;

    public TextMeshProUGUI[] choicesTexts;

    public MainMenu mainMenu;

    public Animator validateAnimator;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        HighlightText(selection);
    }

    private void Update()
    {
        if (isPausing) return;

        //Lancer les events WWISE
        if (!isMoving) {
            if (Input.GetAxisRaw("SelectionVerticalButton") > 0f || Input.GetAxisRaw("SelectionVerticalJoystick") > 0f) {
                validateAnimator.Play("Giggle Up", 0, 0);
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
                case Choice.VERSUS:
                    SceneManager.LoadScene("SampleScene");
                    break;
                case Choice.AI:
                    SceneManager.LoadScene("FightSceneAI");
                    break;
                case Choice.CONTROLS:
                    //TODO : popup controls
                    break;
                case Choice.OPTIONS:
                    mainMenu.DisplayOptionsPanel();
                    break;
                default:
                    break;
            }
        }
    }

    public void Reset()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f);
        selection = Choice.VERSUS;
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
        StartCoroutine(MoveAnimation(0.2f, -distanceBetweenChoices * 3));
    }

    private void MoveBottom()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.2f, distanceBetweenChoices * 3));
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

    public void Pause(bool pause)
    {
        isPausing = pause;
    }
}
