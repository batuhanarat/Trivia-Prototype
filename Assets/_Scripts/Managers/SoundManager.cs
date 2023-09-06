using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectSource;
    
    
    //Make it singleton persistant
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }


    public void PlayAudioEffect(AudioClip _clip)
    {
        _effectSource.PlayOneShot(_clip);
    }

    public void MuteTickTock()
    {
        _musicSource.mute = true;
    }
 
    public void UnMuteTickTock()
    {
        _musicSource.mute = false;
    }
    
    

    
}
