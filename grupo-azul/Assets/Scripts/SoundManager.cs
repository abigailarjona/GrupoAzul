using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource uiAudioSource;
    [SerializeField] private AudioClip uiOnHoverSound;
    [SerializeField] private AudioClip uiOnClickSound;
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioClip[] ambientSounds;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioSource sfxAudioSource;
    public static SoundManager Instance { get; private set; }

    private IEnumerator playMusicCoroutine;
    private IEnumerator playAmbientSoundsCoroutine;

    private enum GameScene
    {
        MainMenu = 0,
        InGame = 1,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        playMusicCoroutine = PlayMusic(mainMenuMusic);
        playAmbientSoundsCoroutine = PlayAmbientSounds();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch ((GameScene)scene.buildIndex)
        {
            case GameScene.MainMenu:
                musicAudioSource.volume = 0.5f;
                StartCoroutine(playMusicCoroutine);
                break;
            case GameScene.InGame:
                musicAudioSource.volume = 0.1f;
                StartCoroutine(playAmbientSoundsCoroutine);
                break;
            default:
                return;
        }
    }

    private void StopAllAudio()
    {
        StopMusic();
        StopAmbientSounds();
    }

    public void PlayUiOnHoverSound()
    {
        SoundManager.Instance.uiAudioSource.PlayOneShot(uiOnHoverSound);
    }

    public void PlayUiOnClickSound()
    {
        SoundManager.Instance.uiAudioSource.PlayOneShot(uiOnClickSound);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        SoundManager.Instance.sfxAudioSource.PlayOneShot(audioClip);
    }


    private IEnumerator PlayMusic(AudioClip musicClip)
    {
        while (true)
        {
            SoundManager.Instance.musicAudioSource.clip = musicClip;
            SoundManager.Instance.musicAudioSource.Play();
            yield return new WaitForSeconds(musicClip.length);
        }
    }

    private void StopMusic()
    {
        if (playMusicCoroutine != null)
            StopCoroutine(playMusicCoroutine);
        musicAudioSource.Stop();
    }

    private IEnumerator PlayAmbientSounds()
    {
        if (SoundManager.Instance.ambientSounds.Length < 1) yield break;
        while (true)
        {
            int index = Random.Range(0, SoundManager.Instance.ambientSounds.Length);
            SoundManager.Instance.ambientAudioSource.clip = ambientSounds[index];
            SoundManager.Instance.ambientAudioSource.Play();
            yield return new WaitForSeconds(SoundManager.Instance.ambientSounds[index].length);
        }
    }

    private void StopAmbientSounds()
    {
        if (playAmbientSoundsCoroutine != null)
            StopCoroutine(playAmbientSoundsCoroutine);
        ambientAudioSource.Stop();
    }
}