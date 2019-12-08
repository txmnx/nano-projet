using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReActiveStrategy : IAIStrategy
{
    public IAIStrategy NewInstance()
    {
        return new ReActiveStrategy();
    }

    public void Iteration(PlayerAI ai, Player opponent, float elapsedTime)
    {

    }
}
