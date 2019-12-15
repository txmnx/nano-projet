using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceText : MonoBehaviour
{
    public Animator animFight;
    public Animator round1Fight;
    public Animator round2Fight;
    public Animator victory1Fight;
    public Animator victory2Fight;

    public void Pause(bool pause)
    {
        animFight.enabled = !pause;
        round1Fight.enabled = !pause;
        round2Fight.enabled = !pause;
        victory1Fight.enabled = !pause;
        victory2Fight.enabled = !pause;
    }
}
