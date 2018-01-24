using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class player : NetworkBehaviour {

	// Use this for initialization
	public float speed = 5f;
	private CharacterController controller;
	private AudioSource sound, footstep;
	[SyncVar] public uint my_id;

	public Camera cam;

	void Start () {
		controller = GetComponent<CharacterController>();
		sound = GetComponent<AudioSource> ();
		footstep = GetComponent<AudioSource> ();

		if (!isLocalPlayer)
			//this stands for the component ==> script
	         Destroy (cam.gameObject);
		else {
			cam.transform.position=transform.position;
			cam.transform.rotation=transform.rotation;
			cam.transform.LookAt(transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isLocalPlayer) {

			playerMotion();


		}
	}

	void OnColllisionEnter(Collision col){
		if (col.gameObject.tag == "Player") {
			Destroy(col.gameObject);
			Debug.Log ("Player hit");
			if(isServer){
				soundEffect(sound);
				NetworkIdentity id=col.gameObject.GetComponent<NetworkIdentity>();
				if(id.netId.Value==my_id){
					Debug.Log ("Collided player with id "+id);
					//vibrate microcontroller with this id
				}

			}
		
		
		}

	}

	void playerMotion(){
		controller.SimpleMove (speed * cam.transform.TransformDirection (Vector3.forward * Input.GetAxis ("Vertical") +
		                                                                 Vector3.right * Input.GetAxis ("Horizontal")));
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) ||
		    Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.RightArrow))soundEffect(footstep);

		if (Input.GetKey (KeyCode.Space))soundEffect(sound);

	
	}

	//cmd a
	[Command]
	void CmdsoundEffect(){
		//if(!sound.isPlaying)sound.Play();
	}
	void soundEffect(AudioSource s){
		if(!s.isPlaying)s.Play();
		}
	[ClientRpc]
	void RpcsoundEffect(){
		//if(!sound.isPlaying)sound.Play();
	}

	void motion(){
		if(Input.GetKey(KeyCode.LeftArrow))transform.Translate(Vector2.left *speed * Time.deltaTime);
		else if(Input.GetKey(KeyCode.RightArrow))transform.Translate(Vector2.right *speed *Time.deltaTime);
		
		if(Input.GetKey(KeyCode.UpArrow))transform.Translate(Vector2.up*speed*Time.deltaTime);
		else if(Input.GetKey(KeyCode.DownArrow))transform.Translate(Vector2.down*speed*Time.deltaTime);
	}

	void hardcorpSound(){
		if (Input.GetKey (KeyCode.Space)){
			if(isClient){
				CmdsoundEffect();
				//soundEffect();
			}
			if(isServer)RpcsoundEffect();
		}

	}
}



