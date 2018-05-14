using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter2 : MonoBehaviour
{

    public Transform Player;
    public Transform Reciever;

    private bool _playerisOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (!_playerisOverlapping) return;  // sanity check
        Vector3 portaltoPlayer = Player.position - transform.position; // get difference between
        float dotProduct = Vector3.Dot(transform.up, portaltoPlayer); // get the dot product between the two

        // if true player has moved past portal
        if (!(dotProduct < 0f)) return;
        float rotationDiff = Quaternion.Angle(transform.rotation, Reciever.rotation); // get angle of rotation to make it look p
        rotationDiff -= 90; // portal angle yaw rotation
        //Player.Rotate(Vector3.up, rotationDiff);

        Vector3 posOffset = Quaternion.Euler(x: 0f, y: rotationDiff, z: 0f) * portaltoPlayer; // get euler offset that we can set for the position
        Player.position = Reciever.position + posOffset; // set smooth position
        _playerisOverlapping = false; // reset overlapping
    }

    void OnTriggerEnter(Collider other) // use the triggers to check if they're overlapping
    {
        print(other.tag);
        if (other.tag != "Player") return;
        _playerisOverlapping = true;
    }

    void OnTriggerExit(Collider other) // set flag to 0x0 if they leave
    {
        if (other.tag != "Player") return;
        _playerisOverlapping = false;
    }
}
