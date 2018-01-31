using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Follower  : NetworkBehaviour  {
	public Transform pos;
	private GameObject indicador;
	private GameObject indicadorB;
	public GameObject splosion;
	private Rigidbody nave_body;
	private Rigidbody rb;
	public int hp;

	// Use this for initialization
	void Start ()  {
		nave_body = GetComponent<Rigidbody> ();
		hp = 2;
	}
	public void setInd(GameObject nave) {
		if (nave != null) {
			indicador = nave;
			rb = indicador.GetComponent<Rigidbody> ();
			//lengtj = Vector3.Distance (startMarker.position,endMarker.position); 
			//StartCoroutine ("movement");
		}
	}
	public void setIndB(GameObject nave) {
		if (nave != null) {
			indicadorB = nave;
			//lengtj = Vector3.Distance (startMarker.position,endMarker.position); 
			//StartCoroutine ("movement");
		}
	}
	// Update is called once per frame
	public void Update () {
		if (hp > 0) {
			float distancia = Vector3.Distance (indicador.transform.position, transform.position);
			float speeeeeeed = Time.deltaTime * distancia;
			transform.position = Vector3.MoveTowards (transform.position, indicador.transform.position, speeeeeeed);
			transform.rotation = indicadorB.transform.rotation;
		}
	
	}
	public void hit() {
		hp--;
		GameObject exp = Instantiate (splosion, transform.position, transform.GetChild(1).transform.rotation);
		Destroy (exp, 10f);
		if (hp == 0) {
			indicadorB.transform.parent.GetComponent<vidaFlota> ().hit ();
			indicadorB.transform.parent.GetComponent<flotaMulti> ().imDed (this.gameObject);
		}
	}




	// Use this for initialization
	/*void Start () {

	}

	// Update is called once per frame
	void Update () {
		float distCovered = (Time.time - startTime) * 1;
		float fracJourney = distCovered / lengtj;
		transform.position = Vector3.Lerp (startMarker.position,indicador.transform.position,fracJourney);
	}*/
}