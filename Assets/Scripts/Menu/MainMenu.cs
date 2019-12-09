using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText;
    public GameObject controlMenuUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.red; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = Color.white; //Or however you do your color
    }

    public void LoadVersus()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OptionMenu()
    { 
    }

    public void ControlMenu()
    {
        controlMenuUI.SetActive(true);
    }
  public void QuitControlMenu()
    {
        controlMenuUI.SetActive(false);
    }
    public void QuitGame()
    {
        //Application.Quit;
    }
}
//Isfocus
//IShighlight