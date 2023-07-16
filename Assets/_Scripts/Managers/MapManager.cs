using UnityEngine;
using Assets.Scripts.Utilities;
using Assets.Units;

public class MapManager : Singleton<MapManager>
{
    [Header("Listening To")]
    [SerializeField] private GameStateEventChannelSO _onGameStateChange;

    private Grid<MapTile> _mapGrid;
    public Grid<MapTile> MapGrid { get =>  _mapGrid; }

    private void OnEnable()
    {
        _onGameStateChange.OnEventRaised += Initialize;
    }
    public void Initialize(GameState state)
    {
        VariableManager.Instance.Height = 10;
        VariableManager.Instance.Width = 10;
        if (state != GameState.Initialise) return;
        _mapGrid = new(VariableManager.Instance.Width, VariableManager.Instance.Height, 4, new(-20, -20, 0), (Grid<MapTile> g, int x, int y) => new MapTile(g, x, y), true);
    }

}

public class MapTile
{
    private Grid<MapTile> grid;
    private readonly int x;
    private readonly int y;
    private GameObject unit;

    public MapTile(Grid<MapTile> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public GameObject GetUnit()
    {
        return unit;
    }

    public void SetUnit(GameObject unit)
    {
        this.unit = unit;
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
