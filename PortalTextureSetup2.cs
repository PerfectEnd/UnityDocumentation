using UnityEngine;

public class PortalTextureSetup2 : MonoBehaviour
{

    public Camera[] cameraB;
    public Material[] cameraMatB;

	void Start () {
		for (int i = cameraB.Length - 1; i >= 0; i--) {
			Camera cam = cameraB [i];
			Material camMat = cameraMatB [i];
		    if (cam.targetTexture != null)
		        cam.targetTexture.Release(); // release the texture
		    cam.targetTexture = new RenderTexture (Screen.width, Screen.height, 0x18);      // setup the new render texture
			camMat.mainTexture = cam.targetTexture; // set the texture                      // could use 0x18 as hex, or binary
        }
	}
}
