using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class laserin : NetworkBehaviour {
	private LineRenderer laser;
	private float timerino;
	public GameObject explosion;
	public List<GameObject> golpeados;
	private float minW;
	private float maxW;
	private bool disparando;
	// Use this for initialization
	void Start () {
		disparando = false;
		laser = GetComponent<LineRenderer> ();	
		timerino = 0;
		laser.startWidth = 1f;
		laser.endWidth = 1f;
		laser.SetPosition (0, transform.position);
		laser.SetPosition (1, transform.position);
		//lala = (AudioClip)Resources.Load ("Audio/25");
	}
	void OnEnable() {
		timerino = 0;
		minW = 0.5f;
		maxW = 1f;
		Invoke ("setTru", 10f);
	}
	void Update () {
		if (disparando) {
			timerino += Time.deltaTime;
			laser.SetPosition (0, transform.position);
			if (timerino > 2.6f) {
				minW *= 0.75f;
				maxW *= 0.75f;
			}
			laser.startWidth = Random.Range (minW, maxW);
			laser.endWidth = Random.Range (minW, maxW);
			//float dist = Vector3.Distance (laser.GetPosition (1), transform.position);
			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 5000f)) {
				if (hit.rigidbody) {
					if (!golpeados.Contains (hit.rigidbody.gameObject)) {
						hit.rigidbody.gameObject.GetComponent<Follower> ().hit ();
						golpeados.Add (hit.rigidbody.gameObject);
						//GameObject expls = Instantiate (explosion, hit.rigidbody.gameObject.transform.position, transform.rotation);
						//Destroy (expls, 5f);
					}
				} 
			} 
			laser.SetPosition (1, ray.GetPoint (4500f));
		}
			//Debug.Log (transform.forward);// como distancia es fija ahora no cambia el ancho
		//Destroy (this.gameObject, 5f);
	}
	void OnDisable(){
		disparando = false;
		laser.SetPosition (0, transform.position);
		laser.SetPosition (1, transform.position);
	}
	void setTru(){
		disparando = true;
	}
}
