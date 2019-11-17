using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerPulser : MonoBehaviour, OnBeatElement
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        BeatManager.RegisterOnBeatElement(this);
    }

    public void OnBeat()
    {
        animator.Play("pulse_clip");
    }
}
