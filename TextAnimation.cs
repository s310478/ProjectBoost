using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float period = 2f;
    [SerializeField] float minFontSize = 75f;
    [SerializeField] float maxFontSize = 85f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            StartCoroutine(AnimateFontSize());
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on the GameObject.");
        }
    }

    IEnumerator AnimateFontSize()
    {
        while (true)
        {
            if (period <= Mathf.Epsilon) yield break; // Mathf.Epsilon is pretty much equivalent to 0

            float cycles = Time.time / period; // continually growing over time
            const float tau = Mathf.PI * 2; // constant value of 6.283
            float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

            float fontSizeFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 so it's cleaner
            float newFontSize = Mathf.Lerp(minFontSize, maxFontSize, fontSizeFactor);

            if (text != null)
            {
                text.fontSize = newFontSize;
            }

            yield return null; // wait for the next frame
        }
    }
}
