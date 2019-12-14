using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour
{
    public MusicManager musicManager;
    public CueManager cueManager;

    public void StartIntroMusic()
    {
        cueManager.isBeatDetected = true;
        musicManager.StartIntro();
    }

    public void IdleMusicState()
    {
        AkSoundEngine.SetState("MusicToPlay", "ST_Idle");
    }
}
