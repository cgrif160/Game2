using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public Text carrotsText;
    public Text berriesText;

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
    public void UpdateCarrotsText(int carrots)
        {
            carrotsText.text = "Carrots: " + carrots;
        }
    public void UpdateBerriesText(int berries)
        {
            berriesText.text = "Berries: " + berries;
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
