using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool IsUnlocked;

    private GameObject _localPlayer; // for local variables, please use _ and then camelCase 
    private Animator _anim;          // so you don't hurt my eyes and my brain cells :D
	private bool _isOpen;
	public int Type;
	public GameObject Button;

    // Alternate: Use Awake, called before Start
	void Start ()
    {
		_localPlayer = GameObject.FindWithTag("Player"); // FindWithTag is the fastest non-binary string search
        _anim = GetComponent<Animator>(); // Get the animator component attatched to the door, should always be there, but we will nullcheck later
        IsUnlocked = Type == 1 || Type == 3;
        _isOpen = false;
    }
	
    // Put heavy logic here
	void Update ()
	{
	    if (!IsUnlocked) return;
		if (_localPlayer != null)
		{
		    // ensure the findwithtag didnt screw up and our console doesnt get spammed lmao// this will also be our main loop. since we don't want anything to run if we have an error while setting up the player
			if (Type == 0 || Type == 3) {
				if (Vector3.Distance (transform.position, _localPlayer.transform.position) < 3.0f) { // This is where we'll do the distance check to open the door
				    if (_anim && _anim.GetBool("character_nearby") != true
				    ) // null check anim + make sure we havent alrewady set character_nearby to prevent pushing too much to the stack
				    {
                        GetComponent<AudioSource>().Play();
				        _anim.SetBool("character_nearby", true); // set char_nearby to true so the door opens
				    }
				} else { // else....
					if (_anim && _anim.GetBool ("character_nearby")) // this is self explanatory
                    _anim.SetBool ("character_nearby", false); // close the door
				}

				if (_anim.GetBool ("character_nearby") && GetComponent<BoxCollider> ().enabled) {  // we need to do this so we can disable the door when we're close
					GetComponent<MeshCollider> ().enabled = false;
					GetComponent<BoxCollider> ().enabled = false; 								 // disable the box collider so we can walk through
				} else if (_anim.GetBool ("character_nearby") == false && GetComponent<BoxCollider> ().enabled == false) {
					GetComponent<MeshCollider> ().enabled = true;
					GetComponent<BoxCollider> ().enabled = true; 								 // then do the opposite
				}
			}
		    if (Type != 1 || Button == null) return; // we want to reduce nesting here, so we do a null check

		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // get current mouse pos
		    RaycastHit hit;
		    if (Input.GetKeyUp(key: KeyCode.E)) // check for action key
		    {
		        if (Physics.Raycast(ray: ray, hitInfo: out hit, maxDistance: 3.0f)) // raycast in front of us
		        {
		            if (hit.collider.gameObject == Button)
		            {
                        Button.GetComponent<AudioSource>().Play();
		                _isOpen = !_isOpen; // inverse open
                    }
                    
		        }
		    }

		    if (_isOpen)
		    {
		        _anim.SetBool("character_nearby", true); // open the door
		        GetComponent<BoxCollider>().enabled = false; // disable collider because it can get buggy
		    }
		    else
		    {
		        GetComponent<BoxCollider>().enabled = true;
		        _anim.SetBool("character_nearby", false);
		    }
		}
	}
}
