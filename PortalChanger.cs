using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PortalChanger : MonoBehaviour {

	public GameObject PortalObject;
	public GameObject LocalPlayer;
    [NotNull] public GameObject PortalVfx;

	public Transform[] Recievers;
	private Transform _currentReciever;

	public Material[] OtherMats;
	private Material _currentMat;

	public Camera StartCamera;

	private int _currentIdx;

	// Use this for initialization
	void Start () {
		_currentMat = OtherMats [0];// initialize mats
		_currentIdx = 0;
		transform.GetChild (0).GetComponent<Renderer> ().material.color = Color.red; // set standard shader to red
	}
	
	// Update is called once per frame
	void Update () {
	    if (LocalPlayer == null) return;        // make sure localplayer is set
	    if (Input.GetKeyUp(KeyCode.E) && Vector3.Distance(LocalPlayer.transform.position, transform.position) < 3.5f) // check for action key and distance
	    {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // get ray from mouse pos
	        RaycastHit hit;
	        if (Physics.Raycast(ray, out hit, 4f)) // raycats 4f in front of us
	        {
	            if (hit.collider.gameObject == gameObject) // sanity check
	            {
                    GetComponent<AudioSource>().Play();
	                StartCoroutine(ChangePortal()); // start changing portal
	            }
	        }
	    }

	    if (PortalObject != null && PortalObject.transform.GetChild(0).GetComponent<Renderer> ().material != _currentMat) // if it were up to me i would use the null propogating operator '?', but unity doesn't support it
	        PortalObject.transform.GetChild(0).GetComponent<Renderer> ().material = _currentMat;    // change mat of render plane
	    if (_currentReciever != null && PortalObject.transform.GetChild (1).GetComponent<PortalTeleporter> ().reciever != _currentReciever)
	        PortalObject.transform.GetChild (1).GetComponent<PortalTeleporter> ().reciever = _currentReciever;
	}

	IEnumerator ChangePortal()
	{
		for (int i = PortalVfx.transform.childCount - 1; i >= 0; i--) { // loop through particles
			PortalVfx.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().Clear (); // reset particle system one-shots
			PortalVfx.transform.GetChild (i).gameObject.GetComponent<ParticleSystem> ().Play (); // replay the particles
		}
		// suspend execution for 1.5 seconds
		yield return new WaitForSeconds(1.5f);
		if (_currentIdx + 1 > OtherMats.Length - 1) {  // change recievers and material of spawn room portal
			_currentIdx = 0;
			_currentMat = OtherMats [0];
			_currentReciever = Recievers [0];
		} else {
			_currentIdx++;
			_currentMat = OtherMats [_currentIdx];
			_currentReciever = Recievers [_currentIdx];
		}
		Color prevColor = transform.GetChild (0).GetComponent<Renderer> ().material.color;
		switch (_currentIdx) // set the reciever portal depending on index
		{
		    case 0:
		        StartCamera.GetComponent<PortalCamera> ().otherPortal = GameObject.Find ("Portal_B").transform;
		        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
                break;
		    case 1:
		        StartCamera.GetComponent<PortalCamera>().otherPortal = GameObject.Find("Portal_C").transform;
		        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.blue;
                break;
			case 2:
				StartCamera.GetComponent<PortalCamera>().otherPortal = GameObject.Find("Portal_D").transform;
				transform.GetChild(0).GetComponent<Renderer>().material.color = Color.green;
				break;
        }
	}
}
