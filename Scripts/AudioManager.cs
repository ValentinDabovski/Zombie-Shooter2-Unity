using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] Sounds;

        public static AudioManager instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(this.gameObject);

            foreach (var sound in Sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
                sound.Source.volume = sound.Volume;
                sound.Source.pitch = sound.Pitch;
                sound.Source.loop = sound.Loop;
            }
        }

        void Start()
        {
            Play("Theme");
        }

        public void Play(string soundName)
        {
            var soundToPlay = Array.Find(Sounds, sound => sound.Name == soundName);

            if (soundToPlay != null)
            {
                soundToPlay.Source.Play();
            }

            else
            {
                Debug.LogWarning("Sound with " + soundName + "not found");
            }
        }

    }
}
