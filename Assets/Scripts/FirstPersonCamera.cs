using UnityEngine;
using UnityEngine.Rendering;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraHolderTransform;

    private int sensitivity;
    private float rotationX;

    private void Start()
    {
        sensitivity = SettingsUI.Instance.GetSensitivity();

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
    }

    private void Update()
    {
        float mouseY = GameInput.Instance.GetLookInput().y * sensitivity * Time.deltaTime;
        float mouseX = GameInput.Instance.GetLookInput().x * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        playerTransform.Rotate(Vector3.up * mouseX);
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        sensitivity = SettingsUI.Instance.GetSensitivity();
    }
}