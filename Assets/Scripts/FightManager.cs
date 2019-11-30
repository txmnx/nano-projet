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
    private float counter;

    public Text action1Text;
    public Text action2Text;

    private void Start()
    {
        counter = 1;
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
        player1.health.value = player1.currentLife;
        player2.health.value = player2.currentLife;
    }

    public void Flush()
    {
        compareInputs();
    }

    public void compareInputs()     //Actual comparison of the players inputs
    {
       
        for (int i = 0; i < InputTranslator.step; i++) {
            player1.currentLife -= compareMove(player1.buffer[i], player2.buffer[i]);
            Debug.Log("Move " + i + " ; Player 1 : " + -compareMove(player1.buffer[i], player2.buffer[i]));
            player2.currentLife -= compareMove(player2.buffer[i], player1.buffer[i]);
            Debug.Log("Move " + i + " ; Player 2 : " + -compareMove(player2.buffer[i], player1.buffer[i]));
        }
     
        
    }

    public float compareMove(Player.Move move1, Player.Move move2) //return HP lost for the player for his move and his opponent's
    {
        if(move2.isCharged)
        {
            if(move2.move == Player.MoveType.HIT)
            {
                if (move1.move == Player.MoveType.HIT && !move1.isCharged)
                    return basicDamage;
                else if (move1.move == Player.MoveType.REFLECT || move1.move == Player.MoveType.NEUTRAL || move1.move == Player.MoveType.SPECIAL)
                    return basicDamage * 2;
                else
                    return 0;
            }
            else if (move2.move == Player.MoveType.REFLECT)
            {
                if (move1.move == Player.MoveType.REFLECT && !move1.isCharged)
                    return basicDamage;
                else if (move1.move == Player.MoveType.LASER || move1.move == Player.MoveType.NEUTRAL || move1.move == Player.MoveType.SPECIAL)
                    return basicDamage * 2;
                else
                    return 0;
            }
            else if (move2.move == Player.MoveType.LASER)
            {
                if (move1.move == Player.MoveType.LASER && !move1.isCharged)
                    return basicDamage;
                else if (move1.move == Player.MoveType.HIT || move1.move == Player.MoveType.NEUTRAL || move1.move == Player.MoveType.SPECIAL)
                    return basicDamage * 2;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        else if ((move1.move == Player.MoveType.REFLECT && move2.move == Player.MoveType.HIT) ||
           (move1.move == Player.MoveType.LASER && move2.move == Player.MoveType.REFLECT) ||
           (move1.move == Player.MoveType.HIT && move2.move == Player.MoveType.LASER) ||
           (move1.move == Player.MoveType.SPECIAL && (move2.move == Player.MoveType.HIT || move2.move == Player.MoveType.LASER || move2.move == Player.MoveType.REFLECT)) ||
           (move1.move == Player.MoveType.NEUTRAL && (move2.move == Player.MoveType.HIT || move2.move == Player.MoveType.LASER || move2.move == Player.MoveType.REFLECT)))
        {
            return basicDamage;
        }
        else if ((move1.move == Player.MoveType.GUARD && move2.move == Player.MoveType.SPECIAL) || (move1.move == Player.MoveType.NEUTRAL && move2.move == Player.MoveType.SPECIAL))
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
