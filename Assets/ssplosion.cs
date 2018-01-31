using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ssplosion : MonoBehaviour {
	private float size;
	private float timerino;
	// Use this for initialization
	void Start () {
		size = 0f;
		transform.localScale = Vector3.zero;
		timerino = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerino > 1f) {
			if (size < 20) {
				transform.localScale = Vector3.one * size;
				size += 5f;
			}
		}
		timerino += Time.deltaTime;
	}
}
