using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour, OnActionBeatElement, OnInputBeatElement
{
    public KeyCode hitKey = KeyCode.Z;
    public KeyCode reflectKey = KeyCode.Q;
    public KeyCode laserKey = KeyCode.D;
    public KeyCode guardKey = KeyCode.S;
    public KeyCode specialKey = KeyCode.W;
    public KeyCode eraseKey = KeyCode.X;

    public float chargeTime = 0.80f;
    public float chargeCounter = 0;
    private bool isCharging = false;
    private KeyCode chargingMove;

    public Slider health;
    public float maxLife = 1200;
    public float currentLife;
    public FightManager fightManager;               //Script managing fights, on the GameManager
    public InputTranslator inputTranslator;
    public int wins;

    public enum MoveType { HIT, REFLECT, LASER, GUARD, SPECIAL, NEUTRAL }     //List of moves

    public struct Move
    {
        public MoveType move;
        public Sprite sprite;
        public bool isCharged;
    }
    public Move[] buffer = new Move[InputTranslator.step];

    public Image[] inputsImage = new Image[InputTranslator.step];

    public int bufferLength;
    private int currentAction;

    private void Start()
    {
        prepareFight();
    }

    public void prepareFight()
    {
        wins = 0;
        currentLife = maxLife;
        Reset();

        foreach (Image image in inputsImage)
        {
            image.enabled = false;
        }

        InputTranslator.RegisterOnActionBeatElement(this);
        InputTranslator.RegisterOnInputBeatElement(this);
    }

    public void OnInputBeat()
    {

    }

    public void OnEnterInputBeat()
    {
        for(int i = 0; i<inputsImage.Length; i++)
        {
            inputsImage[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void OnActionBeat()
    {
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
            if (!isCharging)
            {
                    if (bufferLength < InputTranslator.step)
                    {
                        if (Input.GetKeyDown(hitKey) && bufferLength < InputTranslator.step)
                        {
                            buffer[bufferLength].move = MoveType.HIT;
                            isCharging = true;
                            chargingMove = hitKey;
                            buffer[bufferLength].sprite = fightManager.hitSprite;
                            inputsImage[bufferLength].sprite = fightManager.hitSprite;
                            inputsImage[bufferLength].enabled = true;
                            bufferLength++;
                        }
                        if (Input.GetKeyDown(reflectKey) && bufferLength < InputTranslator.step)
                        {
                            buffer[bufferLength].move = MoveType.REFLECT;
                            isCharging = true;
                            chargingMove = reflectKey;
                            buffer[bufferLength].sprite = fightManager.reflectSprite;
                            inputsImage[bufferLength].sprite = fightManager.reflectSprite;
                            inputsImage[bufferLength].enabled = true;
                            bufferLength++;
                        }
                        if (Input.GetKeyDown(laserKey) && bufferLength < InputTranslator.step)
                        {
                            buffer[bufferLength].move = MoveType.LASER;
                            isCharging = true;
                            chargingMove = laserKey;
                            buffer[bufferLength].sprite = fightManager.laserSprite;
                            inputsImage[bufferLength].sprite = fightManager.laserSprite;
                            inputsImage[bufferLength].enabled = true;
                            bufferLength++;
                        }
                        if (Input.GetKeyUp(guardKey) && bufferLength < InputTranslator.step)
                        {
                            buffer[bufferLength].move = MoveType.GUARD;
                            buffer[bufferLength].sprite = fightManager.guardSprite;
                            inputsImage[bufferLength].sprite = fightManager.guardSprite;
                            inputsImage[bufferLength].enabled = true;
                            bufferLength++;
                        }
                        if (Input.GetKeyUp(specialKey) && bufferLength < InputTranslator.step)
                        {
                            buffer[bufferLength].move = MoveType.SPECIAL;
                            buffer[bufferLength].sprite = fightManager.specialSprite;
                            inputsImage[bufferLength].sprite = fightManager.specialSprite;
                            inputsImage[bufferLength].enabled = true;
                            bufferLength++;
                        }
                    }
                    if (Input.GetKeyDown(eraseKey))
                    {
                        if (bufferLength > 0)
                        {
                            buffer[bufferLength - 1].move = MoveType.NEUTRAL;
                            buffer[bufferLength - 1].sprite = fightManager.neutralSprite;
                            inputsImage[bufferLength - 1].enabled = false;
                            bufferLength--;
                        }
                    }
                
            }
            else
            {
                chargeCounter += Time.deltaTime;
                if(chargeCounter>chargeTime)
                {
                    if (bufferLength > 0)
                    {
                        if (!buffer[bufferLength - 1].isCharged)
                        {
                            inputsImage[bufferLength - 1].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                            buffer[bufferLength - 1].isCharged = true;
                        }
                    }
                }
                if(Input.GetKeyUp(chargingMove))
                {
                    isCharging = false;
                    chargeCounter = 0;
                }
                if(InputTranslator.sequence == Sequence.ACTION)
                {
                    isCharging = false;
                    chargeCounter = 0;
                }
            }
        }
    }

    public void OnEnterActionBeat()
    {
        isCharging = false;
        chargeCounter = 0;
    }
}
