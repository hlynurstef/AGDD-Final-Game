using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Sequence mySeq = DOTween.Sequence();
		mySeq.Insert(1, Camera.main.transform.DOMove(new Vector3(183, 58, 265), 2));
		mySeq.Insert(1, Camera.main.transform.DORotate(new Vector3(15, 90, 0), 2));
		mySeq.PrependInterval(2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
