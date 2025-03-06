using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{

    public InputActionAsset inputAsset;
    public float speed = 1f;
    private float _axis;

    private void OnEnable()
    {
        inputAsset.FindAction("Player/Jump").started += HandleJump;
        inputAsset.FindAction("Player/Jump").performed += HandleJump;
        inputAsset.FindAction("Player/Jump").canceled += HandleJump;
        
        inputAsset.FindAction("Player/Move").started += HandleMove;
        inputAsset.FindAction("Player/Move").performed += HandleMove;
        inputAsset.FindAction("Player/Move").canceled += HandleMove;

        inputAsset.Enable();
    }
    private void OnDisable()
    {
        inputAsset.FindAction("Player/Jump").started -= HandleJump;
        inputAsset.FindAction("Player/Jump").performed -= HandleJump;
        inputAsset.FindAction("Player/Jump").canceled -= HandleJump;

        inputAsset.FindAction("Player/Move").started -= HandleMove;
        inputAsset.FindAction("Player/Move").performed -= HandleMove;
        inputAsset.FindAction("Player/Move").canceled -= HandleMove;

        inputAsset.Disable();
    }
    public void Update()
    {
        transform.position += Vector3.right * _axis * speed * Time.deltaTime;
    }
    private void HandleJump(InputAction.CallbackContext ctx)
    {
        Debug.Log($"JUMP: Phase = {ctx.phase}");
    } 
    private void HandleMove(InputAction.CallbackContext ctx)
    {
        _axis = ctx.ReadValue<float>();
        Debug.Log($"MOVE: Phase = {ctx.phase}, Axis = {_axis}");
    }
}
