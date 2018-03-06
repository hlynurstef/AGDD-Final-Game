using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DimensionTrigger : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") == true) {
			Sequence mySeq = DOTween.Sequence();
			mySeq.Insert(0, Camera.main.transform.DOMove(new Vector3(200, 18, 340), 2));
			mySeq.Insert(0, Camera.main.transform.DORotate(new Vector3(10, 180, 0), 2));
			mySeq.PrependInterval(1);

			Camera.main.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
			player.GetComponent<BasicBehaviour>().inputEnabled = false;
		}
	}
}
