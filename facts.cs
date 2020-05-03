//a small script which chooses a random fact to display on the start screen
using UnityEngine;
using UnityEngine.UI;

public class facts : MonoBehaviour
{
    public Text facttext;
    public string[] coronafacts;
    void Start()
    {
        facttext.text = coronafacts[Random.Range(0, coronafacts.Length)];
    }
}
