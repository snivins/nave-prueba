using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_followerController : MonoBehaviour {


	private Rigidbody rb;
	public Transform shotSpawn;
	private Vector3 diference;
	private Transform posicion;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		posicion = GameObject.FindGameObjectWithTag ("enemy").transform;
		diference = posicion.position - transform.position;


	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		//rb.transform.Translate ((Vector3.forward )* Time.deltaTime);
		rb.transform.position=(posicion.position + diference);
		rb.transform.rotation = posicion.rotation;
	}
	void Update () {
		/*
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);// as GameObject;
		}  */      
	}
}