using UnityEngine;
using Assets.Scripts.Utilities;

public class MouseManager : Singleton<MouseManager>
{
    public void Update()
    {
        test();
    }

    private void test()
    {
        Vector2 mousePosition = UtilsClass.GetMouseWorldPosition();
        //player.transform.position = MapManager.Instance.MapGrid.GetGridObject(mousePosition).GetCenterPosition();
    }
}
