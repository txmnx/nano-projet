﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour, OnActionBeatElement
{
    public KeyCode hitKey = KeyCode.Z;
    public KeyCode reflectKey = KeyCode.Q;
    public KeyCode laserKey = KeyCode.D;
    public KeyCode guardKey = KeyCode.S;
    public KeyCode specialKey = KeyCode.W;
    public KeyCode eraseKey = KeyCode.X;

    public float chargeTime = 0.20f;
    public float chargeCounter = 0;
    public Slider health;
    public float maxLife = 1200;
    public float currentLife;
    public FightManager fightManager;               //Script managing fights, on the GameManager

    public enum MoveType { HIT, REFLECT, LASER, GUARD, SPECIAL, NEUTRAL }     //List of moves

    public struct Move
    {
        public MoveType move;
        public bool isCharged;
    }
    public Move[] buffer = new Move[InputTranslator.step];

    public Text[] inputsText = new Text[InputTranslator.step];

    private int bufferLength;
    private int currentAction;

    private void Start()
    {
        currentLife = maxLife;
        Reset();

        foreach (Text text in inputsText) {
            text.text = "";
        }

        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnActionBeat()
    {
        Debug.Log("PLAYER");
        inputsText[currentAction++].text = "";
    }

    public void Reset()
    {
        for (int i = 0; i < InputTranslator.step; i++) {     //initialising the buffer
            buffer[i].move = MoveType.NEUTRAL;
            buffer[i].isCharged = false;
        }
        foreach (Text text in inputsText) {
            text.text = "";
        }

        bufferLength = 0;
        currentAction = 0;
    }

    public void BufferReset()
    {
        buffer = new Move[InputTranslator.step];
        Reset();
    }

    void Update()
    {
        if (InputTranslator.sequence == Sequence.INPUT)      
        {
            if (bufferLength < InputTranslator.step)
            {
                if (Input.GetKey(hitKey))
                {
                    chargeCounter += Time.deltaTime;
                    if(Input.GetKeyUp(hitKey))
                    {
                        buffer[bufferLength].move = MoveType.HIT;
                        inputsText[bufferLength].text = "HIT";
                        if (chargeCounter > chargeTime)
                        {
                            buffer[bufferLength].isCharged = true;
                        }
                        bufferLength++; 
                    }
                }
                else if (Input.GetKey(reflectKey)) {
                    chargeCounter += Time.deltaTime;
                    if (Input.GetKeyUp(reflectKey))
                    {
                        buffer[bufferLength].move = MoveType.REFLECT;
                        inputsText[bufferLength].text = "REFLECT";
                        if (chargeCounter > chargeTime)
                        {
                            buffer[bufferLength].isCharged = true;
                        }
                        bufferLength++;
                    }
                }
                else if (Input.GetKey(laserKey)) {
                    chargeCounter += Time.deltaTime;
                    if (Input.GetKeyUp(laserKey))
                    {
                        buffer[bufferLength].move = MoveType.LASER;
                        inputsText[bufferLength].text = "LASER";
                        if (chargeCounter > chargeTime)
                        {
                            buffer[bufferLength].isCharged = true;
                        }
                        bufferLength++;
                    }
                }
                else if (Input.GetKeyUp(guardKey))
                {
                    buffer[bufferLength].move = MoveType.GUARD;
                    Debug.Log("GUARD " + bufferLength);
                    inputsText[bufferLength].text = "GUARD";
                    bufferLength++;
                }
                else if (Input.GetKeyUp(specialKey))
                {
                    buffer[bufferLength].move = MoveType.SPECIAL;
                    Debug.Log("SPECIAL " + bufferLength);
                    inputsText[bufferLength].text = "SPECIAL";
                    bufferLength++;
                }
            }

            if (Input.GetKeyUp(eraseKey)) {
                if (bufferLength > 0) {
                    buffer[bufferLength - 1].move = MoveType.NEUTRAL;
                    Debug.Log("NEUTRAL " + (bufferLength - 1));
                    inputsText[bufferLength - 1].text = "";
                    bufferLength--;
                }
            }
        }
    }

    public void OnEnterActionBeat() { }
}
