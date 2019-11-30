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

    public Image[] inputsImage = new Image[InputTranslator.step];

    private int bufferLength;
    private int currentAction;

    private void Start()
    {
        currentLife = maxLife;
        Reset();

        foreach (Image image in inputsImage) {
            image.enabled = false;
        }

        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnActionBeat()
    {
        Debug.Log("PLAYER");
        inputsImage[currentAction++].enabled = false;
    }

    public void Reset()
    {
        for (int i = 0; i < InputTranslator.step; i++) {     //initialising the buffer
            buffer[i] = Player.Move.NEUTRAL;
        }
        foreach (Image image in inputsImage) {
            image.enabled = false;
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
                    inputsImage[bufferLength].sprite = fightManager.hitSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(guardKey)) {
                    buffer[bufferLength] = Move.GUARD;
                    Debug.Log("GUARD " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.guardSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(grabKey)) {
                    buffer[bufferLength] = Move.GRAB;
                    Debug.Log("GRAB " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.specialSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
            }

            if (Input.GetKeyUp(eraseKey)) {
                if (bufferLength > 0) {
                    buffer[bufferLength - 1] = Move.NEUTRAL;
                    Debug.Log("NEUTRAL " + (bufferLength - 1));
                    inputsImage[bufferLength - 1].enabled = false;
                    bufferLength--;
                }
            }
        }
    }

    public void OnEnterActionBeat() { }
}
