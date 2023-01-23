using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSystemController : MonoBehaviour
{
    //Road Tiles
    public GameObject straightRoadPrefab;
    public GameObject cornerRoadPrefab;
    public GameObject endRoadPrefab;
    public GameObject threeWayPrefab;
    public GameObject fourWayPrefab;

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
                tile.GetComponent<MeshFilter>().mesh = straightRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = straightRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 90, 0);
            }
            //Straight Vertical
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Straight Vertical" + tile.GetLocation());
                tile.GetComponent<MeshFilter>().mesh = straightRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = straightRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 0, 0);
            }


            //Corner Top Right
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Corner Top Right" + tile.GetLocation());
                tile.GetComponent<MeshFilter>().mesh = cornerRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = cornerRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 270, 0);
            }

            //Corner Top Left
            if (tile.topNeighbour.tileType != TileType.ROAD &&
                    tile.bottomNeighbour.tileType == TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Corner Top Left" + tile.GetLocation());
                tile.GetComponent<MeshFilter>().mesh = cornerRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = cornerRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 180, 0);
            }

            //Corner Bottom Right
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType != TileType.ROAD &&
                    tile.rightNeighbour.tileType != TileType.ROAD &&
                    tile.leftNeighbour.tileType == TileType.ROAD)
            {
                Debug.Log("Corner Bottom Right" + tile.GetLocation());
                tile.GetComponent<MeshFilter>().mesh = cornerRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = cornerRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 0, 0);
            }


            //Corner Bottom Left
            if (tile.topNeighbour.tileType == TileType.ROAD &&
                    tile.bottomNeighbour.tileType != TileType.ROAD &&
                    tile.rightNeighbour.tileType == TileType.ROAD &&
                    tile.leftNeighbour.tileType != TileType.ROAD)
            {
                Debug.Log("Corner Bottom Left" + tile.GetLocation());
                tile.GetComponent<MeshFilter>().mesh = cornerRoadPrefab.GetComponent<MeshFilter>().sharedMesh;
                tile.GetComponent<MeshRenderer>().materials = cornerRoadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                tile.gameObject.transform.Rotate(0, 90, 0);
            }
        }
    }
}
