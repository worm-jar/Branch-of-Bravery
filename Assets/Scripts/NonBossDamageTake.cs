using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonBossDamageTake : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;
    public float knockBack;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float timerIFrames;
    public SpriteRenderer _sprite;
    public AudioSource _audioSource;
    public AudioClip _hurt;
    public Slider _slider;
    public Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = maxHealth;
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _canvas.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        _slider.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        _slider.maxValue = maxHealth;
        _slider.value = enemyHealth;
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
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
            this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            if (PlayerAttack.isLightAttacking)
            {
                Debug.Log(enemyHealth);
                timerIFrames = 0.6f;
                PlayerHealth.health += 6.5f;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
                enemyHealth -= 1f;
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(relativePos * knockBack * 1.8f, 3f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);

            }
            else if (PlayerAttack.isStrongAttacking)
            {
                Debug.Log(enemyHealth);
                timerIFrames = 0.6f;
                enemyHealth -= 20;
                _rig.velocity = Vector2.zero;
                float relativePos = transform.position.x - _player.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack * 3f, 2f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -7f, 7f), _rig.velocity.y);
            }
            _sprite.color = Color.black;
        }
    }
}
