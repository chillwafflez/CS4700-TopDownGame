using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    // logic fields
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite, weaponSprite;
    public RectTransform xpBar;

    // character selection logic
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            // if we go too far right (no more characters)
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        } 
        else
        {
            currentCharacterSelection--;

            // if we go too far left (no more characters)
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }


    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }


    // weapon upgrade logic
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // update the character information
    public void UpdateMenu()
    {
        // weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";
        } else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        

        // player stats
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        // xp bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total XP points"; // display total xp if we're at max level
            xpBar.localScale = Vector3.one; // this will fill the bar completely
        } else
        {
            int previousLevelXP = GameManager.instance.GetXpToLevel(currentLevel - 1); 
            int currentLevelXP = GameManager.instance.GetXpToLevel(currentLevel);

            // get ratio to use for updating bar progress
            int difference = currentLevelXP - previousLevelXP;
            int currentXPIntoThisLevel = GameManager.instance.experience - previousLevelXP;

            float completionRatio = (float)currentXPIntoThisLevel / (float)difference;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXPIntoThisLevel.ToString() + " / " + difference;
        }
    }
}
