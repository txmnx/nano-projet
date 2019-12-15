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

    public CreditsPanel creditsPanel;
    public RulesPanel rulesPanel;
    private RectTransform creditsPanelTransform;
    private RectTransform rulesPanelTransform;

    public AnimationCurve transitionCurve;

    private float screenSize = 800f;
    private float distanceBetweenPanels = 450f;
    private bool isMoving = false;

    private void Start()
    {
        menuPanelTransform = menuPanel.GetComponent<RectTransform>();
        creditsPanelTransform = creditsPanel.GetComponent<RectTransform>();
        rulesPanelTransform = rulesPanel.GetComponent<RectTransform>(); 

        menuChoices.Pause(false);
        creditsPanel.Pause(true);
        rulesPanel.Pause(true);
    }

    public void DisplayCreditsPanel()
    {
        if (isMoving) return;

        menuChoices.Pause(true);
        creditsPanel.Pause(false);

        isMoving = true;
        creditsPanelTransform.anchoredPosition = new Vector2(0, creditsPanelTransform.anchoredPosition.y);
        StartCoroutine(MovePanelCoroutine(creditsPanelTransform, 0.3f, distanceBetweenPanels));
    }

    public void DisplayRulesPanel()
    {
        if (isMoving) return;

        menuChoices.Pause(true);
        rulesPanel.Pause(false);

        isMoving = true;
        rulesPanelTransform.anchoredPosition = new Vector2(0, rulesPanelTransform.anchoredPosition.y);
        StartCoroutine(MovePanelCoroutine(rulesPanelTransform, 0.3f, distanceBetweenPanels));
    }

    public void ReturnToMenuChoices(RectTransform rectTransform)
    {
        if (isMoving) return;

        AkSoundEngine.PostEvent("UI_Option_Back", gameObject);

        menuChoices.Pause(false);
        creditsPanel.Pause(true);
        rulesPanel.Pause(true);

        menuChoices.Reset();
        isMoving = true;
        StartCoroutine(MovePanelCoroutine(rectTransform, 0.3f, -distanceBetweenPanels, true));
    }

    private IEnumerator MovePanelCoroutine(RectTransform rectTransform, float duration, float offset, bool rebaseOnEnd = false)
    {
        float timer = 0.0f;
        float initYMenu = menuPanelTransform.anchoredPosition.y;
        float initYOptions = rectTransform.anchoredPosition.y;
        float y = 0f;

        float progress;
        progress = timer / duration;

        while (timer < duration) {
            progress = Mathf.Clamp(timer / duration, 0f, 1f);

            y = transitionCurve.Evaluate(progress) * offset;

            menuPanelTransform.anchoredPosition = new Vector2(menuPanelTransform.anchoredPosition.x, initYMenu + y);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initYOptions + y);

            timer += Time.deltaTime;

            yield return null;
        }

        menuPanelTransform.anchoredPosition = new Vector2(menuPanelTransform.anchoredPosition.x, initYMenu + offset);

        if (rebaseOnEnd) rectTransform.anchoredPosition = new Vector2(screenSize, initYOptions + offset);
        else rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, initYOptions + offset);

        isMoving = false;
    }
}