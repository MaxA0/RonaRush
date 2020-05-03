//this script looks at which levels have been completed and makes it so that some levels are not available it some levels have not been played
//uses for loop to unlock each button systematically
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelbuttons;

    void Start()
    {
        int levelunlocked = PlayerPrefs.GetInt("level_finished");
        for (int i = 0; i< levelbuttons.Length; i++)
        {
            levelbuttons[i].interactable = false;
        }
        for(int i = 0; i < levelunlocked; i++)
        {
            levelbuttons[i].interactable = true;
        }
    }

}
