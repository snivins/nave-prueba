using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class follws : NetworkBehaviour {
	private Transform fleet;
	private Vector3 dif;
	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}

	void Update () {

		float distancia = Vector3.Distance (fleet.TransformPoint(dif), transform.position);
		float speeeeeeed = Time.deltaTime * distancia;
		//Debug.Log(fleet.TransformPoint (dif) + " , " + fleet.position + " , " + dif + " , " + transform.position);
		transform.position = Vector3.MoveTowards (transform.position, fleet.TransformPoint(dif), speeeeeeed);
	}
	public void setFleet(Transform flota){
		fleet = flota;
	}
	public void setDif(Vector3 dif){
		this.dif = dif;
	}
}
