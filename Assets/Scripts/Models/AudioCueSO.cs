using UnityEngine;

namespace Models
{
    [CreateAssetMenu(menuName = "Audio/Audio Cue", order = 0)]
    public class AudioCueSO : ScriptableObject
    {
        public AudioClip audioClip;
        public float volume;
        public float pitch;
        public float pitchVariance;
        public bool randomizePitch;
    }
}