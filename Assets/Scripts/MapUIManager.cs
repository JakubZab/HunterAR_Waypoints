using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    public GameObject mapPanel;
    public Button moveButton;

    void Start()
    {
        moveButton.onClick.AddListener(OpenMap);
    }

    void OpenMap()
    {
        mapPanel.SetActive(true);
    }

    public void CloseMap()
    {
        mapPanel.SetActive(false);
    }
}
