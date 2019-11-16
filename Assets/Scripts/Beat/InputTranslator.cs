using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The InputTranslator class translate the stored inputs and translate them as animations and actions.
 */
public class InputTranslator : MonoBehaviour
{
    private float lastBeat = 0.0f;
    private float step; // We translate the inputs on every step
    // TODO : here we should have a buffer of two inputs as a private member


    void Start()
    {
        step = BeatManager.period * 4;
    }

    void Update()
    {
        if (BeatManager.songPosition > lastBeat + step) {
            // TODO : here we have to read the input buffer and use the inputs
            Debug.Log("Translate inputs");
            lastBeat += step;
        }
    }
}
