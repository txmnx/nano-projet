using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public RectTransform rectangle;

    public float maxAmount = 1200;
    public float currentAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentAmount = maxAmount;
    }

    // Update is called once per frame
    public void refreshHealth()
    {
        rectangle.offsetMax = rectangle.offsetMin * currentAmount / 1200;


    }
}
