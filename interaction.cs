// this script handles the buttons on the replay page, death page and the door opening on the start page
// this script loads scenes based off buttons clicked

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interaction : MonoBehaviour
{
    public GameObject death;
    public GameObject lvlcomplete;
    public GameObject pause;
    public AudioSource audiosource;
    public AudioClip levelsound;
    public AudioClip selection;

    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision) //script reads memory and sees wether player has played game before, otherwise the backstory is loaded
    {
        Debug.Log(PlayerPrefs.GetInt("level_finished"));
        if (collision.gameObject.name == "startgame")
        {
            if (PlayerPrefs.GetInt("level_finished") == 0)
            {
                StartCoroutine(Switcheroo("backstory"));
            }
            else
            {
                StartCoroutine(Switcheroo("level_select"));
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        anim = other.GetComponent<Animator>(); 
        {
            anim.SetBool("open", true); 
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        anim.SetBool("open", false);
    }
    public void home()
    {
        StartCoroutine(Switcheroo("start"));
        audiosource.PlayOneShot(selection, 1f);
    }
    public void replay()
    {
        StartCoroutine(Switcheroo(SceneManager.GetActiveScene().name));
        audiosource.PlayOneShot(selection, 1f);

    }
    public void nextlevel()
    {
        StartCoroutine(Switcheroo("level_select"));
        audiosource.PlayOneShot(selection, 1f);
    }
    public void Pause()
    {
        pause.SetActive(true);
        StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
        StartCoroutine(pause.GetComponent<fade>().fade_control(false));
        GameObject.Find("music").GetComponent<audiofade>().fadeaudioout();
        StartCoroutine(Pausegame());
    }

    public IEnumerator Pausegame()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;

    }
    public void Play()
    {
        Time.timeScale = 1;
        StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(true));
        StartCoroutine(pause.GetComponent<fade>().fade_control(true));
        GameObject.Find("music").GetComponent<audiofade>().fadeaudioin();
    }
    public IEnumerator Playgame()
    {
        yield return new WaitForSeconds(1);
        pause.SetActive(false);

    }
    public IEnumerator Switcheroo(string switch_to) //a function to play the fade, move character and switch scene
    {
        GameObject.Find("music").GetComponent<audiofade>().fadeaudioout();
        StartCoroutine(death.GetComponent<fade>().fade_control(true));
        StartCoroutine(lvlcomplete.GetComponent<fade>().fade_control(true));
        if(SceneManager.GetActiveScene().name == "Start")
        {
            StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(switch_to);
    }

}
