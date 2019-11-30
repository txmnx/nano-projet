using System.Collections;
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
        public Sprite sprite;
        public bool isCharged;
    }
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
            buffer[i].move = MoveType.NEUTRAL;
            buffer[i].isCharged = false;
            buffer[i].sprite = fightManager.neutralSprite;
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
                    buffer[bufferLength].move = MoveType.HIT;
                    buffer[bufferLength].sprite = fightManager.hitSprite;
                    Debug.Log("HIT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.hitSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(reflectKey)) {
                    buffer[bufferLength].move = MoveType.REFLECT;
                    buffer[bufferLength].sprite = fightManager.reflectSprite;
                    Debug.Log("REFLECT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.reflectSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(laserKey)) {
                    buffer[bufferLength].move = MoveType.LASER;
                    buffer[bufferLength].sprite = fightManager.laserSprite;
                    Debug.Log("LASER " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.laserSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(guardKey))
                {
                    buffer[bufferLength].move = MoveType.GUARD;
                    buffer[bufferLength].sprite = fightManager.guardSprite;
                    Debug.Log("GUARD " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.guardSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                else if (Input.GetKeyUp(specialKey))
                {
                    buffer[bufferLength].move = MoveType.SPECIAL;
                    buffer[bufferLength].sprite = fightManager.specialSprite;
                    Debug.Log("SPECIAL " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.specialSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
            }

            if (Input.GetKeyUp(eraseKey)) {
                if (bufferLength > 0) {
                    buffer[bufferLength - 1].move = MoveType.NEUTRAL;
                    buffer[bufferLength - 1].sprite = fightManager.neutralSprite;
                    Debug.Log("NEUTRAL " + (bufferLength - 1));
                    inputsImage[bufferLength - 1].enabled = false;
                    bufferLength--;
                }
            }
        }
    }

    public void OnEnterActionBeat() { }
}
