using UnityEngine;

namespace Models
{
    public class SoundEmitter : MonoBehaviour
    {
        [SerializeField] AudioEventChannelS0 channel;
        [SerializeField] AudioCueSO cue;

        public void PlaySound() => channel.RaiseEvent(cue);
    }
}