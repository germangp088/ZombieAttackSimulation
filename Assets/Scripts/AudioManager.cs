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
        audio.PlayOneShot(audios.Find(x => x.name == clipName));
    }
}
