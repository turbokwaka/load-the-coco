using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")] 
    
    [SerializeField] [Range(5f, 10f)] private float speed = 6f;

    [SerializeField] [Range(1f, 6f)] private float jumpForce = 3f;
    [SerializeField] [Range(15f, 30f)] private float gravity = 20f;

    [Header("Camera Settings")] [SerializeField]
    private Transform cameraTransform;

    private CharacterController _controller;
    private Vector3 _moveDirection = Vector3.zero;
    private float _verticalVelocity;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Debug.Log("PlayerMovement started");
    }

    private void Update()
    {
        HandleVerticalMovement();
        HandleHorizontalMovement();
        ApplyMovement();
    }

    private void HandleVerticalMovement()
    {
        if (_controller.isGrounded && _verticalVelocity < 0)
            _verticalVelocity = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
            _verticalVelocity = jumpForce;

        _verticalVelocity -= gravity * Time.deltaTime;
    }

    private void HandleHorizontalMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 move = (camForward * verticalInput + camRight * horizontalInput);
        if (move.magnitude > 1)
            move.Normalize();

        _moveDirection = move * speed;
    }

    private void ApplyMovement()
    {
        _controller.Move(_moveDirection * Time.deltaTime);

        Vector3 verticalMove = new Vector3(0f, _verticalVelocity, 0f);
        _controller.Move(verticalMove * Time.deltaTime);
    }
}