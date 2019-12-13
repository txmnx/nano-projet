using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The BeatManager class provides methods and properties to be used by everything that needs to be synced to the beat.
 * TODO:
 *      Since there is no music to be synced to at the moment we use Time.time as a timer
 *      but we'll have to change it to AudioSettings.dspTime to be more precise.
 *      
 *      It probably should be a singleton.
 */
public class BeatManager : MonoBehaviour
{
    private static List<OnBeatElement> onBeatElements;
    public CueManager cueManager;


    void Awake()
    {
       customAwake();
    }

    public void customAwake()
    {
        onBeatElements = new List<OnBeatElement>();
    }
    void PlayBeat()
    {
        if(cueManager.isBeatDetected)
        foreach (OnBeatElement element in onBeatElements) {
            element.OnBeat();
        }
    }

    public static void RegisterOnBeatElement(OnBeatElement element)
    {
        onBeatElements.Add(element);
    }
}
