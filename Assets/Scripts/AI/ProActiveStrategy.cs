using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProActiveStrategy : IAIStrategy
{
    private bool firstHit = false;
    private bool secondHit = false;
    private bool lastHit = false;

    public IAIStrategy NewInstance()
    {
        return new ProActiveStrategy();
    }

    public void Iteration(PlayerAI ai, Player opponent, float elapsedTime)
    {
        if (!firstHit) {
            Player.Move newMove = AIMovePicker.RandomSimpleMove(ai.fightManager);
            ai.RegisterMove(newMove, 0f, true);
            firstHit = true;
        }
        else if (!secondHit) {
            if (elapsedTime >= (2 / 3)) {
                if (ai.fightManager.IsCounterMove(ai.lastMove.move, opponent.GetMove(0).move)) {
                    ai.EraseMove(0f);
                    ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(0).move), ai.fightManager), 0.1f);
                    return;
                }

                ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(1).move), ai.fightManager), 0f);
                secondHit = true;
            }
        }
        else if (!lastHit) {
            if (!(ai.fightManager.IsCounterMove(opponent.GetMove(1).move, ai.lastMove.move))) {
                ai.EraseMove(0f);
                ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.lastMove.move), ai.fightManager), 0.1f);

                lastHit = true;
            }
        }
    }
}
