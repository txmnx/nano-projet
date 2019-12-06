using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProActiveStrategy : IAIStrategy
{
    public IAIStrategy NewInstance()
    {
        return new ProActiveStrategy();
    }

    public void Iteration(PlayerAI ai, Player opponent)
    {
        Player.Move newMove;
        newMove.isCharged = false;
        newMove.move = Player.MoveType.HIT;
        newMove.sprite = ai.fightManager.hitSprite;

        ai.RegisterMove(newMove, true);
    }
}
