using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;


public class movement : NetworkBehaviour {

	public float speed = 5f;
	private CharacterController controller;

	private Menu_Net newM;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			Destroy (this);
			return;
		}
			
		else {
			newM = GetComponent<Menu_Net> ();
			controller = GetComponent<CharacterController> ();

		} 
			
		 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isLocalPlayer){
		controller.SimpleMove(speed * Camera.main.transform.TransformDirection(Vector3.forward * Input.GetAxis("Vertical") +
			Vector3.right * Input.GetAxis("Horizontal")));
			if (Input.GetButtonDown ("Fire3")) {
				NetworkManager.singleton.StopHost ();
			}
		}
	}

	   






}
