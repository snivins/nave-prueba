using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
	public GameObject laser;
	private float dif_x;
	private float dif_y;
	// Use this for initialization
	void Start () {
		
	}

	public void setDifs(float x, float y) {
		dif_x = x;
		dif_y = y;
	}	

	public float getX() {
		return dif_x;
	}	

	public float getY() {
		return dif_y;
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	public void disparar() {
		Vector3 pos = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 5f);
		Instantiate (laser, pos, transform.rotation);// as GameObject;
	}
}
