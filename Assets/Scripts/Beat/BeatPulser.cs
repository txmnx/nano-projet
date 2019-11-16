using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * BeatPulser is a sample class to show the use of BeatManager.
 */
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MeshRenderer))]
public class BeatPulser : MonoBehaviour, OnBeatElement, OnActionBeatElement, OnInputBeatElement
{
    private float lastBeat = 0.0f;

    private Animator animator;
    private Material material;

    void Start()
    {
        animator = GetComponent<Animator>();
        material = GetComponent<MeshRenderer>().material;

        BeatManager.RegisterOnBeatElement(this);
        InputTranslator.RegisterOnInputBeatElement(this);
        InputTranslator.RegisterOnActionBeatElement(this);
    }


    public void OnBeat()
    {
        animator.Play("pulse_clip");
    }

    public void OnInputBeat()
    {
        material.color = Color.green;
    }

    public void OnActionBeat()
    {
        material.color = Color.red;
    }
}
