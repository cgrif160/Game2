using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI berryText;

    //Creates one instance of script
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UIManager is null!");
            }
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;
    }

    //Updates UI
    public void UpdateCarrotsText(int carrotCount)
        {
            carrotText.text = "Carrots: " + carrotCount;
        }
    public void UpdateBerriesText(int berryCount)
        {
            berryText.text = "Berries: " + berryCount;
        }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
