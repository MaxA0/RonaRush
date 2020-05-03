// this script handles the backstory aspects of the game, at the start of the game when you enter

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class backstory : MonoBehaviour
{
    public int progress = 0;
    public bool teaching;
    public Text backstory_text;
    public GameObject globe;
    public GameObject hazardsign;
    public GameObject livingroom;
    public GameObject MC;

    IEnumerator showtext(string sentence) // a co routine which displays each letter one by one - typewriter effect
    {
        teaching = true;
        backstory_text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            teaching = true;
            backstory_text.text += letter;
            yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        teaching = false;
    }

    public void Start() //sets up the scene - activates the globe and calls the coroutine for the first message
    {
        globe.SetActive(true);
        StartCoroutine(globe.GetComponent<fade>().fade_control(false));
        StartCoroutine(showtext("A global pandemic known as COVID-19 eveloped the earth"));
    }

    public void second() // activates the hazard sign animation which comes down and calls coroutine for second message
    {
        hazardsign.SetActive(true);
        StartCoroutine(showtext("In order to stop the spread, a quarantine order was issued"));
    }
    public void third() // fades in the house and main character and calls the coroutine for the third message
    {
        StartCoroutine(globe.GetComponent<fade>().fade_control(true));
        StartCoroutine(hazardsign.GetComponent<fade>().fade_control(true));
        StartCoroutine(livingroom.GetComponent<fade>().fade_control(false));
        StartCoroutine(showtext("Unfortunately, this made it very difficult to get food"));
    }
    public void fourth() // fades everything out and leaves the character behind, calls coroutine for fourth message
    {
        StartCoroutine(livingroom.GetComponent<fade>().fade_control(true));
        StartCoroutine(MC.GetComponent<fade>().fade_control(false));
        StartCoroutine(showtext("Your job is to get food without getting the virus!!!"));

    }
    public void next() //a function for when the screen is clicked so tht the next message is displayed and various functions are called 
    {
        if(teaching == false)
        {
            progress++;
            if(progress == 1)
            {
                second();
            }
            if(progress == 2)
            {
                third();
            }
            if(progress == 3)
            {
                fourth();
            }
            if(progress == 4) //ending for the backstory, fades the scene and calls a co routine to switch scene
            {
                StartCoroutine(MC.GetComponent<fade>().fade_control(true));
                backstory_text.text = " ";
                StartCoroutine(Switcheroo());
            }
        }
    }
    public IEnumerator Switcheroo() //a function to switch scene to the first level, also fades the scene
    {
        GameObject.Find("music").GetComponent<audiofade>().fadeaudioout();
        StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("level 1");
    }
}
