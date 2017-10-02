using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    public List<AudioClip> audios;

    private void Start()
    {
    }

    public void PlayAudio(string clipName)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(GetAudioclip(clipName));
    }

    public void PlayAudio(string clipName, float volumeScale)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(GetAudioclip(clipName), volumeScale);
    }

    private AudioClip GetAudioclip(string clipName)
    {
        return audios.Find(x => x.name == clipName);
    }
}
