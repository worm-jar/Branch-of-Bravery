using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject _player;
    public AudioSource _audioSource;
    public AudioClip _towers;
    public AudioClip _title;
    public float timer;
    public void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _title;
        _audioSource.Play();
    }
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StartGame();
                timer = 0;
            }
        }
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void BackToStart()
    {
        SceneManager.LoadScene("TitleMenu");
    }
    public void StartCutscene()
    {
        _audioSource.Stop();
        Time.timeScale = 1f;
        SceneManager.LoadScene("CutScene");
        timer = 20f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Bridge");
        _audioSource.clip = _towers;
        _audioSource.Play();
        timer = -1f;
        Time.timeScale = 1f;
        Pause.paused = false;
        EncounterDone.doneSet = false;
        DoorHasBeenDestroyed.destroyed = false;
        FakeAnnabethGone.gone = false;
        PlayerHealth.health = 100f;
        Instantiate(_player, new Vector3(-21.78f, -0.84f, -5.81f), Quaternion.identity);
    }
    public void ResetNow()
    {
        _audioSource.clip = _towers;
        _audioSource.Play();
        _player = GameObject.Find("Player");
        EncounterDone.doneSet = false;
        _player.transform.position = new Vector3(-21.78f, -0.84f, -5.81f);
        Time.timeScale = 1f;
        Pause.paused = false;
        DoorHasBeenDestroyed.destroyed = false;
        FakeAnnabethGone.gone = false;
        PlayerHealth.health = 100f;
        SceneManager.LoadScene("Bridge");
    }
    public void UnpauseButton()
    {
        Debug.Log("yay");
        Time.timeScale = 1f;
        Pause.paused = false;
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}
