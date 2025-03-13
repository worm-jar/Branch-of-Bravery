using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator _animator;
    public float timer;
    public float timerStrongAttack;
    public InputActionAsset _asset;
    public static bool isStrongAttacking;
    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }
        if (timerStrongAttack > 0)
        {
            timerStrongAttack -= Time.deltaTime;
            if (timerStrongAttack <= 0)
            {
                timerStrongAttack = 0;
                isStrongAttacking = false;
            }
        }
    }
    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Light Attack").started += HandleLightAttack;
        _asset.FindAction("Player/Light Attack").performed += HandleLightAttack;
        _asset.FindAction("Player/Light Attack").canceled += HandleLightAttack;

        _asset.FindAction("Player/Heavy Attack").started += HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").performed += HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").canceled += HandleHeavyAttack;
    }
    private void OnDisable()
    {
        _asset.Disable();
        _asset.FindAction("Player/Light Attack").started -= HandleLightAttack;
        _asset.FindAction("Player/Light Attack").performed -= HandleLightAttack;
        _asset.FindAction("Player/Light Attack").canceled -= HandleLightAttack;

        _asset.FindAction("Player/Heavy Attack").started -= HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").performed -= HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").canceled -= HandleHeavyAttack;
    }
    public void HandleLightAttack(InputAction.CallbackContext ctx)
    {
        if (timer == 0)
        {
            _animator.Play("Light Attack");
            timer = 0.25f;
        }
        else if (timer > 0)
        {
            _animator.Play("Light Attack Follow");
            timer = 0.25f;
        }
    }
    public void HandleHeavyAttack(InputAction.CallbackContext ctx)
    {
        isStrongAttacking = true;
        _animator.Play("Heavy Attack");
        timerStrongAttack = 1.1f;
    }
}
