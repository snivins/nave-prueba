using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class vidaFlota : NetworkBehaviour {
	[SyncVar]
	public int vida = 10;
	public bool activo = true;
	private Text numNaves;
	// Use this for initialization

	public void setVida(int hp){
		vida = hp;
		numNaves = GameObject.FindGameObjectWithTag("hud9").GetComponent<Text> ();
		numNaves.text = vida.ToString();
	}
	public void hit(){
		if (!isServer)
			return;
		vida--;
		numNaves.text = vida.ToString();
		if (vida == 0) {
			activo = false;
		}
	}

}
