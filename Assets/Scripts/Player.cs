using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum PlayerID
{
    Player1,
    Player2
}

public class Player : MonoBehaviour, OnActionBeatElement, OnInputBeatElement
{
    public PlayerID id;
    private string idString;

    public float chargeTime = 0.80f;
    public float chargeTimer = 0;
    protected bool isCharging = false;
    private string chargingMove;

    public Slider health;
    public float maxLife = 1200;
    public float currentLife;
    public FightManager fightManager;               //Script managing fights, on the GameManager
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

    public Animator animator;

    protected virtual void Start()
    {
        Init();
    }

    public void Init()
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
        idString = (id == PlayerID.Player1) ? "1" : "2";
    }

    public virtual void OnEnterInputBeat()
    {
        for(int i = 0; i<inputsImage.Length; i++)
        {
            inputsImage[i].transform.localScale = new Vector3(1, 1, 1);
        }

        Reset();
    }
    public void OnActionBeat()
    {
        inputsImage[currentAction++].enabled = false;
    }

    public virtual void Reset()
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
            if (bufferLength < InputTranslator.step)
            {
                if (Input.GetButtonDown("HitKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.HIT;
                    isCharging = true;
                    chargingMove = "HitKey" + idString;
                    buffer[bufferLength].sprite = fightManager.hitSprite;
                    Debug.Log("HIT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.hitSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("ReflectKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.REFLECT;
                    isCharging = true;
                    chargingMove = "ReflectKey" + idString;
                    buffer[bufferLength].sprite = fightManager.reflectSprite;
                    Debug.Log("REFLECT " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.reflectSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("LaserKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.LASER;
                    isCharging = true;
                    chargingMove = "LaserKey" + idString;
                    buffer[bufferLength].sprite = fightManager.laserSprite;
                    Debug.Log("LASER " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.laserSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("GuardKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.GUARD;
                    buffer[bufferLength].sprite = fightManager.guardSprite;
                    Debug.Log("GUARD " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.guardSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
                if (Input.GetButtonDown("SpecialKey" + idString) && bufferLength < InputTranslator.step)
                {
                    buffer[bufferLength].move = MoveType.SPECIAL;
                    buffer[bufferLength].sprite = fightManager.specialSprite;
                    Debug.Log("SPECIAL " + bufferLength);
                    inputsImage[bufferLength].sprite = fightManager.specialSprite;
                    inputsImage[bufferLength].enabled = true;
                    bufferLength++;
                }
            }
            if (isCharging)
            {
                chargeTimer += Time.deltaTime;
                if(chargeTimer>chargeTime)
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
                if(Input.GetButtonUp(chargingMove))
                {
                    isCharging = false;
                    chargeTimer = 0;
                }
                if(InputTranslator.sequence == Sequence.ACTION)
                {
                    isCharging = false;
                    chargeTimer = 0;
                }
            }
            else {
                if (Input.GetButtonDown("EraseKey" + idString)) {
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
    }

    public void OnEnterActionBeat()
    {
        isCharging = false;
        chargeTimer = 0;
    }
    public virtual void OnInputBeat() {}


    // Return the last move made by this player
    public Move lastMove {
        get { return buffer[(bufferLength - 1 >= 0) ? bufferLength - 1 : 0]; }
    }

    public Move GetMove(int index)
    {
        return buffer[index];
    }

    public void PlayAnim(Move move, bool takeDamage = false)
    {
        if (takeDamage) {
            switch (move.move) {
                case MoveType.REFLECT:
                    animator.SetTrigger("doDamageReflect");
                    break;
                case MoveType.LASER:
                    animator.SetTrigger("doDamageLaser");
                    break;
                default:
                    animator.SetTrigger("doDamage");
                    break;
            }
        }
        else {
            switch (move.move) {
                case MoveType.HIT:
                    animator.SetTrigger("doFente");
                    break;
                case MoveType.REFLECT:
                    animator.SetTrigger("doReflect");
                    break;
                case MoveType.LASER:
                    animator.SetTrigger("doLaser");
                    break;
                case MoveType.SPECIAL:
                    animator.SetTrigger("doSpecial");
                    break;
                case MoveType.GUARD:
                    animator.SetTrigger("doGuard");
                    break;
                default:
                    break;
            }
        }
    }
}
