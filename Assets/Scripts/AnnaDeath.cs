using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaDeath : MonoBehaviour
{
    public GameObject _player;
    public Rigidbody2D _rig;
    public bool once;
    public float normDirection;
    public SpriteRenderer _spriteRenderer;
    public static bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = GameObject.Find("Player");
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        once = true;
    }
    private void Awake()
    {
        dead = true;
        foreach (Canvas canvas in FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if(canvas.name == "DeathCanvas")
            {
                canvas.gameObject.SetActive(true);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        float direction = (_player.transform.position.x - transform.position.x);
        if (direction < 0)
        {
            normDirection = -1;
        }
        else if (direction > 0)
        {
            normDirection = 1;
        }
        if(once)
        {
            if (normDirection < 0)
            {
                _spriteRenderer.flipX = true;
            }
            _rig.AddForce(new Vector2(-normDirection * 5f, 3f), ForceMode2D.Impulse);
            once = false;
        }
    }
}
