// a script using co-routines to fade and unfade a giant image attached to the canvas
// this is used on a lot of the gameobjects for when they need to be faded
// a lot of my scripts are generic like this and can be attached to a lot of gameobjects to be used in different ways - as they are class scripts

using UnityEngine;
using System.Collections;

public class fade : MonoBehaviour
{
    void Start()
    {
        if (gameObject.name == "fade")
        {
            GetComponent<CanvasGroup>().alpha = 1;
            StartCoroutine(fade_control(true));
        }
        
    }
    public IEnumerator fade_control(bool fadeAway)
    {
        //fade
        if (fadeAway)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
        }
        //unfade
        else
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                GetComponent<CanvasGroup>().alpha = i;
                yield return null;
            }
        }
    }
}
