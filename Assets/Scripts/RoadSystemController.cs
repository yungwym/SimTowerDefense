using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSystemController : MonoBehaviour
{
    //Road Tiles
    public GameObject straightRoadPrefab;
    public GameObject cornerRoadPrefab;
    public GameObject endRoadPrefab;
    public GameObject threeWayRoadPrefab;
    public GameObject fourWayRoadPrefab;

    public GameObject doubleWideRoadPrefab;
    public GameObject splitRoadPrefab;
    public GameObject FunnelRoadPrefab;  

    public void CalculatePendingRoadTiles(List<GameObject> pendingRoadTiles)
    {
        //Mark all tile in list as road tiles
        foreach (GameObject roadTile in pendingRoadTiles)
        {
            roadTile.GetComponent<Tile>().SetTileType(TileType.ROAD);
        }

        //Determine each individual road tile 
        foreach (GameObject roadTile in pendingRoadTiles)
        {
            Tile tile = roadTile.GetComponent<Tile>();

            //Straight Horizontal 
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                tile.bottomNeighbour.tileType != TileType.ROAD &&
                tile.rightNeighbour.tileType == TileType.ROAD &&
                tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Straight Horizontal" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, straightRoadPrefab, 90.0f);
            }
            //Straight Vertical
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Straight Vertical" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, straightRoadPrefab, 0.0f);
            }


            //Corner Top Right
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Corner Top Right" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, cornerRoadPrefab, 270.0f);
            }

            //Corner Top Left
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Corner Top Left" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, cornerRoadPrefab, 180.0f);
            }

            //Corner Bottom Right
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType != TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Corner Bottom Right" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, cornerRoadPrefab, 0.0f);
            }
             

            //Corner Bottom Left
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType != TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Corner Bottom Left" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, cornerRoadPrefab, 90.0f);
            }


            //Threeway Facing Top
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType != TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Threeway Top" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, threeWayRoadPrefab, 0.0f);
            }

            //Threeway Facing Bottom
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Threeway Top" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, threeWayRoadPrefab, 180.0f);
            }

            //Threeway Facing Right
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Threeway Top" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, threeWayRoadPrefab, 90.0f);
            }


            //Threeway Facing Left
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Threeway Top" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, threeWayRoadPrefab, 270.0f);
            }

            //Fourway
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Threeway Top" + tile.GetLocation());
                SetMeshMaterialAndRotation(tile, fourWayRoadPrefab, 0.0f);
            }
        }
    }
     

    private void SetMeshMaterialAndRotation(Tile tile, GameObject tilePrefab, float yRotation)
    {
        tile.b_available = false;
        tile.GetComponent<MeshFilter>().mesh = tilePrefab.GetComponent<MeshFilter>().sharedMesh;
        tile.GetComponent<MeshRenderer>().materials = tilePrefab.GetComponent<MeshRenderer>().sharedMaterials;
        tile.gameObject.transform.Rotate(0, yRotation, 0);
    }

    public void SetTileAsDoubleWide(Tile tile)
    {
        tile.GetComponent<MeshFilter>().mesh = doubleWideRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
        tile.GetComponent<MeshRenderer>().materials = doubleWideRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
        tile.gameObject.transform.Rotate(0, 0, 0);
    }


}
