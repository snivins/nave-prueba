using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class fleto : NetworkBehaviour {
	public GameObject follower;
	public List<GameObject> followers;
	public List<Vector3> posiciones;
	// Use this for initialization
	void Start () {
		posiciones = new List<Vector3>(); 
		followers = new List<GameObject>(); 
		float columnas = 5;
		float filas = 2;
		Vector3 pos = Vector3.zero;
		//for (int i = 0; i < 9; i++) {//formaciones

			for (int j = 0; j < columnas; j++) {
				for (int k = 0; k < filas; k++) {
				float izq = j * 15f;
				float up = k * 15f;
				Vector3 dif = new Vector3 (izq, up, 0f);// referencia d la posicion d la flota con cada nave
				pos = transform.TransformPoint (dif);//posicion ajustada a la posicion d la flota

				//Debug.Log ("pos: "+ pos + ", dif: " + dif);
				GameObject nave = Instantiate (follower, pos, transform.rotation);
				nave.GetComponent<follws> ().setDif (dif);
				nave.GetComponent<follws> ().setFleet (transform);
				followers.Add (nave);
				posiciones.Add (pos);
				}
			}
		
		//}
		//CmdReee();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.TransformPoint(new Vector3(-1f,1f,0f) * 4f));
	}
	/*[Command]
	void CmdReee() {
		float columnas = 5;
		float filas = 2;
		Vector3 pos = Vector3.zero;
		for (int j = 0; j < columnas; j++) {
			for (int k = 0; k < filas; k++) {
				float izq = j * 15f;
				float up = k * 15f;
				Vector3 dif = new Vector3 (izq, up, 0f);
				pos = transform.TransformPoint (dif);
				Debug.Log ("pos: "+ pos + ", dif: " + dif);
				GameObject nave = Instantiate (follower, pos, transform.rotation);
				NetworkServer.Spawn (nave); 
				nave.GetComponent<follws> ().setDif (dif);
				nave.GetComponent<follws> ().setFleet (transform);
				followers.Add (nave);
				posiciones.Add (pos);
			}
		}
	}*/
}
