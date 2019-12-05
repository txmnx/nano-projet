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

    public Player winner;
    private int winnerID;
    public bool isWon = false;

    private bool gameIsPaused = false;
    private bool roundIsEnd = false;
    private bool matchIsEnd = false;


    void Start()
    {
        BeatManager.RegisterOnBeatElement(this);

        for (int i = 0; i<winSliders.Length; i++)
        {
            winSliders[i].maxValue = roundToWin;
        }

        
    }

    public void OnBeat()
    {
        /*if (isWon)
        {
            winner.wins += 1;
            winSliders[winnerID].value = winner.wins;
            onRoundEnd();
            resetRound();

            if (winner != null && winner.wins == roundToWin)
            {
                // onMatchEnd(players[winner]);
            }

        }*/
    }

    void Update()
    {
        if (!gameIsPaused && Input.GetKeyDown(KeyCode.Space))
        {
            pause();
        }
        if(gameIsPaused && Input.GetKeyDown(KeyCode.Space))
        {
            resume();
        }
        if (isWon)
        {
            winner.wins += 1;
            winSliders[winnerID].value = winner.wins;
            onRoundEnd();
            resetRound();

            if (winner != null && winner.wins == roundToWin)
            {
                // onMatchEnd(players[winner]);
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
    }

    public void onRoundEnd()
    {

        //anim/sons de fin de round
    }

    public void resetRound()
    {
        for(int i = 0; i<players.Length; i++)
        {
            players[i].currentLife = players[i].maxLife;
            players[i].health.value = players[i].currentLife;
        }
        winner = null;
        isWon = false;
    }

    public void onMatchEnd(Player winner)
    {
        //anim/son de fin de partie
    }

    public void resume()
    {
        AkSoundEngine.PostEvent("UI_Menu_UnPauseGame", gameObject);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void pause()
    {
        AkSoundEngine.PostEvent("UI_Menu_PauseGame", gameObject);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Debug.Log("PAUSE");
    }

}
