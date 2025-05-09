using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public InputActionAsset _asset;
    public static bool paused;
    public GameObject _player;
    public PlayerMovement PlayerMovement;
    public bool once;
    // Start is called before the first frame update
    public void Start()
    {
        _player = GameObject.Find("Player");
        PlayerMovement = _player.GetComponent<PlayerMovement>();
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _asset.Enable();
        once = true;
        _asset.FindAction("Player/Pause").started += HandlePause;
        _asset.FindAction("Player/Pause").performed += HandlePause;
        _asset.FindAction("Player/Pause").canceled += HandlePause;
    }
    public void HandlePause(InputAction.CallbackContext ctx)
    {
        if (once)
        {
            if (this.gameObject.activeInHierarchy == true && PlayerMovement.isInteracting == false)
            {
                Time.timeScale = 1f;
                paused = false;
                this.gameObject.SetActive(false);
                once = false;
            }
            else if (this.gameObject.activeInHierarchy == false && PlayerMovement.isInteracting == false)
            {
                Time.timeScale = 0f;
                paused = true;
                this.gameObject.SetActive(true);
                once = false;
            }
            else if (PlayerMovement.isInteracting == true)
            {
                PlayerMovement._interactSewer.SetActive(false);
                PlayerMovement._interactJump.SetActive(false);
                PlayerMovement._interactDash.SetActive(false);
                PlayerMovement._interactAttack.SetActive(false);
                PlayerMovement._text.text = "";
                PlayerMovement.isInteracting = false;
                once = false;
            }
        }
        if(ctx.canceled)
        {
            once = true;
        }
    }
}
