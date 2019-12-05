using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    public override void OnInputBeat()
    {
        buffer[bufferLength].move = MoveType.HIT;
        buffer[bufferLength].sprite = fightManager.hitSprite;
        inputsImage[bufferLength].enabled = true;
        bufferLength++;
    }
}
