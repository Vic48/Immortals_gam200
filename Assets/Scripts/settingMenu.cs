using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingMenu : MonoBehaviour
{
    public Slider sound;
    public Slider music;

    [Range(0, 1)] public float defaultSoundValue;
    [Range(0, 1)] public float defaultMusicValue;

    public string BGM_name = "Theme";
    // Start is called before the first frame update
    void Start()
    {
        // when start set default value
        sound.value = defaultSoundValue;
        music.value = defaultMusicValue;

        sound.onValueChanged.AddListener(delegate { soundValueChange(); });
        music.onValueChanged.AddListener(delegate { musicValueChange(); });
    }

    // Update is called once per frame
    void Update()
    {}

    void soundValueChange() 
    {
        // sound.value
        Sound[] sounds = FindObjectOfType<AudioManager>().sounds;
        Array.ForEach(sounds, s => {
            // If the sound is not bgm music
            if (s.name != BGM_name)
            {
                s.source.volume = sound.value;
            }
        });
    }

    void musicValueChange()
    {
        Sound[] sounds = FindObjectOfType<AudioManager>().sounds;
        // Theme is the BGM music
        Sound s = Array.Find(sounds, sound => sound.name == BGM_name);
        s.source.volume = music.value;
    }
}
