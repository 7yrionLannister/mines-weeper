using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public const float MAX_HEIGHT_PIXELS = 6.5f;

    private Grid<MapTileGridObject> grid;
    private int revealedTiles;
    private int mines;

    public Map(int width, int height, Vector3 origin) {
        int maxDim = System.Math.Max(width, height);
        grid = new Grid<MapTileGridObject>(width, height, 6.5f / maxDim, origin, (Grid<MapTileGridObject> g, int x, int y) => new MapTileGridObject(g, x, y));
        mines = (int)(width * height * 0.15f); //15% of cells will be mined
        MineMap();
        revealedTiles = 0;
    }

    private void MineMap()
    {
        int cont = mines;
        while (cont > 0)
        {
            int x = Random.Range(0, grid.Width);
            int y = Random.Range(0, grid.Height);
            if (grid.GetGridObject(x, y).TypeOfCell == MapTileGridObject.Type.Empty)
            {
                grid.GetGridObject(x, y).TypeOfCell = MapTileGridObject.Type.Mine;
                cont--;
            }
        }
        CountMines();
    }

    private void CountMines()
    {
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                if (grid.GetGridObject(i, j).TypeOfCell != MapTileGridObject.Type.Mine)
                {
                    int surroundingMines = 0;
                    if (i > 0) //Has left tiles
                    {
                        if (j > 0) //Has down tiles
                        {
                            surroundingMines += grid.GetGridObject(i - 1, j - 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        }
                        surroundingMines += grid.GetGridObject(i - 1, j).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        if (j < grid.Height - 1) //Has up tiles
                        {
                            surroundingMines += grid.GetGridObject(i - 1, j + 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        }
                    }

                    if (j > 0) //Has down tiles
                    {
                        surroundingMines += grid.GetGridObject(i, j - 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                    }

                    if (i < grid.Width - 1) //Has right tiles
                    {
                        if (j > 0) //Has down tiles
                        {
                            surroundingMines += grid.GetGridObject(i + 1, j - 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        }
                        surroundingMines += grid.GetGridObject(i + 1, j).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        if (j < grid.Height - 1) //Has up tiles
                        {
                            surroundingMines += grid.GetGridObject(i + 1, j + 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                        }
                    }

                    if (j < grid.Height - 1) //Has up tiles
                    {
                        surroundingMines += grid.GetGridObject(i, j + 1).TypeOfCell == MapTileGridObject.Type.Mine ? 1 : 0;
                    }

                    MapTileGridObject.Type TypeOfCell = MapTileGridObject.Type.Empty;
                    switch (surroundingMines)
                    {
                        case 1:
                            TypeOfCell = MapTileGridObject.Type.Mine1;
                            break;
                        case 2:
                            TypeOfCell = MapTileGridObject.Type.Mine2;
                            break;
                        case 3:
                            TypeOfCell = MapTileGridObject.Type.Mine3;
                            break;
                        case 4:
                            TypeOfCell = MapTileGridObject.Type.Mine4;
                            break;
                        case 5:
                            TypeOfCell = MapTileGridObject.Type.Mine5;
                            break;
                        case 6:
                            TypeOfCell = MapTileGridObject.Type.Mine6;
                            break;
                        case 7:
                            TypeOfCell = MapTileGridObject.Type.Mine7;
                            break;
                        case 8:
                            TypeOfCell = MapTileGridObject.Type.Mine8;
                            break;
                    }
                    grid.GetGridObject(i, j).TypeOfCell = TypeOfCell;
                }
            }
        }
    }

    public void Flag(Vector3 mousePosition)
    {
        int x, y;
        grid.GetXY(mousePosition, out x, out y);
        if (x >= 0 && x < grid.Width && y >= 0 && y < grid.Height && !grid.GetGridObject(x, y).Revealed)
        {
            grid.GetGridObject(x, y).Tile.GetComponent<SpriteRenderer>().sprite = MinesWeeperGameHandler.spritesArray[MinesWeeperGameHandler.FLAG_INDEX];
        }
    }

    public MapTileGridObject.Type Reveal(Vector3 mousePosition)
    {
        int x, y;
        grid.GetXY(mousePosition, out x, out y);
        if (x >= 0 && x < grid.Width && y >= 0 && y < grid.Height)
        {
            MapTileGridObject mgo = grid.GetGridObject(x, y);

            FloodFill(mgo);

            return mgo.TypeOfCell;
        }

        return default;
    }

    private void FloodFill(MapTileGridObject current)
    {
        if (current.Revealed || current.TypeOfCell == MapTileGridObject.Type.Mine)
        {
            return;
        }

        int x = current.X;
        int y = current.Y;
        current.Reveal();
        revealedTiles++;

        if (current.TypeOfCell == MapTileGridObject.Type.Empty)
        {
            if (current.X > 0) //Has left tiles
            {
                FloodFill(grid.GetGridObject(x - 1, y));
            }
            if (x < grid.Width - 1) //Has right tiles
            {
                FloodFill(grid.GetGridObject(x + 1, y));
            }
            if (y > 0) //Has down tiles
            {
                FloodFill(grid.GetGridObject(x, y - 1));
            }
            if (y < grid.Height - 1) //Has up tiles
            {
                FloodFill(grid.GetGridObject(x, y + 1));
            }
        }
    }

    public void Uncover()
    {
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                grid.GetGridObject(i, j).Reveal();
            }
        }
    }

    public int RevealedTiles
    {
        get
        {
            return revealedTiles;
        }
    }

    public int Width
    {
        get
        {
            return grid.Width;
        }
    }

    public int Height
    {
        get
        {
            return grid.Height;
        }
    }

    public int Mines
    {
        get
        {
            return mines;
        }
    }
}
