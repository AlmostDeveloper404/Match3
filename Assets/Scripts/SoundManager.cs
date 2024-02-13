using System;
using UnityEngine;
using Zenject;
using System.Collections;

namespace Main
{
    public class SoundManager : MonoBehaviour
    {
        private AudioSource[] _audioSources;

        private void Awake()
        {
            _audioSources = new AudioSource[transform.childCount];
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            }
        }

        public void PlaySound(AudioClip audioClip)
        {
            foreach (AudioSource audio in _audioSources)
            {
                if (audioClip == audio.clip)
                {
                    audio.Play();
                }
            }
        }

        public void StopSound(AudioClip audioClip)
        {
            foreach (AudioSource audio in _audioSources)
            {
                if (audioClip == audio.clip)
                {
                    audio.Stop();
                }
            }
        }

        public void SetupSound()
        {
            PlayerData playerData = PlayerResources.GetPlayerData;
            AudioListener.volume = playerData.SoundSwitcher == 0 ? 1 : 0;
        }

        public void ToggleSound()
        {
            PlayerData playerData = PlayerResources.GetPlayerData;
            playerData.SoundSwitcher = playerData.SoundSwitcher == 0 ? 1 : 0;
            AudioListener.volume = playerData.SoundSwitcher == 0 ? 1 : 0;
            PlayerResources.SetupData(playerData);
        }

        public void PauseSound()
        {
            if (PlayerResources.GetPlayerData.SoundSwitcher == 1) return;

            AudioListener.volume = 0;
        }

        public void ResumeSound()
        {
            if (PlayerResources.GetPlayerData.SoundSwitcher == 1) return;

            AudioListener.volume = 1;
        }
    }
}

