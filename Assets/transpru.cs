﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transpru : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3(1f,1f,1f)* Time.deltaTime);
	}
}
