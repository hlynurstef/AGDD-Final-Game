using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public Transform player;

	void LateUpdate()
	{
		Vector3 newMinimapPosition = player.position;
		newMinimapPosition.y = transform.position.y;
		transform.position = newMinimapPosition;
	}
}
