using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private RectTransform rectTransform;

    public AnimationCurve selectionCurve;
    public float distanceBetweenChoices = 60f;

    private bool isMoving = false;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isMoving) {
            if (Input.GetAxisRaw("SelectionVerticalButton") > 0f || Input.GetAxisRaw("SelectionVerticalJoystick") > 0f) {
                Debug.Log("UP");
                MoveDown();
            }
            else if (Input.GetAxisRaw("SelectionVerticalButton") < 0f || Input.GetAxisRaw("SelectionVerticalJoystick") < 0f) {
                Debug.Log("DOWN");
                MoveUp();
            }
        }

        if (Input.GetButtonDown("Validate")) {
            //valider
        }
    }

    private void MoveUp()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.1f, distanceBetweenChoices));
    }

    private void MoveDown()
    {
        isMoving = true;
        StartCoroutine(MoveAnimation(0.1f, -distanceBetweenChoices));
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

            y = Mathf.Lerp(0, offset, progress);

            Debug.Log(y);

            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initY + y);

            timer += Time.deltaTime;

            yield return null;
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initY + offset);
        isMoving = false;
    }
}
