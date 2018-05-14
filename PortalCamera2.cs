using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera2 : MonoBehaviour
{

    public Transform PlayerCamera;
    public Transform Portal;
    public Transform OtherPortal;
	
	void Update ()
	{
        transform.position = Portal.position + -(PlayerCamera.position - OtherPortal.position);     // we some some fat math here 
        transform.rotation = Quaternion.LookRotation(-(Quaternion.AngleAxis(Quaternion.Angle(a: Portal.rotation, b: OtherPortal.rotation), -Vector3.down) * PlayerCamera.forward), Vector3.up); // have to do some quaternion math here
	}                                                                                                                                                          // to get the correct angle
}
