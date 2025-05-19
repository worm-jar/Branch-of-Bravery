using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;
    public GameObject _player;
    public Transform _camera;
    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _camera = _player.transform.GetChild(4);
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = _camera.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos * distance, transform.position.y, transform.position.z);
    }
}
