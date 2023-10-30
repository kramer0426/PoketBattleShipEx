using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptbleObject/Item")]
    public class ItemData : ScriptableObject
    {
        public enum ItemType { Melee, Range, Glove, shoe, Heal }

        [Header("# Main Info")]
        public ItemType itemType_;
        public int itemId_;
        public string itemName_;
        [TextArea]
        public string itemDesc_;
        public Sprite itemIcon_;


        [Header("# Level Data")]
        public float baseDamage_;
        public int baseCount_;
        public float[] damages_;
        public int[] counts_;


        [Header("# Weapon")]
        public GameObject projectile_;
    }
}