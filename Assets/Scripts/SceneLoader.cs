using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject _player;
    public GameObject _image;
    public TransitionFadeToBlack FadeTrans;
    public void Start()
    {
        _player = GameObject.Find("Player");
        FadeTrans = _image.GetComponent<TransitionFadeToBlack>();
    }
    public void Load()
    {
        if (TriggerLoadScene.sceneName == "Bridge Right")
        {
            FadeTrans.Fade();
            Debug.Log("LoadScened");
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(-24.91f, -2.78f);
        }
    }
}
