using UnityEngine;

public abstract class OnBeatElement : MonoBehaviour
{
    public abstract void OnBeat();

    protected virtual void Start()
    {
        BeatManager.RegisterOnBeatElement(this);
    }
}