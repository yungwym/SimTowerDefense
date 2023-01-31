using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileType
{
    NONE,
    BLANK,
    ELEVATED,
    ROAD,
    RESOURCE,
    ENEMY,
    TOWER,
    BORDER
}

public enum RoadTileType
{
    NONE,
    STRAIGHT,
    CORNER,
    END,
    THREEWAY,
    FOURWAY,
    MERGE,
    DOUBBLEWIDE,
    FUNNEL

}
public class Tile : MonoBehaviour
{
    //Tile Member Variables 
    //Tile Member Variables 
    public TileType tileType;
    public GameObject tileItem;

    //Tile Adjacent Variables 
    public Tile topNeighbour;
    public Tile rightNeighbour;
    public Tile bottomNeighbour;
    public Tile leftNeighbour;

    public List<Tile> neighbourTiles;

    //Tile Location 
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private int tileNumber;

    //Tile Icons 
    [SerializeField] private GameObject availableTileIcon;
    [SerializeField] private GameObject unavailableTileIcon;

    public bool b_available = true;

    private bool b_buildActive = false;


    private void OnEnable()
    {
        BuildController.onBuildActivated += ShowAvailabilityIcon;
    }

    private void OnDisable()
    {
        b_buildActive = false;
        BuildController.onBuildActivated -= ShowAvailabilityIcon;
    }

  



    public void ShowAvailabilityIcon()
    {
        b_buildActive = true;

        if (b_available)
        {
            availableTileIcon.SetActive(true);
        }
        else
        {
            unavailableTileIcon.SetActive(true);
        }
    }

    public void HideAvailabilityIcon()
    {
        if (b_available)
        {
            availableTileIcon.SetActive(false);
        }
        else
        {
            unavailableTileIcon.SetActive(false);
        }
    }



    public void SetTileType(TileType _tileType)
    {
        tileType = _tileType;
    }

    public Vector2 GetLocation()
    {
        return new Vector2(row, column);
    }

    public void SetTileLocation(int _row, int _column)
    {
        row = _row;
        column = _column;
    }

    public void SetTileNumber(int _tileNum)
    {
        tileNumber = _tileNum;
    }

    public List<Tile> GetNeighbourTiles()
    {
        neighbourTiles.Add(topNeighbour);
        neighbourTiles.Add(rightNeighbour);
        neighbourTiles.Add(bottomNeighbour);
        neighbourTiles.Add(leftNeighbour);
        return neighbourTiles;
    }

    

    public void OnMouseOver()
    {
        if (b_buildActive)
        {
            HideAvailabilityIcon();
        }
    }

    private void OnMouseExit()
    {
        if (b_buildActive)
        {
            ShowAvailabilityIcon();
        }
    }
}
