using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Controller : NetworkBehaviour {

    // Use this for initialization
    public Camera cam;
    private float range = 100f;
    public GameObject ControllerPointer;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ControllerPointer.GetComponent<Renderer>().material.color = new Color(255,0,0);
        Pointer(); 
          
        
        
	}


    void Pointer() {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range);
            string name = hit.transform.name;
          //  print(name);
            Option option = hit.transform.GetComponent<Option>();
        if (name == "Music" || name == "Exit")
        {
            ControllerPointer.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            
        }
  
           
            if (Input.GetButtonDown("Fire3"))
            {
                if (name == "Music")
                {
                    option.Music();
                }
                else if (name == "Exit")
                {
                    option.ExitGame();
                }
            }
        }
    
}
