using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	//declare GameObjects and create isShooting boolean.
	public GameObject gun;
	public GameObject amoR;
	public GameObject spawnPoint;
	public GameObject aiming;
	private CharacterController player;
	private float speed = 10f;
	private bool isShooting;
	private float amo = 1f;
	private string data = "";



	// Use this for initialization
	void Start () {


		player = GetComponent<CharacterController>();
		//set isShooting bool to default of false
		isShooting = false;
	}

	//Shoot function is IEnumerator so we can delay for seconds
	IEnumerator Shoot() {
		//set is shooting to true so we can't shoot continuosly
		isShooting = true;
		//instantiate the bullet
		GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
		//Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.rotation = spawnPoint.transform.rotation;
		bullet.transform.position = spawnPoint.transform.position;
		//add force to the bullet in the direction of the spawnPoint's forward vector
		rb.AddForce(spawnPoint.transform.forward * 300f);
		//play the gun shot sound and gun animation

			gun.GetComponent<AudioSource> ().Play ();
			gun.GetComponent<Animation> ().Play ();

		if(amo%5 == 0){
			
			amoR.GetComponent<AudioSource> ().Play ();
			amoR.GetComponent<Animation> ().Play ();
			amo = 1 ;

		}

		//destroy the bullet after 1 second
		Destroy (bullet, 1f);
		//wait for 1 second and set isShooting to false so we can shoot again
		yield return new WaitForSeconds (0.5f);
		isShooting = false;
	}

	// Update is called once per frame
	void Update () {


		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");


			
		player.SimpleMove (Camera.main.transform.forward*speed*v);
		player.SimpleMove (Camera.main.transform.right*speed*h);




		
		//declare a new RayCastHit
		RaycastHit hit;
		//draw the ray for debuging purposes (will only show up in scene view)
		Debug.DrawRay(spawnPoint.transform.position, aiming.transform.forward, Color.green);

		//cast a ray from the spawnpoint in the direction of its forward vector
		if (Physics.Raycast(spawnPoint.transform.position, aiming.transform.forward, out hit, 200)){

			//if the raycast hits any game object where its name contains "zombie" and we aren't already shooting we will start the shooting coroutine
			if (Input.GetButtonDown("Fire1")) {
				if (!isShooting) {
					StartCoroutine ("Shoot");
					amo++;
				}
			}
		}
	}
}
