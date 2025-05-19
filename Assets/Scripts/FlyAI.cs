using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Pathfinding;
using Unity.VisualScripting;

public class FlyAI : MonoBehaviour
{
    public AIPath _AIPath;
    public AudioSource _audioSource;
    public AIDestinationSetter _destination;
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float distanceX;
    public float distanceY;
    public float timer;
    public bool waitingDamage;
    public bool detected;

    public AudioClip _flap;
    // Start is called before the first frame update
    void Start()
    {
        detected = false;
        _AIPath = GetComponent<AIPath>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.Find("Player");
        _destination = GetComponent<AIDestinationSetter>();
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        StartCoroutine(Sound());
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = _player.transform.position.x - transform.position.x;
        distanceY = _player.transform.position.y - transform.position.y;
        if((distanceX < 6f &&  distanceX > -6 && distanceY < 6f && distanceY > -6 && !waitingDamage) || detected)
        {
            _destination.target = _player.transform;
            detected = true;
        }
        else if (detected == false)
        {
            Rando();
        }
        if(distanceX > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if(distanceX < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                waitingDamage = false;
                timer = 0;
            }
        }
    }
    public void Rando()
    {
        transform.position = new Vector2((transform.position.x + Mathf.Sin(Time.time) / 1100), (transform.position.y + Mathf.Cos(Time.time*2) / 750));
    }
    public IEnumerator Sound()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _audioSource.PlayOneShot(_flap);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            waitingDamage = true;
            _AIPath.Move(new Vector3(-distanceX * 2, 0, 0));
            timer = 0.01f;
        }
    }
}
