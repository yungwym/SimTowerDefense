 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardSectionType
{
    HOME,
    GRASS
}

public class GameboardSectionGenerator : MonoBehaviour
{
    //Road System Controller
    [SerializeField] private RoadSystemController roadSystemController;
    

    //Gameboard Variables 
    public GameObject[,] grid;
    public Vector3 gridStartPosition;

    private int GridRowSize;
    private int GridColumnSize;

    private float tileSize = 1.0f;

    //Tile Prefabs 
    public GameObject blankTilePrefab;
    public GameObject blankElevatedPrefab;


    public GameObject gameboardSectionPrefab;
    private GameboardSectionController newGameBoardSection;

    //Pending Road List for Road System
    List<GameObject> pendingRoadList;


    public void GenerateBlankBoard(int rows, int columns, BoardSectionType sectionType)
    {
        //Instantiate new Gameboard Section Prefab 
        newGameBoardSection = Instantiate(gameboardSectionPrefab).GetComponent<GameboardSectionController>();

        GridRowSize = rows;
        GridColumnSize = columns;

        int tileNumber = 0;

        grid = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float posY = (gridStartPosition.z) - row * tileSize;
                float posX = (gridStartPosition.x) + column * tileSize;

                grid[row, column] = SpawnBlankTile(row, column, tileNumber, posX, posY);

                tileNumber += 1;
            }
        }

        SetNeighbouringTiles();

        if (sectionType == BoardSectionType.HOME)
        {
            BuildHomeLoop();
        }
    }

    private GameObject SpawnBlankTile(int row, int column, int tileNum, float posX, float posY)
    {
        GameObject blankTile = Instantiate(blankTilePrefab, new Vector3(posX, 0.0f, posY), Quaternion.identity);

        Tile blankT = blankTile.GetComponent<Tile>();

        blankT.SetTileLocation(row, column);
        blankT.SetTileNumber(tileNum);
        blankT.SetTileType(TileType.BLANK);
        blankTile.transform.parent = gameObject.transform;

        //Add Blank Tile to List 
        newGameBoardSection.blankTiles.Add(blankTile);

        return blankTile;
    }

    private void SetNeighbouringTiles()
    {
        for (int row = 0; row < GridRowSize; row++)
        {
            for (int col = 0; col < GridColumnSize; col++)
            {
                Tile currentTile = grid[row, col].GetComponent<Tile>();

                //Top -row, col
                if (GetTileAtRowAndColumn(row - 1, col))
                {
                    currentTile.topNeighbour = GetTileAtRowAndColumn(row - 1, col).GetComponent<Tile>();
                }
                //Right row, +col
                if (GetTileAtRowAndColumn(row, col + 1))
                {
                    currentTile.rightNeighbour = GetTileAtRowAndColumn(row, col + 1).GetComponent<Tile>();
                }
                //Bottom +row, col
                if (GetTileAtRowAndColumn(row + 1, col))
                {
                    currentTile.bottomNeighbour = GetTileAtRowAndColumn(row + 1, col).GetComponent<Tile>();
                }
                //Left row, -col
                if (GetTileAtRowAndColumn(row, col - 1))
                {
                    currentTile.leftNeighbour = GetTileAtRowAndColumn(row, col - 1).GetComponent<Tile>();
                }
            }
        }
    }

    private GameObject GetTileAtRowAndColumn(int row, int col)
    {
        //Check if Row and Column are Valid 
        if (row >= 0 && row < GridRowSize)
        {
            if (col >= 0 && col < GridColumnSize)
            {
                return grid[row, col];
            }
        }
        return null;
    }

    private void BuildHomeLoop()
    {
        //Build Inner Circle Roads 

        pendingRoadList = new List<GameObject>();



        //Horizontal 
        for (int i = 5; i < 13; i++)
        {
            pendingRoadList.Add(GetTileAtRowAndColumn(5, i));
            pendingRoadList.Add(GetTileAtRowAndColumn(12, i));
        }

        //Vertical
        for (int i = 6; i < 12; i++)
        {
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 5));
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 12));
        }

        //Rows 17 and 18, Columns 15-16
        for (int i = 10; i < 12; i++)
        {
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 8));
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 9));
        }
        
        roadSystemController.CalculatePendingRoadTiles(pendingRoadList);

        Debug.Log(pendingRoadList.Count);

        //Remove Road Tiles from Blank List and Re-add them to Road Tile List
        foreach (GameObject roadTile in pendingRoadList)
        {
            newGameBoardSection.blankTiles.Remove(roadTile);
            newGameBoardSection.roadTiles.Add(roadTile);
        }
        pendingRoadList.Clear();
    }

}
