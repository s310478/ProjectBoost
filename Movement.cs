using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputAction SideThrusters;
    [SerializeField] InputAction MainThrust;
    [Header("Thrust values")]
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotateThrust = 100f;
    [Header("Thrust Audio")]
    [SerializeField] AudioClip mainEngineAudio;
    [SerializeField] AudioClip rotateEngineAudio;
    [Header("Thrust Particles")]
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource thrustAudioSource;
    AudioSource rotateAudioSource;

    void OnEnable()
    {
        SideThrusters.Enable();
        MainThrust.Enable();
    }

    void OnDisable()
    {
        SideThrusters.Disable();
        MainThrust.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
        AudioSource[] audioSources = GetComponents<AudioSource>();
        thrustAudioSource = audioSources[0];
        rotateAudioSource = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMainThrust();
        ProcessSideThrusters();
    }

    void ProcessMainThrust()
    {
        if (MainThrust.ReadValue<float>() > 0.5f)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessSideThrusters()
    {
        Vector2 sideThrusterInput = SideThrusters.ReadValue<Vector2>();
        
        if (sideThrusterInput.x < -0.5f)
        {
            RotateLeft();
        }
        else if (sideThrusterInput.x > 0.5f)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!thrustAudioSource.isPlaying)
        {
            thrustAudioSource.clip = mainEngineAudio;
            thrustAudioSource.loop = true;
            thrustAudioSource.Play();
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopThrusting()
    {
        if (thrustAudioSource.isPlaying)
        {
            thrustAudioSource.Stop();
        }
        mainBoosterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(rotateThrust);

        if (!rotateAudioSource.isPlaying)
        {
            rotateAudioSource.clip = rotateEngineAudio;
            rotateAudioSource.loop = true;
            rotateAudioSource.Play();
        }

        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotateThrust);

        if (!rotateAudioSource.isPlaying)
        {
            rotateAudioSource.clip = rotateEngineAudio;
            rotateAudioSource.loop = true;
            rotateAudioSource.Play();
        }

        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    private void StopRotating()
    {
        if (rotateAudioSource.isPlaying)
        {
            rotateAudioSource.Stop();
        }
        leftBoosterParticles.Stop();
        rightBoosterParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
