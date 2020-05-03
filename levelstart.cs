// this script handles tutorial part of level 1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class levelstart: MonoBehaviour
{
    public Text tutorial_text;
    public GameObject dialogueback;
    public PlayableDirector director;
    public Animator playerAnimator;
    public RuntimeAnimatorController playerAnim;
    public string tutorialtext;
    public string tutorialtext2;
    private bool teaching;
    public int tutorial;

    //this sets up the first tutorial text and makes the player uninteractable - all controls are hidden
    private void Start() 
    {
        playerAnim = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null; //theres a bug in unity which turns off the animator controller, as a workaround i assigned it as null which is reassigned later
        if (PlayerPrefs.GetInt("tutorial_played") == 0)
        {
            dialogueback.GetComponent<Button>().interactable = true;
            StartCoroutine(dialogueback.GetComponent<fade>().fade_control(false));
            StartCoroutine(showtext(tutorialtext));
        }
        else
        {
            StartCoroutine(start_level());
            dialogueback.GetComponent<Button>().interactable = false;
        }
        
    }
    //displays text a letter at a time - typewriter effect - its a coroutine which can be called to display any text
    IEnumerator showtext(string sentence)
    {
        teaching = true;
        yield return new WaitForSeconds(0.4f);
        tutorial_text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            teaching = true;
            tutorial_text.text += letter;
            yield return null;
        }
        teaching = false;
    }
    public void buttonclicked() //giant button on the screen which is called with this function
    {
        if (teaching == false)
        {
            StartCoroutine(shownext());
        }

    }
    IEnumerator shownext() //displays each tutorial text depending on how many times button has been clicked
    {
        if((PlayerPrefs.GetInt("tutorial_played") == 0) && (teaching == false))
        {
            StartCoroutine(showtext(tutorialtext2));
            PlayerPrefs.SetInt("tutorial_played", 1); 
        }
        if((PlayerPrefs.GetInt("tutorial_played") == 1) && (teaching == false))
        {
            StartCoroutine(dialogueback.GetComponent<fade>().fade_control(true));
            StartCoroutine(GameObject.Find("mc_ui").GetComponent<fade>().fade_control(false));
            yield return new WaitForSeconds(1.2f);
            dialogueback.SetActive(false);
            playerAnimator.runtimeAnimatorController = playerAnim;
            PlayerPrefs.SetInt("tutorial_played", 1);
        }
    }
    IEnumerator start_level() //reassigns the animator controller and unfades the controls so the player can play
    {
        StartCoroutine(GameObject.Find("mc_ui").GetComponent<fade>().fade_control(false));
        yield return new WaitForSeconds(2f);
        playerAnimator.runtimeAnimatorController = playerAnim;
        dialogueback.SetActive(false);
    }
    

}
