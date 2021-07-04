using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform player;
	public Transform linkedPortal;

	private bool playerIsOverlapping = false;

	private void Update()
	{
		if (playerIsOverlapping)
		{
			Debug.Log($"playerIsOverlapping = {playerIsOverlapping}");
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			if (dotProduct < 0f)
			{
				float rotationDiff = -Quaternion.Angle(transform.rotation, linkedPortal.rotation);
				rotationDiff += 180;
				player.Rotate(Vector3.up, rotationDiff);

				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = linkedPortal.position + positionOffset;

				playerIsOverlapping = false; 
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			Debug.Log($"playerIsOverlapping = {playerIsOverlapping}");
			playerIsOverlapping = true;
		}
	}


	//public Teleport linkedTeleport;

	//private void OnTriggerStay(Collider other)
	//{
	//	var zPosition = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

	//	if (zPosition < 0)
	//	{
	//		Teleportation(other.transform);
	//	}
	//}

	//private void Teleportation(Transform obj)
	//{
	//	// Position
	//	var localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
	//	localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
	//	obj.position = linkedTeleport.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

	//	// Rotation
	//	var difference = linkedTeleport.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0f, 180f, 0f));
	//	obj.rotation = difference * obj.rotation;
	//}
}
