using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * BeatPulser is a debug class to show the use of BeatManager.
 */
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public class BeatPulser : OnBeatElement
{
    private float lastBeat = 0.0f;

    private Animator animator;
    private Material material;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        material = GetComponent<MeshRenderer>().material;
    }

    public override void OnBeat()
    {
        material.color = (InputTranslator.sequence == Sequence.INPUT) ? Color.green : Color.red;
        animator.Play("pulse_clip");
    }
}
