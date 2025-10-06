using UnityEngine;
using UnityEngine.Rendering;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Rigidbody playerRigidbody;

    private int sensitivity;
    private float rotationX;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        sensitivity = SettingsUI.Instance.GetSensitivity();

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
    }

    private void Update()
    {
        float mouseY = GameInput.Instance.GetLookInput().y * sensitivity * Time.deltaTime;
        float mouseX = GameInput.Instance.GetLookInput().x * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        Quaternion playerRotation = playerRigidbody.rotation;
        playerRotation *= Quaternion.Euler(0f, mouseX, 0f);
        playerRigidbody.MoveRotation(playerRotation);     
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        sensitivity = SettingsUI.Instance.GetSensitivity();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}