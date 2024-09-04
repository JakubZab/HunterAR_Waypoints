using UnityEngine;
using UnityEngine.UI;

public class MissionPoint : MonoBehaviour
{
    public string missionDescription = "New mission: raise the level of each attribute to level two";
    
    private bool missionActive = false;
    private bool missionCompleted = false;

    public GameObject missionPopup;
    public Text missionText;
    public GameObject missionCompletePopup;
    

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

        if (missionPopup != null)
        {
            missionPopup.SetActive(false);
        }
        if (missionCompletePopup != null)
        {
            missionCompletePopup.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !missionActive && !missionCompleted)
        {
            missionActive = true;
            ShowMissionPopup();
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
                ShowMissionCompletePopup();
            }
        }
    }

    void ShowMissionCompletePopup()
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
    }
}
