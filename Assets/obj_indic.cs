using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_indic : MonoBehaviour {

	// Use this for initialization
	public Transform objetivo;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (objetivo != null) {
			Vector3 pos = Camera.main.WorldToScreenPoint (objetivo.position);
			if (pos.z > 0)
				pos.z = 13f;
			transform.position = pos;
		} else {
			//Debug.Log ("nay");
			transform.position = Vector3.zero;
		}
	}
	public void setObj(Transform t) {
		objetivo = t;
	}
	public void setNull() {
		objetivo = null;
	}
}
