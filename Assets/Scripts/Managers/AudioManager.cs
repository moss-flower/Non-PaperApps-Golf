using System;
using Models;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioEventChannelS0 sfxChannel;
        [SerializeField] private AudioEventChannelS0 musicChannel;
        [SerializeField] private AudioSourcePool pool;
        private SoundPlayer backgroundMusicPlayer;
        private bool backgroundMusicIsPlaying = false;

        private void OnEnable()
        {
            sfxChannel.onAudioCueRequested += HandleAudioCueRequested;
            musicChannel.onAudioCueRequested += HandleMusicCue;
        }

        private void OnDisable()
        {
            sfxChannel.onAudioCueRequested -= HandleAudioCueRequested;
            musicChannel.onAudioCueRequested -= HandleMusicCue;
        }

        private void HandleMusicCue(AudioCueSO audioCue)
        {
            backgroundMusicPlayer = pool.Get();
            backgroundMusicPlayer.PlaySfx(audioCue, OnTrackFinished);
            backgroundMusicIsPlaying = true;
        }

        public void StopBackgroundMusic()
        {
            if (backgroundMusicIsPlaying)
            {
                backgroundMusicPlayer.Stop();
                backgroundMusicIsPlaying = false;
                OnTrackFinished(backgroundMusicPlayer);
                backgroundMusicPlayer = null;
            }
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