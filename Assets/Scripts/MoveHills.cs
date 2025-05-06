using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHills : MonoBehaviour
{
    public GameObject _player;
    public Transform _camera;
    public float speedx = 2f;
    public float speedy = 25f;
    public float offsetx = 6.75f;
    public float offsety = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _camera = _player.transform.GetChild(4);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Mathf.Clamp((_camera.transform.position.x / speedx) - offsetx, -13f, 5f), (_camera.position.y / speedy)-offsety, 2f);
    }
}
