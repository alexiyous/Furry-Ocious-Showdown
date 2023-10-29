using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    [ListDrawerSettings(ShowIndexLabels = true)]
    public AudioSource[] mainMusic;
    public AudioSource currentMainMusic;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public AudioSource[] sfx;

    public IEnumerator PlayMainAfterFinish(int index)
    {
        yield return new WaitForSeconds(currentMainMusic.clip.length);
        PlayMainMusic(index);
    }

    public void PlayMainMusic(int index)
    {
        if (currentMainMusic != null)
        {
            currentMainMusic.Stop();
        }

        currentMainMusic = mainMusic[index];
        currentMainMusic.Play();
    }
    public void PlayMainMusic(int index, float fadeTime)
    {
        if (currentMainMusic != null)
        {
            StartCoroutine(FadeOut(currentMainMusic, fadeTime));
        }

        currentMainMusic = mainMusic[index];
        StartCoroutine(FadeIn(currentMainMusic, fadeTime));
    }

    public void StopMainMusic(float fadeTime)
    {
        StartCoroutine(FadeOut(currentMainMusic, fadeTime));
    }

    public void StopMainMusic()
    {
        currentMainMusic.Stop();
    }

    public void PlaySFX(int index)
    {
        sfx[index].Stop();
        sfx[index].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {

        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }

    public void PlaySFX(int index, float fadeTime)
    {
        StartCoroutine(FadeIn(sfx[index], fadeTime));
    }

    public void StopSFX(int index)
    {
        sfx[index].Stop();
    }

    public void StopSFX(int index, float fadeTime)
    {
        StartCoroutine(FadeOut(sfx[index], fadeTime));
    }
    IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
