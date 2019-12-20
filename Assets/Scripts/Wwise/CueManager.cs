using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{
    public FinalMenu finalMenu;

    public int cueCounter = 0;
    public static bool isCounting = false;
    public bool isBeatDetected = false;

    public GameObject Player1Body;
    public GameObject Player2Body;

    public MatchManager matchManager;

    public InputTranslator it;

    //NEW FUNCTIONS
    public void OnActionCue() //Début de phase d'action
    {
        InputTranslator.sequence = Sequence.ACTION;
        Debug.Log("OnActionCue");
        InputTranslator.currentStep = 1;
        AkSoundEngine.PostEvent("SFX_InputPhase_Out", gameObject);
    }

    public void OnBeat()
    {
        Debug.Log(InputTranslator.sequence);
    }

    public void OnInputCue() //Début de phase d'Input
    {
        InputTranslator.sequence = Sequence.INPUT;
        Debug.Log("OnInputCue");
        AkSoundEngine.PostEvent("SFX_InputPhase_In", gameObject);
        AkSoundEngine.SetSwitch("Charged", "No", Player1Body);
        AkSoundEngine.SetSwitch("Charged", "No", Player2Body);
        InputTranslator.currentStep = 1;
    }

    public void OnIdleCue() //Début de phase d'Idle
    {
        InputTranslator.sequence = Sequence.IDLE;
        Debug.Log("OnIdleCue");
        InputTranslator.currentStep = 1;
    }

    public void OnIntroStartCue() //Début de la musique d'intro
    {
        Debug.Log("OnIntroStartCue");
    }

    public void OnIntroRoundCue() //Annonce du round
    {
        Debug.Log("OnIntroRoundCue");
    }

    public void OnIntroFightCue() //Annonce de début de fight
    {
        Debug.Log("OnIntroFightCue");
    }

    public void OnRoundOutroCue() //Début de la musique d'outro de round
    {
        InputTranslator.sequence = Sequence.IDLE;
        matchManager.onRoundEnd();
        Debug.Log("OnRoundOutroCue");
    }

    public void OnRoundResetCue() //Fin de la musique d'outro du round
    {
        matchManager.resetRound();


        Debug.Log("OnRoundResetCue");
    }

    public void OnMatchOutroCue() //Début de la musique d'outro du match
    {
        InputTranslator.sequence = Sequence.IDLE;
        Debug.Log("OnMatchOutroCue");
    }

    public void OnMatchEndCue() //Affichage du menu de fin de match par-dessus la loop musicale du thème du vainqueur
    {
        if (matchManager.matchIsEnd)
            finalMenu.Display();
        Debug.Log("OnMatchEndCue");
    }





    //C'est vieux et c'est de la marde
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
                    
                    
                    break;

                case 5:         //Fin d'outro / Initialisation prochain round
                    //Functions to call
                    

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
