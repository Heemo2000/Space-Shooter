using System;
using System.Collections.Generic;

using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : GenericSingleton<SoundManager>
{

    
    [SerializeField]private Sound[] sounds;
    private Dictionary<SoundType,Sound> soundDictionary;
    protected override void Awake() {
        
        base.Awake();
        
        soundDictionary = new Dictionary<SoundType, Sound>();        
        foreach(Sound sound in sounds)
        {
            soundDictionary.Add(sound.SoundType,sound);
        }
    }


    public void Play(SoundType soundType,AudioSource source)
    {
        if(soundDictionary.TryGetValue(soundType,out Sound sound))
        {
            source.clip = sound.Clip;
            source.Play();
        }        
    }

    public void Pause(AudioSource source)
    {
        source.Pause();
    }

    public void Resume(AudioSource source)
    {
        source.UnPause();
    }
    public void Stop(AudioSource source)
    {
        source.Stop();
    }

    public void PlayInstantly(SoundType soundType,AudioSource source)
    {
        if(soundDictionary.TryGetValue(soundType,out Sound sound))
        {
            source.PlayOneShot(sound.Clip);
        }
        else
        {
            Debug.Log("Sound not found.");
        }
    }

    public void PlayAtPoint(SoundType soundType,Vector3 position)
    {
        if(soundDictionary.TryGetValue(soundType,out Sound sound))
        {
            AudioSource.PlayClipAtPoint(sound.Clip,position,1.0f);
        }
    }
}
