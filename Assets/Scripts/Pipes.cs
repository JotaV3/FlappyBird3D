using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class Pipes : MonoBehaviour
{
    private const float DEADZONE = -50f;
    private const string IS_CLOSED = "IsClosed";

    [SerializeField] private Transform topPipeTransform;
    [SerializeField] private Transform lowerPipeTransform;
    [SerializeField] private float pipeSpeed = 10f;
    [SerializeField] private int point = 1;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        animator.SetBool(IS_CLOSED, false);
    }

    private void Update()
    {
        HandleMovement();
    }

    public void ClosePipes()
    {
        animator.SetBool(IS_CLOSED, true);
    }

    private void HandleMovement()
    {
        // pipes move backwards
        transform.position += Vector3.back * pipeSpeed * Time.deltaTime;

        if (transform.position.z < DEADZONE)
        {
            Destroy(gameObject);
        }
    }

    public int GetPoint()
    {
        return point;
    }
}
