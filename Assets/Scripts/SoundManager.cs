using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public AudioClip[] audioClips;
    private LoopedList<AudioClip> clips;

    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioSource humSource;

    [Space]
    public AudioClip jumpFx;
    public AudioClip transitionFx;
    public AudioClip mergeFx;
    public AudioClip connectionHum;

    private void Start()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

        //source = gameObject.GetComponent<AudioSource>();

        clips = new LoopedList<AudioClip>(audioClips);

        musicSource.clip = clips.GetNext();
        musicSource.Play();
        Invoke("BackGroundMusicLoop", musicSource.clip.length);
    }

    private void BackGroundMusicLoop()
    {
        musicSource.clip = clips.GetNext();
        musicSource.Play();
        Invoke("BackGroundMusicLoop", musicSource.clip.length);
    }

    public static void JumpSound()
    {
        _instance.effectSource.PlayOneShot(_instance.jumpFx);
    }

    public static void TransitionSound()
    {
        _instance.effectSource.PlayOneShot(_instance.transitionFx);
    }

    public static void MergeSound()
    {
        _instance.effectSource.PlayOneShot(_instance.mergeFx);
    }

    public static SoundManager GetSoundManager()
    {
        return _instance;
    }
}
