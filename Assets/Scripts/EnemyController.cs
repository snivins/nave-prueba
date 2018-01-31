using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {



	public GameObject follower;
	public GameObject enemy;
	public GameObject shootspawn;
	void Start()
	{

		float x_dif = transform.position.x;
		for (int i = 0; i < 10; i++) {
			x_dif += 20.0f;
			//primera linea
			Vector3 pos = new Vector3 (transform.position.x + x_dif, transform.position.y, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y + 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y - 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			pos = new Vector3 (transform.position.x - x_dif, transform.position.y, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif, transform.position.y + 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif, transform.position.y - 20.0f, transform.position.z);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			//segunda linea
			pos = new Vector3 (transform.position.x + x_dif - 10.0f, transform.position.y + 10.0f, transform.position.z - 20.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x + x_dif - 10.0f, transform.position.y - 10.0f, transform.position.z - 20.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;

			pos = new Vector3 (transform.position.x - x_dif + 10.0f, transform.position.y + 10.0f, transform.position.z - 20.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
			pos = new Vector3 (transform.position.x - x_dif + 10.0f, transform.position.y - 10.0f, transform.position.z - 20.0f);
			Instantiate (follower, pos, transform.rotation);// as GameObject;
		}    
	}

	void FixedUpdate ()
	{
		
	}


	// Use this for initialization

	// Update is called once per frame
	void Update () {
		transform.LookAt (enemy.transform);
		float distance = Vector3.Distance(enemy.transform.position, transform.position);
		if (distance > 1000.0f) {
			transform.Translate (Vector3.forward * 10 * Time.deltaTime);
		} else if (distance > 900 && distance <= 1000) {
			shootspawn.GetComponent<Ai_laserTime> ().shoot ();
		} else {

			transform.Translate (Vector3.back * 10 * Time.deltaTime);
		}
	}
}
