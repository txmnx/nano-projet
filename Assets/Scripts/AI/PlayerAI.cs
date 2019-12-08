using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : Player
{
    private IAIStrategy currentStrategy;
    private float decisionPeriod = 0.5f;
    private float decisionPeriodMin = 0.7f;
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

        currentStrategy = AIStrategyPicker.RandomStrategy(fightManager.random);
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

        Debug.Log(inputSequenceProgression);

        inputSequenceTimer += Time.deltaTime;
    }

    public void RegisterMove(Move move, float timeToWait, bool charge = false)
    {
        StartCoroutine(RegisterMoveCoroutine(move, timeToWait, charge));
    }

    private IEnumerator RegisterMoveCoroutine(Move move, float timeToWait, bool charge)
    {
        if (bufferLength < buffer.Length && InputTranslator.sequence == Sequence.INPUT) {
            isWaiting = true;
            yield return new WaitForSeconds(timeToWait);

            if (bufferLength < buffer.Length && InputTranslator.sequence == Sequence.INPUT) {
                chargingMove = move;
                buffer[bufferLength].move = move.move;
                buffer[bufferLength].isCharged = move.isCharged;
                buffer[bufferLength].sprite = move.sprite;

                inputsImage[bufferLength].sprite = move.sprite;
                inputsImage[bufferLength].enabled = true;

                chargeTimer = 0.0f;
                if (move.move == MoveType.HIT || move.move == MoveType.LASER || move.move == MoveType.REFLECT) {
                    isCharging = charge;
                }

                isWaiting = false;
                bufferLength++;
            }
        }
    }

    public void EraseMove(float timeToWait)
    {
        StartCoroutine(EraseMoveCoroutine(timeToWait));
    }

    private IEnumerator EraseMoveCoroutine(float timeToWait)
    {
        if (bufferLength != 0 && InputTranslator.sequence == Sequence.INPUT) {
            isWaiting = true;
            yield return new WaitForSeconds(timeToWait);

            if (bufferLength != 0 && InputTranslator.sequence == Sequence.INPUT) {
                buffer[bufferLength - 1].move = MoveType.NEUTRAL;
                buffer[bufferLength - 1].isCharged = false;
                buffer[bufferLength - 1].sprite = fightManager.neutralSprite;

                inputsImage[bufferLength - 1].sprite = fightManager.neutralSprite;
                inputsImage[bufferLength - 1].enabled = false;

                chargeTimer = 0.0f;
                isCharging = false;

                isWaiting = false;
                bufferLength--;
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
        currentStrategy = AIStrategyPicker.RandomStrategy(fightManager.random);

        decisionPeriod = Random.Range(decisionPeriodMin, decisionPeriodMax);
    }
}
