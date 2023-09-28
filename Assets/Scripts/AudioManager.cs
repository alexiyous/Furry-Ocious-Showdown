using System.Collections;
using System.Collections.Generic;
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

    public AudioSource mainMenuMusic, levelMusic, bossMusic;

    public AudioSource[] sfx;

    public float fadeOutDuration = 1.0f;

    public void PlayMainMenuMusic()//method to play main menu music, set it on the test main menu script
    {
        levelMusic.Stop();//stop the level music from playing

        mainMenuMusic.Play();//play the main menu music
    }

    public void PlayLevelMusic()//method to play level music, set it on camera controller script
    {
        if (!levelMusic.isPlaying)//if level music is not playing
        {
            mainMenuMusic.Stop();//stop the main menu music from playing
            levelMusic.Play();//play the level music
        }
    }

    public void PlayBossMusic(float duration)
    {
        if (!bossMusic.isPlaying)
        {
            StartCoroutine(FadeOutAndPlay(levelMusic, bossMusic, duration));
        }
    }

    public void StopBossMusic(float duration)
    {
        if (bossMusic.isPlaying)
        {
            StartCoroutine(FadeOutAndPlay(bossMusic, levelMusic, duration));
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        sfx[sfxToAdjust].pitch = Random.Range(.8f, 1.2f);
        PlaySFX(sfxToAdjust);
    }

    private IEnumerator FadeOutAndPlay(AudioSource audioSourceToFadeOut, AudioSource audioSourceToPlay, float duration)
    {
        float fadeDuration = duration; // Adjust the duration for fading out
        float startVolume = audioSourceToFadeOut.volume;
        float timer = 0;

        while (timer < fadeDuration)
        {
            audioSourceToFadeOut.volume = Mathf.Lerp(startVolume, 0, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        audioSourceToFadeOut.volume = 0;
        audioSourceToFadeOut.Stop();

        // Reset the volume back to the original level
        audioSourceToFadeOut.volume = startVolume;

        // Start playing the new audio source
        audioSourceToPlay.Play();
    }
}
