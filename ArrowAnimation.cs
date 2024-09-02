using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    [SerializeField] float animationDuration = 0.75f;
    [SerializeField] float yValueMin = 4f;
    [SerializeField] float yValueMax = 5f;

    Transform arrowTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        arrowTransform = transform;
        StartCoroutine(AnimateArrow());
    }

    IEnumerator AnimateArrow()
    {
        while (true)
        {
            yield return StartCoroutine(MoveArrow(yValueMin, yValueMax, animationDuration));
            yield return StartCoroutine(MoveArrow(yValueMax, yValueMin, animationDuration));
        }
    }

    IEnumerator MoveArrow(float startY, float endY, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = new Vector3(arrowTransform.position.x, startY, arrowTransform.position.z);
        Vector3 endPosition = new Vector3(arrowTransform.position.x, endY, arrowTransform.position.z);

        while (elapsedTime < duration)
        {
            arrowTransform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        arrowTransform.position = endPosition; // Ensure the position is set to the end value after the loop
    }
}
