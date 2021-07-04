using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float jumpForce = 300f;

    private bool isGrounded;
    private Rigidbody rb;
    private PlayerInput actions;

	private void Awake()
	{
        actions = new PlayerInput();
		actions.Player.Jump.performed += x=> Jump();
	}

	private void OnEnable()
	{
        actions.Enable();
	}

	private void OnDisable()
	{
		actions.Disable();
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Movement();
	}

	private void Movement()
	{
		var moveDirection = actions.Player.Move.ReadValue<Vector2>();
		var move = new Vector3(moveDirection.x, 0f, moveDirection.y);
		rb.AddRelativeForce(move * speed);
	}

	private void Jump()
	{
		Debug.Log($"Is Grounded = {isGrounded}");
		if (isGrounded)
		{
			Debug.Log($"AddForce = {Vector3.up * jumpForce}");
			rb.AddRelativeForce(Vector3.up * jumpForce);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		rb.angularVelocity = new Vector3(0f, 0f, 0f);

		IsGroundedUpdate(collision, true);
	}

	private void OnCollisionExit(Collision collision)
	{
		rb.angularVelocity = new Vector3(0f, 0f, 0f);


		IsGroundedUpdate(collision, false);
	}

	private void IsGroundedUpdate(Collision collision, bool value)
	{
		if (collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = value;
		}
	}
}
