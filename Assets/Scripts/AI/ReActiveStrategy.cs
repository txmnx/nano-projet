using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReActiveStrategy : IAIStrategy
{
    private bool lastHit = false;
    private int hitIndexAI = 0; 
    private int hitIndexOpponent = 0;

    private Player.MoveType opponentHit1 = Player.MoveType.NEUTRAL;
    private Player.MoveType opponentHit2 = Player.MoveType.NEUTRAL;

    public IAIStrategy NewInstance()
    {
        return new ReActiveStrategy();
    }

    public void Iteration(PlayerAI ai, Player opponent, float elapsedTime)
    {
        if (elapsedTime < (3f / 4f)) {
            if (opponent.GetMove(1).move != opponentHit2) {
                if (ai.GetMove(0).move != Player.MoveType.NEUTRAL) {
                    if (ai.GetMove(1).move == Player.MoveType.NEUTRAL) {
                        ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(1).move), ai.fightManager), 0f);
                    }
                    else {
                        ai.EraseMove(0f);
                        ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(1).move), ai.fightManager), 0.1f);
                    }
                }
                else {
                    ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(0).move), ai.fightManager), 0f);
                    opponentHit1 = opponent.GetMove(0).move;
                    return;
                }
            }
            else if (opponent.GetMove(0).move != opponentHit1) {
                if (ai.GetMove(0).move != Player.MoveType.NEUTRAL) {
                    ai.EraseMove(0f);
                }
                else {
                    ai.RegisterMove(AIMovePicker.CreateMove(ai.fightManager.GetCounterMoveType(opponent.GetMove(0).move), ai.fightManager), 0f);
                }
            }

            opponentHit1 = opponent.GetMove(0).move;
            opponentHit2 = opponent.GetMove(1).move;
        }
        else {
            if (!lastHit) {
                System.Random random = new System.Random();
                ai.RegisterMove(AIMovePicker.RandomSimpleMove(random, ai.fightManager), 0f);
                ai.RegisterMove(AIMovePicker.RandomSimpleMove(random, ai.fightManager), 0.2f);

                lastHit = true;
            }
        }
    }
}
