using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laserrr : MonoBehaviour {

	private Rigidbody rb;
	public int speed;
	public int range;
	private Vector3 p_inicial;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.velocity = Vector3.forward * speed;
		p_inicial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (p_inicial, transform.position) > range)	Destroy (this.gameObject);

	}
}
