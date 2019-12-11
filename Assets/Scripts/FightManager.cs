using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class FightManager : MonoBehaviour, OnInputBeatElement, OnActionBeatElement, OnIdleBeatElement
{
    public Player player1;
    public Player player2;
    public float basicDamage = 300.0f;
    public float chargedCoeff = 3f;
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

    public System.Random random = new System.Random();


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
        customAwake();
    }

    public void customAwake()
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
        customStart();
    }

    public void customStart()
    {
        counter = 0;
        InputTranslator.RegisterOnInputBeatElement(this);
        InputTranslator.RegisterOnActionBeatElement(this);
        InputTranslator.RegisterOnIdleBeatElement(this);
    }

    public void OnEnterInputBeat()
    {
        counter = 0;
        action1Image.enabled = false;
        action2Image.enabled = false;
    }

    public void OnEnterIdleBeat()
    {
        counter = 0;
        action1Image.enabled = false;
        action2Image.enabled = false;

        Debug.Log("INCROYABLE");

        player1.Reset();
        player2.Reset();
    }

    public void OnActionBeat()
    {
        float damagePlayer1 = CompareMove(player1.buffer[counter], player2.buffer[counter]);
        float damagePlayer2 = CompareMove(player2.buffer[counter], player1.buffer[counter]);

        player1.PlayAnim(player1.buffer[counter], damagePlayer1 != 0f);
        player2.PlayAnim(player2.buffer[counter], damagePlayer2 != 0f);

        player1.currentLife -= damagePlayer1;
        player2.currentLife -= damagePlayer2;
        player1.health.value = player1.currentLife;
        AkSoundEngine.SetRTPCValue("RTPC_American_Health", player1.currentLife);
        player2.health.value = player2.currentLife;
        AkSoundEngine.SetRTPCValue("RTPC_Japan_Health", player2.currentLife);

        counter = counter + 1;

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
            if (move1.move == move2.move) {
                if (move1.isCharged) {
                    if(coefficients[(int)move1.move, (int)move2.move] > 0f)
                        postHitEvent(move2.move);
                    return basicDamage * coefficients[(int)move1.move, (int)move2.move];
                }
                else {
                    if (coefficients[(int)move1.move, (int)move2.move] > 0f)
                        postHitEvent(move2.move);
                    return basicDamage * chargedCoeff;
                }
            }
            else {
                if (coefficients[(int)move1.move, (int)move2.move] > 0f)
                    postHitEvent(move2.move);
                return basicDamage * coefficients[(int)move1.move, (int)move2.move] * chargedCoeff;
            }
        }
        else {
            if (coefficients[(int)move1.move, (int)move2.move] > 0f)
                postHitEvent(move2.move);
            return basicDamage * coefficients[(int)move1.move, (int)move2.move];
        }
    }

    public Sprite GetMoveSprite(Player.MoveType move)
    {
        switch (move) {
            case Player.MoveType.HIT:
                return hitSprite;
            case Player.MoveType.REFLECT:
                return reflectSprite;
            case Player.MoveType.LASER:
                return laserSprite;
            case Player.MoveType.GUARD:
                return guardSprite;
            case Player.MoveType.SPECIAL:
                return specialSprite;
            default:
                return neutralSprite;
        }
    }

    public void postHitEvent(Player.MoveType move)
    {
        switch (move)
        {
            case Player.MoveType.LASER:
                AkSoundEngine.SetState("Damage", "Laser");
                break;
            case Player.MoveType.HIT:
                AkSoundEngine.SetState("Damage", "Fente");
                break;
            case Player.MoveType.SPECIAL:
                AkSoundEngine.SetState("Damage", "Special");
                break;
            case Player.MoveType.REFLECT:
                AkSoundEngine.SetState("Damage", "Reflect");
                break;
            default:
                AkSoundEngine.SetState("Damage", "Neutral");
                break;
        }
    }

    public bool IsCounterMove(Player.MoveType type1, Player.MoveType type2)
    {
        return (coefficients[(int)type1, (int)type2] != 0);
    }

    public Player.MoveType GetCounterMoveType(Player.MoveType type)
    {
        switch(type) {
            case Player.MoveType.HIT:
                return Player.MoveType.LASER;
            case Player.MoveType.LASER:
                return Player.MoveType.REFLECT;
            case Player.MoveType.REFLECT:
                return Player.MoveType.HIT;
            case Player.MoveType.SPECIAL:
                return AIMovePicker.RandomSimpleMove(this).move;
            case Player.MoveType.GUARD:
                return Player.MoveType.SPECIAL;
            default:
                return AIMovePicker.RandomMove(this).move;
        }
    }

    public void OnInputBeat() { }
    public void OnIdleBeat() { }
    public void OnEnterActionBeat() { }


    
}
