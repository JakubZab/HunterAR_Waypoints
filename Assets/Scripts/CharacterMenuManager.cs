using UnityEngine;
using UnityEngine.UI;

public class CharacterMenuManager : MonoBehaviour
{
    public GameObject characterMenuPanel;
    public Text attackText;
    public Text defenseText;
    public Text alchemyText;
    public Text specialSkillText;

    private Character character;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            character = player.GetComponent<Character>();
            if (character == null)
            {
                Debug.LogError("No Character component found on Player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found.");
        }

        UpdateCharacterStats();
    }

    public void OpenCharacterMenu()
    {
        characterMenuPanel.SetActive(true);
        UpdateCharacterStats();
    }

    public void CloseCharacterMenu()
    {
        characterMenuPanel.SetActive(false);
    }

    public void UpdateCharacterStats()
    {
        if (character == null)
        {
            Debug.LogError("Character reference is null.");
            return;
        }

        Debug.Log("Updating character stats...");
        Debug.Log("Character attack: " + character.attack);

        if (attackText == null)
        {
            Debug.LogError("AttackText is not assigned.");
        }
        if (defenseText == null)
        {
            Debug.LogError("DefenseText is not assigned.");
        }
        if (alchemyText == null)
        {
            Debug.LogError("AlchemyText is not assigned.");
        }
        if (specialSkillText == null)
        {
            Debug.LogError("SpecialSkillText is not assigned.");
        }

        if (attackText != null && defenseText != null && alchemyText != null && specialSkillText != null)
        {
            attackText.text = "Attack: " + character.attack;
            defenseText.text = "Defense: " + character.defense;
            alchemyText.text = "Alchemy: " + character.alchemy;
            specialSkillText.text = "Special Skill: " + character.specialSkill;
        }
        else
        {
            Debug.LogError("One or more Text components are not assigned.");
        }
    }
}
