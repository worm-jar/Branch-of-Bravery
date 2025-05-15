using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    public float timer;
    public GameObject BLB0;
    public GameObject BLB1;
    public Animator _animator0;
    public Animator _animator1;
    public GameObject _gameManager;
    public AudioSource _audioSource;
    public static bool done = false;

    public AudioClip _fight;
    public AudioClip _towers;
    // Start is called before the first frame update
    void Start()
    {
;
    }
    private void Awake()
    {
        timer = -1;
        _gameManager = GameObject.Find("GameManager");
        _audioSource = _gameManager.GetComponent<AudioSource>();
        _animator0 = BLB0.GetComponent<Animator>();
        _animator1 = BLB1.GetComponent<Animator>();
        if (!EncounterDone.doneSet)
        {
            _audioSource.clip = _fight;
            _audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(BLB0);
                Destroy(BLB1);
                timer = -1;
            }
        }
        bool foundEnemy = GameObject.Find("JumpMan") || GameObject.Find("Fly");
        if (!foundEnemy && !done)
        {
            timer = 3f;
            _audioSource.clip = _towers;
            _audioSource.Play();
            _animator0.Play("FadeBLB");
            _animator1.Play("FadeBLB");
            done = true;
        }
        if (EncounterDone.doneSet && timer == -1)
        {
            Destroy(GameObject.Find("JumpMan"));
            Destroy(GameObject.Find("Fly"));
            Destroy(BLB0);
            Destroy(BLB1);
        }
    }
}
