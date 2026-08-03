using System;
using Models;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelS0 sfxChannel;

        private void OnEnable()
        {
            sfxChannel.onAudioCueRequested += HandleAudioCueRequested;
        }

        private void OnDisable()
        {
            sfxChannel.onAudioCueRequested -= HandleAudioCueRequested;
        }

        private void HandleAudioCueRequested(AudioCueSO audioCue)
        {
            
        }
    }
}