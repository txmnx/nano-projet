using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FightManager : MonoBehaviour, OnInputBeatElement, OnActionBeatElement
{
    public Player player1;
    public Player player2;
    public float basicDamage = 100.0f;
    public float specialDamage = 500.0f;

    public Text action1Text;
    public Text action2Text;

    private void Start()
    {
        InputTranslator.RegisterOnInputBeatElement(this);
        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnEnterInputBeat()
    {
        action1Text.text = "";
        action2Text.text = "";

        player1.Reset();
        player2.Reset();

    }

    public void OnActionBeat()
    {
        Debug.Log("ACTION");
        if (action1Text.text == "") {
            action1Text.text = player1.buffer[0].ToString();
            action2Text.text = player2.buffer[0].ToString();
        }
        else {
            action1Text.text = player1.buffer[1].ToString();
            action2Text.text = player2.buffer[1].ToString();
        }
    }

    public void Flush()
    {
        compareInputs();
    }

    public void compareInputs()     //Actual comparison of the players inputs
    {
       
        for (int i = 0; i < InputTranslator.step; i++) {
            player1.currentLife -= compareMove(player1.buffer[i].move, player2.buffer[i].move);
            Debug.Log("Move " + i + " ; Player 1 : " + -compareMove(player1.buffer[i].move, player2.buffer[i].move));
            player2.currentLife -= compareMove(player2.buffer[i].move, player1.buffer[i].move);
            Debug.Log("Move " + i + " ; Player 2 : " + -compareMove(player2.buffer[i].move, player1.buffer[i].move));
        }
     
        
    }

    public float compareMove(Player.MoveType move1, Player.MoveType move2) //return HP lost for the player for his move and his opponent's
    {
        if((move1 == Player.MoveType.REFLECT && move2 == Player.MoveType.HIT) || 
           (move1 == Player.MoveType.LASER && move2 == Player.MoveType.REFLECT) || 
           (move1 == Player.MoveType.HIT && move2 == Player.MoveType.LASER) || 
           (move1 == Player.MoveType.SPECIAL && (move2 == Player.MoveType.HIT || move2 == Player.MoveType.LASER || move2 == Player.MoveType.REFLECT)) || 
           (move1 == Player.MoveType.NEUTRAL && (move2 == Player.MoveType.HIT || move2 == Player.MoveType.LASER || move2 == Player.MoveType.REFLECT)))
        {
            return basicDamage;
        }
        else if ((move1 == Player.MoveType.GUARD && move2 == Player.MoveType.SPECIAL) || (move1 == Player.MoveType.NEUTRAL && move2 == Player.MoveType.SPECIAL))
        {
            return specialDamage;
        }
        else
        {
            return 0.0f;
        }
    }

    public void OnInputBeat() { }
    public void OnEnterActionBeat() { }
}
