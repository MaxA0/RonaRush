// script fades the audio to ensure no clunky audio

//libraries for certain commands needed for audio fading
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class audiofade : MonoBehaviour
{
    public AudioSource thememusic;
    public Sprite yesaudio; 
    public Sprite noaudio;
    public Image audio;
    public void Start() // this turns on or off the audio at the start of each level depending on what preferences were chosen
    {
        if (PlayerPrefs.GetInt("audio") == 0)
        {
            if (SceneManager.GetActiveScene().name == "Start")
            {
                audio.sprite = yesaudio;
            }
            fadeaudioin();
        }
        if (PlayerPrefs.GetInt("audio") == 1)
        {
            if (SceneManager.GetActiveScene().name == "Start")
            {
                audio.sprite = noaudio;
            }

            thememusic.volume = 0;
        }

    }
    public void volumebutton() //button displayed on the start screen, this script turns audio on and off
    {
        if(PlayerPrefs.GetInt("audio") == 1)
        {
            PlayerPrefs.SetInt("audio", 0);
            audio.sprite = yesaudio;
            thememusic.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("audio", 1);
            audio.sprite = noaudio;
            thememusic.volume = 0;
        }
    }
    public void fadeaudioout() // this is a function to activate the fade out
    {
        StartCoroutine(FadeOut(thememusic, 1));
    }
    public void fadeaudioin() // function to activate the fade in
    {
        StartCoroutine(FadeIn(thememusic, 1));
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime) // this is a co routine to fade in or out the audio
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }
    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

}