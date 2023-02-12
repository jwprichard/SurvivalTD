using System;
using UnityEngine;

namespace Assets.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class ItemScriptableBase : ScriptableObject
    {
        public new string name;
        public ItemType ItemType;

        [SerializeField] private ItemStats _stats;
        public ItemStats BaseStats => _stats;

        public GameObject prefab;
    }

    [Serializable]
    public struct ItemStats
    {
        public int Damage;
        public int KnockBack;
        public float Range;
    }

    public enum ItemType
    {
        basic = 0,
    }
}
