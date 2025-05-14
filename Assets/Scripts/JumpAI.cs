using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAI : MonoBehaviour
{
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float distance;
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
        distance = _player.transform.position.x - transform.position.x;
        if(distance < 0)
        {
            normDistance = -1f;
        }
        if(distance > 0)
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
        if (distance < -6f || distance > 6f)
        {
            _rig.AddForce(new Vector2(distance * 0.6f, 7f), ForceMode2D.Impulse);
        }
        else if ((distance > -1f || distance < 1f) && (distance < 1 || distance > -1))
        {
            distance = _player.transform.position.x - transform.position.x;
            _rig.AddForce(new Vector2(-normDistance * 24f, 1f), ForceMode2D.Impulse);
        }
        else
        {
            distance = _player.transform.position.x - transform.position.x;
            _rig.AddForce(new Vector2(-normDistance * 24f, 1f), ForceMode2D.Impulse);
        }
    }
}

