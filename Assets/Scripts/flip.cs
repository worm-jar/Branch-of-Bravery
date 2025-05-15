using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flip : MonoBehaviour
{
    public float distance;
    public float normDistance;
    public Rigidbody2D _rig;
    public GameObject _player;
    public SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rig = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        distance = _player.transform.position.x - transform.position.x;
        if(distance < 0)
        {
            normDistance = -1;
        }
        else if(distance > 0)
        {
            normDistance = 1;
        }
        _rig.AddForce(new Vector2(-normDistance * 5f, 3.5f), ForceMode2D.Impulse);
        if (normDistance < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
