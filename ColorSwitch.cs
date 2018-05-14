using UnityEngine;

public class ColorSwitch : MonoBehaviour {

	public eColors RoomColor; // eColors is basically an enum to loop through 3 primary colors. Room color is manually set
	public GameObject Localplayer; 

	private PlayerController _localent;

	// Use this for initialization
	void Start ()
    {
        _localent = Localplayer.GetComponent<PlayerController>();
    }

    // Update is called once per frame
	void Update () {
	    if (_localent == null) // an update frame is actually run once before start, so we need to nullcheck first
	        return;
	    var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // setup vars
	    RaycastHit hit;
	    if (Input.GetKeyUp(KeyCode.E))
		    if (Physics.Raycast (ray, out hit, 3.0f))    // raycast right in front of us 
		        if (hit.collider.gameObject == gameObject && _localent.NextColor == RoomColor) //  make sure this is our next color
		        {
		            if(_localent.ColorID != 3) // make sure we haven't won
		                _localent.ColorID++; // setup for the next color
		            for (int i = _localent.SelectorBlocks.Length - 1; i >= 0; i--) // for loop is much faster than foreach btw
		            {
		                GameObject block = _localent.SelectorBlocks [i]; // loop 
		                if (block.GetComponent<BlockController>().ID != _localent.ColorID) continue; // continue loop if not color we want

		                Color col = block.GetComponent<Renderer> ().material.color; // get current block color
		                eColors nextCol = 0;  // loop through block colors
		                if (col == Color.red)
		                    nextCol = eColors.kRed;
		                if (col == Color.blue)
		                    nextCol = eColors.kBlue;
		                if (col == Color.green)
		                    nextCol = eColors.kGreen;
		                _localent.NextColor = nextCol; // set next color
		            }
                    GetComponent<AudioSource>().Play();
		        }
	}
}
