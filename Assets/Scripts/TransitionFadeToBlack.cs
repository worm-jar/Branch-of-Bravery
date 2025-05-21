using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionFadeToBlack : MonoBehaviour
{
    public Animator _animator;
    public GameObject _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = this.gameObject;
        _animator = _image.GetComponent<Animator>();
    }

    public void Fade()
    {
        _animator.Play("Fade");
    }
    public void FadeIn()
    {
        _animator.Play("FadeIn");
    }
    public void FadeOut()
    {
        _animator.Play("FadeOut");
    }
}
