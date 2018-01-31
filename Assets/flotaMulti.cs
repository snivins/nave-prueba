using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//using UnityEngine.SceneManagement;

public class flotaMulti : NetworkBehaviour {
	public GameObject markers;
	public GameObject nave_f;
	public GameObject camara_f;
	public GameObject lazor;
	private Rigidbody rb;
	public List<GameObject> posiciones;
	public List<GameObject> formaciones;
	public List<GameObject> lasers;
	public List<GameObject> followers;
	public List<GameObject> hud_list;
	public int columnas;
	public int filas;

	public Transform camara;

	private LineRenderer indicadorV;

	private bool girando;
	private bool disparando;
	private bool recargando;
	private bool apuntando = false;
	private bool flota_activa;
	[SyncVar] 
	public bool enablerino;
	public bool sentido = true;


	public float energy_speed;
	private Vector3 speed_f;

	private float timerino;
	private float timerino_beta;

	private int estado_trail;

	public GameObject objetivo;

	public AudioClip preCarga;
	public AudioClip disparo;


	private Text estado;
	private Text speeeeeed;
	private Text energiaaaa;
	/*public GameObject formacion_modifier;
	public GameObject marker;
	public GameObject cam_formacion;*/
	private bool moviendo_cam = false;

	private float distancia_paneta;
	private bool atraccion = false;
	//private GameObject planeta;
	public GameObject lock_on;

	public GameObject vector;
	public Transform vectorB;

