using JetBrains.Annotations;
using UnityEngine;

public class BackPortal : MonoBehaviour  // this is a very misc. script used for one purpose only
{ 

    [NotNull] public Transform LocalPlayer;     // We only need position of our player
	
	// Update is called once per frame
	void Update ()
	{
	    if (Vector3.Distance(a: transform.position, b: LocalPlayer.position) > 15f) // Distance check, disable rendering if we're in another world
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false; // disable rendering if not close, saves memory
            for (int i = transform.childCount - 1; i >= 0; i--) // loop through children, only used in a few 
            {
                var o = transform.GetChild(index: i).gameObject; // null check to see if it even has a child
                if (o.GetComponent<MeshRenderer>() != null)
                    o.GetComponent<MeshRenderer>().enabled =
                        false; // we should do a nullcheck 
            }
        }
        else                                                                       // re-enable the rendering once we're back, we should use else here to save on sqrt math
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                var o = transform.GetChild(i).gameObject;
                if (o.GetComponent<MeshRenderer>() != null)
                    o.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
