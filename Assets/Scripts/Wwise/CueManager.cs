using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{

    public int cueCounter = 0;
    public static bool isCounting = false;
    public bool isBeatDetected = false;

    public MatchManager matchManager;

    public void CueFunction()
    {
        if (isCounting)
        {
            cueCounter++;

            switch (cueCounter)
            {
                case 1:         //Début de la musique d'intro
                    
                    break;

                case 2:         //Flag annonce round
                    //Functions to call

                    break;

                case 3:         //Flag lancement fight
                    //Functions to call
                    StopCueCounting();
                    break;

                case 4:         //Début d'outro
                    //Functions to call
                    isBeatDetected = false;
                    matchManager.onRoundEnd();
                    break;

                case 5:         //Fin d'outro / Initialisation prochain round
                    //Functions to call
                    matchManager.resetRound();
                    cueCounter = 0;
                    break;
            }
        }
    }

    public void StartCueCounting()
    {
        isCounting = true;
    }

    public void StopCueCounting()
    {
        isCounting = false;
    }
}
