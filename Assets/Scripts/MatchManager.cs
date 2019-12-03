using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class MatchManager : MonoBehaviour
{
    public Player[] players;
    public int roundToWin;

    public Slider[] winSliders;

    private int winner;


    void Start()
    {
        winner = -1;

        //for(int i = 0; i<)
    }

    void Update()
    {
        if(players[0].currentLife <= 0)
        {
            winner = 0;
        }

        if(players[1].currentLife <= 0)
        {
            winner = 1;
        }

        if(winner>=0)
        {
            players[winner].wins += 1;
            onRoundEnd();
            resetRound();

            if(players[winner].wins == roundToWin)
            {
                onMatchEnd(players[winner]);
            }
            
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
        }
        winner = -1;
    }

    public void onMatchEnd(Player winner)
    {
        //anim/son de fin de partie
    }

   

}
