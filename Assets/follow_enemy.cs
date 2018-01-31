using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

	public Transform target;

	void Update ()
	{
		Vector3 wantedPos = Camera.main.WorldToScreenPoint (target.position);
		transform.position = wantedPos;
	}
}
