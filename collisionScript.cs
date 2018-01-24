using UnityEngine;
using System.Collections;

public class collisionScript : MonoBehaviour {

	void OnControllerColliderHit(ControllerColliderHit hit){
		
		Debug.Log(hit.gameObject.name);
		if (hit.gameObject.tag == "Foe") {
			//Destroy(col.gameObject);
			Debug.Log ("Player hit");
			
		}
		
	}
}
