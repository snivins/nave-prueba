using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour {
	public GameObject asteroid;
	public GameObject player;
	public float timerino;
	public float rangoMax;
	public float rangoMin;

	private float extraTimerino;
	void SpawAsteroids() {
		Vector3 spawPosition = new Vector3(Random.Range (rangoMin,rangoMax), Random.Range (rangoMin,rangoMax), Random.Range (rangoMin,rangoMax));
		Quaternion spawnRotation = Quaternion.identity;
		Instantiate (asteroid,spawPosition, spawnRotation);
	}
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 100; i++) {
			SpawAsteroids ();
		}
	}
	// Update is called once per frame
	void Update () {
		/*
		if (Time.time > extraTimerino) {

			SpawAsteroids ();
			extraTimerino = Time.time + timerino;
		}*/

	}
}
