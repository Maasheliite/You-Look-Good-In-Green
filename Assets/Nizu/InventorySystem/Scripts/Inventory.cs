using Nizu.Util.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Nizu.InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public int slotCount;
        public List<InventoryItem> items;
        public GameEvent inventoryUpdated;
        public int selectedSlot = 0;
        [SerializeField] private KeyCode useButton = KeyCode.None;
        [SerializeField] private KeyCode dropButton = KeyCode.None;

        private void Awake()
        {
            for (int i = 0; i < slotCount; i++)
            {
                items.Add(null);
            }
        }
        private void Update()
        {
            CheckUseButton();
            CheckDropButton();
            CheckNumberKeys();
            CheckScrollWheel();
        }

        private void CheckDropButton()
        {
            if (Input.GetKeyDown(dropButton))
            {
                if (items[selectedSlot] != null)
                {
                    items[selectedSlot].gameObject.SetActive(true);
                    items[selectedSlot].transform.position = transform.position;
                    RemoveItem();
                }
            }
        }

        private void CheckUseButton()
        {
            if (Input.GetKeyDown(useButton))
            {
                if (items[selectedSlot] != null)
                {
                    ItemEffect.DoAction(items[selectedSlot].itemDetails.effect,
                        items[selectedSlot].itemDetails.actionTarget,
                        items[selectedSlot].itemDetails.actionValue);
                }
            }
        }
        private void CheckScrollWheel()
        {
            if (Input.mouseScrollDelta.y < 0)
            {
                SelectPreviousSlot();
                inventoryUpdated.Raise();
            }
            else if (Input.mouseScrollDelta.y > 0)
            {
                SelectNextSlot();
                inventoryUpdated.Raise();
            }
        }

        private void CheckNumberKeys()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetSelection(9);
                inventoryUpdated.Raise();
            }

            for (int i = 1; i < slotCount; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    selectedSlot = i - 1;
                    inventoryUpdated.Raise();
                }
            }
        }

        public bool AddItem(InventoryItem item)
        {
            for (int i = 0; i < slotCount; i++)
            {
                if (i < items.Count && items[i] is not null)
                {
                    if (items[i].itemDetails.itemType == item.itemDetails.itemType)
                    {
                        return AddItem(item, i);
                    }
                }
            }
            bool isItemAdded = false;
            for (int j = 0; j < slotCount && !isItemAdded; j++)
            {
                    inventoryUpdated.Raise();
                    isItemAdded = AddItem(item, j);
                if (isItemAdded)
                {
                    return true;
                }
            }

            Debug.LogWarning("Inventory is full.");
            return false;
        }
    
        public bool AddItem(InventoryItem item, int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= slotCount)
            {
                Debug.LogWarning("Invalid slot index.");
                return false;
            }

            if (slotIndex < items.Count && items[slotIndex] is not null)
            {
                if (items[slotIndex].itemDetails.itemType != item.itemDetails.itemType)
                {
                    Debug.LogWarning("Slot is already occupied with a different item type.");
                    return false;
                }
                else if (!items[slotIndex].itemDetails.isStackable)
                {
                    Debug.LogWarning("Slot is already occupied with a non-stackable item.");
                    return false;
                }

                items[slotIndex].stackSize += item.stackSize;
            }
            else
            {
                items[slotIndex] = item;
            }

            inventoryUpdated.Raise();
            return true;
        }

        public bool RemoveItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= slotCount)
            {
                Debug.LogWarning("Invalid slot index.");
                return false;
            }

            if (items[slotIndex] == null)
            {
                Debug.LogWarning("Slot is already empty.");
                return false;
            }

            items[slotIndex].gameObject.SetActive(true);
            items[slotIndex] = null;

            inventoryUpdated.Raise();
            return true;
        }

        public bool RemoveItem()
        {
            return RemoveItem(selectedSlot);
        }

        public void SetSelection(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= slotCount)
            {
                Debug.LogWarning("Invalid slot index when setting selection.");
                return;
            }

            inventoryUpdated.Raise();
            selectedSlot = slotIndex;
        }

        public void SelectPreviousSlot()
        {
            if (selectedSlot - 1 < 0)
            {
                SetSelection(slotCount - 1);
            }
            else
            {
                SetSelection(selectedSlot - 1);
            }
        }

        public void SelectNextSlot()
        {
            if (selectedSlot + 1 >= slotCount)
            {
                SetSelection(0);
            }
            else
            {
                SetSelection(selectedSlot + 1);
            }
        }
    }
}