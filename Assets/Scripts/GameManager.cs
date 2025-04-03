using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // this script will be available anywhere in ur code
    public static GameManager instance; // everywhere in our code can access this specific "instance"
    private void Awake()  // this ensures that only one instance of GameManager exists 
    {
        // turns our GameManager into a singleton basically
        // this ensures that if we go back to the Main Scene, we wont create duplicate GameManager instances
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll(); // this line essentially 'starts over' clears the players stuff 

        instance = this;
        SceneManager.sceneLoaded += LoadState;  // every time we enter a new scene we called LoadState
        DontDestroyOnLoad(gameObject);  // this ensures that as soon as u start the game or change scenes, the GameManager will persist across scenes (whether it be main or Dungeon1, etc.)
    }

    // resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // references (to diff things such as the player script, weapon script, etc.)
    public Player player;
    // public Weapon weapon; etc
    public FloatingTextManager floatingTextManager;

    // logic
    public int pesos;
    public int experience;

    // floating text
    // we doing this bcuz we dont want a reference to the FloatingTextManager from everywhere, we just want to have it at one place
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }


    // Save state
    /* 
     * INT preferredSkin (character ur player likes the most / which character theyre playing rn)
     * INT pesos
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("Save State");
    }

    public void LoadState(Scene s, LoadSceneMode mode) 
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        // exp: assume u have sum like this: '0|10|15|2' this puts it into an array: [0, 10, 15, 2]

        // change player skin

        // change pesos
        pesos = int.Parse(data[1]);

        // change experience
        experience = int.Parse(data[2]);

        // change weapon level

        Debug.Log("Load State");
    }
}
