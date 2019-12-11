using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour
{
    public MusicManager musicManager;

    public void StartIntroMusic()
    {
        musicManager.StartIntro();
    }
}
