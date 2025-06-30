using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource cachedAudioSource;
    [SerializeField] private AudioClip confirmSfx;
    [SerializeField] private AudioClip cancelSfx;

    private void Awake()
    {
        cachedAudioSource = GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayConfirmSfx()
    {
        cachedAudioSource.clip = confirmSfx;
        cachedAudioSource.Play();
    }
    public void PlayCancelSfx()
    {
        cachedAudioSource.clip = cancelSfx;
        cachedAudioSource.Play();
    }
}
