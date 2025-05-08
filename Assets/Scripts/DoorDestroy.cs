using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    public int health = 4;
    public AudioSource _audioSource;
    public AudioClip _loud;
    public AudioClip _soft;
    public float timer;
    public static bool _healthOnce;
    // Start is called before the first frame update
    void Start()
    {
        _healthOnce = true;
        _audioSource = GetComponent<AudioSource>();
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
                DoorHasBeenDestroyed.destroyed = true;
                timer = 0;
            }
        }
        if (health == 0)
        {
            if(_healthOnce)
            {
                timer = 0.3f;
                _healthOnce = false;
            }
        }
        if (DoorHasBeenDestroyed.destroyed)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isStrongAttacking)
        {
            _audioSource.volume = 0.8f;
            _audioSource.PlayOneShot(_loud);
            health -= 1;
        }
        if (other.gameObject.CompareTag("Player Attack") && PlayerAttack.isLightAttacking)
        {
            _audioSource.volume = 1f;
            _audioSource.PlayOneShot(_soft);
            PlayerHealth.health += 8f;
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 0, 100);
        }
    }
}
