using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovePicker
{
    public static Player.Move CreateMove(Player.MoveType moveType, FightManager fightManager)
    {
        Player.Move newMove = new Player.Move();
        newMove.move = moveType;
        newMove.isCharged = false;
        newMove.sprite = fightManager.GetMoveSprite(moveType);
        return newMove;
    }

    public static Player.Move RandomMove(FightManager fightManager)
    {
        Player.MoveType [] moveTypes = new Player.MoveType[]
        {
            Player.MoveType.HIT,
            Player.MoveType.REFLECT,
            Player.MoveType.LASER,
            Player.MoveType.SPECIAL
        };

        Player.Move randomMove = new Player.Move();
        randomMove.move = (Player.MoveType)moveTypes.GetValue((new System.Random()).Next(moveTypes.Length));
        randomMove.isCharged = false;
        randomMove.sprite = fightManager.GetMoveSprite(randomMove.move);
        return randomMove;
    }

    public static Player.Move RandomSimpleMove(System.Random random, FightManager fightManager)
    {
        Player.MoveType[] moveTypes = new Player.MoveType[]
        {
            Player.MoveType.HIT,
            Player.MoveType.REFLECT,
            Player.MoveType.LASER
        };

        Player.Move randomMove = new Player.Move();
        randomMove.move = (Player.MoveType)moveTypes.GetValue(random.Next(moveTypes.Length));
        randomMove.isCharged = false;
        randomMove.sprite = fightManager.GetMoveSprite(randomMove.move);
        return randomMove;
    }

    public static Player.Move RandomSimpleMove(FightManager fightManager)
    {
        System.Random random = new System.Random();
        return RandomSimpleMove(random, fightManager);
    }
}
