using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Vector3 originalScale = new Vector3(0.3f, 0.3f, 0.3f);
    private Vector3 targetScale = new Vector3(0.5f, 0.5f, 0.5f);

    private float duration = 0.6f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameObject ball = collision.gameObject;
            StartCoroutine(ScaleUpAndDownWithCallback(OnCoroutineComplete, ball));
        }
    }

    IEnumerator ScaleUpAndDownWithCallback(System.Action<GameObject> callback, GameObject ball)
    {
        yield return StartCoroutine(ScaleUpAndDown(ball));
        callback?.Invoke(ball);
    }

    IEnumerator ScaleUpAndDown(GameObject ball)
    {
        Physics2D.IgnoreLayerCollision(0, 6, true);
        AudioManager audioManager = (AudioManager)Object.FindFirstObjectByType(typeof(AudioManager));
        audioManager.Play("Boing");
        yield return StartCoroutine(LerpScale(ball, originalScale, targetScale, duration));
        yield return StartCoroutine(LerpScale(ball, targetScale, originalScale, duration));
    }

    IEnumerator LerpScale(GameObject  ball, Vector3 startScale, Vector3 endScale, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            ball.transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        ball.transform.localScale = endScale;
    }
    void OnCoroutineComplete(GameObject ball)
    {
        Physics2D.IgnoreLayerCollision(0, 6, false);
    }
}
