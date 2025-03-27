using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject _player;
    public void Load()
    {
        if (TriggerLoadScene.sceneName == "Bridge Right")
        {
            Debug.Log("LoadScened");
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(0, 0);
        }
    }
}
