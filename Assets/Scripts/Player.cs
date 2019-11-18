using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : MonoBehaviour
{
    public KeyCode hitKey = KeyCode.Z;
    public KeyCode guardKey = KeyCode.Q;
    public KeyCode grabKey = KeyCode.D;
    public KeyCode eraseKey = KeyCode.S;

    public float maxLife = 100;
    public float currentLife;
    public static int bufferSize = 2;
    public FightManager fightManager;               //Script managing fights, on the GameManager

    public enum Move { HIT, GUARD, GRAB , NEUTRAL }     //List of moves, will be changed to a class

    public Move[] buffer = new Move[bufferSize];


    private void Start()
    {
        currentLife = maxLife;
        for (int i = 0; i < bufferSize; i++)        //initialising the buffer
        {
            buffer[i] = Player.Move.NEUTRAL;
        }

    }

    void Update()
    {
        if (fightManager.isInputPhase)      
        {
            if (!(buffer[0] == Move.NEUTRAL))       //all of this because of flexibility, I hate you GDs
            {
                for (int i = 1; i < bufferSize; i++)        
                {
                    if (buffer[i] == Move.NEUTRAL)
                    {
                            if (!(buffer[i - 1] == Move.NEUTRAL))
                            {
                                if (Input.GetKeyUp(hitKey))
                                {
                                    buffer[i] = Move.HIT;
                                    Debug.Log("HIT " + i);
                                }

                                if (Input.GetKeyUp(guardKey))
                                {
                                    buffer[i] = Move.GUARD;
                                    Debug.Log("GUARD " + i);
                                }

                                if (Input.GetKeyUp(grabKey))
                                {
                                    buffer[i] = Move.GRAB;
                                    Debug.Log("GRAB " + i);
                                }

                                if (Input.GetKeyUp(eraseKey))
                                {
                                    buffer[i - 1] = Move.NEUTRAL;
                                    Debug.Log("NEUTRAL " + (i - 1));
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
                }

                if (Input.GetKeyUp(guardKey))
                {
                    buffer[0] = Move.GUARD;
                    Debug.Log("GUARD " + 0);
                }

                if (Input.GetKeyUp(grabKey))
                {
                    buffer[0] = Move.GRAB;
                    Debug.Log("GRAB " + 0);
                }
            }

            if(!(buffer[bufferSize - 1] == Move.NEUTRAL))   //case "erasing the last key of the buffer"
            {
                if (Input.GetKeyUp(eraseKey))
                {
                    buffer[bufferSize-1] = Move.NEUTRAL;
                    Debug.Log("NEUTRAL " + (bufferSize-1));
                }
            }
        }
    }
}
