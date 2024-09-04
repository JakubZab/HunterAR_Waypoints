using UnityEngine;

public class AttributePoint : MonoBehaviour
{
    public enum AttributeType { Attack, Defense, Alchemy, SpecialSkill }
    public AttributeType attributeType; 
    public int amount; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Character character = other.GetComponent<Character>();
            if (character != null)
            {
                string message;
                bool success = character.IncreaseAttribute(attributeType, amount, out message);

                
                UIManager.Instance.ShowMessage(message);
            }
        }
    }
}
