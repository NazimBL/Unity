using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;



public class movement : NetworkBehaviour {
	

	public float speed = 5f;
	private CharacterController controller;
	private AudioSource sound,collisonSound;
	public AudioClip footstep,jump;
	public Camera cam;
	private float offsety;
	// Use this for initialization
	void Awake(){
		sound = AddAudio (footstep);
		collisonSound = AddAudio (jump);

	}

	void Start () {
		if (!isLocalPlayer) {
            cam.GetComponent<AudioListener>().enabled = false;
            cam.enabled = false;
			Destroy (this);
			return;
		}
			
		else {
			controller = GetComponent<CharacterController> ();
		} 
			
		 
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(isLocalPlayer){
			
		    motion ();
		}
	}

	void motion(){
		
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");
		if(v != 0 || h != 0){
			controller.SimpleMove(speed * cam.transform.TransformDirection(Vector3.forward * v +
				Vector3.right * h));
			soundEffect (sound);
		}
	}

	public AudioSource AddAudio(AudioClip clip) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		return newAudio; 
	}

	void soundEffect(AudioSource s){

		if(!s.isPlaying)s.Play();
	}

	[Command]
	void CmdPlayerCollision(){
		soundEffect (collisonSound);
	}



	void OnControllerColliderHit(ControllerColliderHit col){

		if (col.gameObject.tag == "Player") {
			//Destroy(col.gameObject);
			Debug.Log ("Player hit");

			CmdPlayerCollision();
			NetworkIdentity id=col.gameObject.GetComponent<NetworkIdentity>();
			Debug.Log ("Collided player with id "+id.netId.Value);
			//vibrate microcontroller with this id
		}

	}

}


	

