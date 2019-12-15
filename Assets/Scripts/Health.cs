using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{



    public float maxAmount = 1200;
    public float currentAmount;
    private float xInit;

    // Start is called before the first frame update
    void Start()
    {
        xInit = transform.localScale.x;
        currentAmount = maxAmount;
    }

    // Update is called once per frame
    public void refreshHealth()
    {
        transform.localScale = new Vector3(xInit*currentAmount/maxAmount, transform.localScale.y, transform.localScale.y);
    }
}
