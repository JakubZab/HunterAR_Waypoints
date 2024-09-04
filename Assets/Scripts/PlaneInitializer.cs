using UnityEngine;

public class PlaneInitializer : MonoBehaviour
{
    public GameObject planePrefab;

    void Start()
    {
        if (planePrefab != null)
        {
            Vector3 initialPosition = new Vector3(0, -0.5f, 1f);
            Quaternion initialRotation = Quaternion.identity; 

            GameObject planeInstance = Instantiate(planePrefab, initialPosition, initialRotation);
            
            planeInstance.SetActive(true);
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
