using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    private IAIStrategy currentStrategy;
    private float decisionPeriod = 0.8f;
    private float decisionPeriodMin = 0.6f;
    private float decisionPeriodMax = 1f;

    private float lastDecision = 0.0f;
    private float decisionTimer = 0.0f;

    //TODO : this variable should be global and linked to the real speed music
    private float inputSequenceLength = 3f;
    private float inputSequenceTimer = 0.0f;
    private float inputSequenceProgression {
        get { return Mathf.Clamp(inputSequenceTimer / inputSequenceLength, 0f, 1f); }
    }
    
    private bool isWaiting = false;

    private Move chargingMove;

    public Player opponent;

    protected override void Start()
    {
        base.Start();

        decisionPeriod = Random.Range(decisionPeriodMin, decisionPeriodMax);

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
            else if (!isWaiting) {
                if (decisionTimer > lastDecision + decisionPeriod) {
                    currentStrategy.Iteration(this, opponent, inputSequenceProgression);
                    lastDecision += decisionPeriod;
                }

                decisionTimer += Time.deltaTime;
            }
        }

        inputSequenceTimer += Time.deltaTime;
    }

    public void RegisterMove(Move move, float timeToWait, bool charge = false)
    {
        StartCoroutine(RegisterMoveCoroutine(move, timeToWait, charge));
    }

    public IEnumerator RegisterMoveCoroutine(Move move, float timeToWait, bool charge)
    {
        if (bufferLength < buffer.Length && InputTranslator.sequence == Sequence.INPUT) {
            yield return new WaitForSeconds(timeToWait);
            isWaiting = true;

            if (bufferLength < buffer.Length && InputTranslator.sequence == Sequence.INPUT) {
                chargingMove = move;
                buffer[bufferLength].move = move.move;
                buffer[bufferLength].isCharged = move.isCharged;
                buffer[bufferLength].sprite = move.sprite;

                inputsImage[bufferLength].sprite = move.sprite;
                inputsImage[bufferLength].enabled = true;

                bufferLength++;

                chargeTimer = 0.0f;
                if (move.move == MoveType.HIT || move.move == MoveType.LASER || move.move == MoveType.REFLECT) {
                    isCharging = charge;
                }

                isWaiting = false;
            }
        }
    }

    public void EraseMove(float timeToWait)
    {
        isWaiting = true;
        StartCoroutine(EraseMoveCoroutine(timeToWait));
    }

    public IEnumerator EraseMoveCoroutine(float timeToWait)
    {
        if (bufferLength != 0 && InputTranslator.sequence == Sequence.INPUT) {
            yield return new WaitForSeconds(timeToWait);
            isWaiting = true;

            if (bufferLength != 0 && InputTranslator.sequence == Sequence.INPUT) {

                bufferLength--;
                buffer[bufferLength].move = MoveType.NEUTRAL;
                buffer[bufferLength].isCharged = false;
                buffer[bufferLength].sprite = fightManager.neutralSprite;

                inputsImage[bufferLength].sprite = fightManager.neutralSprite;
                inputsImage[bufferLength].enabled = false;

                chargeTimer = 0.0f;
                isCharging = false;

                isWaiting = false;
            }
        }
    }

    public override void Reset()
    {
        base.Reset();

        lastDecision = 0.0f;
        decisionTimer = 0.0f;
        inputSequenceTimer = 0.0f;
        isWaiting = false;
        currentStrategy = AIStrategyPicker.RandomStrategy();

        decisionPeriod = Random.Range(decisionPeriodMin, decisionPeriodMax);
    }
}
