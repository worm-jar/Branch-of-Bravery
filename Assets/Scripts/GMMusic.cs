using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMMusic : MonoBehaviour
{
    public AudioClip LoftyTowersClip;
    public AudioClip AnnaFightClip;
    public AudioSource AudioSource;
    public float timer;
    // Start is called before the first frame update
    public void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    public void Update()

    {
        if (AnnaDeath.dead == true)
        {
            AudioSource.enabled = false;
        }
    }
    public void OnEnable()
    {
        GetComponent<SceneLoader>()._onChangeAudio.AddListener(ChangeMusic);
    }
    public void OnDisable()
    {
        GetComponent<SceneLoader>()._onChangeAudio.RemoveListener(ChangeMusic);
    }
    public void ChangeMusic(string name)
    {
        Debug.Log("Will chnage music " + name);
        if (name == "Pre Fight Right")
        {
            ChangeToAnna();
            PlayMusic();
        }
        else if (name == "Respawn Pre Fight" || name == "Respawn Bridge")
        {
            ChangeToLofty();
            PlayMusic();
        }
    }

    public void ChangeToLofty()
    {
        AudioSource.clip = LoftyTowersClip;
    }
    public void ChangeToAnna()
    {
        AudioSource.clip = AnnaFightClip;
    }
    public void PlayMusic()
    {
        AudioSource.Play();
    }
}
