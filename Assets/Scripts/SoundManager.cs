using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] steps_AR;

    public void step_sound_play()
    {
        GetComponent<AudioSource>().PlayOneShot(steps_AR[Random.Range(0, steps_AR.Length-1)]);
    }
}
