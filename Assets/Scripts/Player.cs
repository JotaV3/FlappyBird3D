using System;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask pipeLayerMask;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float customGravity = 60f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // gravidade = false pra o jogador não cair enquanto está na tela de pressionar espaço para começar
        rb.useGravity = false;
    }

    private void Start()
    {
        GameInput.Instance.OnJump += GameInput_OnJump;
    }

    // Encontou no cano
    private void OnCollisionEnter(Collision collision)
    {
        // cria um bitmask, o bitmask é um bit referente ao valor da mascara
        if ((1 << collision.gameObject.layer & pipeLayerMask.value) != 0)
        {// se a layer que o jogador encostou for a do cano
            GameManager.Instance.GameOver();
        }
    }

    private void OnDestroy()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnJump -= GameInput_OnJump;
        }
    }

    private void GameInput_OnJump(object sender, System.EventArgs e)
    {
        // se o jogo não estiver acontecendo ou estiver pausado return
        if (!GameManager.Instance.IsGamePlaying() || GameManager.Instance.GetIsGamePaused()) return;

        // ativa a gravidade
        rb.useGravity = true;

        // zera a velocidade de y para que o pulo seja consistente
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        // adiciona uma força para cima, força de modo instantâneo
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        SoundManager.Instance.PlayJumpSound(transform.position);
    }

    private void FixedUpdate()
    {
        // se o jogo não estiver acontecendo return
        if (!GameManager.Instance.IsGamePlaying()) return;

        // Faz o player cair constantemente
        rb.AddForce(Vector3.down * customGravity * Time.deltaTime, ForceMode.Acceleration);
    }
}
