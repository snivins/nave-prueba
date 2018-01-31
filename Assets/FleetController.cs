using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour {
	public GameObject ship;
	public GameObject[,] shiplist;
	public GameObject energy_ind;
	public float energy_speed;

	private int energy;
	// Use this for initialization
	void Start () {
		energy = 0;
		float x_dif = -200f;
		shiplist = new GameObject[5, 20];
		for (int i = 0; i < 20; i++) {
			x_dif += 20.0f;
			//primera linea
			Vector3 pos = new Vector3 (transform.position.x + x_dif, transform.position.y, transform.position.z);
			shiplist[0,i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
			shiplist[0,i].GetComponent<ShipController>().setDifs(x_dif,0f);
			shiplist [0, i].transform.parent = transform;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y + 20.0f, transform.position.z);
			shiplist[1,i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
			shiplist[1,i].GetComponent<ShipController>().setDifs(x_dif,20f);
			shiplist[1,i].transform.parent = transform;
			pos = new Vector3 (transform.position.x + x_dif, transform.position.y - 20.0f, transform.position.z);
			shiplist[2,i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
			shiplist[2,i].GetComponent<ShipController>().setDifs(x_dif,20f);
			shiplist[2,i].transform.parent = transform;

			if (i != 19) {
				pos = new Vector3 (transform.position.x + x_dif + 10, transform.position.y - 10.0f, transform.position.z - 10f);
				shiplist [3, i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
				shiplist [3, i].GetComponent<ShipController> ().setDifs (x_dif, 20f);
				shiplist [3, i].transform.parent = transform;


				pos = new Vector3 (transform.position.x + x_dif + 10, transform.position.y + 10.0f, transform.position.z - 10f);
				shiplist [4, i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
				shiplist [4, i].GetComponent<ShipController> ().setDifs (x_dif, 20f);
				shiplist [4, i].transform.parent = transform;
			} else {

				pos = new Vector3 (transform.position.x, transform.position.y + 30f, transform.position.z);
				shiplist [3, i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
				shiplist [3, i].GetComponent<ShipController> ().setDifs (x_dif, 20f);
				shiplist [3, i].transform.parent = transform;


				pos = new Vector3 (transform.position.x, transform.position.y - 30f, transform.position.z);
				shiplist [4, i] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
				shiplist [4, i].GetComponent<ShipController> ().setDifs (x_dif, 20f);
				shiplist [4, i].transform.parent = transform;
			}

		}
		/*
		Vector3 pos = new Vector3 (Mathf.Abs(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad))* 10f,Mathf.Abs(Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad))* 10f, transform.position.z);
		shiplist[0,0] = (GameObject)Instantiate (ship, pos, transform.rotation);// as GameObject;
		shiplist[0,0].GetComponent<ShipController>().setDifs(10f,10f);
		*/
		foreach (GameObject sp in shiplist ) {
			sp.GetComponent<ShipController>().disparar();
		}
		//shiplist[0,5].GetComponent<ShipController>().disparar();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//transform.Rotate (Vector3.forward * Time.deltaTime * 20);


		/*
		foreach (GameObject sp in shiplist ) {

			Rigidbody spRb = sp.GetComponent<Rigidbody> ();

			float dif_x = sp.GetComponent<ShipController>().getX();
			float dif_y = sp.GetComponent<ShipController>().getY();
			Vector3 pos = new Vector3 (Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad)* dif_x ,Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)*dif_y, transform.position.z);

			//Vector3 pos = new Vector3 (Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad)* dif_x ,Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad)*10f, transform.position.z);
			//spRb.MovePosition(pos);
			sp.transform.position = pos;
			sp.transform.rotation = transform.rotation;
		}*/
	}

	void Update() {
		transform.Translate (Vector3.forward * Time.deltaTime * energy_speed);
		if (Input.GetButtonDown ("Fire1")) {
			foreach (GameObject sp in shiplist ) {
				sp.GetComponent<ShipController>().disparar();
			}
		}

		if (Input.GetButton ("speederino")) {
			float sped = Input.GetAxis ("speederino");
			energy_speed += sped / 2f;

		}
		if (Input.GetKeyDown("f")) {
			StartCoroutine("stop");
		}
		LineRenderer indicador =  energy_ind.GetComponent<LineRenderer>();
		indicador.SetPosition (1, new Vector3 (0f, energy_speed / 20f, 0f));
	}

	IEnumerator stop() {
		if (energy_speed > 0) {
			while (energy_speed > 0) {
				if (energy_speed > 1f)
					energy_speed -= 0.5f;
				else
					energy_speed = 0f;
				yield return new WaitForSeconds (.01f);
			}
		} else {
			while (energy_speed < 0) {
				if (energy_speed < 1f)
					energy_speed += 0.5f;
				else
					energy_speed = 0f;
				yield return new WaitForSeconds (.01f);
			}
		}
		
	}
}
