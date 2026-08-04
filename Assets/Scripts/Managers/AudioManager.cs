using System;
using Models;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelS0 sfxChannel;
        [SerializeField] private AudioSourcePool pool;

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
            var emitter = pool.Get();
            emitter.PlaySfx(audioCue, OnTrackFinished);
        }

        private void OnTrackFinished(SoundPlayer player)
        {
            pool.Release(player);
        }
    }
}