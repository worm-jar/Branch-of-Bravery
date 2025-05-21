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
    public GameObject _canvas;
    // Start is called before the first frame update
    void Awake()
    {
        dieOnce = true;
        _animator = GetComponent<Animator>();
        health = 70;
    }

    // Update is called once per frame
    void Update()
    {
        _canvas = GameObject.Find("CanvasHealth");
        _healthSlider = _canvas.transform.Find("Health").gameObject.GetComponent<Slider>();
        if (_healthSlider == null)
            return;
        _healthSlider.value = health;
        if (health <= 0 && dieOnce == true)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            dieOnce = false;
        }
    }
}
