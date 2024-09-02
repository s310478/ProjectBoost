using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed = -50f;
    private float currentRotationZ;

    private void Start()
    {
        LoadRotation();
    }

    private void OnDestroy()
    {
        SaveRotation();
    }

    private void Update()
    {
        // Rotate around the z-axis
        currentRotationZ += spinSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
    }

    private void SaveRotation()
    {
        PlayerPrefs.SetFloat(gameObject.name + "_rotationZ", currentRotationZ);
        PlayerPrefs.Save();
    }

    private void LoadRotation()
    {
        if (PlayerPrefs.HasKey(gameObject.name + "_rotationZ"))
        {
            currentRotationZ = PlayerPrefs.GetFloat(gameObject.name + "_rotationZ");
            transform.rotation = Quaternion.Euler(0, 0, currentRotationZ);
        }
    }
}
