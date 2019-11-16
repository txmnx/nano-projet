using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * BeatPulser is an exemple class to show the use of BeatManager.
 */
public class BeatPulser : MonoBehaviour
{
    private float lastBeat = 0.0f;

    void Update()
    {
        if (BeatManager.songPosition > lastBeat + BeatManager.period) {
            Debug.Log("boum");
            lastBeat += BeatManager.period;
        }
    }
}
