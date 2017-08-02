namespace Assets.Scripts
{
    using System;
    using UnityEngine.Audio;
    using UnityEngine;

    [System.Serializable]
    public class Sound
    {
        public string Name;

        public AudioClip Clip;

        public bool Loop;

        [HideInInspector]
        public AudioSource Source;

        [Range(0f, 1f)]
        public float Pitch;

        [Range(.1f, 3f)]
        public float Volume;
    }
}
