using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

public class BuildController : MonoBehaviour
{
    //Controls
    [SerializeField] private GameControls gameControls;

    //Ui Elements 
    [SerializeField] private Button buildMenuButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private GameObject buildMenuPanel;

    //Bool for whether build menu is active 
    private bool b_ControllerIsActive = false;

    private bool b_buildIsActive = false; 

    //Event Action when button is selected
  //  public static event Action onBuildActivated;

    //Gameboard Section Variables
    [SerializeField] private GameboardSectionController currentGameboardSectionUnderEdit;

    //Current Building Object
    [SerializeField] private GameObject buildObject;
    [SerializeField] private TileType currentBuildObjectType;
    private Transform selectedTile;

    //Variables for roads 
    [SerializeField] private RoadSystemController roadSystemController;
    [SerializeField] private List<Tile> pendingRoadList;

    //Tower Prefabs 
    [SerializeField] private GameObject towerBallista_Prefab;
    [SerializeField] private GameObject towerCannon_Prefab;
    [SerializeField] private GameObject towerIce_Prefab;
    [SerializeField] private GameObject towerLaserTurret_Prefab;


    private void Awake()
    {
        //Enable Game Controls 
        gameControls = new GameControls();
        gameControls.BuildMenu.Enable();

        //Assign Functions
        gameControls.BuildMenu.ToggleBuildMenu.performed += ctx => ToggleBuildController();

        gameControls.BuildMenu.LeftMouseSelect.performed += ctx => SelectTileToBuildOn();
        gameControls.BuildMenu.LeftMouseSelect.canceled += ctx => CompletedBuildOnTile();
    }

    private void Update()
    {
        if (b_buildIsActive)
        {
            HighlightCurrentTile();
        }

        //Add Function call to check for additional input 
        if (currentBuildObjectType == TileType.ROAD && gameControls.BuildMenu.LeftMouseSelect.IsInProgress())
        {
            HandleDragForMultipleRoadsPlacement();
        }
    }

    private void HighlightCurrentTile()
    {
        if (selectedTile != null)
        {
            selectedTile.gameObject.GetComponent<Tile>().HideAvailabilityIcon();
            selectedTile = null;
        }

        //Setup Raycast
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Check for hit with tile
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Tile")
        {
           // Debug.Log("Hit Tile");


            Tile tile = hit.transform.gameObject.GetComponent<Tile>();

            //Highlight Current Tile
            tile.ShowAvailabilityIcon();
            selectedTile = tile.transform;
        }
        else
        {
            //Debug.Log(hit.transform.gameObject.name);
        }
    }


