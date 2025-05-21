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
    public AudioSource _audioSource;
    public AudioClip _hurt;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
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
                this.gameObject.layer = LayerMask.NameToLayer("Enemy");
                timerIFrames = 0;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Attack"))
        {
            _audioSource.PlayOneShot(_hurt);
            if (PlayerAttack.isLightAttacking && this.gameObject.layer != LayerMask.NameToLayer("Invincible"))
            {
                timerIFrames = 0.6f;
                PlayerHealth.health += 6.5f;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
                EnemyHealth.health -= 1f;
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(relativePos * knockBack * 1.8f, 3f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            }
            else if (PlayerAttack.isStrongAttacking && this.gameObject.layer != LayerMask.NameToLayer("Invincible"))
            {
                timerIFrames = 0.6f;
                EnemyHealth.health -= 20;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 32, 100);
                _rig.velocity = Vector2.zero;
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack * 2.2f, 2f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5.8f, 5.8f), _rig.velocity.y);
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            }
            _sprite.color = Color.black;
        }
    }
}
