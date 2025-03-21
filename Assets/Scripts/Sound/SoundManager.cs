//Author: Small Hedge Games
//Updated: 13/06/2024

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace SmallHedge.SoundManager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundsSO SO;
        private static SoundManager instance = null;
        private AudioSource audioSource;
        private static float delayTimer;

        private void Awake()
        {
            if(!instance)
            {
                instance = this;
                audioSource = GetComponent<AudioSource>();
            }
        }

        public static void PlaySound(SoundType sound, AudioSource source = null, float volume = 1, float delayTime = 0)
        {
            SoundList soundList = instance.SO.sounds[(int)sound];
            AudioClip[] clips = soundList.sounds;
            AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

            if(source)
            {
                source.outputAudioMixerGroup = soundList.mixer;
                source.clip = randomClip;
                source.volume = volume * soundList.volume;
                
                delayTimer += Time.deltaTime;
                if (delayTimer > delayTime)
                {
                    source.Play();
                    delayTimer = 0;
                }
            }
            else
            {
                instance.audioSource.outputAudioMixerGroup = soundList.mixer;

                delayTimer += Time.deltaTime;
                if (delayTimer > delayTime)
                {
                    instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
                    delayTimer = 0;
                }
            }
        }
    }

    [Serializable]
    public struct SoundList
    {
        public string name;
        [Range(0, 1)] public float volume;
        public AudioMixerGroup mixer;
        public AudioClip[] sounds;
    }
}