using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public CharacterController character;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public float jumpHeight = 3f;
	public LayerMask groundMask;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float gravity = -9.81f;

	private PlayerInput actions;
	private Vector3 velocity;
	private bool isGrounded;

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

	private void Update()
	{
		UpdateVelosity();

		var moveDirection = actions.Player.Move.ReadValue<Vector2>();
		Move(moveDirection);
	}

	private void Move(Vector2 direction)
	{
		var scaledMoveSpeed = moveSpeed * Time.deltaTime;

		if (character != null)
		{
			MoveWithCharacterController(direction, scaledMoveSpeed);
		}
		else
		{
			MoveWithOutCharacterController(direction, scaledMoveSpeed);
		}
	}

	private void MoveWithCharacterController(Vector2 direction, float scaledMoveSpeed)
	{
		var move = (transform.right * direction.x + transform.forward * direction.y) * scaledMoveSpeed;
		character.Move(move);
		velocity.y += gravity * Time.deltaTime;
		character.Move(velocity * Time.deltaTime);
	}

	private void MoveWithOutCharacterController(Vector2 direction, float scaledMoveSpeed)
	{
		var moveDirection = new Vector3(direction.x, 0, direction.y);
		transform.position += moveDirection * scaledMoveSpeed;
		velocity.y += gravity * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;
	}

	private void UpdateVelosity()
	{
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		if (isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}
	}

	private void Jump()
	{
		if (isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
	}
}
