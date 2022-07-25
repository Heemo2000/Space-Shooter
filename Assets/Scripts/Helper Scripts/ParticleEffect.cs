using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : PoolObject
{
    private ParticleSystem _effect;


    private void Awake() {
        _effect = GetComponent<ParticleSystem>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var main = _effect.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public override void Reuse()
    {
        _effect.Play();
    }
    private void OnParticleSystemStopped() 
    {
        base.Destroy();    
    }
    
}
