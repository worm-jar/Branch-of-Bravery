using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public InputActionAsset _asset;
    public static bool paused;
    // Start is called before the first frame update
    public void Start()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Pause").started += HandlePause;
        _asset.FindAction("Player/Pause").performed += HandlePause;
        _asset.FindAction("Player/Pause").canceled += HandlePause;
    }
    public void HandlePause(InputAction.CallbackContext ctx)
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            Time.timeScale = 1f;
            paused = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            paused = true;
            this.gameObject.SetActive(true);
        }
    }
}
