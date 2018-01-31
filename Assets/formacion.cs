using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class formacion : MonoBehaviour {
	private int id;
	private Vector3 old_pos;
	// Use this for initialization
	void Start () {
		old_pos = transform.position;
	}

	public void setId(int i){
		id = i;
	}
	// Update is called once per frame
	void Update () {
		if (transform.position != old_pos) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<prueba2>().setPos(id, transform.position);
			old_pos = transform.position;
		}
	}

}
