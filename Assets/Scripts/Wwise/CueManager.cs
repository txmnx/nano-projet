using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueManager : MonoBehaviour
{
    public FinalMenu finalMenu;

    public int cueCounter = 0;
    public static bool isCounting = false;
    public bool isBeatDetected = false;
    public GameObject round1Sprite;
    public GameObject round2Sprite;
    public GameObject finalRoundSprite;
    public GameObject fightSprite;
    public GameObject victory1Sprite;
    public GameObject victory2Sprite;

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
                    if (matchManager.players[0].wins == 0 && matchManager.players[1].wins == 0)
                        round1Sprite.GetComponent<Animator>().Play("AnimRound", 0, 0);
                    else if ((matchManager.players[0].wins == 0 && matchManager.players[1].wins == 1) || (matchManager.players[0].wins == 1 && matchManager.players[1].wins == 0))
                        round2Sprite.GetComponent<Animator>().Play("AnimRound", 0, 0);
                    else
                        finalRoundSprite.GetComponent<Animator>().Play("AnimRound", 0, 0);
                    //Functions to call

                    break;

                case 3:         //Flag lancement fight
                    //Functions to call
                    fightSprite.GetComponent<Animator>().Play("AnimFight");
                    StopCueCounting();
                    break;

                case 4:         //Début d'outro
                    //Functions to call
                    InputTranslator.sequence = Sequence.IDLE;

                    if (matchManager.matchIsEnd)
                    {
                        if (matchManager.players[0].wins > matchManager.players[1].wins)
                            victory1Sprite.GetComponent<Animator>().Play("AnimVictory", 0, 0);
                        else
                            victory2Sprite.GetComponent<Animator>().Play("AnimVictory", 0, 0);
                    }
                   
                    isBeatDetected = false;
                    matchManager.onRoundEnd();
                    if (matchManager.matchIsEnd)
                        finalMenu.Display();
                    break;

                case 5:         //Fin d'outro / Initialisation prochain round
                    //Functions to call
                    if (!matchManager.matchIsEnd)
                        matchManager.resetRound();

                    InputTranslator.currentStep = 1;

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
