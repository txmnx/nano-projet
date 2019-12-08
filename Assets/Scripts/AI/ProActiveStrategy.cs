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
                Player.MoveType counterLastMove;
                if (ai.fightManager.CanCounter(opponent.GetMove(0).move)) {
                    counterLastMove = ai.fightManager.GetCounterMoveType(ai.lastMove.move);
                    if (counterLastMove == opponent.GetMove(0).move) {
                        ai.EraseMove(0f);
                        ai.RegisterMove(AIMovePicker.SimpleMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(0).move), ai.fightManager), 0.2f);

                        secondHit = true;
                        return;
                    }
                }

                ai.RegisterMove(AIMovePicker.SimpleMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(1).move), ai.fightManager), 0f);

                secondHit = true;
            }
        }
        else if (!lastHit) {
            if (ai.fightManager.CanCounter(opponent.GetMove(1).move)) {
                Player.MoveType counterLastMove = ai.fightManager.GetCounterMoveType(opponent.lastMove.move);
                if (counterLastMove != ai.lastMove.move) {
                    ai.EraseMove(0f);
                    ai.RegisterMove(AIMovePicker.SimpleMove(counterLastMove, ai.fightManager), 0.2f);

                    lastHit = true;
                }
            }
        }
    }
}
