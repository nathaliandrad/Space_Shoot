using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public Vector3 offset;
    private float moveSpeed = 6f;
    public Transform player; // Drop the player in the inspector of the camera

    void Update()
    {
        //making the camera ony follow x position of player
        Vector3 newPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }


}
