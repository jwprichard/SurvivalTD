using UnityEngine;
using Assets.Scripts.Utilities;

public class MapManager : Singleton<MapManager>
{
    private Grid<MapGridObject> _mapGrid;
    public Grid<MapGridObject> MapGrid { get =>  _mapGrid; }
    public void Initialize()
    {
        _mapGrid = new(10, 10, 4, new(-20, -20, 0), (Grid<MapGridObject> g, int x, int y) => new MapGridObject(g, x, y), true);
    }

}

public class MapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private Grid<MapGridObject> grid;
    private int x;
    private int y;
    private int value = 0;

    public MapGridObject(Grid<MapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public Vector3 GetCenterPosition()
    {
        return new Vector3(x * grid.cellSize + grid.cellSize/2, y * grid.cellSize + grid.cellSize / 2) + grid.originPosition;
    }

    public override string ToString()
    {
        return x + "," + y;
    }
}
