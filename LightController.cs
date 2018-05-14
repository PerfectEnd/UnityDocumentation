using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour { // oh god my eyes

	public GameObject LocalPlayer;
	public int Id;
	public Material Glass;
	public Material White;
	public Material Base;
	public Material Lattice;
	public int Type;
    public GameObject Door;

	private PlayerController _localent;

	private MeshRenderer _rend;

	private Material[] _baseMat;
	private Material[] _finishMat;
	private Material[] _ceilingMat;

	// Use this for initialization
	void Start () {
		_localent = LocalPlayer.GetComponent<PlayerController> ();
		_rend = GetComponent<MeshRenderer> ();

		_finishMat = new Material[2];

        switch (Type) // setting up all materials here
        {
            case 0:
                _baseMat = new Material[2];
                _baseMat[0] = Base;
                _baseMat[1] = Glass;
                break;
            case 1:
                _baseMat = new Material[3];
                _ceilingMat = new Material[3];
                _baseMat[0] = Base;
                _baseMat[1] = Lattice;
                _baseMat[2] = Glass;
                _ceilingMat[0] = Base;
                _ceilingMat[1] = Lattice;
                _ceilingMat[2] = White;
                break;
            default:
                break;
        }
        //finalize materials
        _finishMat [0] = Base;
		_finishMat [1] = White;
		_rend.materials = _baseMat;
    }

    // Update is called once per frame
    void Update ()
    { // lets just ignore this never happened
        switch (Type)
        { // here we set all the materials
            case 0:
                if (Id == 3 && _localent.ColorID >= 0 && _rend.materials [1] != White)
                    _rend.materials = _finishMat;
			
                if (Id == 1 && _localent.ColorID >= 1 && _rend.materials [1] != White)
                    _rend.materials = _finishMat;
		
                if (Id == 2 && _localent.ColorID >= 2 && _rend.materials [1] != White)
                    _rend.materials = _finishMat;
                break;
            case 1:
                if (Id == 3 && _localent.ColorID >= 0 && _rend.materials[2] != White)
                    _rend.materials = _ceilingMat;

                if (Id == 1 && _localent.ColorID >= 1 && _rend.materials[2] != White)
                    _rend.materials = _ceilingMat;

                if (Id == 2 && _localent.ColorID >= 2 && _rend.materials[2] != White)
                    _rend.materials = _ceilingMat;

                if (Id == 3 && _localent.ColorID >= 0)
                    transform.GetChild(0).GetComponent<Light>().enabled = true;

                if (Id == 1 && _localent.ColorID >= 1)
                    transform.GetChild(0).GetComponent<Light>().enabled = true;

                if (Id == 2 && _localent.ColorID >= 2)
                    transform.GetChild(0).GetComponent<Light>().enabled = true;
                if(_localent.ColorID >= Id)
                    Door.GetComponent<DoorController>().IsUnlocked = true;
                break;
        }
    }
}
