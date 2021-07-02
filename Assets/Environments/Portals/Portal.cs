using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public Portal linkedPortal;
	public Camera portalView;

	private void Start()
	{
		linkedPortal.portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		GetComponent<MeshRenderer>().sharedMaterial.mainTexture = linkedPortal.portalView.targetTexture;
	}

	private void Update()
	{
		// Position
		var lookerPosition = linkedPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
		portalView.transform.localPosition = -lookerPosition;

		// Rotation
		var angleDifference = transform.rotation * Quaternion.Inverse(linkedPortal.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
		portalView.transform.rotation = angleDifference * Camera.main.transform.rotation;

		// Clipping
		portalView.nearClipPlane = lookerPosition.magnitude;
	}
}
