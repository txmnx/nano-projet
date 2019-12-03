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
    public float chargedCoeff = 2f;
    public float specialCoeff = 5f;
    private int counter;

    public Image action1Image;
    public Image action2Image;

    public Sprite hitSprite;
    public Sprite reflectSprite;
    public Sprite laserSprite;
    public Sprite guardSprite;
    public Sprite specialSprite;
    public Sprite neutralSprite;

    private float[,] coefficients;


    /**
     * Coefficients :
     * Damage received by player 1
     * Player 1     Player 2
     * 
     *              HIT   REFLECT   LASER   GUARD   SPECIAL   NEUTRAL
     * HIT          0     0         1       0       0         0
     * REFLECT      1     0         0       0       0         0
     * LASER        0     1         0       0       0         0
     * GUARD        0     0         0       0       spe       0
     * SPECIAL      1     1         1       0       0         0
     * NEUTRAL      1     1         1       0       spe       0
     *
     */

    private void Awake()
    {
        int movesLength = Enum.GetNames(typeof(Player.MoveType)).Length;
        coefficients = new float[movesLength, movesLength];
        
        coefficients[(int)Player.MoveType.HIT, (int)Player.MoveType.LASER] = 1f;
        coefficients[(int)Player.MoveType.REFLECT, (int)Player.MoveType.HIT] = 1f;
        coefficients[(int)Player.MoveType.LASER, (int)Player.MoveType.REFLECT] = 1f;
        coefficients[(int)Player.MoveType.GUARD, (int)Player.MoveType.SPECIAL] = specialCoeff;
        coefficients[(int)Player.MoveType.SPECIAL, (int)Player.MoveType.HIT] = 1f;
        coefficients[(int)Player.MoveType.SPECIAL, (int)Player.MoveType.REFLECT] = 1f;
        coefficients[(int)Player.MoveType.SPECIAL, (int)Player.MoveType.LASER] = 1f;
        coefficients[(int)Player.MoveType.NEUTRAL, (int)Player.MoveType.HIT] = 1f;
        coefficients[(int)Player.MoveType.NEUTRAL, (int)Player.MoveType.REFLECT] = 1f;
        coefficients[(int)Player.MoveType.NEUTRAL, (int)Player.MoveType.LASER] = 1f;
        coefficients[(int)Player.MoveType.NEUTRAL, (int)Player.MoveType.SPECIAL] = specialCoeff;
    }

    private void Start()
    {
        counter = 0;
        InputTranslator.RegisterOnInputBeatElement(this);
        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnEnterInputBeat()
    {
        counter = 0;
        action1Image.enabled = false;
        action2Image.enabled = false;

        player1.Reset();
        player2.Reset();
    }

    public void OnActionBeat()
    {
        player1.currentLife -= CompareMove(player1.buffer[counter], player2.buffer[counter]);
        player2.currentLife -= CompareMove(player2.buffer[counter], player1.buffer[counter]);
        player1.health.value = player1.currentLife;
        player2.health.value = player2.currentLife;

        counter = counter + 1;

        Debug.Log("ACTION");
        if (!action1Image.enabled) {
            action1Image.enabled = true;
            action2Image.enabled = true;
            //action1Text.text = player1.buffer[0].ToString();
            //action2Text.text = player2.buffer[0].ToString();

            action1Image.sprite = player1.buffer[0].sprite;
            action2Image.sprite = player2.buffer[0].sprite;
        }
        else {
            //action1Text.text = player1.buffer[1].ToString();
            //action2Text.text = player2.buffer[1].ToString();

            action1Image.sprite = player1.buffer[1].sprite;
            action2Image.sprite = player2.buffer[1].sprite;
        }
        
    }
    
    public float CompareMove(Player.Move move1, Player.Move move2) //return HP lost for the player for his move and his opponent's
    {
        if (move2.isCharged) {
            if (move1.move == move2.move && move1.isCharged) {
                return basicDamage * coefficients[(int)move1.move, (int)move2.move];
            }
            else {
                return basicDamage * chargedCoeff;
            }
        }
        else {
            return basicDamage * coefficients[(int)move1.move, (int)move2.move];
        }
    }

    public void OnInputBeat() { }
    public void OnEnterActionBeat() { }
}
