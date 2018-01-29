using UnityEngine;
using System.Collections;
using UnityEngine.Networking;



public class player : NetworkBehaviour {

	// Use this for initialization
	public float speed = 5f;
	private CharacterController controller;
	private AudioSource sound,collisonSound;
	public AudioClip footstep,jump;
	public Camera cam;


	public AudioSource AddAudio(AudioClip clip) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip; 
		return newAudio; 
	}

	void Awake(){
		sound = AddAudio (footstep);
		collisonSound = AddAudio (jump);
	
	}
	void Start () {
		
		if (!isLocalPlayer) {
			Destroy(cam);
			Destroy (this);
			return;

		} else {

			controller = GetComponent<CharacterController> ();

		}

	}
	
	// Update is called once per frame
	void Update () {

		playerMotion();
	
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
	
	void playerMotion(){
		controller.SimpleMove (speed * cam.transform.TransformDirection (Vector3.forward * Input.GetAxis ("Vertical") +
		                                                                 Vector3.right * Input.GetAxis ("Horizontal")));
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) ||
		    Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow))soundEffect(sound);

		if (Input.GetKey (KeyCode.Space))soundEffect(sound);
	}
	void soundEffect(AudioSource s){

		if(!s.isPlaying)s.Play();
	}
	[Command]
	void CmdPlayerCollision(){
		soundEffect (collisonSound);
	}

}



