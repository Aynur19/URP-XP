using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Teleport linkedTeleport;

	private void OnTriggerStay(Collider other)
	{
		var zPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

		if (zPosition < 0)
		{
			Teleportation(other.transform);
		}
	}

	private void Teleportation(Transform obj)
	{
		// Position
		var localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
		localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
		obj.position = linkedTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

		// Rotation
		var difference = linkedTeleport.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0f, 180f, 0f));
		obj.rotation = difference * obj.rotation;
	}
}
