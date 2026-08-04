using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Models
{
    public class SoundPlayer : MonoBehaviour
    {
        private AudioSource audioSource;
        private Coroutine playingCoroutine;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlaySfx(AudioCueSO audioCueSo, UnityAction<SoundPlayer> onComplete)
        {
            audioSource.clip = audioCueSo.audioClip;
            audioSource.volume = audioCueSo.volume;
            audioSource.pitch = audioCueSo.pitch;
            audioSource.Play();
            
            if (playingCoroutine != null) StopCoroutine(playingCoroutine);
            playingCoroutine = StartCoroutine(WaitForClipToEnd(audioCueSo.audioClip.length, onComplete));
        }
        
        private IEnumerator WaitForClipToEnd(float duration, UnityAction<SoundPlayer> onComplete)
        {
            yield return new WaitForSeconds(duration);
            onComplete.Invoke(this);
        }
        
        public void Stop()
        {
            if (playingCoroutine != null) StopCoroutine(playingCoroutine);
            audioSource.Stop();
        }

    }
}