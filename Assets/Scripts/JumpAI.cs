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
    // Start is called before the first frame update
    public void Start()
    {
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
                wait = 2;
            }  
        }
    }
    public void Attack()
    {
        if ((distanceX < -6f || distanceX > 6f) && (distanceY < 2.5f || distanceY > -2.5f))
        {
            _rig.AddForce(new Vector2(distanceX * 0.6f, 7f), ForceMode2D.Impulse);
        }
        else if ((distanceX > -1f || distanceX < 1f) && (distanceX < 1 || distanceX > -1) && (distanceY < 2.5f || distanceY > -2.5f))
        {
            distanceX = _player.transform.position.x - transform.position.x;
            _rig.AddForce(new Vector2(-normDistance * 15f, 1f), ForceMode2D.Impulse);
        }
        else
        {
            return;
        }
    }
}

