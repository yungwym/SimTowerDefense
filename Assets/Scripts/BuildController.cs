using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BuildController : MonoBehaviour
{

    [SerializeField] private GameControls gameControls;

    [SerializeField] private Button buildMenuButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private GameObject buildMenuPanel;

    private bool b_ControllerIsActive = false;

    public static event Action onBuildActivated;

    private void Awake()
    {
        //Enable Game Controls 
        gameControls = new GameControls();
        gameControls.BuildMenu.Enable();

        //Assign Functions
        gameControls.BuildMenu.ToggleBuildMenu.performed += ctx => ToggleBuildController();
    }



    public void ToggleBuildController()
    {
        if (b_ControllerIsActive)
        {
            //Hide
            Debug.Log("Controller is Not Active");
            buildMenuPanel.SetActive(false);
            b_ControllerIsActive = false;
            buildMenuButton.gameObject.SetActive(true);
            closeMenuButton.gameObject.SetActive(false);
        }
        else
        {
            //Show
            Debug.Log("Controller is Active");
            buildMenuPanel.SetActive(true);
            b_ControllerIsActive = true;
            buildMenuButton.gameObject.SetActive(false);
            closeMenuButton.gameObject.SetActive(true);
        }
        
    }


    public void BuildRoadsButtonPressed()
    {
        onBuildActivated?.Invoke();
    }

    
}
