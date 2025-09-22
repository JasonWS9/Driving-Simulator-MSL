using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI gearText;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        
    }


    public void UpdateGearText(string newText)
    {
        gearText.text = newText;
    }
}
