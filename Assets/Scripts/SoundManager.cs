using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] steps_AR;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void step_sound_play()
    {
        audioSource.PlayOneShot(steps_AR[Random.Range(0, steps_AR.Length)]);
        print("звук шага");
    }
}
