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

    public Sequence currentSequence;
    public Player winner;
    public Player loser;
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
        if (isWon) {
            if (!hasIncremented) {
                winner.wins += 1;
                winSliders[winnerID].value = winner.wins;
                hasIncremented = true;
            }

            onRoundEnd();

            if (winner != null && winner.wins == roundToWin) {
                //SONDIER
                if (winnerID == 0) {
                    onMatchEnd(players[0], players[1]);
                    musicManager.WinUS();
                }
                if (winnerID == 1) {
                    onMatchEnd(players[1], players[0]);
                    musicManager.WinJP();
                }
                //SONDIER;
            }
            else
            {
                //SONDIER
                if (winnerID == 0)
                {
                    musicManager.RoundWinUS();
                }
                if (winnerID == 1)
                {
                    musicManager.RoundWinJP();
                }
                //SONDIER;
            }


        }
        else if (players[0].currentLife <= 0) {
            winner = players[1];
            loser = players[0];
            winnerID = 1;
            isWon = true;
        }

        else if (players[1].currentLife <= 0) {
            winner = players[0];
            loser = players[1];
            winnerID = 0;
            isWon = true;
        }

        //if (matchIsEnd)

    }

    public void customResetRound()
    {
        camera.GetComponent<Animator>().SetTrigger("Start");
    }

    public void onRoundEnd()
    {
        loser.animator.Play("Death", 0);
        //anim/sons de fin de round
    }

    public void resetRound()
    {
        for (int i = 0; i < players.Length; i++) {
            players[i].currentLife = players[i].maxLife;
            players[i].health.value = players[i].currentLife;
            players[i].BufferReset();
        }
        camera.GetComponent<Animator>().SetTrigger("Start");
        loser.animator.Play("Idle", 0);
        winner = null;
        loser = null;
        isWon = false;
        hasIncremented = false;
    }


    public void onMatchEnd(Player winner, Player loser)
    {
        matchIsEnd = true;

        winner.animator.Play("Victory", 0);
        loser.animator.Play("Death", 0);
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
    }
}