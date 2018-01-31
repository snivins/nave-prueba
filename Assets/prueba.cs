using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class prueba : MonoBehaviour {
	private Transform punto;
	public GameObject markers;
	public GameObject nave_f;
	private Rigidbody rb;
	public Text velocidad;
	public Text energia;
	public Slider ene_sli;
	public List<Transform> posiciones;
	public List<GameObject> followers;
	public Text b_giro_texto;
	public Image obj_ind_hud;
	public Text distancia_obj;
	private GameObject lock_on;
	public GameObject vector;
	public Transform vectorB;
	private LineRenderer indicador;
	private Vector3 speed_f;

	private bool girando;

	public float energy_speed;

	private float timerino;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		punto = transform;
		posiciones = new List<Transform>(); 
		followers = new List<GameObject>(); 

		indicador =  GetComponent<LineRenderer>();
		for (int i = 0; i < 60; i++) {
			//primera linea
			Vector3 pos = Vector3.zero;
			if (i < 10) {
				pos = new Vector3 (transform.position.x + (10f * i), transform.position.y, transform.position.z + 3f);
			} else if (i >= 10 && i < 20) {
				pos = new Vector3 (transform.position.x + (10f * i) - 200f, transform.position.y, transform.position.z + 3f);
			} else if (i >= 20 && i < 40) {
				pos = new Vector3 (transform.position.x + (10f * i) - 300f, transform.position.y + 10f, transform.position.z);
			} else {
				pos = new Vector3 (transform.position.x + (10f * i) - 500f, transform.position.y - 10f, transform.position.z);
			}


			GameObject mimi = (GameObject)Instantiate (markers, pos, transform.rotation);
			//GameObject shps = (GameObject)Instantiate (ship, pos, transform.rotation);
			posiciones.Add(mimi.transform);// as GameObject;
			GameObject nav = (GameObject)Instantiate (nave_f, pos, transform.rotation);
			//nav.GetComponent<Follower> ().setPos(mimi.transform);

			if (i == 0) {
				GameObject cam = GameObject.FindGameObjectWithTag("MainCameraPointer");
				cam.transform.SetParent (nav.transform);
				speed_f = nav.transform.position;
			}
			nav.GetComponent<Follower> ().setInd(mimi);
			followers.Add(nav);
			mimi.transform.parent = transform;
		}
		ene_sli.minValue = -10f;
		ene_sli.maxValue = 10f;
		girando = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		for (int i = 0; i < followers.Count; i++) {
			followers [i].setPos(posiciones[i]);
		}*/
		if (!girando) {
			if (Input.GetButton ("Vertical")) {
				float sped = Input.GetAxis ("Vertical");
				if (energy_speed <= 10f && energy_speed >= -10f)
					energy_speed += sped / 2f; 
				if (energy_speed > 10f)
					energy_speed = 10f;
				else if (energy_speed < -10f)
					energy_speed = -10f;

			}
			if (Input.GetKeyDown ("f")) {
				StartCoroutine ("stop_energy");
				timerino = Time.time + 2f;
			}
			if (Input.GetKey ("f") && (timerino > Time.time && timerino < Time.time + 1f)) {
				StartCoroutine ("stop");
			}
		} else {
			float pitch = Input.GetAxis ("Vertical");
			float yaw = Input.GetAxis ("Horizontal");
			float roll = Input.GetAxis ("speederino");
			//transform.rotation = new Quaternion (transform.rotation.x + pitch, transform.rotation.y + yaw, transform.rotation.z + roll, 1f);

			transform.Rotate(new Vector3 (pitch, yaw, roll) * Time.deltaTime * 10f, Space.Self);
		}
		/*float velocity = ((followers[0].transform.position - speed_f).magnitude) / Time.deltaTime;
		speed_f = followers[0].transform.position;
		velocidad.text = "Vel: "+ velocity +" km/s";*/
		energia.text = Mathf.Round(energy_speed * 10f) + " %";
		velocidad.text = "Vel: "+ Mathf.Round(rb.velocity.magnitude) +" km/s";

		/*LineRenderer indicador =  energy_ind.GetComponent<LineRenderer>();
		indicador.SetPosition (1, new Vector3 (0f, energy_speed / 20f, 0f));*/
		ene_sli.value = energy_speed;
		ene_sli.interactable = true;
		//ene_sli.OnDrag (energy_speed = ene_sli.value);

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 1000000f)) {
				if (hit.rigidbody) { 
					lock_on = hit.rigidbody.gameObject;
					obj_ind_hud.GetComponent<obj_indic> ().setObj (lock_on.transform);
				} else {
					Debug.Log ("x");
				}
			} else {
				lock_on = null;
				obj_ind_hud.GetComponent<obj_indic> ().setNull ();
				distancia_obj.text = "";
			}
		}
		if (lock_on != null) distancia_obj.text = Vector3.Distance(lock_on.transform.position , transform.position) + " kms";
		if (rb.velocity.magnitude != 0) {
			if (rb.velocity.magnitude >= 20f) {
				vector.transform.localPosition = rb.velocity.normalized * 5f;
				indicador.SetPosition (0, vector.transform.position);
				indicador.SetPosition (1, vectorB.position);
			} else {
				vector.transform.localPosition = rb.velocity.normalized * (rb.velocity.magnitude / 4f);

				indicador.SetPosition (0, vector.transform.position);
				indicador.SetPosition (1, vectorB.position);
			}

		}
		vectorB.rotation = transform.rotation;
		Debug.Log (followers[0].GetComponent<Rigidbody> ().velocity.magnitude);
	}

	public void b_giro() {
		if (girando) {
			girando = false;
			b_giro_texto.text = "Girar";
		} else {
			girando = true;
			b_giro_texto.text = "Girando";
			StartCoroutine("stop_energy");
		}
	}
	IEnumerator stop_energy() {
		if (energy_speed > 0) {
			while (energy_speed > 0) {
				if (energy_speed > 1f)
					energy_speed -= 0.1f;
				else
					energy_speed = 0f;
				yield return new WaitForSeconds (.01f);
			}
		} else {
			while (energy_speed < 0) {
				if (energy_speed < 1f)
					energy_speed += 0.1f;
				else
					energy_speed = 0f;
				yield return new WaitForSeconds (.01f);
			}
		}
	}
	IEnumerator stop() {
		while (rb.velocity != Vector3.zero) {

			rb.AddForce (-1f * rb.velocity.x, -1f * rb.velocity.y, -1f * rb.velocity.z);
			yield return new WaitForSeconds (.01f);
		}
	}
	void FixedUpdate () {

		rb.AddForce (transform.forward * energy_speed);
	}
}
