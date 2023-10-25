using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SeaController : MonoBehaviour
{
    public float waveSpeed = 1.0f; // Adjust the speed as needed
    public float waveHeight = 1.0f; // Adjust the wave height as needed
    public float waveWidth = 2.0f; // Adjust the wave width as needed

    private void Start()
    {
        // Start a coroutine to move the waves
        StartCoroutine(MoveWaves());
    }

    IEnumerator MoveWaves()
    {
        while (true) // Infinite loop for continuous motion
        {
            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform wave = transform.GetChild(i);
                Vector3 originalPosition = wave.localPosition;

                float newY = originalPosition.y + Mathf.Sin(Time.time * waveSpeed + i) * waveHeight;
                float newX = originalPosition.x + Mathf.Cos(Time.time * waveSpeed + i) * waveWidth;

                // Use DoTween to smoothly move the waves
                wave.DOLocalMoveY(newY, 1.0f).SetEase(Ease.InOutQuad); // Adjust the duration and ease type as needed
                wave.DOLocalMoveX(newX, 1.0f).SetEase(Ease.InOutQuad); // Adjust the duration and ease type as needed
            }

            yield return new WaitForSeconds(0.1f); // Adjust the interval as needed
        }
    }
}
