using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EScene : MonoBehaviour
{
    public AudioClip touchSound = null;
    private AudioSource audioSource;
    public GameObject EscUI = null;
    public AudioSource bgm;
    public AudioSource sound;
    private bool mute = false;
    
    public Image setSound;
    [SerializeField]
    private Sprite muteImage;
    [SerializeField]
    private Sprite unmuteImage;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string idle)
    {
        switch (idle)
        {
            case "touch":
                audioSource.clip = touchSound;
                break;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void Exit()
    {
        PlaySound("touch");
        Application.Quit();
    }

    public void Stage()
    {
        PlaySound("touch");
        Time.timeScale = 1;
        SceneManager.LoadScene("select");
    }

    public void Resume()
    {
        EscUI.SetActive(false);
        if (!mute)
        {
            bgm.Play();
                }
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("new");
    }

    public void Mute()
    {
        if (!mute)
        {
            setSound.sprite = muteImage;
            mute = true;
            bgm.Stop();
            sound.volume = 0;
            
        }
        else
        {
            setSound.sprite = unmuteImage;
            mute = false;
            sound.volume = 0.8f;
        }
    }
}
