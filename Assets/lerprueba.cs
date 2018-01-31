using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lerprueba : MonoBehaviour {

	private GameObject lala;
	private Transform startMarker;
	private Transform endMarker;
	public float speed;
	public float startTime;
	public float lengtj;

 	// Use this for initialization
	void Start () {
		lala = GameObject.FindGameObjectWithTag("Player");
		startTime = Time.time;
		startMarker = transform;
		endMarker = lala.transform;
		lengtj = Vector3.Distance (startMarker.position,endMarker.position); 
		
	}
	
	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / lengtj;
		transform.position = Vector3.Lerp (startMarker.position,endMarker.position,fracJourney);
	}
}
