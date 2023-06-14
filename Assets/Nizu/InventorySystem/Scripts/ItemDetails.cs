using UnityEngine;

namespace Nizu.InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/ItemDetails")]
    public class ItemDetails : ScriptableObject
    {
        public Sprite inventorySprite;
        public bool isStackable;
        public string itemType;
        [Range(1, 100)]
        public int maxStackSize;
        public ItemEffectOptions effect;
    }
}