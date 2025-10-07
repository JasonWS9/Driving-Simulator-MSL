using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

     public TextMeshProUGUI gearText;
     public TextMeshProUGUI engineText;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        
    }


    public void UpdateText(TextMeshProUGUI text, string newText)
    {
        text.text = newText;
    }
}
