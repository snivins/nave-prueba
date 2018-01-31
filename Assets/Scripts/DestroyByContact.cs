using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" || other.tag == "laser") {
			return;
		}
		Destroy (other.gameObject);
		Instantiate (explosion, other.transform.position,other.transform.rotation);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
