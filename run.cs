//run indicator script which lets the character run faster when the run button is pressed
using UnityEngine;
using UnityEngine.UI;

public class run : MonoBehaviour
{
    public float runlevel = 100;
    public bool running;
    public bool canrun;

    private void OnLevelWasLoaded(int level)
    {
        runlevel = 100;
    }
    public void onPress()
    {
        if (canrun == true) {
            GameObject.Find("mc").GetComponent<MC>().moveSpeed = 2f;
            running = true;
        }
    }

    public void onRelease()
    {
        GameObject.Find("mc").GetComponent<MC>().moveSpeed = 1f;
        running = false;
    }
    private void Update()
    {
        
        GameObject.Find("run_ind").GetComponent<Slider>().value = runlevel/100;
        if(running == true)
        {
            runlevel -= Time.deltaTime * 20;
        }
        if (runlevel > 0)
        {
            canrun = true;
        }
       
    }

}
