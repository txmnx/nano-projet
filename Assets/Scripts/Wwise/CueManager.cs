using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{

    public int cueCounter = 0;
    public bool isCounting = false;

    public void CueFunction()
    {
        if (isCounting)
        {
            cueCounter++;

            switch (cueCounter)
            {
                case 1:         //Début de la musique d'intro
                    //Functions to call
                    
                    break;

                case 2:         //Flag annonce round
                    //Functions to call

                    break;

                case 3:         //Flag lancement fight
                    //Functions to call

                    break;

                case 4:         //Début d'outro
                    //Functions to call

                    break;

                case 5:         //Fin d'outro / Initialisation prochain round
                    //Functions to call

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
