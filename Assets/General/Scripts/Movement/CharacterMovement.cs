using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
	[SerializeField]
	public Transform groundCheck;

	[SerializeField]
	public float groundDistance = 0.4f;

	[SerializeField]
	public float jumpForce = 3f;

	[SerializeField]
	public LayerMask groundMask;

	[SerializeField]
	private float speed;

	[SerializeField]
	private float gravity = -9.81f;

	private CharacterController character;
	private PlayerInput actions;
	private Vector3 velocity;

	private Vector3 MovementDirection
	{
		get
		{
			var moveDirection = actions.Player.Move.ReadValue<Vector2>();
			return new Vector3(moveDirection.x, 0f, moveDirection.y);
		}
	}
	
	private bool IsGrounded
	{
		get
		{
			return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		}
	}

	private void Awake()
	{
		actions = new PlayerInput();
		actions.Player.Jump.performed += x => Jump();
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
		character = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		UpdateVelosity();
		Movement();
	}

	private void Movement()
	{
		velocity.y += gravity * Time.deltaTime;
		
		var move = MovementDirection;
		move = (transform.right * move.x + transform.forward * move.z) * speed;
		
		character.Move((move + velocity) * Time.deltaTime);
	}

	private void UpdateVelosity()
	{
		if (IsGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}
	}

	private void Jump()
	{
		if (IsGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
		}
	}
}
