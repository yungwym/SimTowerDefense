using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        public List<Tile> neighbourTiles;

        //Tile Location 
        [SerializeField] private int row;
        [SerializeField] private int column;
        [SerializeField] private int tileNumber;


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
}
