using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FightManager : MonoBehaviour, OnInputBeatElement, OnActionBeatElement
{
    public Player player1;
    public Player player2;
    public float hitDamage = 20.0f;
    public float grabDamage = 30.0f;

    public Image action1Image;
    public Image action2Image;

    public Sprite hitSprite;
    public Sprite guardSprite;
    public Sprite specialSprite;

    private void Start()
    {
        InputTranslator.RegisterOnInputBeatElement(this);
        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnEnterInputBeat()
    {
        action1Image.enabled = false;
        action2Image.enabled = false;

        player1.Reset();
        player2.Reset();
    }

    public void OnActionBeat()
    {
        Debug.Log("ACTION");
        if (!action1Image.enabled) {
            action1Image.enabled = true;
            action2Image.enabled = true;
            //action1Text.text = player1.buffer[0].ToString();
            //action2Text.text = player2.buffer[0].ToString();

            action1Image.sprite = hitSprite;
            action2Image.sprite = hitSprite;
        }
        else {
            //action1Text.text = player1.buffer[1].ToString();
            //action2Text.text = player2.buffer[1].ToString();

            action1Image.sprite = hitSprite;
            action2Image.sprite = hitSprite;
        }
    }

    public void Flush()
    {
        compareInputs();
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
        for (int i = 0; i < InputTranslator.step; i++) {
            Debug.Log(player1.buffer[i]);
        }
        Debug.Log("Moves Player 2 : ");
        for (int i = 0; i < InputTranslator.step; i++) {
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

    public void OnInputBeat() { }
    public void OnEnterActionBeat() { }
}
