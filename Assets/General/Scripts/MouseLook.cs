using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public Transform playerBody;
	
	[SerializeField]
	private float mouseSensitivity = 10f;

    private PlayerInput actions;
	private float xRotation = 0;

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
		var deltaX = deltaLook.x * mouseSensitivity * Time.deltaTime;
		var deltaY = deltaLook.y * mouseSensitivity * Time.deltaTime;

		xRotation -= deltaY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * deltaX);
	}
}
