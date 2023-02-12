using UnityEngine;
//using Assets.Units;
using System.Linq;

public class ResourceSystem : Singleton<ResourceSystem>
{
    //public List<UnitScriptableBase> Units = new();
    //private Dictionary<UnitType, UnitScriptableBase> _units = new();

    protected override void Awake()
    {
        base.Awake();
        //AssembleResources();
    }

    //private void AssembleResources()
    //{
    //    Units = Resources.LoadAll<UnitScriptableBase>("Units").ToList();
    //    _units = Units.ToDictionary(r => r.unitType, r => r);
    //}

    //public UnitScriptableBase GetUnit(UnitType t) => _units[t];
}
