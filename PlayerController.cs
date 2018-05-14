using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eColors
{
	kRed = 0,
	kBlue = 1,
	kGreen = 2
};

public class ColorBlock
{
	public int ID;

	public ColorBlock(int _id)
	{
		ID = _id;
	}
}

public class PlayerController : MonoBehaviour
{

    public int ColorID;
	public eColors NextColor;

	public GameObject[] SelectorBlocks;
	public GameObject[] FogObjects;

	// Use this for initialization
	void Start () {
		if (SelectorBlocks != null) { // make sure we have blocks set
			for (int i = 3 - 1; i >= 0; i--) { // loop through the blocks 
				GameObject go = SelectorBlocks [i];
				go.name = "ColorBlock_" + i; // setting name for debug reasons
				int randomcolor = Random.Range (min: 0, max: 3); // get random number
				switch (randomcolor) { // go through our random colors
				case (int) eColors.kRed:
					go.GetComponent<Renderer> ().material.color = Color.red;
					break;
				case (int) eColors.kBlue:
					go.GetComponent<Renderer> ().material.color = Color.blue;
					break;
				case (int) eColors.kGreen:
					go.GetComponent<Renderer> ().material.color = Color.green;
					break;
				}

				BlockController bc = go.AddComponent<BlockController> (); // we have to do something stupid here
				bc.ID = i;                                                // since we cant use static classes

			    if (i != 0) continue;
			    ColorID = 0;
			    NextColor = (eColors)randomcolor; // set our next color
			}
		}

	    if (FogObjects == null) return;                     // This was something I was working on but decided to scratch it due to preformance issues. It's basically never used or called
	    {
	        for (int i = 0; i < FogObjects.Length; i++)
            {
	            GameObject go = FogObjects [i];
	            for (int k = 0; k < go.transform.childCount; k++)
                {
	                ParticleSystem Ps = go.transform.GetChild (k).transform.GetChild (0).GetComponent<ParticleSystem> ();
	                var main = Ps.main;
	                int randomcolor = Random.Range (0, 3);
	                Color mincol = Color.white;
	                Color maxcol = Color.white;
	                ParticleSystem.MinMaxGradient tempStartcolor = new ParticleSystem.MinMaxGradient ();
	                switch (randomcolor)
                    {
	                    case (int) eColors.kRed:
	                        ColorUtility.TryParseHtmlString ("FFA1A114", out mincol);
	                        ColorUtility.TryParseHtmlString ("BC561E20", out maxcol);
					
	                        break;
	                    case (int) eColors.kBlue:
	                        ColorUtility.TryParseHtmlString ("FFA1A114", out mincol);
	                        ColorUtility.TryParseHtmlString ("BC561E20", out maxcol);
	                        break;
	                    case (int) eColors.kGreen:
	                        ColorUtility.TryParseHtmlString ("FFA1A114", out mincol);
	                        ColorUtility.TryParseHtmlString ("BC561E20", out maxcol);
	                        break;
	                }
	                tempStartcolor.colorMin = mincol;
	                tempStartcolor.colorMin = maxcol;
	                main.startColor = tempStartcolor;
	            }
	        }
	    }
	}
}
