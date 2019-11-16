using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Determines on which sequence the game is.
 */
public enum Sequence
{
    INPUT,
    ACTION
}

/**
 * The InputTranslator class translates the stored inputs and translate them as animations and actions.
 */
public class InputTranslator : MonoBehaviour
{
    private float lastBeat = 0.0f;
    private readonly int step = 2; // How much beats to await inputs

    // TODO : here we should have a buffer of two inputs as a private member

    public static Sequence sequence;

    void Awake()
    {
        sequence = Sequence.INPUT;
    }

    void Update()
    {
        if (BeatManager.songPosition > lastBeat + BeatManager.period * step) {
            sequence = (sequence == Sequence.INPUT) ? Sequence.ACTION : Sequence.INPUT;
            Debug.Log("BOUM");
            // TODO : here we have to read the input buffer or use the inputs depending on sequence

            lastBeat += BeatManager.period * step;
        }
    }
}
