using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class JumpAI : MonoBehaviour
{
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float distanceX;
    public float distanceY;
    public float normDistance;
    public float wait;
    public bool detected;
    public AudioSource _audioSource;
    public bool jumpedBack;
    public float phase;

    public AudioClip _attack;
    public AudioClip _dodge;
    public bool isAttacking;
    // Start is called before the first frame update
    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        detected = false;
        wait = 1 + phase;
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        distanceX = _player.transform.position.x - transform.position.x;
        distanceY = _player.transform.position.y - transform.position.y;
        if (distanceX > 0 && !isAttacking)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (distanceX < 0 && !isAttacking)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (distanceX < 0)
        {
            normDistance = -1f;
        }
        if(distanceX > 0)
        {
            normDistance = 1f;
        }
        if (wait > 0)
        {
            wait -= Time.deltaTime;
            if (wait <= 0)
            {
                Attack();
                wait = Random.Range(0.35f, 1.5f);
            }  
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            detected = true;
        }
    }
    public void Attack()
    {
        if ((distanceX < -5f || distanceX > 5f) && (distanceY < 1.25f && distanceY > -1.25f) && detected)
        {
            _audioSource.PlayOneShot(_attack);
            _rig.AddForce(new Vector2(distanceX * 0.7f, 16.5f), ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
            jumpedBack = false;
        }
        else if ((distanceX > -3f || distanceX < 3f) && (distanceX < 1 || distanceX > -1) && (distanceY < 1.25f && distanceY > -1.25f) && detected && jumpedBack == false)
        {
            _audioSource.PlayOneShot(_dodge);
            distanceX = _player.transform.position.x - transform.position.x;
            _rig.AddForce(new Vector2(-normDistance * Random.Range(7, 18), 1f), ForceMode2D.Impulse);
            _animator.SetTrigger("JumpBack");
            jumpedBack = true;
        }
        else if (jumpedBack) 
        {
            _audioSource.PlayOneShot(_attack);
            _rig.AddForce(new Vector2(distanceX * 0.7f, 16.5f), ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
            jumpedBack = false;
        }
    }
}

