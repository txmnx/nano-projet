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
 * The InputTranslator class translates the stored inputs as animations and actions.
 * TODO:
 *      It probably should be a singleton.
 */
public class InputTranslator : MonoBehaviour
{
    private static List<OnInputBeatElement> onInputBeatElements;
    private static List<OnActionBeatElement> onActionBeatElements;

    private static float lastBeat;
    public static int step; // How much beats for a sequence

    // TODO : here we should have a buffer of two inputs as a private member

    public static Sequence sequence;

    void Awake()
    {
        onInputBeatElements = new List<OnInputBeatElement>();
        onActionBeatElements = new List<OnActionBeatElement>();

        lastBeat = 0.0f;
        step = 2;

        sequence = Sequence.ACTION;
    }

    /**
     * TODO : should have an Init method to first call OnInputBeat and OnActionBeat when the music starts
     */

    void Update()
    {
        // On each kind of beat we call the corresponding method for the stored beat elements
        if (BeatManager.songPosition > lastBeat + BeatManager.period * step) {
            if (sequence == Sequence.INPUT) {
                foreach(OnActionBeatElement element in onActionBeatElements) {
                    element.OnActionBeat();
                }
                sequence = Sequence.ACTION;
            }
            else {
                foreach (OnInputBeatElement element in onInputBeatElements) {
                    element.OnInputBeat();
                }
                sequence = Sequence.INPUT;
            }

            lastBeat += BeatManager.period * step;
        }
    }


    /**
     * Register Input and Action BeatElements
     */
    public static void RegisterOnInputBeatElement(OnInputBeatElement element)
    {
        onInputBeatElements.Add(element);
    }

    public static void RegisterOnActionBeatElement(OnActionBeatElement element)
    {
        onActionBeatElements.Add(element);
    }

    /**
     * Reset the beat.
     */
    public static void Reset()
    {
        lastBeat = 0.0f;
    }
}
