using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class NonBossDamageTake : MonoBehaviour
{
    public AIDestinationSetter _destination;
    public AIPath _AIPath;
    public float enemyHealth;
    public float maxHealth;
    public float knockBack;
    public Rigidbody2D _rig;
    public GameObject _player;
    public float timerIFrames;
    public float timerMovement;
    public SpriteRenderer _sprite;
    public AudioSource _audioSource;
    public Slider _slider;
    public Canvas _canvas;
    public GameObject dead;
    public static bool isInv;
    public float relativePos;
    public float relativePosNorm;

    public AudioClip hit;
    // Start is called before the first frame update
    void Start()
    {
        _AIPath = GetComponent<AIPath>();
        _destination = GetComponent<AIDestinationSetter>();
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player");
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _canvas.transform.position = new Vector2(transform.position.x, transform.position.y + 1.25f);
        _slider.transform.position = new Vector2(transform.position.x, transform.position.y + 1.25f);
        _slider.maxValue = maxHealth;
        _slider.value = enemyHealth;
        if (enemyHealth <= 0)
        {
            Instantiate(dead, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if (timerIFrames > 0)
        {
            timerIFrames -= Time.deltaTime;
            if (timerIFrames <= 0)
            {
                _audioSource.volume = 1.0f;
                _sprite.color = Color.white;
                this.gameObject.layer = LayerMask.NameToLayer("Enemy");
                timerIFrames = 0;
            }
        }
        if (timerMovement > 0)
        {
            _AIPath.Move(Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(750f * relativePosNorm, 0, 0) * Time.deltaTime, 0.3f));
            timerMovement -= Time.deltaTime;
            if (timerMovement <= 0)
            {
                timerMovement = 0;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player Attack"))
        {
            Debug.Log(enemyHealth + "before hit");
            _audioSource.volume = 0.7f;
            _audioSource.PlayOneShot(hit);
            if (PlayerAttack.isLightAttacking)
            {
                Debug.Log(enemyHealth);
                PlayerHealth.health += 6.5f;
                PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
                enemyHealth -= 1f;
                relativePos = transform.position.x - _player.transform.position.x;
                if (relativePos < 0)
                {
                    relativePosNorm = -1;
                }
                else if (relativePos > 0)
                {
                    relativePosNorm = 1;
                }
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(relativePos * knockBack * 1.8f, 3f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);
                if (this.gameObject.name == "Fly")
                {
                    //timerMovement = 0.01f;
                    _AIPath.Move(new Vector3(0.75f * relativePosNorm, 0, 0));
                }
            }
            else if (PlayerAttack.isStrongAttacking && this.gameObject.layer != LayerMask.NameToLayer("JumpManInvincible"))
            {
                enemyHealth -= 20;
                _rig.velocity = Vector2.zero;
                _audioSource.volume = 0.7f;
                _audioSource.PlayOneShot(hit);
                relativePos = transform.position.x - _player.transform.position.x;
                if (relativePos < 0)
                {
                    relativePosNorm = -1;
                }
                else if (relativePos > 0)
                {
                    relativePosNorm = 1;
                }
                _rig.AddForce(new Vector2(relativePos * knockBack * 3f, 2f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -7f, 7f), _rig.velocity.y);
                if (this.gameObject.name == "Fly")
                {
                    //timerMovement = 0.015f;
                    _AIPath.Move(new Vector3(1.5f * relativePosNorm, 0, 0));
                }
                Debug.Log(enemyHealth + "after hit");
            }
            this.gameObject.layer = LayerMask.NameToLayer("JumpManInvincible");
            timerIFrames = 0.6f;
            _sprite.color = Color.black;
        }
    }
}
