
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Camcontr : NetworkBehaviour {
	public float camera_speed;
	public GameObject camara;
	public GameObject camaraPitch;
	public GameObject camaraYaw;
	// Use this for initialization
	void Start () {



	}

	// Update is called once per frame
	void Update () {	
		//if (!gameObject.transform.parent.gameObject.GetComponent<NetworkView>().isMine)			return;

		if (Input.GetButton ("Fire2")) {
			Screen.lockCursor = true;
			float moveRotacionX = Input.GetAxis("Mouse X");
			float moveRotacionY = Input.GetAxis("Mouse Y");

			camaraYaw.transform.Rotate(new Vector3 (0.0f, moveRotacionX,0.0f), Time.deltaTime * camera_speed,Space.Self);
			camaraPitch.transform.Rotate(new Vector3 (-moveRotacionY,0.0f, 0.0f), Time.deltaTime * camera_speed,Space.Self);
		} else  Screen.lockCursor = false;

		if (Input.GetAxis ("Mouse ScrollWheel") != 0.0f) {
			float zoom = Input.GetAxis("Mouse ScrollWheel");
			camara.transform.Translate(new Vector3 (0.0f, 0.0f, zoom) * Time.deltaTime * 1000,Space.Self);
		}
	}
}