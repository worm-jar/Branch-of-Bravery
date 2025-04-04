using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject _player;
    public void StartGame()
    {
        Time.timeScale = 1f;
        Pause.paused = false;
        DoorHasBeenDestroyed.destroyed = false;
        FakeAnnabethGone.gone = false;
        PlayerHealth.health = 100f;
        SceneManager.LoadScene("Bridge");
    }
    public void ResetNow()
    {
        _player = GameObject.Find("Player");
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
        Time.timeScale = 1f;
        Pause.paused = false;
    }
}
