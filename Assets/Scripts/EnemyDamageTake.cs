using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTake : MonoBehaviour
{
    public float knockBack;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float timerIFrames;
    public SpriteRenderer _sprite;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyHealth.health <= 0)
        {
            //Instantiate dead annabeth and Destroy Annabeth
        }
        if (timerIFrames > 0)
        {
            timerIFrames -= Time.deltaTime;
            if (timerIFrames <= 0)
            {
                _sprite.color = Color.white;
                this.gameObject.layer = 8;
                timerIFrames = 0;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Attack"))
        {
            this.gameObject.layer = 10;
            if (PlayerAttack.isLightAttacking)
            {
                PlayerHealth.health += 8;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
                EnemyHealth.health -= 1f;
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack, 2f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -3.5f, 3.5f), _rig.velocity.y);

            }
            else if (PlayerAttack.isStrongAttacking)
            {
                EnemyHealth.health -= 20;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 32, 100);
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack * 2.5f, 2f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);
            }
            _sprite.color = Color.black;
            timerIFrames = 0.6f;
        }
    }
}
