using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public GameObject healthSlider;
    public GameObject midSlider;
    public GameObject _healthCanvas;
    public GameObject _player;
    public AudioSource _audioSource;
    public AudioClip _towers;
    public AudioClip _title;
    public float timer;
    public string funcName;
    public void Awake()
    {
        if(healthSlider != null && midSlider != null)
        {
            healthSlider.SetActive(false);
            midSlider.SetActive(false);
        }
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
        _healthCanvas.SetActive(false);
        _audioSource.Stop();
        Time.timeScale = 1f;
        SceneManager.LoadScene("CutScene");
        timer = 20f;
    }
    public void StartGame()
    {
        funcName = "start";
        StartCoroutine(FadeAction());
        _audioSource.clip = _towers;
        _audioSource.Play();
        timer = -1f;
        Time.timeScale = 1f;
        Pause.paused = false;
        EncounterDone.doneSet = false;
        DoorHasBeenDestroyed.destroyed = false;
        FakeAnnabethGone.gone = false;
        PlayerHealth.health = 70f;
    }
    public void ResetNow()
    {
        funcName = "reset";
        StartCoroutine(FadeAction());
        _audioSource.clip = _towers;
        _audioSource.Play();
        _player = GameObject.Find("Player");
        EncounterDone.doneSet = false;
        Time.timeScale = 1f;
        Pause.paused = false;
        DoorHasBeenDestroyed.destroyed = false;
        FakeAnnabethGone.gone = false;
        PlayerHealth.health = 100f;
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
    //public GameObject FindHealthCanvas()
    //{
    //    Canvas[] _canvas = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    //    foreach(Canvas c in _canvas)
    //    {
    //        Debug.Log(c.name);
    //        if(c.name == "CanvasHealth")
    //        {
    //            return c.gameObject;
    //        }
    //    }
    //    return null;
    //}
    public TransitionFadeToBlack FindTransition(GameObject healthCanvas)
    {
        //TransitionFadeToBlack[] _gm = FindObjectsByType<TransitionFadeToBlack>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        //foreach (TransitionFadeToBlack c in _gm)
        //{
        //    if (c.name == "GameManager")
        //    {
        //        return c.;
        //    }
        //}
        //return null;
        if(healthCanvas != null)
        {
            GameObject _blackScreenImage = healthCanvas.transform.Find("Image").gameObject;
            return _blackScreenImage.GetComponent<TransitionFadeToBlack>();
        }
        return null;
    }
    public IEnumerator FadeAction()    
    {
        //GameObject _canvas = FindHealthCanvas();
        //if (_canvas != null)
        //{
        //   _canvas.SetActive(true);
        //}
        _healthCanvas.SetActive(true);

        TransitionFadeToBlack FadeToBlackTransition = FindTransition(_healthCanvas);
        if (FadeToBlackTransition != null)
        {
            FadeToBlackTransition.FadeIn();
        }

        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("Bridge");
        if (funcName == "start")
        {
            Instantiate(_player, new Vector3(-21.78f, -0.84f, -5.81f), Quaternion.identity);
        }
        else if (funcName == "reset")
        {
            _player.transform.position = new Vector3(-21.78f, -0.84f, -5.81f);
        }
        yield return new WaitForSeconds(0.5f);
        healthSlider.SetActive(true);
        midSlider.SetActive(true);
        if (FadeToBlackTransition != null)
        {
            FadeToBlackTransition.FadeOut();
        }
    }
}
