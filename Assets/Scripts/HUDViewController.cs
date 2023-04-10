using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDViewController : MonoBehaviour
{
    //HUD Elements
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI crystalText;

    private GameObject experienceBar;
    private GameObject surgeBar;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateResourceText(int woodAmnt, int stoneAmnt, int crystalAmnt)
    {
        woodText.text = $"Wood: {woodAmnt}";
        stoneText.text = $"Stone: {stoneAmnt}";
        crystalText.text = $"Crystal: {crystalAmnt}";
    }
}
