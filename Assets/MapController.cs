using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {
	public GameObject mapa;
	public GameObject asteroid;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 1000 ; i++) {
			Vector3 spawPosition = new Vector3(Random.Range (-1000f,1000f), Random.Range (-1000f,1000f), Random.Range (-1000f,1000f));
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (asteroid,spawPosition, spawnRotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Map") && transform.position.x < mapa.transform.position.x) {
			transform.Translate (Vector3.right * 50f * Time.deltaTime);
		} else if (!Input.GetButton ("Map") && transform.position.x > 5000f) {
			transform.Translate (Vector3.left * 50f * Time.deltaTime);
		}
	}
}
