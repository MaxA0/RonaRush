// main script which controls game within levels
//controls player movement and interaction with fruit objects and other shoppers
//main character script is generic and can be used on each level

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MC : MonoBehaviour
{
    public float moveSpeed = 1f; //movement speed
    public Rigidbody2D rb; //for movement controls
    public Animator animator; //activates movement animations
    Vector2 movement; //for movement animation
    public VariableJoystick variableJoystick;

    public GameObject death;
    public GameObject lvlcomplete;
    public GameObject mc_ui;

    public bool hold_item;
    public bool cancollect;
    private GameObject collected_item;

    public float collected;
    public float limit;

    public GameObject tutorialtext;
    public GameObject tutorialtext2;

    public AudioSource audiosource;
    public AudioClip pickupsound;
    public AudioClip deathsound;
    public AudioClip winsound;


    private void OnLevelWasLoaded(int level) //resets count to 0 each level
    {
        collected = 0;
    }
    private void Start()
    {
        animator.SetFloat("Vertical_stat", -1); //makes character face down at start
    }

    void Update()
    {
        movement.x = variableJoystick.Horizontal;
        movement.y = variableJoystick.Vertical; //inputs 1 or 0 for moving up and down
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //takes inputs and moves character

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", (movement.y));
        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetFloat("Horizontal_stat", 0);
        animator.SetFloat("Vertical_stat", -1);
        //sets how many need to be collected depending on what level is used
        if (SceneManager.GetActiveScene().name == "level 1") 
        {
            limit = 1;
        }
        if (SceneManager.GetActiveScene().name == "level 2")
        {
            limit = 3;
        }
        if (SceneManager.GetActiveScene().name == "level 3")
        {
            limit = 5;
        }
        if (SceneManager.GetActiveScene().name == "level 4")
        {
            limit = 6;
        }
        //initiates level finished script depending on how many food items have been collected and brought to the cashier
        if ((SceneManager.GetActiveScene().name == "level 1") && (collected == 1))
        {
            if (PlayerPrefs.GetInt("level_finished") < 2)
            {
                PlayerPrefs.SetInt("level_finished", 2);
            }
            level_finished();
        }
        if ((SceneManager.GetActiveScene().name == "level 2") && (collected == 3))
        {
            if (PlayerPrefs.GetInt("level_finished") < 3)
            {
                PlayerPrefs.SetInt("level_finished", 3);
            }
            level_finished();
        }
        if ((SceneManager.GetActiveScene().name == "level 3") && (collected == 5))
        {
            if (PlayerPrefs.GetInt("level_finished") < 4)
            {
                PlayerPrefs.SetInt("level_finished", 4);
            }
            level_finished();
        }
        if ((SceneManager.GetActiveScene().name == "level 4") && (collected == 6))
        {
            if (PlayerPrefs.GetInt("level_finished") < 5)
            {
                PlayerPrefs.SetInt("level_finished", 5);
            }
            level_finished();
        }
        //this is an indicator for how many food items are collected, displayed on screen
        GameObject.Find("coll_ind").GetComponentInChildren<Slider>().value = collected / limit;

    }
    //script for collision with enemy, when colliding with enemy it activates a death screen and disables the controls
    public void OnCollisionEnter2D(Collision2D collided)
    {
        if(collided.gameObject.name == "enemyarea")
        {
            audiosource.PlayOneShot(deathsound, 1f);
            death.SetActive(true);
            moveSpeed = 0f;
            variableJoystick.enabled = false;
            StartCoroutine(mc_ui.GetComponent<fade>().fade_control(true));
            StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
            collided.gameObject.SetActive(false);
            StartCoroutine(death.GetComponent<fade>().fade_control(false));
        }
    }
    //detects wether character is in the area of a food item, if picked up it assigns it to an item slot for display
    public void clickpickup()
    {
        if (cancollect == true)
        {
            audiosource.PlayOneShot(pickupsound, 1f);
            StartCoroutine(GameObject.Find("item_slot").GetComponent<fade>().fade_control(false));
            GameObject.Find("picked_up").GetComponent<Image>().sprite = collected_item.GetComponent<SpriteRenderer>().sprite;
            GameObject.Find("picked_up").GetComponent<CanvasGroup>().alpha = 1;
            hold_item = true;
            collected_item.SetActive(false);
            if (SceneManager.GetActiveScene().name == "level 1")
            {
                tutorialtext.SetActive(false);
                tutorialtext2.SetActive(true);
                StartCoroutine(tutorialtext2.GetComponent<fade>().fade_control(false));
            }
        }
    }
    //sets a bool to true if character is within the area of a collectible item, and ends the level when character has collected enough
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if((trigger.gameObject.tag == "collectable") && (hold_item == false))
        {
            cancollect = true;
            GameObject.Find("grab").GetComponent<CanvasGroup>().alpha = 1f;
            collected_item = trigger.gameObject;
        }
        if ((trigger.gameObject.name == "endlevel") && (hold_item == true))
        {
            audiosource.PlayOneShot(winsound, 1f);
            StartCoroutine(GameObject.Find("item_slot").GetComponent<fade>().fade_control(false));
            collected++;
            hold_item = false;
            GameObject.Find("picked_up").GetComponent<CanvasGroup>().alpha = 0f;
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject.Find("grab").GetComponent<CanvasGroup>().alpha = 0.8f;
        collected_item = null;
        cancollect = false;
    }
    //function for when the level is finished, fades begin and level complete script loads up
    private void level_finished()
    {
        lvlcomplete.SetActive(true);
        StartCoroutine(mc_ui.GetComponent<fade>().fade_control(true));
        StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
        StartCoroutine(lvlcomplete.GetComponent<fade>().fade_control(false));
        
    }
}

