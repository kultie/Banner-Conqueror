using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    Stack<AudioSource> listeners = new Stack<AudioSource>();
    List<AudioSource> playingAudios = new List<AudioSource>();

    public static void PlaySound(AudioClip clip)
    {
        var a = GetListener();
        a.clip = clip;
        a.Play();
        Instance.playingAudios.Add(a);
    }

    public static void UpdateTimeScale(float timeScale)
    {
        AudioSource[] audios = Instance.playingAudios.ToArray();
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].pitch = timeScale;
        }
    }

    private void Update()
    {
        AudioSource[] audios = Instance.playingAudios.ToArray();
        for (int i = 0; i < audios.Length; i++)
        {
            if (!audios[i].isPlaying)
            {
                playingAudios.Remove(audios[i]);
                ReturnListener(audios[i]);
            }
        }
    }

    static AudioSource GetListener()
    {
        if (Instance.listeners.Count == 0)
        {
            AudioSource tmp = (new GameObject()).AddComponent<AudioSource>();
            Instance.listeners.Push(tmp);
            tmp.transform.parent = Instance.transform;
        }
        return Instance.listeners.Pop();
    }

    static void ReturnListener(AudioSource listener)
    {
        Instance.listeners.Push(listener);
    }
}
