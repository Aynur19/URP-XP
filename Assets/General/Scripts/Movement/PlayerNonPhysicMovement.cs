using UnityEngine;

public class PlayerNonPhysicMovement : MonoBehaviour
{
	[SerializeField]
	private float speed;

	[SerializeField]
	private float jumpForce;

	private bool isGrounded;
	private PlayerInput actions;

	private void Awake()
	{
		actions = new PlayerInput();
	}

	private void OnEnable()
	{
		actions.Enable();
	}

	private void OnDisable()
	{
		actions.Disable();
	}


}

