using System.Collections;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1.5f;
    private bool isQuitting = false;
    public void QuitGame()
    {
        if (isQuitting)
        {
            return;
        }
        isQuitting = true;
        
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float startvolume = AudioListener.volume;
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(startvolume, 0f, currentTime / fadeDuration);
            yield return null;
        }
        AudioListener.volume = 0;
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
