using UnityEngine;
using UnityEngine.Assertions;

using System.Collections.Generic;
using System.Linq;

using Breakout.CrossScene.Audio.Data;
using Breakout.CrossScene.Enums;
using Breakout.Infrastructure;

namespace Breakout.CrossScene.Audio
{
    public class AudioPlayer : UnitySingleton<AudioPlayer>
    {
        [SerializeField] private AudioConfigSO AudioConfigSO;
        [SerializeField] private AudioClipsSO AudioClipsSO;
        [SerializeField] private AudioSource MusicPlayerSource;
        
        private int _oneShotAudioSourceIndex = 0;

        private readonly List<AudioSource> _audioSources = new();
         private void Start()
         {
             InitializeOneShotAudioPlayers();
             InitializeMusicPlayer();
         }

         private void InitializeMusicPlayer()
         {
             MusicPlayerSource.loop = true;
         }
         
         //although instantiating components during runtime is computationally expensive,
         //this gets initialized during the startup process
         //and the flexibility of choosing amount of audio sources available through
         //game config data objects (AudioConfigSO) make it worthwhile.
         private void InitializeOneShotAudioPlayers()
         {
             for (int i = 0; i < AudioConfigSO.oneShotAudioSourceCount; i++)
             {
                 Component addedComponent = gameObject.AddComponent(typeof(AudioSource));
                 AudioSource audioSource = addedComponent as AudioSource;
                 Assert.IsNotNull("Casting Component to AudioSource when initializing AudioPlayer failed!");
                 
                 _audioSources.Add(audioSource);
             }
         }

         public void PlayOneShotAudio(AudioClipID audioClipID)
         {
             AudioSource audioSource = _audioSources[GetOneShotAudioSourceIndex()];
             AudioClip clip = GetAudioClip(audioClipID);

             if (clip != null)
             {
                audioSource.PlayOneShot(clip);
             }
             else
             {
                 Debug.LogWarning($"Audio clip with ID {audioClipID} is not present in AudioClipsSO!");
             }
         }
         
         public void PlayMusic(AudioClipID audioClipID)
         {
             AudioClip clip = GetAudioClip(audioClipID);
             
             MusicPlayerSource.clip = clip;
             MusicPlayerSource.Play();
         }
            
         private AudioClip GetAudioClip(AudioClipID audioClipID)
         {
             return AudioClipsSO.enumeratedAudioClips
                                 .FirstOrDefault(x => x.AudioClipID == audioClipID)
                                 .AudioClip;
         }
         
         //loops indexes to get a different audio source each time
         private int GetOneShotAudioSourceIndex()
         {
             int oneShotAudioSourceIndex = _oneShotAudioSourceIndex;
             _oneShotAudioSourceIndex = _oneShotAudioSourceIndex + 1 == AudioConfigSO.oneShotAudioSourceCount
                                            ? 0
                                            : _oneShotAudioSourceIndex++;
             
             return oneShotAudioSourceIndex;
         }
    }
}