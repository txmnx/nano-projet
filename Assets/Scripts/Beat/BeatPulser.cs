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
    public Camera camera;
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
        camera.backgroundColor = new Color(0.747597f, 0.9056604f, 0.8131712f);
    }

    public void OnActionBeat()
    {
        material.color = Color.red;
        camera.backgroundColor = new Color(0.9433962f, 0.7331346f, 0.6630473f);
    }
}
