using UnityEngine;
using UnityEngine.UI;

public class MissionPoint2 : MonoBehaviour
{
    public string missionDescription = "New Mission: Hunt and defeat the monster!";
    private bool missionActive = false;
    private bool missionCompleted = false;

    public GameObject missionPopup; 
    public Text missionText;
   // public GameObject missionCompletePopup;
    public GameObject monster;
    public int requiredAttributeLevel = 2;

    private Character character;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            character = player.GetComponent<Character>();
        }

        missionPopup?.SetActive(false);
        //missionCompletePopup?.SetActive(false);

        if (monster != null)
        {
            monster.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !missionActive && !missionCompleted)
        {
            if (character != null)
            {
                bool canStartMission =
                    character.GetAttributeLevel(AttributePoint.AttributeType.Attack) >= requiredAttributeLevel &&
                    character.GetAttributeLevel(AttributePoint.AttributeType.Defense) >= requiredAttributeLevel &&
                    character.GetAttributeLevel(AttributePoint.AttributeType.Alchemy) >= requiredAttributeLevel &&
                    character.GetAttributeLevel(AttributePoint.AttributeType.SpecialSkill) >= requiredAttributeLevel;

                if (!canStartMission)
                {
                    ShowMessage("Can't start this mission yet. Gain some experience.");
                }
                else
                {
                    missionActive = true;
                    ShowMissionPopup();
                }
            }
        }
    }

    void ShowMessage(string message)
    {
        if (missionPopup != null && missionText != null)
        {
            missionText.text = message;
            missionPopup.SetActive(true);
        }
    }

    void ShowMissionPopup()
    {
        if (missionPopup != null)
        {
            missionPopup.SetActive(true);
            if (missionText != null)
            {
                missionText.text = missionDescription;
            }

            if (monster != null)
            {
                monster.SetActive(true);
            }
        }
    }

    public void CloseMissionPopup()
    {
        if (missionPopup != null)
        {
            missionPopup.SetActive(false);
        }
    }

    void Update()
    {
        if (missionActive && character != null)
        {
            if (character.GetAttributeLevel(AttributePoint.AttributeType.Attack) >= 2 &&
                character.GetAttributeLevel(AttributePoint.AttributeType.Defense) >= 2 &&
                character.GetAttributeLevel(AttributePoint.AttributeType.Alchemy) >= 2 &&
                character.GetAttributeLevel(AttributePoint.AttributeType.SpecialSkill) >= 2)
            {
                missionActive = false;
                missionCompleted = true; 
               // ShowMissionCompletePopup();
            }
        }
    }

    /*void ShowMissionCompletePopup()
    {
        if (missionCompletePopup != null)
        {
            missionCompletePopup.SetActive(true);
        }
    }

    public void CloseMissionCompletePopup()
    {
        if (missionCompletePopup != null)
        {
            missionCompletePopup.SetActive(false);
        }
    }*/
}
