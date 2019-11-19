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

    public enum Move { HIT, GUARD, GRAB , NEUTRAL }     //List of moves, will be changed to a class

    public Move[] buffer = new Move[InputTranslator.step];

    public Text input1Text;
    public Text input2Text;

    private void Start()
    {
        currentLife = maxLife;
        Reset();

        input1Text.text = "";
        input2Text.text = "";

        InputTranslator.RegisterOnActionBeatElement(this);
    }

    public void OnActionBeat()
    {
        if (input1Text.text != "") {
            input1Text.text = "";
        }
        else {
            input2Text.text = "";
        }
    }

    public void Reset()
    {
        for (int i = 0; i < InputTranslator.step; i++) {     //initialising the buffer
            buffer[i] = Player.Move.NEUTRAL;
        }
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
            if (!(buffer[0] == Move.NEUTRAL))       //all of this because of flexibility, I hate you GDs
            {
                for (int i = 1; i < InputTranslator.step; i++)        
                {
                    if (buffer[i] == Move.NEUTRAL)
                    {
                            if (!(buffer[i - 1] == Move.NEUTRAL))
                            {
                                if (Input.GetKeyUp(hitKey))
                                {
                                    buffer[i] = Move.HIT;
                                    Debug.Log("HIT " + i);
                                    input2Text.text = "HIT";
                                }

                                if (Input.GetKeyUp(guardKey))
                                {
                                    buffer[i] = Move.GUARD;
                                    Debug.Log("GUARD " + i);
                                    input2Text.text = "GUARD";
                                }

                                if (Input.GetKeyUp(grabKey))
                                {
                                    buffer[i] = Move.GRAB;
                                    Debug.Log("GRAB " + i);
                                    input2Text.text = "GRAB";
                                }

                                if (Input.GetKeyUp(eraseKey))
                                {
                                    buffer[i - 1] = Move.NEUTRAL;
                                    Debug.Log("NEUTRAL " + (i - 1));
                                    input1Text.text = "";
                                }
                            }
                    }              
                }
            }
            else    //case buffer[0]
            {
                if (Input.GetKeyUp(hitKey))
                {
                    buffer[0] = Move.HIT;
                    Debug.Log("HIT " + 0);
                    input1Text.text = "HIT";
                }

                if (Input.GetKeyUp(guardKey))
                {
                    buffer[0] = Move.GUARD;
                    Debug.Log("GUARD " + 0);
                    input1Text.text = "GUARD";
                }

                if (Input.GetKeyUp(grabKey))
                {
                    buffer[0] = Move.GRAB;
                    Debug.Log("GRAB " + 0);
                    input1Text.text = "GRAB";
                }
            }

            if(!(buffer[InputTranslator.step - 1] == Move.NEUTRAL))   //case "erasing the last key of the buffer"
            {
                if (Input.GetKeyUp(eraseKey))
                {
                    buffer[InputTranslator.step - 1] = Move.NEUTRAL;
                    Debug.Log("NEUTRAL " + (InputTranslator.step - 1));
                    input2Text.text = "";
                }
            }
        }
    }

    public void OnEnterActionBeat() { }
}
