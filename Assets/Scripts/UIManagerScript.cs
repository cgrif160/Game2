using UnityEngine;
using TMPro;

public class UIManagerScript : MonoBehaviour
{
    public TextMeshProUGUI carrotText;
    public TextMeshProUGUI berryText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}