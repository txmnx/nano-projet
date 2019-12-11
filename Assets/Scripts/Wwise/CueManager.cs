using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{

    public int cueCounter = 0;
    public bool isCounting = true;

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

                    break;

                case 3:         //Flag lancement fight

                    break;

                case 4:         //Début d'outro

                    break;

                case 5:         //Fin d'outro / Initialisation prochain round

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
