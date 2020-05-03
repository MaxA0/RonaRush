// this script is attached to a trigger system, so when the character touches the trigger, it signals the camera to move to the triggers location


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameratrigger : MonoBehaviour
{
    public GameObject camera;
    public Transform cameraTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("whack");
        camera.GetComponent<camerafollow>().target = cameraTarget;
    }

}

