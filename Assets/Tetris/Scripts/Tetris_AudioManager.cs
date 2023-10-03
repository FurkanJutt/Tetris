using System;
using UnityEngine.Audio;
using UnityEngine;

public class Tetris_AudioManager : MonoBehaviour
{
    public Tetris_Sound[] sounds;

    void Awake() {
        foreach (Tetris_Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) {
        Tetris_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name) {
        Tetris_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void Mute(string name, bool isMute) {
        Tetris_Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.mute = isMute;
    }
}
