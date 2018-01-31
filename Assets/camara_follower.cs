using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_follower : MonoBehaviour {
	
	private GameObject indicadorA;
	private GameObject indicadorB;
	private Rigidbody nave_body;


	// Use this for initialization
	void Start ()  {
		nave_body = GetComponent<Rigidbody> ();
	}/*
	public void setPos(Transform poss) {
		pos = poss;
	}*/
	public void setInd(GameObject nave, int letra) {
		if (nave != null) {
			if (letra == 0)
				indicadorA = nave;
			else
				indicadorB = nave;
		}
	}
	// Update is called once per frame
	void Update () {
		Vector3 midPoint = (indicadorA.transform.position + indicadorB.transform.position) * 0.5f;
		float distancia = Vector3.Distance (midPoint, transform.position);

		float speeeeeeed = Time.deltaTime * distancia;
		transform.position = Vector3.MoveTowards (transform.position, midPoint, speeeeeeed);
		transform.rotation = indicadorA.transform.rotation;
	}
}
