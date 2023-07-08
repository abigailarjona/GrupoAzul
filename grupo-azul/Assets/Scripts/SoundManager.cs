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
    public static SoundManager Instance { get; private set; }

    private static SoundManager instance;
    private IEnumerator _playAmbientSounds;

    private enum GameScene
    {
        MainMenu = 0,
        Map1 = 1,
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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        switch ((GameScene)scene.buildIndex)
        {
            case GameScene.MainMenu:
                StopCoroutine(PlayMainMenuMusic());
                break;
            case GameScene.Map1:
                StartCoroutine(PlayAmbientSounds());
                break;
            default:
                return;
        }
    }

    public void PlayUiOnHoverSound()
    {
        SoundManager.Instance.uiAudioSource.PlayOneShot(uiOnHoverSound);
    }

    public void PlayUiOnClickSound()
    {
        SoundManager.Instance.uiAudioSource.PlayOneShot(uiOnClickSound);
    }

    public static IEnumerator PlayClipAtPoint(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position);
        yield return new WaitForSeconds(audioClip.length);
    }

    public static IEnumerator PlayMainMenuMusic()
    {
        yield return new WaitForSeconds(10f);
    }

    private IEnumerator PlayAmbientSounds()
    {
        if (ambientSounds.Length < 1) yield break;
        while (true)
        {
            int index = Random.Range(0, ambientSounds.Length);
            ambientAudioSource.clip = ambientSounds[index];
            ambientAudioSource.Play();
            yield return new WaitForSeconds(ambientSounds[index].length);
        }
    }
}