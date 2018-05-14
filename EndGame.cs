using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public GameObject LocalPlayer;
    public Image Black;
    public Canvas CanvasRenderer;

    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
	{
	    if (LocalPlayer == null) return;
	    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    RaycastHit hit;
	    if (Input.GetKeyUp(KeyCode.E))
	    {
	        if (Physics.Raycast(ray, out hit, 3.5f))
	        {
	            if (hit.collider.gameObject != gameObject) return;
	            Black.CrossFadeAlphaWithCallBack(1f, 2.5f, delegate
	            {
	                SceneManager.LoadScene(2);
	            });
	        }
	    }
	}

    IEnumerator Fade()
    {
        Black.CrossFadeAlpha(1.0f, 2.5f, true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(2);
    }
}
