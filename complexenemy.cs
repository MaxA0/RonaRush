// this script is for the behaviours for the enemies which move to more than one location

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class complexenemy : MonoBehaviour
{
    public Vector2[] locations; //locations for enemy to move to, assigned in the engine
    public bool[] updown; //this is to tell the enemy wether its moving up or down
    public Animator animator; //to animate the enemy

    public int chosen; //which location chosen to move to
    public int plusminus = 1;
    public float speed; //speed of enemy

    void Start()
    {
        chosen = 1;
    }

    // handles what location for the character to move to and where
    void Update()
    {
        if (chosen == (locations.Length-1))
        {
            plusminus = -1;
        }
        if (chosen == 0)
        {
            plusminus = 1;
        }

        animator.SetFloat("Speed", 1);
        transform.position = Vector2.MoveTowards(transform.position, locations[chosen], (speed * Time.deltaTime));
        Vector2 position = gameObject.transform.position;
        if (updown[chosen] == true)
        {
            Debug.Log("banana");
            if (transform.position.y < locations[chosen].y)
            {
                animator.SetFloat("Vertical", 1);
                animator.SetFloat("Horizontal", 0);
            }
            else
            {
                animator.SetFloat("Vertical", -1);
                animator.SetFloat("Horizontal", 0);
            }
        }
        if(updown[chosen] == false)
        {
            Debug.Log("xy");
            if (transform.position.x < locations[chosen].x)
            {
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            else
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
        }
        if (position == locations[chosen])
        {
            chosen = chosen + plusminus;
        }
    }
}