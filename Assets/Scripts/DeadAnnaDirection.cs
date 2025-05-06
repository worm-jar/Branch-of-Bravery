using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAnnaDirection : MonoBehaviour
{
    private float normDirection;
    public GameObject _player;
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("Player");
        float direction = (_player.transform.position.x - transform.position.x);
        if (direction < 0)
        {
            normDirection = -1;
        }
        else if (direction > 0)
        {
            normDirection = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (normDirection < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
