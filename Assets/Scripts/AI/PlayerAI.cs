using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    private IAIStrategy currentStrategy;
    private float decisionPeriod = 0.5f;
    private float lastDecision = 0.0f;
    private float timer = 0.0f;

    private Move chargingMove;

    public Player opponent;

    protected override void Start()
    {
        base.Start();

        currentStrategy = AIStrategyPicker.RandomStrategy();
    }

    private void Update()
    {
        if (InputTranslator.sequence == Sequence.INPUT) {
            if (isCharging) {
                chargeTimer += Time.deltaTime;
                if (chargeTimer > chargeTime) {
                    inputsImage[bufferLength - 1].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    buffer[bufferLength - 1].isCharged = true;

                    chargeTimer = 0.0f;
                    isCharging = false;
                }
            }
            else {
                if (timer > lastDecision + decisionPeriod) {
                    currentStrategy.Iteration(this, opponent);
                    lastDecision += decisionPeriod;
                }

                timer += Time.deltaTime;
            }
        }
    }

    public void RegisterMove(Move move, bool charge = false)
    {
        if (bufferLength < buffer.Length) {
            chargingMove = move;
            buffer[bufferLength].move = move.move;
            buffer[bufferLength].isCharged = move.isCharged;
            buffer[bufferLength].sprite = move.sprite;

            inputsImage[bufferLength].sprite = move.sprite;
            inputsImage[bufferLength].enabled = true;

            bufferLength++;

            chargeTimer = 0.0f;
            isCharging = charge;
        }
    }
}
