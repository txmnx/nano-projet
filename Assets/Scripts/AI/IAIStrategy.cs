using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIStrategy
{
    IAIStrategy NewInstance();

    void Iteration(PlayerAI ai, Player opponent, float elapsedTime);
}
