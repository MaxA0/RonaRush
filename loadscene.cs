//assigned to each button in the level selection area to load level
//this is a class script which can be assigned to each button but will load a unique level

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadscene : MonoBehaviour
{
    public string leveltobeloaded;
    public AudioSource audiosource;
    public AudioClip levelsound;
    public void Loadscene()
    {
        StartCoroutine(switchscene());
    }
    IEnumerator switchscene()
    {
        audiosource.PlayOneShot(levelsound, 1f);
        StartCoroutine(GameObject.Find("fade").GetComponent<fade>().fade_control(false));
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(leveltobeloaded);
    }
    
}
