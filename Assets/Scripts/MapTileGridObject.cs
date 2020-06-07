using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MapTileGridObject
{
    public enum Type
    { 
        Empty,
        Mine,
        Mine1,
        Mine2,
        Mine3,
        Mine4,
        Mine5,
        Mine6,
        Mine7,
        Mine8
    }

    private Grid<MapTileGridObject> grid;
    private int x;
    private int y;
    private Type typeOfCell;
    private bool revealed;
    private GameObject tile;

    public MapTileGridObject(Grid<MapTileGridObject> grid, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        TypeOfCell = Type.Empty;
        tile = UtilsClass.CreateWorldSprite(ToString(), MinesWeeperGameHandler.spritesArray[MinesWeeperGameHandler.COVERED_INDEX], grid.GetWorldPosition(x, y) + new Vector3(grid.CellSize, grid.CellSize) * .5f, Vector3.one, 1, Color.white);
        tile.GetComponent<SpriteRenderer>().sprite = MinesWeeperGameHandler.spritesArray[MinesWeeperGameHandler.COVERED_INDEX];
        float newScale = grid.CellSize / 0.65f;
        tile.transform.localScale = new Vector3(newScale, newScale);
    }

    public Type TypeOfCell
    {
        get 
        {
            return typeOfCell;
        }
        set
        {
            typeOfCell = value;
        }
    }

    public bool Revealed
    {
        get 
        {
            return revealed;
        }
    }

    public void Reveal()
    {
        revealed = true;
        tile.GetComponent<SpriteRenderer>().sprite = RepresentativeSprite();
    }

    public override string ToString()
    {
        return TypeOfCell.ToString();
    }

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public GameObject Tile
    {
        get
        {
            return tile;
        }
    }

    private Sprite RepresentativeSprite()
    {
        switch (TypeOfCell)
        {
            case Type.Empty:
                return MinesWeeperGameHandler.spritesArray[0];
            case Type.Mine:
                return MinesWeeperGameHandler.spritesArray[MinesWeeperGameHandler.MINE_INDEX];
            case Type.Mine1:
                return MinesWeeperGameHandler.spritesArray[1];
            case Type.Mine2:
                return MinesWeeperGameHandler.spritesArray[2];
            case Type.Mine3:
                return MinesWeeperGameHandler.spritesArray[3];
            case Type.Mine4:
                return MinesWeeperGameHandler.spritesArray[4];
            case Type.Mine5:
                return MinesWeeperGameHandler.spritesArray[5];
            case Type.Mine6:
                return MinesWeeperGameHandler.spritesArray[6];
            case Type.Mine7:
                return MinesWeeperGameHandler.spritesArray[7];
            case Type.Mine8:
                return MinesWeeperGameHandler.spritesArray[8];
            default:
                return MinesWeeperGameHandler.spritesArray[MinesWeeperGameHandler.COVERED_INDEX];
        }
    }
}
