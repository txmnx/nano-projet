using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public MenuChoices menuChoices;
    public GameObject menuPanel;
    private RectTransform menuPanelTransform;

    public OptionsPanel optionsPanel;
    private RectTransform optionsPanelTransform;

    public AnimationCurve transitionCurve;

    private float distanceBetweenPanels = 450f;
    private bool isMoving = false;

    private void Start()
    {
        menuPanelTransform = menuPanel.GetComponent<RectTransform>();
        optionsPanelTransform = optionsPanel.GetComponent<RectTransform>();
        menuChoices.Pause(false);
        optionsPanel.Pause(true);
    }

    public void DisplayOptionsPanel()
    {
        if (isMoving) return;

        menuChoices.Pause(true);
        optionsPanel.Pause(false);

        isMoving = true;
        StartCoroutine(MovePanelCoroutine(0.5f, distanceBetweenPanels));
    }

    public void ReturnToMenuChoices()
    {
        if (isMoving) return;

        menuChoices.Pause(false);
        optionsPanel.Pause(true);

        menuChoices.Reset();
        isMoving = true;
        StartCoroutine(MovePanelCoroutine(0.5f, -distanceBetweenPanels));
    }

    private IEnumerator MovePanelCoroutine(float duration, float offset)
    {
        float timer = 0.0f;
        float initYMenu = menuPanelTransform.anchoredPosition.y;
        float initYOptions = optionsPanelTransform.anchoredPosition.y;
        float y = 0f;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = Mathf.Clamp(timer / duration, 0f, 1f);

            y = transitionCurve.Evaluate(progress) * offset;

            menuPanelTransform.anchoredPosition = new Vector2(menuPanelTransform.anchoredPosition.x, initYMenu + y);
            optionsPanelTransform.anchoredPosition = new Vector2(optionsPanelTransform.anchoredPosition.x, initYOptions + y);

            timer += Time.deltaTime;

            yield return null;
        }

        menuPanelTransform.anchoredPosition = new Vector2(menuPanelTransform.anchoredPosition.x, initYMenu + offset);
        optionsPanelTransform.anchoredPosition = new Vector2(optionsPanelTransform.anchoredPosition.x, initYOptions + offset);
        isMoving = false;
    }
}