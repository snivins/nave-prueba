using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * 5f;
		Destroy (this.gameObject, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
