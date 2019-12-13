using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MatchManager : MonoBehaviour, OnBeatElement
{
    public Player[] players;
    public int roundToWin = 2;

    public Slider[] winSliders;

    private Sequence currentSequence;
    public Player winner;
    public int winnerID;
    public bool isWon = false;

    private bool gameIsPaused = false;
    private bool roundIsEnd = false;
    private bool matchIsEnd = false;
    private bool hasIncremented = false;
    
    public Color victoryJapColor;
    public Color victoryUsColor;
    public Color baseColor;
    public CueManager cueManager;

    //SONDIER
    public MusicManager musicManager;
    public GameObject camera;
    //SONDIER


    void Start()
    {

        customStart();
        
    }

    public void customStart()
    {
        Camera.main.backgroundColor = baseColor;

        BeatManager.RegisterOnBeatElement(this);

        for (int i = 0; i < winSliders.Length; i++)
        {
            winSliders[i].maxValue = roundToWin;
        }
    }

    public void OnBeat()
    {
     
    }

    void Update()
    {
        if (!gameIsPaused && Input.GetKeyDown(KeyCode.Space))
        {
            pause();
        }
        else if(gameIsPaused && Input.GetKeyDown(KeyCode.Space))
        {
            resume();
        }

        if (isWon)
        {
            if(!hasIncremented)
            {
                winner.wins += 1;
                winSliders[winnerID].value = winner.wins;
                hasIncremented = true;
            }


            //SONDIER
            if(winnerID == 0)
            {
                musicManager.RoundWinUS();
            }
            if (winnerID == 1)
            {
                musicManager.RoundWinJP();
            }
            //SONDIER;

            onRoundEnd();

            if (winner != null && winner.wins == roundToWin)
            {
                

                //SONDIER
                if (winnerID == 0)
                {
                    onMatchEnd(players[0], players[1]);
                    musicManager.WinUS();
                }
                if (winnerID == 1)
                {
                    onMatchEnd(players[1], players[0]);
                    musicManager.WinJP();
                }
                //SONDIER;
            }


        }
        else if (players[0].currentLife <= 0)
        {
            winner = players[1];
            winnerID = 1;
            isWon = true;
        }

        else if(players[1].currentLife <= 0)
        {
            winner = players[0];
            winnerID = 0;
            isWon = true;
        }

        //if (matchIsEnd)
           
    }

    public void resetRound()
    {
        camera.GetComponent<Animator>().SetTrigger("Start");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].currentLife = players[i].maxLife;
            players[i].health.value = players[i].currentLife;
            players[i].BufferReset();
        }
        winner = null;
        isWon = false;
        hasIncremented = false;
    }

    public void onRoundEnd()
    {
        //anim/sons de fin de round
    }

  

    public void onMatchEnd(Player winner, Player loser)
    {
        matchIsEnd = true;

        winner.animator.Play("Victory", 0);
        loser.animator.Play("Death", 0);

       
    }

    public void resume()
    {
        AkSoundEngine.PostEvent("UI_Menu_UnPauseGame", gameObject);
        gameIsPaused = false;
        InputTranslator.sequence = currentSequence;
    }

    public void pause()
    {
        AkSoundEngine.PostEvent("UI_Menu_PauseGame", gameObject);
        gameIsPaused = true;
        Debug.Log("PAUSE");
        currentSequence = InputTranslator.sequence;
        InputTranslator.sequence = Sequence.IDLE;
    }

    public void resetGame()
    {
        Camera.main.backgroundColor = baseColor;
        for (int i = 0; i < players.Length; i++)
        {
            players[i].wins = 0;
            winSliders[i].value = players[i].wins;
        }
        resetRound();
        resume();
    }
}
