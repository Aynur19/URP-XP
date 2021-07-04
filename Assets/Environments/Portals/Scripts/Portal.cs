using UnityEngine;

public class Portal : MonoBehaviour
{
	public Portal linkedPortal;
	public Camera portalView;

	private void Start()
	{
		linkedPortal.portalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
		GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = linkedPortal.portalView.targetTexture;
	}

	private void Update()
	{
		// Position
		var lookerPosition = linkedPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
		portalView.transform.localPosition = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);

		// Rotation
		var difference = transform.rotation * Quaternion.Inverse(linkedPortal.transform.rotation * Quaternion.Euler(0f, 180f, 0f));
		portalView.transform.rotation = difference * Camera.main.transform.rotation;

		// Clipping
		portalView.nearClipPlane = lookerPosition.magnitude;
	}
}
