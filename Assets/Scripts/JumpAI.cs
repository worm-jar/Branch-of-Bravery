using System.Collections;
using System.Collections.Generic;
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

    public AudioClip _attack;
    public AudioClip _dodge;
    // Start is called before the first frame update
    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        detected = false;
        wait = 1.6f;
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    public void Update()
    {
        distanceX = _player.transform.position.x - transform.position.x;
        distanceY = _player.transform.position.y - transform.position.y;
        if (distanceX > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (distanceX < 0)
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
                Debug.Log("yay");
                Attack();
                wait = 1;
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
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            detected = false;
        }
    }
    public void Attack()
    {
        if ((distanceX < -5f || distanceX > 5f) && (distanceY < 1.25f && distanceY > -1.25f) && detected)
        {
            _audioSource.PlayOneShot(_attack);
            _rig.AddForce(new Vector2(distanceX * 0.8f, 10f), ForceMode2D.Impulse);
            _animator.SetTrigger("Jump");
        }
        else if ((distanceX > -3f || distanceX < 3f) && (distanceX < 1 || distanceX > -1) && (distanceY < 1.25f && distanceY > -1.25f) && detected)
        {
            _audioSource.PlayOneShot(_dodge);
            distanceX = _player.transform.position.x - transform.position.x;
            _rig.AddForce(new Vector2(-normDistance * 15f, 1f), ForceMode2D.Impulse);
            _animator.SetTrigger("JumpBack");
        }
        else
        {
            return;
        }
    }
}

