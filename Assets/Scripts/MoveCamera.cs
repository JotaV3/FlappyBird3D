using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraOrientation;

    private void Update()
    {
        transform.position = cameraOrientation.position;
    }
}
