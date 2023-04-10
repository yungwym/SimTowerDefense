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
    BORDER,
    GHOST
}

public enum RoadTileType
{
    NONE,
    STRAIGHT,
    CORNER,
    END,
    THREEWAY,
    FOURWAY,
}
public class Tile : MonoBehaviour
{
    //Tile Member Variables 
    public TileType tileType;
    public GameObject tileItem;

    //Tile Adjacent Variables 
    public Tile topNeighbour;
    public Tile rightNeighbour;
    public Tile bottomNeighbour;
    public Tile leftNeighbour;

    public List<Tile> neighbourTiles = new List<Tile>();

    //Tile Location 
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private int tileNumber;

    //Tile Icons
    [SerializeField] private GameObject availableIcon;
    [SerializeField] private GameObject unavailableIcon;

    public bool b_available = true;

    public bool b_selected = false;
    [SerializeField] private GameObject selectedIcon;

    [SerializeField] private bool b_parentSectionUnderEdit = false;


    //**Future:: Can change these to a Toggle function 
    public void ShowAvailabilityIcon()
    {
        if (b_available)
        {
            availableIcon.SetActive(true);
        }
        else
        {
            unavailableIcon.SetActive(true);
        }
    }

    public void HideAvailabilityIcon()
    {
        if (b_available)
        {
            availableIcon.SetActive(false);
        }
        else
        {
            unavailableIcon.SetActive(false);
        }
    }

    public void SelectTile()
    {
        b_selected = true;
        selectedIcon.SetActive(true);
        
    }

    public void DeselectTile()
    {
        b_selected = false;
        selectedIcon.SetActive(false);
    }

    //Getterss and Setters 

    public void SetTileAvailability(bool availability)
    {
        b_available = availability;
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

    public void SetBoolParentSectionUnderEdit(bool isUnderEdit)
    {
        b_parentSectionUnderEdit = isUnderEdit;
    }

    public void UpdatedRoadTile(GameObject newRoadItem, float yRotation)
    {
        //Remove Old Item
        Destroy(tileItem.gameObject);

        tileItem = Instantiate(newRoadItem, transform.position, transform.rotation);

        tileItem.transform.parent = gameObject.transform;
        tileItem.transform.eulerAngles = new Vector3(0, yRotation, 0);

        SetTileAvailability(false);
    }

    public void InstantiateNewTowerOnTile(GameObject newTower)
    {
        //Make new position for tower
        Vector3 towerPosition = new Vector3(transform.position.x, 0.20f, transform.position.z);
        //Instaniate new tower under Tile's transform
        tileItem = Instantiate(newTower, towerPosition, transform.rotation);
        tileItem.transform.parent = gameObject.transform;

        //Set New Tile Type
        tileType = TileType.TOWER;
        //Set availability to false
        SetTileAvailability(false);
    }

    public void ElevateTile(GameObject elevatedItem)
    {
        //Remove old Item
        Destroy(tileItem.gameObject);

        tileType = TileType.ELEVATED;

        tileItem = Instantiate(elevatedItem, transform.position, transform.rotation);
        tileItem.transform.parent = gameObject.transform;
    }

    public void InstantiateNewResourceOnTile(GameObject newResource)
    {
        //Remove Old Item
        Destroy(tileItem.gameObject);

        //Instantiate new resource tile as the tile's item, under the tile's transform
        tileItem = Instantiate(newResource, transform.position, transform.rotation);
        tileItem.transform.parent = gameObject.transform;

        //Set new Tile Type 
        tileType = TileType.RESOURCE;
        //Set Availability
        SetTileAvailability(false);
    }
}
