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
    public static int bpm = 80;

    public static float period {
        get { return 60 / (float)bpm; }
    }
    
    public static float songPosition {
        get { return Time.time; }
    }

    private static List<OnBeatElement> onBeatElements;
    private float lastBeat;

    void Awake()
    {
        onBeatElements = new List<OnBeatElement>();
        lastBeat = 0.0f;
    }

    /**
     * TODO : should have an Init method to first call OnBeat when the music starts
     */

    void Update()
    {
        // On each beat we call OnBeat of all of the beat elements
        if (BeatManager.songPosition > lastBeat + BeatManager.period) {
            foreach (OnBeatElement element in onBeatElements) {
                element.OnBeat();
            }

            lastBeat += BeatManager.period;
        }
    }

    public static void RegisterOnBeatElement(OnBeatElement element)
    {
        onBeatElements.Add(element);
    }
}
