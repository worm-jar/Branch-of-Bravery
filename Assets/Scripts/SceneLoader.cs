using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject _player;
    public GameObject _canvas;
    public Transform _image;
    public TransitionFadeToBlack FadeTrans;
    public void Update()
    {
        _canvas = GameObject.Find("CanvasHealth");
        _player = GameObject.Find("Player");
        _image = _canvas.transform.Find("Image");
        FadeTrans = _image.GetComponent<TransitionFadeToBlack>();
    }
    public void Load()
    {
        if (TriggerLoadScene.sceneName == "Bridge Right")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(-24.91f, -2.78f);
        }
        else if (TriggerLoadScene.sceneName == "Pre Fight Left")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("Bridge");
            _player.transform.position = new Vector2(28.5f, 1.72f);
        }
        else if (TriggerLoadScene.sceneName == "Pre Fight Right")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("Fight");
            _player.transform.position = new Vector2(-9.4f, -4.48f);
        }
        else if (TriggerLoadScene.sceneName == "Respawn Bridge")
        {
            PlayerHealth.health = 100f;
            FadeTrans.Fade();
            SceneManager.LoadScene("Bridge");
            _player.transform.position = new Vector3(-21.78f, -0.84f, -5.81f);
        }
        else if (TriggerLoadScene.sceneName == "Respawn Pre Fight")
        {
            PlayerHealth.health = 100f;
            FadeTrans.Fade();
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(-18.28f, -2.03f);
        }
    }
}