	void Start () {
		estado_trail = 0;
		rb = GetComponent<Rigidbody> ();
		posiciones = new List<GameObject>(); 
		followers = new List<GameObject>(); 
		formaciones = new List<GameObject>(); 
		for (int i = 1; i < 10; i++) {
			formaciones.Add (transform.GetChild(i).gameObject);
		}

		camara_f = (GameObject)Instantiate (camara_f, transform.position, transform.rotation); //gameobject centrado en la flota a la q la camara sigue
		camara = GameObject.FindGameObjectWithTag("MainCameraPointer").transform;
		//planeta = GameObject.FindGameObjectWithTag ("planet");
		vector = GameObject.FindGameObjectWithTag("hud7");
		vectorB = GameObject.FindGameObjectWithTag("hud6").transform;
		speeeeeed = GameObject.FindGameObjectWithTag("hud3").GetComponent<Text> ();
		energiaaaa = GameObject.FindGameObjectWithTag("hud4").GetComponent<Text> ();

		estado = GameObject.FindGameObjectWithTag("hud8").GetComponent<Text> ();

		enablerino = false;

		columnas = 5;
		filas = 2;
		Vector3 pos = Vector3.zero;
		for (int i = 0; i < 9; i++) {//formaciones
			for (int j = 0; j < columnas; j++) {
				for (int k = 0; k < filas; k++) {
					pos = new Vector3 (formaciones[i].transform.position.x + j * 15f, formaciones[i].transform.position.y + k * 15f, formaciones[i].transform.position.z);
					GameObject mimi = (GameObject)Instantiate (markers, pos, transform.rotation);
					posiciones.Add(mimi);
					GameObject nav = (GameObject)Instantiate (nave_f, pos, transform.rotation);

					nav.GetComponent<Follower> ().setInd(mimi);
					nav.GetComponent<Follower> ().setIndB(objetivo);
					followers.Add(nav);
					mimi.transform.parent = formaciones[i].transform;

				}
			}
		}

		girando = false;
		disparando = false;
		recargando = false;
		flota_activa = true;
		camara_f.GetComponent<camara_follower> ().setInd (posiciones[0], 0);
		camara_f.GetComponent<camara_follower> ().setInd (posiciones[posiciones.Count - 1], 1);
		radar ();
		GetComponent<vidaFlota> ().setVida (columnas*filas*9);
		GameObject lekl = (GameObject)Instantiate (lock_on, transform.position,transform.rotation);
		lekl.transform.SetParent (GameObject.FindGameObjectWithTag("canvas").transform);
		hud_list.Add (lekl);
		//lock_on.transform.SetParent(canvas.transform);
		//GameObject.FindGameObjectWithTag("hud5").GetComponent<Image> ().GetComponent<obj_indic> ().setObj (planeta.transform);
		indicadorV =  GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {


		if (!isLocalPlayer) {
			return;
		} 


		camara.transform.parent = camara_f.transform;
		camara.position = camara_f.transform.position;
		camara.rotation = camara_f.transform.rotation;


		//Movimiento
		if (!girando && !disparando && !apuntando) { //Velocidad y giro leve
			if (Input.GetButton ("speed")) {
				float sped = Input.GetAxis ("speed");
				if (energy_speed <= 10f && energy_speed >= -5f)
					energy_speed += sped / 2f; 
				if (energy_speed > 10f)
					energy_speed = 10f;
				else if (energy_speed < -5f)
					energy_speed = -5f;

			}
			if (Input.GetKeyDown ("f")) {
				StartCoroutine ("stop_energy");
				timerino = Time.time + 2f;
			}
			if (Input.GetKey ("f") && (timerino > Time.time && timerino < Time.time + 1f)) {
				StartCoroutine ("stop");
			}

			float pitch = Input.GetAxis ("Vertical");
			float yaw = Input.GetAxis ("Horizontal");
			float roll = Input.GetAxis ("speederino");

			transform.Rotate (new Vector3 (pitch, yaw, roll) * Time.deltaTime * 1f, Space.Self);
		} else if (!disparando && !apuntando) { //Giro
			float pitch = Input.GetAxis ("Vertical");
			float yaw = Input.GetAxis ("Horizontal");
			float roll = Input.GetAxis ("speederino");

			transform.Rotate (new Vector3 (pitch, yaw, roll) * Time.deltaTime * 10f, Space.Self);
		} else if (apuntando) {//Apuntando
			float pitch = Input.GetAxis ("Vertical");
			float yaw = Input.GetAxis ("Horizontal");
			float roll = Input.GetAxis ("speederino");

			objetivo.transform.Rotate (new Vector3 (pitch, yaw, roll) * Time.deltaTime * 10f, Space.Self);			
		}








		//disparar
		if (Input.GetButtonDown ("Jump") && !disparando && !recargando) {
			if (girando) 
				girando = false;

			estado.text = "Disparando";
			StartCoroutine ("stop_energy");
			//gameObject.GetComponent<NetworkView>().RPC ("shooterino",1);
			enablerino = true;
			CmdShot ();		
			AudioSource audio = GetComponent<AudioSource> ();
			audio.PlayOneShot (preCarga);
			Invoke("disparoSound",10f);
			//shooterino();
			disparando = true;

			timerino_beta = Time.time + 20f;
		}

		//apuntar
		if (Input.GetKeyDown ("h")) {
			//posiciones [0].transform.position = posiciones [0].transform.position + posiciones [0].transform.up * 7f;
			if (apuntando) {
				StartCoroutine ("centrar");
				estado.text = "Centrando";
			} else {
				apuntando = true;
				StartCoroutine ("stop_energy");
				estado.text = "Apuntando";
			}
		}
		if (!moviendo_cam && Input.GetKeyDown ("c")) {
			moviendo_cam = true;
			StartCoroutine ("centrar_cam_formacion");
		}

		if (Mathf.Abs (Mathf.Round (energy_speed)) != estado_trail)			trail_upd ();



		if (Input.GetKeyDown ("g")) 	b_giro ();





		if (disparando) {
			if (timerino_beta > Time.time - 5f) {
				disparando = false;
				recargando = true;
			}
		}
		if (recargando) {
			if (timerino_beta > Time.time) {
				lasers.Clear ();
				recargando = false;
				if (apuntando)
					estado.text = "Apuntando";
				else
					estado.text = "Movimiento";
			}
		}
		//distancia_paneta = Vector3.Distance (planeta.transform.position, transform.position);
		//Debug.Log (distancia_paneta);
		if (distancia_paneta < 10000)
			atraccion = true;
		else
			atraccion = false;


		if (rb.velocity.magnitude != 0) {
			if (rb.velocity.magnitude >= 20f) {
				vector.transform.localPosition = rb.velocity.normalized * 5f;
				indicadorV.SetPosition (0, vector.transform.position);
				indicadorV.SetPosition (1, vectorB.position);
			} else {
				vector.transform.localPosition = rb.velocity.normalized * (rb.velocity.magnitude / 4f);

				indicadorV.SetPosition (0, vector.transform.position);
				indicadorV.SetPosition (1, vectorB.position);
			}

		}
		//Debug.Log (followers.Count);
		vectorB.rotation = transform.rotation;



		float velocity = ((followers [0].transform.position - speed_f).magnitude) / Time.deltaTime;
		speed_f = followers [0].transform.position;
		energiaaaa.text = " Energy: "+ energy_speed *10f + " %";
		speeeeeed.text = "Vel: " + Mathf.RoundToInt( velocity) + " km/s";

	}
	public void setPos(int id, Vector3 pos){
		Debug.Log (posiciones [id].transform.localPosition + "mimimimi" + pos);
		posiciones [id].transform.position = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y , transform.position.z);
	}
	void disparoSound() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.PlayOneShot (disparo);
	}

	void shooterino(){
		foreach (GameObject nave in lasers) {
			nave.SetActive (true);
		}
		enablerino = false;
	}

	[Command]
	void CmdReee(GameObject laser) {
		NetworkServer.Spawn (laser); 
	}
	[Command]
	void CmdShot() {
		/*foreach (GameObject nave in lasers) {
			nave.SetActive (enablerino);
		}*/
		enablerino = false;

		foreach (GameObject nave in followers) {
			GameObject laser = Instantiate (lazor, nave.transform.position + nave.transform.forward*7f, nave.transform.rotation);
			NetworkServer.Spawn (laser);
			lasers.Add (laser);
			laser.transform.parent = nave.transform;
			Destroy (laser, 20f);
		}
	}
	IEnumerator centrar() {
		while (transform.rotation != objetivo.transform.rotation) {
			objetivo.transform.rotation = Quaternion.Lerp (objetivo.transform.rotation, transform.rotation, 0.01f);
			/*foreach (Transform t in posiciones) {
				t.rotation = Quaternion.Lerp (t.rotation, transform.rotation, 0.01f);
			}*/
			yield return new WaitForSeconds (.01f);
		}
		apuntando = false;
		estado.text = "Movimiento";

	}



	void radar() {
		if (!isLocalPlayer)
			return;
		while (GameObject.FindGameObjectsWithTag("Player").Length >= hud_list.Count) {

			GameObject lekl = (GameObject)Instantiate (lock_on, transform.position,transform.rotation);
			lekl.transform.SetParent (GameObject.FindGameObjectWithTag("canvas").transform);
			hud_list.Add (lekl);
		}
		int i = 1;
		foreach (GameObject memem in GameObject.FindGameObjectsWithTag("Player")) {
			if (memem != gameObject) {
				GameObject.FindGameObjectsWithTag ("hud5") [i].GetComponent<Image> ().GetComponent<obj_indic> ().setObj (memem.transform);
				GameObject.FindGameObjectsWithTag ("hud5") [i].transform.GetChild (0).GetComponent<Text> ().text = Vector3.Distance (memem.transform.position, transform.position) + " KM";
				i++;
			}
		}

		//GameObject.FindGameObjectsWithTag ("hud5") [0].transform.GetChild (0).GetComponent<Text> ().text = Vector3.Distance (planeta.transform.position, transform.position) + " KM";
		Invoke ("radar", 5f);
	}
	public float getEnergy() {
		return Mathf.Abs(energy_speed);
	}
	public void b_giro() {
		if (girando) {
			girando = false;
			estado.text = "Movimiento";
		} else {
			girando = true;
			estado.text = "Girando";
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
			//Debug.Log (rb.velocity.x > 20f || rb.velocity.y > 20f || rb.velocity.z > 20f );

			//rb.AddForce (-1f * rb.velocity.x, -1f * rb.velocity.y, -1f * rb.velocity.z);
			if (rb.velocity.x > 10f || rb.velocity.y > 10f || rb.velocity.z > 10f)
				rb.AddForce (-10f * rb.velocity.normalized);
			else if (rb.velocity.magnitude >= 2f)
				rb.AddForce (-1f * rb.velocity.normalized);
			else
				rb.velocity = Vector3.zero;
			yield return new WaitForSeconds (.01f);
		}
	}
	void FixedUpdate () {

		rb.AddForce (transform.forward * energy_speed);
		//if (atraccion)		rb.AddForce (planeta.transform.position.normalized * 5f);
	}
	void trail_upd () {
		float life = Mathf.Abs (energy_speed) / 2f;
		bool cambio = false;
		if ((energy_speed > 0 && !sentido) || (energy_speed < 0 && sentido)) {
			cambio = true;
			if (sentido)
				sentido = false;
			else
				sentido = true;
		}
		//Debug.Log ((energy_speed > 0) +"&&"+ !sentido);
		foreach (GameObject nave in followers) {
			if (life > 0) {
				nave.GetComponentsInChildren<ParticleSystem> () [0].startLifetime = life + 5f;
			} else {
				nave.GetComponentsInChildren<ParticleSystem> () [0].startLifetime = 0.5f;
			}
			if (cambio) {
				nave.transform.Find ("trail").transform.Rotate(new Vector3(180f,0f,0f));
			}
		}
		estado_trail = (int)Mathf.Abs (Mathf.Round (energy_speed));
	}
	public void imDed(GameObject other){
		if (followers.Count == 1){
			flota_activa = false;
		}
		followers.Remove (other);
		//	Debug.Log (followers.Count);
	}
}
