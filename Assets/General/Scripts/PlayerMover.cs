using UnityEngine;

public class PlayerMover : MonoBehaviour
{
	public CharacterController character;

	[SerializeField]
	private float moveSpeed;

	[SerializeField]
	private float jumpForce;

	private PlayerInput actions;

	private void Awake()
	{
		actions = new PlayerInput();
		//actions.Player.Jump.performed += x => Jump();
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
		var moveDirection = actions.Player.Move.ReadValue<Vector2>();
		Move(moveDirection);
	}

	private void Move(Vector2 direction)
	{
		var scaledMoveSpeed = moveSpeed * Time.deltaTime;

		if (character != null)
		{
			var move = (transform.right * direction.x + transform.forward * direction.y) * scaledMoveSpeed;
			character.Move(move);
		}
		else
		{
			var moveDirection = new Vector3(direction.x, 0, direction.y);
			transform.position += moveDirection * scaledMoveSpeed;
		}
	}

	//private void Jump()
	//{

	//}
}
