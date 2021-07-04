using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerPhysicMovement : MonoBehaviour
{
	[SerializeField]
	private float speed = 10f;

	[SerializeField]
	private float jumpForce = 200f;

	[SerializeField]
	private LayerMask groundLayer = 1;

	private Rigidbody rb;
	private CapsuleCollider playerCollider;
	private PlayerInput actions;

	private bool IsGrounded
	{
		get
		{
			var bottomCenterPoint = new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y, playerCollider.bounds.center.z);

			return Physics.CheckCapsule(playerCollider.bounds.center, bottomCenterPoint, playerCollider.bounds.size.x / 2 * 0.9f, groundLayer);
		}
	}

	private Vector3 MovementDirection
	{
		get
		{
			var moveDirection = actions.Player.Move.ReadValue<Vector2>();
			return new Vector3(moveDirection.x, 0f, moveDirection.y);
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
		rb = GetComponent<Rigidbody>();
		playerCollider = GetComponent<CapsuleCollider>();

		if (groundLayer == gameObject.layer)
		{
			Debug.LogError("Player SortingLayer must be different from Ground SourtingLayer!");
		}
	}

	private void FixedUpdate()
	{
		Movement();
	}

	private void Movement()
	{
		rb.AddRelativeForce(MovementDirection * speed);
	}

	private void Jump()
	{
		if (IsGrounded)
		{
			rb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

	private void OnCollisionStay(Collision collision)
	{
		rb.angularVelocity = new Vector3(0f, 0f, 0f);
	}
}