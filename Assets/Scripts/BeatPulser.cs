using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * BeatPulser is an exemple class to show the use of BeatManager.
 */
[RequireComponent(typeof(Animator))]
public class BeatPulser : MonoBehaviour
{
    private float lastBeat = 0.0f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (BeatManager.songPosition > lastBeat + BeatManager.period) {
            animator.Play("pulse_clip");
            lastBeat += BeatManager.period;
        }
    }
}
