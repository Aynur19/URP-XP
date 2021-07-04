using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public Transform playerBody;
	
	[SerializeField]
	private float mouseSensitivity = 10f;

	[SerializeField]
	private float lowerGazeAngle = 70f;

	[SerializeField]
	private float upperGazeAngle = -70f;

	private PlayerInput actions;
	private float yRotation = 0;

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

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		var deltaLook = actions.Player.Look.ReadValue<Vector2>();
		Look(deltaLook);
	}

	private void Look(Vector2 deltaLook)
	{
		var deltaX = deltaLook.x * mouseSensitivity * Time.deltaTime;
		var deltaY = deltaLook.y * mouseSensitivity * Time.deltaTime;

		yRotation -= deltaY;
		yRotation = Mathf.Clamp(yRotation, upperGazeAngle, lowerGazeAngle);

		transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * deltaX);
	}
}
