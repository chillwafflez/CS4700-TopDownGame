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
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        //PlayerPrefs.DeleteAll(); // this line essentially 'starts over' clears the players stuff 

        instance = this;
        SceneManager.sceneLoaded += LoadState;  // every time we enter a new scene we called LoadState
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // references (to diff things such as the player script, weapon script, etc.)
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitPointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;

    // logic
    public int pesos;
    public int experience;

    // floating text
    // we doing this bcuz we dont want a reference to the FloatingTextManager from everywhere, we just want to have it at one place
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    

    // Upgrade weapon logic
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level? if so return false meaning we cant upgade anymore
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Hitpoint Bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3(1, ratio, 1);
    }

    private void Update()
    {
        //Debug.Log(GetCurrentLevel());
    }

    // Experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // if we at max level
            {
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())  // if current level is the same after we got xp, we didnt level up
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("Player leveled up!");
        player.OnLevelUp();
        OnHitPointChange();
    }


    // On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        // spawn the player at the SpawnPoint gameobject whenever they enter a scene
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Death Menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
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
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode) 
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|'); // exp: assume u have sum like this: '0|10|15|2' this puts it into an array: [0, 10, 15, 2]

        // change player skin

        // change pesos
        pesos = int.Parse(data[1]);

        // change experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        // change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }


}
