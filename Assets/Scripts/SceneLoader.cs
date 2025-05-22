using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject _player;
    public GameObject _canvashealth;
    public GameObject _moonCanvas;
    public TriggerLoadScene MusicChange;
    public Transform _image;
    public TransitionFadeToBlack FadeTrans;
    public float timerStart;
    public float timerSewer;
    public UnityEvent<string> _onChangeAudio;
    public void Update()
    {
        _moonCanvas = GameObject.Find("MoonCanvas");
        _canvashealth = GameObject.Find("CanvasHealth");
        _player = GameObject.Find("Player");
        _image = _canvashealth.transform.Find("Image");
        if (_image == null)
            return;
        FadeTrans = _image.GetComponent<TransitionFadeToBlack>();
        MusicChange = _player.GetComponent<TriggerLoadScene>();
    }
    public void Load()
    {
        if (TriggerLoadScene.sceneName == "Bridge Right")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("DashTuto");
            _player.transform.position = new Vector2(-10f, 0f);
        }
        else if (TriggerLoadScene.sceneName == "EnemyLeft")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("DashTuto");
            _player.transform.position = new Vector2(50.88f, 11.44f);
        }
        else if (TriggerLoadScene.sceneName == "EnemyRight")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(-24f, -1.588789f);
        }
        else if (TriggerLoadScene.sceneName == "Pre Fight Right")
        {
            Debug.Log("Will load scene: Fight");
            _onChangeAudio.Invoke(TriggerLoadScene.sceneName);
            FadeTrans.Fade();
            SceneManager.LoadScene("Fight");
            _player.transform.position = new Vector2(-9.4f, -4.48f);
        }
        else if (TriggerLoadScene.sceneName == "Pre Fight Left")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("EnemyBattle");
            _player.transform.position = new Vector2(9.94f, -1.39f);
        }
        else if (TriggerLoadScene.sceneName == "Dash Left")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("Bridge");
            _player.transform.position = new Vector2(55f, -1.39f);
        }
        else if (TriggerLoadScene.sceneName == "Dash Right")
        {
            Debug.Log("Will load scene: Fight");
            FadeTrans.Fade();
            SceneManager.LoadScene("EnemyBattle");
            _player.transform.position = new Vector2(-10.29f, -1.43f);
        }
        else if (TriggerLoadScene.sceneName == "Respawn Bridge")
        {
            Debug.Log("Will load scene: Bridge");
            _onChangeAudio.Invoke(TriggerLoadScene.sceneName);
            PlayerHealth.health = 100f;
            FadeTrans.Fade();
            _player.transform.position = new Vector3(-21.78f, -0.84f, -5.81f);
            SceneManager.LoadScene("Bridge"); 
        }
        else if (TriggerLoadScene.sceneName == "Respawn Pre Fight")
        {
            Debug.Log("Will load scene: Prefight");
            _onChangeAudio.Invoke(TriggerLoadScene.sceneName);
            PlayerHealth.health = 100f;
            FadeTrans.Fade();
            SceneManager.LoadScene("Prefight");
            _player.transform.position = new Vector2(-18.28f, -2.03f);
        }
        else if (TriggerLoadScene.sceneName == "Respawn Dash")
        {
            _onChangeAudio.Invoke(TriggerLoadScene.sceneName);
            PlayerHealth.health = 100f;
            FadeTrans.Fade();
            SceneManager.LoadScene("DashTuto");
            _player.transform.position = new Vector2(-2.59f, -0.85f);
        }
        else if (TriggerLoadScene.sceneName == "EndCut")
        {
            FadeTrans.Fade();
            SceneManager.LoadScene("EndCutscene");
            Destroy(_player);
            Destroy(_moonCanvas);
            Destroy(_canvashealth);
        }
    }
}
