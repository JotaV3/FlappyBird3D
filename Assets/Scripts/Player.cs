using System;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private LayerMask pipeLayerMask;

    [Header("Player movement config")]
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private float customGravity = 1200f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float positionXLimit;

    [Header("Camera")]
    [SerializeField] private Transform cameraOrientation;

    public event EventHandler OnPlayerHitPipe;

    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
        // gravidade = false pra o jogador não cair enquanto está na tela de pressionar espaço para começar
        rb.useGravity = false;
    }

    private void Start()
    {
        GameInput.Instance.OnJump += GameInput_OnJump;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void FixedUpdate()
    {
        // se o jogo não estiver acontecendo return
        if (!GameManager.Instance.IsGamePlaying()) return;

        // player falls slowly
        rb.AddForce(Vector3.down * customGravity * Time.deltaTime, ForceMode.Acceleration);
    }

    // hit pipe
    private void OnCollisionEnter(Collision collision)
    {
        // cria um bitmask, o bitmask é um bit referente ao valor da mascara
        if ((1 << collision.gameObject.layer & pipeLayerMask.value) != 0)
        {// se a layer que o jogador encostou for a do cano
            OnPlayerHitPipe?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private void HandleMovement()
    {
        Vector3 inputVector = GameInput.Instance.GetInputNormalized();
        Vector3 moveDir = rb.transform.forward * inputVector.z + rb.transform.right * inputVector.x;
        
        Vector3 targetPosition = rb.position + moveDir * moveSpeed * Time.deltaTime;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -positionXLimit, positionXLimit);

        rb.MovePosition(targetPosition);
    }

    private void GameInput_OnJump(object sender, System.EventArgs e)
    {
        // ativa a gravidade
        rb.useGravity = true;

        // zera a velocidade de y para que o pulo seja consistente
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // adiciona uma força para cima, força de modo instantâneo
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        SoundManager.Instance.PlayJumpSound(transform.position);
    }
}
