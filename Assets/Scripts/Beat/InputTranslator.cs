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
    private static List<OnIdleBeatElement> onIdleBeatElements;

    public static int step = 2; // How much beats for a sequence
    public static int currentStep;
    public GameObject Player1Body;
    public GameObject Player2Body;

    public GameObject inputFilter;

    public MatchManager mm;
    

    public FightManager fightManager;

    // TODO : here we should have a buffer of two inputs as a private member

    public static Sequence sequence;

    void Awake()
    {
        customAwake();
    }

    public void customAwake()
    {
        onInputBeatElements = new List<OnInputBeatElement>();
        onActionBeatElements = new List<OnActionBeatElement>();
        onIdleBeatElements = new List<OnIdleBeatElement>();

        step = 2;

        currentStep = 2;
        fightManager.OnEnterIdleBeat();
        sequence = Sequence.IDLE;

        if(mm.isFinalPhase)
        {
            currentStep = 1;
        }
        
    }

    void Start()
    {
        customStart();
    }

    public void customStart()
    {
        BeatManager.RegisterOnBeatElement(this);
    }

    /**
     * TODO : should have an Init method to first call OnInputBeat and OnActionBeat when the music starts
     */
    
    public void OnBeat()
    {
        
        if (currentStep == step) {
            if (sequence == Sequence.INPUT) {
                foreach (OnActionBeatElement element in onActionBeatElements) {
                    element.OnEnterActionBeat();
                    element.OnActionBeat();
                    AkSoundEngine.PostEvent("SFX_InputPhase_Out", gameObject);
                }
                inputFilter.SetActive(false);
                sequence = Sequence.ACTION;
            } 
            else if(sequence == Sequence.IDLE)
            {
               
                foreach (OnIdleBeatElement element in onIdleBeatElements)
                {
                    
                    element.OnEnterIdleBeat();
                    element.OnIdleBeat();
                }
                inputFilter.SetActive(false);
                sequence = Sequence.ACTION;

            }
            else {
                foreach (OnInputBeatElement element in onInputBeatElements) {
                    element.OnEnterInputBeat();
                    element.OnInputBeat();
                }
                inputFilter.SetActive(true);
                sequence = Sequence.INPUT;
                AkSoundEngine.PostEvent("SFX_InputPhase_In", gameObject);
                AkSoundEngine.SetSwitch("Charged", "No", Player1Body);
                AkSoundEngine.SetSwitch("Charged", "No", Player2Body);

            }
            currentStep = 1;
        }
        else{
            if (sequence == Sequence.INPUT) {
                foreach (OnInputBeatElement element in onInputBeatElements) {
                    element.OnInputBeat();
                }
            }
            else if(sequence == Sequence.ACTION)
            {
                foreach (OnActionBeatElement element in onActionBeatElements) {
                    element.OnActionBeat();
                }
            }
            else
            {
                foreach (OnIdleBeatElement element in onIdleBeatElements)
                {
                    element.OnIdleBeat();
                }

                if (currentStep == 0)
                {
                    //step = 4; //modify parameters for accelerated phase
                }
            }
            currentStep++;
        }
        Debug.Log(sequence);
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

    public static void RegisterOnIdleBeatElement(OnIdleBeatElement element)
    {
        onIdleBeatElements.Add(element);
    }

    /**
     * Reset the beat.
     */
    public static void Reset()
    {
        currentStep = 1;
    }
}
