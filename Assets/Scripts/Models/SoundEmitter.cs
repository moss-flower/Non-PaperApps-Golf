using System;
using UnityEngine;

namespace Models
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] AudioEventChannelS0 channel;
        [SerializeField] AudioCueSO cue;
        [SerializeField] private bool PlayOnEnable = false;

        public void PlaySound() => channel.RaiseEvent(cue);

        private void OnEnable()
        {
            if (PlayOnEnable)
            {
                channel.RaiseEvent(cue);
            }
        }
    }
}