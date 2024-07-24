using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Farmanji.Managers
{
    public class SoundManager : SingletonPersistent<SoundManager>
    {
        #region FIELDS
        [Header("AUDIO SOURCE")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _SFXSource;

        [Header("AUDIO MIXER")]
        [SerializeField] private AudioMixer _generalAudioMixer;

        [Header("AUDIO CLIP")]
        [SerializeField] private AudioClip _backgroundTheme;

        public Action _OnMusicMuted = delegate { };
        public Action _OnMusicUnMuted = delegate { };
        #endregion

        #region PROPERTIES
        public AudioClip Theme { get { return _backgroundTheme; } }
        #endregion

        #region UNITY METHODS
        private void Start()
        {
            _musicSource.loop = true;
            _musicSource.playOnAwake = true;
        }
        #endregion

        #region PUBLIC METHODS
        public void MusicLoop(bool value)
        {
            _musicSource.loop = value;
        }
        public void MuteMusic()
        {
            _OnMusicMuted();
            _musicSource.mute = true;
        }

        public void UnMuteMusic()
        {
            _OnMusicUnMuted();
            _musicSource.mute = false;
        }

        public void MuteSFX()
        {
            _SFXSource.mute = true;
        }

        public void UnMuteSFX()
        {
            _SFXSource.mute = false;
        }
        public void SetMusic(AudioClip audioClip)
        {
            _musicSource.clip = audioClip;
        }
        public void PlayMusic()
        {
            _musicSource.Play();
        }
        public void SetSFX(AudioClip audioClip)
        {
            _SFXSource.clip = audioClip;
        }
        public void PlaySFX()
        {
            _SFXSource.Play();
        }
        public void SetMusicVolume(float volume)
        {
            StartCoroutine(LerpVolume(_musicSource, volume, 150f));
            //_musicSource.volume = volume;
        }
        public void SetSFXVolume(float volume)
        {
            StartCoroutine(LerpVolume(_SFXSource, volume, 150f));
            //_SFXSource.volume = volume;
        }
        #endregion

        #region COROUTINES
        IEnumerator LerpVolume(AudioSource source, float volume, float duration)
        {
            float timeElapsed = 0;
            while (timeElapsed < duration)
            {
                source.volume = Mathf.Lerp(source.volume, volume, timeElapsed / duration);
                //valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            source.volume = volume;
        }
        #endregion
    }
}