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
        Player.Move newMove = AIMovePicker.RandomSimpleMove(ai.fightManager);

        ai.RegisterMove(newMove, true);
    }
}
