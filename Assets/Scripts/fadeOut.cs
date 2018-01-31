using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOut : MonoBehaviour {

	private float endFire;
	public float fadeTimeout;
	// Use this for initialization
	void Start () {
		endFire = Time.time + fadeTimeout;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > endFire) {
			Destroy(gameObject);
		}        
	}
}
