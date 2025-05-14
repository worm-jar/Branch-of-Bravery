using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Pathfinding;

public class FlyAI : MonoBehaviour
{
    public AIDestinationSetter _destination;
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float distanceX;
    public float distanceY;
    // Start is called before the first frame update
    void Start()
    {
        _destination = GetComponent<AIDestinationSetter>();
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceX = _player.transform.position.x - transform.position.x;
        distanceY = _player.transform.position.y - transform.position.y;
        if(distanceX < 9f &&  distanceX > -9 && distanceY < 9f && distanceY > -9f)
        {
            //_destination.target.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 0f);
            _destination.target = _player.transform;
        }
        if(distanceX > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if(distanceX < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
