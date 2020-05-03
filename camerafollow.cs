// this script handles the camera movement for when the character hits a trigger, to signal movement to the next area on the map

using UnityEngine;
using System.Collections;

public class camerafollow : MonoBehaviour
{

    public float cameraSpeed = 2; //the speed of the camera movement
    public Transform target;

    void Start()
    {
        target = transform;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraSpeed);
    }
}