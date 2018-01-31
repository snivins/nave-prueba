using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {


	private Rigidbody rb;
	public GameObject shot;
	public Transform shotSpawn;
	private float nextFire;
	public float fireRate;
	private Vector3 diference;
	private Transform posicion;
	private float difx;
	private float dify;
	private float difz;


	void Start()
	{
		rb = GetComponent<Rigidbody>();
		posicion = GameObject.FindGameObjectWithTag ("Player").transform;
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
