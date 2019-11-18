using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FightManager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public float hitDamage = 20.0f;
    public float grabDamage = 30.0f;
    public bool isInputPhase;
    public bool actionIsDone = false;
    

    public float phaseTime = 4.0f;
    public float timeCounter;
    public float phaseTimeCounter;

    private void Start()
    {
        timeCounter = 0.0f;
        phaseTimeCounter = 0.0f;
        isInputPhase = true;

    }

    void Update()
    {
        /*
        timeCounter += Time.deltaTime;
        phaseTimeCounter += Time.deltaTime;
        managePhase();

        if (!isInputPhase)      
        {
            actionPhase();
        }
        */

    }

    public void managePhase()   //Timer switching phases
    {
        if(phaseTimeCounter > phaseTime)
        {
            phaseTimeCounter = 0.0f;
            if(isInputPhase)
            {
                isInputPhase = false;
                actionIsDone = false;
            }
            else
            {
                isInputPhase = true;
                for(int i = 0; i < Player.bufferSize; i++)
                {
                    player1.buffer[i] = Player.Move.NEUTRAL;
                    player2.buffer[i] = Player.Move.NEUTRAL;
                }
               
            }
        }
    }

    public void Flush()
    {
        compareInputs();
        player1.Reset();
        player2.Reset();
    }

    public void actionPhase()   //action phase managing
    {
        if(!actionIsDone)
        {
            compareInputs();
            actionIsDone = true;        
        }                              
    }

    public void inputPhase()     //input phase managing
    {
                     
    }

    public void compareInputs()     //Actual comparison of the players inputs
    {
        /*
        for (int i = 0; i < Player.bufferSize; i++) {
            player1.currentLife -= compareMove(player1.buffer[i], player2.buffer[i]);
            Debug.Log("Move " + i + " ; Player 1 : " + -compareMove(player1.buffer[i], player2.buffer[i]));
            player2.currentLife -= compareMove(player2.buffer[i], player1.buffer[i]);
            Debug.Log("Move " + i + " ; Player 2 : " + -compareMove(player2.buffer[i], player1.buffer[i]));
        }
        */
        Debug.Log("Moves Player 1 : ");
        for (int i = 0; i < Player.bufferSize; i++) {
            Debug.Log(player1.buffer[i]);
        }
        Debug.Log("Moves Player 2 : ");
        for (int i = 0; i < Player.bufferSize; i++) {
            Debug.Log(player2.buffer[i]);
        }
    }

    public float compareMove(Player.Move move1, Player.Move move2) //return HP lost for the player for his move and his opponent's
    {
        if((move1 == Player.Move.GUARD && move2 == Player.Move.GRAB) || (move1 == Player.Move.NEUTRAL && move2 == Player.Move.GRAB))
        {
            return grabDamage;
        }
        else if ((move1 == Player.Move.GRAB && move2 == Player.Move.HIT) || (move1 == Player.Move.NEUTRAL && move2 == Player.Move.HIT))
        {
            return hitDamage;
        }
        else
        {
            return 0.0f;
        }
    }
}
