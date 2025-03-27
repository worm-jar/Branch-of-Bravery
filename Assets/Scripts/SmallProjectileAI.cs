using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallProjectileAI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _player;
    public Rigidbody2D _rig;
    private float direction;
    private float normDirection;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("Player");
        direction = _player.transform.position.x - transform.position.x;
        if (direction < 0)
        {
            normDirection = -1;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (direction > 0)
        {
            normDirection = 1;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rig.velocity = new Vector2(speed * normDirection, 0f);
        Destroy(this.gameObject, 10f);
    }
}
