using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Determines on which sequence the game is.
 */
public enum Sequence
{
    INPUT,
    ACTION,
    IDLE
}

/**
 * The InputTranslator class translates the stored inputs as animations and actions.
 * TODO:
 *      It probably should be a singleton.
 */
public class InputTranslator : MonoBehaviour, OnBeatElement
{
    private static List<OnInputBeatElement> onInputBeatElements;
    private static List<OnActionBeatElement> onActionBeatElements;
    
    public static int step = 2; // How much beats for a sequence
    private static int currentStep;

    public FightManager fightManager;

    // TODO : here we should have a buffer of two inputs as a private member

    public static Sequence sequence;

    void Awake()
    {
        onInputBeatElements = new List<OnInputBeatElement>();
        onActionBeatElements = new List<OnActionBeatElement>();
        
        step = 2;
        currentStep = 1;

        sequence = Sequence.ACTION;
    }

    void Start()
    {
        BeatManager.RegisterOnBeatElement(this);
    }

    /**
     * TODO : should have an Init method to first call OnInputBeat and OnActionBeat when the music starts
     */
    
    public void OnBeat()
    {
        Debug.Log("test");
        if (currentStep == step) {
            if (sequence == Sequence.INPUT) {
                foreach (OnActionBeatElement element in onActionBeatElements) {
                    element.OnEnterActionBeat();
                    element.OnActionBeat();
                }
                sequence = Sequence.ACTION;
            }
            else {
                foreach (OnInputBeatElement element in onInputBeatElements) {
                    element.OnEnterInputBeat();
                    element.OnInputBeat();
                }
                sequence = Sequence.INPUT;
            }

            currentStep = 1;
        }
        else {
            if (sequence == Sequence.INPUT) {
                foreach (OnInputBeatElement element in onInputBeatElements) {
                    element.OnInputBeat();
                }
            }
            else {
                foreach (OnActionBeatElement element in onActionBeatElements) {
                    element.OnActionBeat();
                }
            }

            currentStep++;
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
        currentStep = 1;
    }
}