    private void HandleDragForMultipleRoadsPlacement()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "Tile")
        {
            Tile tile = hit.transform.gameObject.GetComponent<Tile>();

            if (tile.b_available && !tile.b_selected)
            {
                Debug.Log("Added Tile to New Road List");
                tile.SelectTile();
                tile.HideAvailabilityIcon();

                //Add if statement to check if player can afford tile 
                pendingRoadList.Add(tile);
            }
            else
            {
                Debug.Log("Tile Already Selected");
            }
        }
    }

    private void SelectTileToBuildOn()
    {

        if (b_buildIsActive)
        {
            pendingRoadList.Clear();

            Debug.Log("Click Performed");


            if (currentBuildObjectType == TileType.TOWER)
            {
                RaycastHit hit;
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.CompareTag("Tile"))
                {
                    Tile tile = hit.transform.gameObject.GetComponent<Tile>();

                    if (tile.b_available)
                    {
                        tile.InstantiateNewTowerOnTile(buildObject);
                    }
                }
            }
        }
       

    }
    private void CompletedBuildOnTile()
    {
        if (b_buildIsActive)
        {
            Debug.Log("Completed Tile");


            //Set Selected Back to null and Hide Icon 
            if (selectedTile != null)
            {
                selectedTile.gameObject.GetComponent<Tile>().HideAvailabilityIcon();
                selectedTile = null;
            }

            //Pass pending List to Road Controller and Buld Roads
            roadSystemController.CalculatePendingRoadTiles(pendingRoadList);
            roadSystemController.UpdateAdjacentRoadTiles(pendingRoadList);
            CheckTilesForEnemyAndResourceSpawners(pendingRoadList);

            //Deselect added tile 
            foreach (Tile tile in pendingRoadList)
            {
                currentGameboardSectionUnderEdit.AddRoadTiles(tile);
                tile.DeselectTile();
            }

            currentGameboardSectionUnderEdit.RebuildNavMesh();

            buildMenuPanel.SetActive(true);
            closeMenuButton.gameObject.SetActive(true);
            b_buildIsActive = false;
            //  currentGameboardSectionUnderEdit.SetGameboardSectionUnderEdit(false);
        }
    }


    private void HideBuildMenuPanel()
    {
        if (b_buildIsActive)
        {
            buildMenuPanel.SetActive(false);
            closeMenuButton.gameObject.SetActive(false);
        }
    } 

    public void ToggleBuildController()
    {
        if (b_ControllerIsActive)
        {
            //Hide
          //  Debug.Log("Controller is Not Active");
            buildMenuPanel.SetActive(false);
            b_ControllerIsActive = false;
            buildMenuButton.gameObject.SetActive(true);
            closeMenuButton.gameObject.SetActive(false);
        }
        else
        {
            //Show
         //   Debug.Log("Controller is Active");
            buildMenuPanel.SetActive(true);
            b_ControllerIsActive = true;
            buildMenuButton.gameObject.SetActive(false);
            closeMenuButton.gameObject.SetActive(true);
        }
    }


    //Roads Button Function 
    public void BuildRoadsButtonPressed()
    {
        ActivateTileBuilding(TileType.ROAD);
    }

    //Resource Collector Button Function
    public void BuildResourceHarvest_ButtonPressed()
    {
        ActivateTileBuilding(TileType.RESOURCE);
    }

    //Ballista Tower Button Function
    public void BuildTower1_ButtonPressed()
    {
        ActivateTileBuilding(TileType.TOWER);
        buildObject = towerBallista_Prefab;
    }

    //Cannon Tower Button Function
    public void BuildTower2_ButtonPressed()
    {
        ActivateTileBuilding(TileType.TOWER);
        buildObject = towerCannon_Prefab;
    }

    //Ice Tower Button Function
    public void BuildTower3_ButtonPressed()
    {
        ActivateTileBuilding(TileType.TOWER);
        buildObject = towerIce_Prefab;
    }

    //Laser Turret Button Function
    public void BuildTower4_ButtonPressed()
    {
        ActivateTileBuilding(TileType.TOWER);
        buildObject = towerLaserTurret_Prefab;
    }


    private void ActivateTileBuilding(TileType buildObjectType)
    {
        currentBuildObjectType = buildObjectType;
        currentGameboardSectionUnderEdit.SetGameboardSectionUnderEdit(true);
        b_buildIsActive = true;

        HideBuildMenuPanel();
    }


    //Setters and Getters
    public void SetCurrentGameboardSectionUnderEdit(GameboardSectionController gameboardSection)
    {
        currentGameboardSectionUnderEdit = gameboardSection;
    }

    private void CheckTilesForEnemyAndResourceSpawners(List<Tile> tiles)
    {
        foreach(Tile tile in tiles)
        {
            List<Tile> neightbourTiles = tile.GetNeighbourTiles();

            foreach (Tile item in neightbourTiles)
            {
                if (item != null)
                {
                    if (item.tileType == TileType.ENEMY)
                    {
                        item.GetComponentInChildren<EnemySpawnerController>().SetIsConnectedToRoad(true);
                    }
                    else if (item.tileType == TileType.RESOURCE)
                    {
                        item.GetComponentInChildren<ResourceController>().SetIsConnectedToRoad(true);
                    }
                }
            }
        }
    }

}
