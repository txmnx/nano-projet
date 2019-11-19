using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour, OnActionBeatElement
{
    public KeyCode hitKey = KeyCode.Z;
    public KeyCode guardKey = KeyCode.Q;
    public KeyCode grabKey = KeyCode.D;
    public KeyCode eraseKey = KeyCode.S;

    public float maxLife = 100;
    public float currentLife;
    public FightManager fightManager;               //Script managing fights, on the GameManager

    public enum Move { HIT, GUARD, GRAB, NEUTRAL }     //List of moves, will be changed to a class

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
            buffer[i] = Player.Move.NEUTRAL;
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
            if (bufferLength < InputTranslator.step) {
                if (Input.GetKeyUp(hitKey)) {
                    buffer[bufferLength] = Move.HIT;
                    Debug.Log("HIT " + bufferLength);
                    inputsText[bufferLength].text = "HIT";
                    bufferLength++;
                }
                else if (Input.GetKeyUp(guardKey)) {
                    buffer[bufferLength] = Move.GUARD;
                    Debug.Log("GUARD " + bufferLength);
                    inputsText[bufferLength].text = "GUARD";
                    bufferLength++;
                }
                else if (Input.GetKeyUp(grabKey)) {
                    buffer[bufferLength] = Move.GRAB;
                    Debug.Log("GRAB " + bufferLength);
                    inputsText[bufferLength].text = "GRAB";
                    bufferLength++;
                }
            }

            if (Input.GetKeyUp(eraseKey)) {
                if (bufferLength > 0) {
                    buffer[bufferLength - 1] = Move.NEUTRAL;
                    Debug.Log("NEUTRAL " + (bufferLength - 1));
                    inputsText[bufferLength - 1].text = "";
                    bufferLength--;
                }
            }
        }
    }

    public void OnEnterActionBeat() { }
}
