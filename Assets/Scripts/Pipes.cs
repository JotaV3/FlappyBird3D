using JetBrains.Annotations;
using System;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] float pipeSpeed = 10f;
    [SerializeField] float deadzone = -50f;
    [SerializeField] int point = 1;

    private void Update()
    {
        // movimentação dos canos em direção ao jogador
        transform.position += Vector3.back * pipeSpeed * Time.deltaTime;

        if (transform.position.z < deadzone)
        {
            Destroy(gameObject);
        }
    }

    public int GetPoint()
    {
        return point;
    }
}
