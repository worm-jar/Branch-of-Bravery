using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Pathfinding;

public class FlyAI : MonoBehaviour
{
    public AudioSource _audioSource;
    public AIDestinationSetter _destination;
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float distanceX;
    public float distanceY;

    public AudioClip _flap;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Sound());
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
        if(distanceX < 6f &&  distanceX > -6 && distanceY < 6f && distanceY > -6 && NonBossDamageTake.isInv == false)
        {
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
    public IEnumerator Sound()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _audioSource.PlayOneShot(_flap);
        }
    }
}
