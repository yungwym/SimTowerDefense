using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public void CalculatePendingRoadTiles(List<Tile> pendingRoadTiles)
    {
        //Mark all tile in list as road tiles
        foreach (Tile roadTile in pendingRoadTiles)
        {
            roadTile.SetTileType(TileType.ROAD);
        }

        //Determine each individual road tile 
        foreach (Tile roadTile in pendingRoadTiles)
        {

            //Straight Horizontal 
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.bottomNeighbour) && 
                CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, straightRoadPrefab, 90.0f);
            }
            //Straight Vertical
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, straightRoadPrefab, 0.0f);
            }

            //Corner Top Right
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour)  &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.rightNeighbour)   &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour)) 
            {
                SetMeshMaterialAndRotation(roadTile, cornerRoadPrefab, 270.0f);
            }

            //Corner Top Left
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour)  &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour)  &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour)  &&
                    !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, cornerRoadPrefab, 180.0f);
            }

            //Corner Bottom Right
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.bottomNeighbour)  &&
                    !CheckIfTileIsRoadTile(roadTile.rightNeighbour)   &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour)) 
            {
                SetMeshMaterialAndRotation(roadTile, cornerRoadPrefab, 0.0f);
            }
             
            //Corner Bottom Left
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                   !CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.leftNeighbour)) 
            {
                SetMeshMaterialAndRotation(roadTile, cornerRoadPrefab, 90.0f);
            }

            //Threeway Facing Top
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.bottomNeighbour)  &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, threeWayRoadPrefab, 0.0f);
            }

            //Threeway Facing Bottom
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, threeWayRoadPrefab, 180.0f);
            }

            //Threeway Facing Right
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, threeWayRoadPrefab, 90.0f);
            }


            //Threeway Facing Left
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    !CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, threeWayRoadPrefab, 270.0f);
            }

            //Fourway
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                    CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, fourWayRoadPrefab, 0.0f);
            }

            //End Tiles 
            //End Up 
            if (CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, endRoadPrefab, 0.0f);
            }

            //End Down
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
               CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, endRoadPrefab, 180.0f);
            }

            //End Right 
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, endRoadPrefab, 90.0f);
            }

            //End Left
            if (!CheckIfTileIsRoadTile(roadTile.topNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.bottomNeighbour) &&
                !CheckIfTileIsRoadTile(roadTile.rightNeighbour) &&
                CheckIfTileIsRoadTile(roadTile.leftNeighbour))
            {
                SetMeshMaterialAndRotation(roadTile, endRoadPrefab, 270.0f);
            }
        }
    }


    private bool CheckIfTileIsRoadTile(Tile tile)
    {
        if (tile != null)
        {
            if (tile.tileType == TileType.ROAD)
            {
                return true;
            }
            return false;
        }
        return false;
    }
     

    private void SetMeshMaterialAndRotation(Tile tile, GameObject tilePrefab, float yRotation)
    {
       // tile.SetTileAvailability(false);
        tile.UpdatedRoadTile(tilePrefab, yRotation);
    }

    public void UpdateAdjacentRoadTiles(List<Tile> pendingList)
    {
        List<Tile> roadTilesToUpdate = new List<Tile>();

        foreach (Tile tile in pendingList)
        {
           // tile.SetupNeighboursList();
            List<Tile> adjacentTiles = tile.GetNeighbourTiles();

            //Loop through adjacent tiles and check whether they are road Tile 
            foreach (Tile adjTile in adjacentTiles)
            {
                if (adjTile != null)
                {
                    if (adjTile.tileType == TileType.ROAD && !pendingList.Contains(adjTile))
                    {
                        adjTile.gameObject.transform.Rotate(0, 0, 0);

                        roadTilesToUpdate.Add(adjTile);
                    }
                }
            } 
        }
        //After all tiles have been added, pass list to Calulcate new Road Tile Type
        CalculatePendingRoadTiles(roadTilesToUpdate);
    }

}
