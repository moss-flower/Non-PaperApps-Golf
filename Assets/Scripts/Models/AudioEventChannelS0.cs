using System;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Audio/Audio Event Channel", order = 0)]
    public class AudioEventChannelS0 : ScriptableObject
    {
        public event Action<AudioCueSO> onAudioCueRequested;

        public void RaiseEvent(AudioCueSO audioCue)
        {
            onAudioCueRequested?.Invoke(audioCue);
        }
    }
}