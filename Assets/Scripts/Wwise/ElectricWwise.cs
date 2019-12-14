using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWwise : MonoBehaviour
{
    public GameObject PlayerBody1;
    public GameObject PlayerBody2;

    public void ElectricAmerican()
    {
        AkSoundEngine.PostEvent("SFX_Common_Electric_Sparks_American", PlayerBody1);
    }

    public void ElectricJapan()
    {
        AkSoundEngine.PostEvent("SFX_Common_Electric_Sparks_Japan", PlayerBody2);
    }
}
