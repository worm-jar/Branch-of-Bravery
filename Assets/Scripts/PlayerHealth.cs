using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static float health;
    public Slider _healthSlider;
    public Animator _animator;
    private bool dieOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        dieOnce = true;
        _animator = GetComponent<Animator>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        _healthSlider.value = health;
        if (health <= 0 && dieOnce == true)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            dieOnce = false;
        }
    }
}
