using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour, OnBeatElement
{
    public Camera camera;
    public AnimationCurve screenShakeCurve;

    void Start()
    {
        BeatManager.RegisterOnBeatElement(this);
    }

    public void OnBeat()
    {
        ScreenShake(0.3f, 0.5f);
    }

    public void ScreenShake(float duration, float magnitude)
    {
        StartCoroutine(ScreenShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ScreenShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPos = camera.transform.localPosition;

        float timer = 0.0f;

        while (timer < duration) {
            float range = 1 - (screenShakeCurve.Evaluate(timer / duration));

            float posX = Random.Range(-range, range) * magnitude;
            float posY = Random.Range(-range, range) * magnitude;

            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(posX, posY, originalPos.z), 0.5f);

            timer += Time.deltaTime;

            yield return null;
        }

        camera.transform.localPosition = originalPos;
    }
}
