using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class prueba2 : NetworkBehaviour {
	private Transform punto;
	public GameObject markers;
	public GameObject nave_f;
	public GameObject camara_f;
	public GameObject lazor;
	private Rigidbody rb;
	public List<Transform> posiciones;
	//[SyncVar(hook = "CmdShot")]
	//[SyncList<GameObject>()]
	public List<GameObject> lasers;
	public List<GameObject> followers;
	public int size;
	public int filas;

	public Text velocidad;
	public Text energia;
	public Text num_naves;
	public Slider ene_sli;
	public Text b_giro_texto;
	public Image obj_ind_hud;
	public Text distancia_obj;
	public GameObject vector;
	public Transform camara;
	public Transform vectorB;

	private GameObject lock_on;
	private LineRenderer indicador;
	private Vector3 speed_f;

	private bool girando;
	private bool disparando;
	private bool recargando;
	private bool apuntando = false;
	private bool flota_activa;
	[SyncVar] 
	public bool enablerino;
	public bool sentido = true;


	public float energy_speed;

	private float timerino;
	private float timerino_beta;
	[SyncVar]
	public float energia_multiplier;

	private int estado_trail;
	public GameObject objetivo;

	public AudioClip preCarga;
	public AudioClip disparo;



	public GameObject formacion_modifier;
	public GameObject marker;
	public GameObject cam_formacion;
	private bool moviendo_cam = false;

	void Start () {
		estado_trail = 0;
		rb = GetComponent<Rigidbody> ();
		punto = transform;
		posiciones = new List<Transform>(); 
		followers = new List<GameObject>(); 

		velocidad = GameObject.FindGameObjectWithTag("hud1").GetComponent<Text> ();
		energia = GameObject.FindGameObjectWithTag("hud2").GetComponent<Text> ();
		ene_sli = GameObject.FindGameObjectWithTag("hud3").GetComponent<Slider> ();
		b_giro_texto = GameObject.FindGameObjectWithTag("hud4").GetComponent<Text> ();
		obj_ind_hud = GameObject.FindGameObjectWithTag("hud5").GetComponent<Image> ();
		distancia_obj = GameObject.FindGameObjectWithTag("hud6").GetComponent<Text> ();
		vector = GameObject.FindGameObjectWithTag("hud7");
		vectorB = GameObject.FindGameObjectWithTag("hud8").transform;
		num_naves = GameObject.FindGameObjectWithTag("hud9").GetComponent<Text> ();

		camara_f = (GameObject)Instantiate (camara_f, transform.position, transform.rotation);
		camara = GameObject.FindGameObjectWithTag("MainCameraPointer").transform;


		formacion_modifier = GameObject.FindGameObjectWithTag("formacion");
		cam_formacion = GameObject.FindGameObjectWithTag("cam_formacion");



		enablerino = false;






		indicador =  GetComponent<LineRenderer>();
		size = 200;
		filas = 5;
		for (int i = 0; i < size; i++) {
			//primera linea
			Vector3 pos = Vector3.zero;
			/*
			if (i < 10) {
				pos = new Vector3 (transform.position.x + (10f * i), transform.position.y, transform.position.z + 3f);
			} else if (i >= 10 && i < 20) {
				pos = new Vector3 (transform.position.x + (10f * i) - 200f, transform.position.y, transform.position.z + 3f);
			} else if (i >= 20 && i < 40) {
				pos = new Vector3 (transform.position.x + (10f * i) - 300f, transform.position.y + 10f, transform.position.z);
			} else {
				pos = new Vector3 (transform.position.x + (10f * i) - 500f, transform.position.y - 10f, transform.position.z);
			}*/
			//10f entre naves
			float difY = 20;/*
			if (i >= (size / filas) * 2) {
				difY = -10f;
			} else if ( i >= (size / filas) ){
				difY = 0f;
			} else  difY = 10f; 
*/
			float mlk = i;
			float multiplier = 0;
			while(mlk >= 40f) {
				mlk -= 40f;
				multiplier++;
			}
			difY += -1 * multiplier * 10f;
			float difX = 0;
			int rel_pos = i + 1;
			while (rel_pos > (size / filas)) {
				rel_pos -= (size / filas);
			}
			difX = (((size / filas) / 2f) * -10f) + rel_pos * 10f;
			//Debug.Log ("i=>"+i+": "+difX + ", " + difY+ ", " + rel_pos);
			pos = new Vector3 (transform.position.x + difX, transform.position.y + difY, transform.position.z);


			GameObject mimi = (GameObject)Instantiate (markers, pos, transform.rotation);
			GameObject mimiEx = (GameObject)Instantiate (marker, new Vector3 (pos.x, pos.y, formacion_modifier.transform.position.z), transform.rotation, formacion_modifier.transform);
			mimiEx.GetComponent<formacion> ().setId (i);
			//GameObject shps = (GameObject)Instantiate (ship, pos, transform.rotation);
			posiciones.Add(mimi.transform);// as GameObject;
			GameObject nav = (GameObject)Instantiate (nave_f, pos, transform.rotation);
			//nav.GetComponent<Follower> ().setPos(mimi.transform);

			nav.GetComponent<Follower> ().setInd(mimi);
			nav.GetComponent<Follower> ().setIndB(objetivo);
			followers.Add(nav);
			mimi.transform.parent = transform;






			//GameObject laser = Instantiate (lazor, nav.transform.position + nav.transform.forward*7f, nav.transform.rotation);
			//CmdReee (laser);
			//lasers.Add (laser);
			//laser.transform.parent = nav.transform;
			//laser.SetActive (enablerino);


		}
		ene_sli.minValue = -10f;
		ene_sli.maxValue = 10f;
		girando = false;
		disparando = false;
		recargando = false;
		flota_activa = true;
		camara_f.GetComponent<camara_follower> ().setInd (this.gameObject, 0);
		radar ();
		GetComponents<NetworkTransformChild>()[1].target = followers[1].transform;
	}
	
	// Update is called once per frame
	void Update () {
		

		energia_multiplier = Mathf.Abs (energy_speed);
		if (!isLocalPlayer) {
				return;
		} 

		//Actualizar camara para el multiplayer no se vuelva loco

		camara.transform.parent = camara_f.transform;
		camara.position = camara_f.transform.position;
		camara.rotation = camara_f.transform.rotation;


		//Movimiento
		if (!girando && !disparando && !apuntando) { //Velocidad y giro leve
			if (Input.GetButton ("speed")) {
				float sped = Input.GetAxis ("speed");
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
			StartCoroutine ("stop_energy");
			//gameObject.GetComponent<NetworkView>().RPC ("shooterino",1);
			enablerino = true;
			CmdShot ();		
			AudioSource audio = GetComponent<AudioSource> ();
			audio.PlayOneShot (preCarga);
			Invoke("disparoSound",10f);
			//shooterino();
			disparando = true;

			timerino_beta = 0f;
		}

		//apuntar
		if (Input.GetKeyDown ("h")) {
			if (apuntando) {
				StartCoroutine ("centrar");
			} else {
				apuntando = true;
				StartCoroutine ("stop_energy");
			}
		}
		if (!moviendo_cam && Input.GetKeyDown ("c")) {
			moviendo_cam = true;
			StartCoroutine ("centrar_cam_formacion");
		}

		float velocity = ((followers [0].transform.position - speed_f).magnitude) / Time.deltaTime;
		speed_f = followers [0].transform.position;
		velocidad.text = "Vel: " + velocity + " km/s";
		energia.text = Mathf.Round (energy_speed * 10f) + " %";
		velocidad.text = "Vel: " + Mathf.Round (rb.velocity.magnitude) + " km/s";
		string lala = "Total active ships: " + followers.Count.ToString ();
		num_naves.text = lala + "/" + size;


		if (Mathf.Abs (Mathf.Round (energy_speed)) != estado_trail)			trail_upd ();



		if (Input.GetKeyDown ("g")) 	b_giro ();






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
					if (!followers.Contains (hit.rigidbody.gameObject)) {
						lock_on = hit.rigidbody.gameObject;
						obj_ind_hud.GetComponent<obj_indic> ().setObj (lock_on.transform);
					}
				} else {
					Debug.Log (followers.Count);
				}
			} /*else {
				lock_on = null;
				obj_ind_hud.GetComponent<obj_indic> ().setNull ();
				distancia_obj.text = "";
			}*/
		}
		if (lock_on != null)
			distancia_obj.text = Vector3.Distance (lock_on.transform.position, transform.position) + " kms";
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
		//Debug.Log (followers.Count);
		vectorB.rotation = transform.rotation;
		//Debug.Log (followers[0].GetComponent<Rigidbody> ().velocity.magnitude);
		if (disparando) {
			timerino_beta += Time.deltaTime;
			if (timerino_beta > 15f) {
				//foreach (GameObject nav in lasers) {
				//nav.transform.parent = transform.parent;
				//nav.GetComponent<Rigidbody> ().velocity = rb.velocity;
				//}
				disparando = false;
				recargando = true;
			}
		}
		if (recargando) {
			timerino_beta += Time.deltaTime;
			if (timerino_beta > 20f) {
				lasers.Clear ();
				recargando = false;
			}
		}


	}
	public void setPos(int id, Vector3 pos){
		Debug.Log (posiciones [id].localPosition + "mimimimi" + pos);
		posiciones [id].position = new Vector3(transform.position.x + pos.x, transform.position.y + pos.y , transform.position.z);
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

	}



	IEnumerator centrar_cam_formacion() {
		float dif = -600f;
		if (cam_formacion.transform.localPosition.x != 0f)
			dif = 0f;
		
		while (cam_formacion.transform.localPosition.x != dif) {
			cam_formacion.transform.localPosition = Vector3.MoveTowards (cam_formacion.transform.localPosition, new Vector3(dif, 0f, 10f), 5f);
			/*foreach (Transform t in posiciones) {
				t.rotation = Quaternion.Lerp (t.rotation, transform.rotation, 0.01f);
			}*/
			yield return new WaitForSeconds (.01f);
		}
		moviendo_cam = false;
	}
	void radar() {
		if (!isLocalPlayer)
			return;
		foreach (GameObject memem in GameObject.FindGameObjectsWithTag("Player")) {
			if (memem != gameObject) {
				
				float distancia = Vector3.Distance (memem.transform.position, transform.position);
				energia_multiplier = memem.GetComponent<prueba2> ().energia_multiplier;
				if (distancia < (3000f + 5000f * energia_multiplier)) {
					obj_ind_hud.GetComponent<obj_indic> ().setObj (memem.transform);
					lock_on = memem;
				}
				Debug.Log (energia_multiplier);
			}
		}
		Invoke ("radar", 5f);
	}
	public float getEnergy() {
		return Mathf.Abs(energy_speed);
	}
	public void b_giro() {
		if (girando) {
			girando = false;
			b_giro_texto.text = "G => Girar";
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
			num_naves.text = "You are dead faggot";
		}
		followers.Remove (other);
    //	Debug.Log (followers.Count);
	}
}
