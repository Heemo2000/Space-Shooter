using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField]private SoundType soundType;

    [SerializeField]private AudioClip _clip;

    public AudioClip Clip { get => _clip; }
    public SoundType SoundType { get => soundType;}

}
