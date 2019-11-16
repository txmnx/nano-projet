using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The BeatManager class provides methods and properties to be used by everything that needs to be synced to the beat.
 * TODO:
 *      Since there is no music to be synced to at the moment we use Time.time as a timer
 *      but we'll have to change it to AudioSettings.dspTime to be more precise.
 */
public class BeatManager : MonoBehaviour
{
    public static int bpm = 120;

    public static float period {
        get { return 60 / (float)bpm; }
    }
    
    public static float songPosition {
        get { return Time.time; }
    }

    private static List<OnBeatElement> onBeatElements;
    private float lastBeat;

    void Start()
    {
        onBeatElements = new List<OnBeatElement>();
        lastBeat = 0.0f;
    }

    void Update()
    {
        if (BeatManager.songPosition > lastBeat + BeatManager.period) {
            foreach (OnBeatElement element in onBeatElements) {
                element.OnBeat();
            }
        }
    }

    public static void RegisterOnBeatElement(OnBeatElement element)
    {
        onBeatElements.Add(element);
    }
}
