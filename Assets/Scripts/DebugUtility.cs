using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Utility class for debugging purpose.
 */
public class DebugUtility : MonoBehaviour
{
    public int frameRate = 60;
    public int bpm = 80;
    public int step = 2;

    private int oldFrameRate;
    private int oldBpm;
    private int oldStep;

    void Start()
    {
        QualitySettings.vSyncCount = 0;

        Application.targetFrameRate = frameRate;
        BeatManager.bpm = bpm;
        InputTranslator.step = step;

        GetComponent<FightManager>().player1.BufferReset();
        GetComponent<FightManager>().player2.BufferReset();

        oldFrameRate = frameRate;
        oldBpm = bpm;
        oldStep = step;
    }
    
    void Update()
    {
        if (frameRate != oldFrameRate) {
            Application.targetFrameRate = frameRate;
            oldFrameRate = frameRate;
            BeatManager.Reset();
            InputTranslator.Reset();
        }

        if (bpm != oldBpm) {
            BeatManager.bpm = bpm;
            oldBpm = bpm;
            BeatManager.Reset();
            InputTranslator.Reset();
        }
    }
}
