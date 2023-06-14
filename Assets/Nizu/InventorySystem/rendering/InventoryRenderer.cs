using UnityEngine;
using UnityEngine.UI;

namespace Nizu.InventorySystem
{
    public class InventoryRenderer : MonoBehaviour
    {
        public Inventory inventoryToRender;

        public GameObject SelectorPrefab;
        public GameObject itemPrefab;


        private void Start()
        {
            updateInventory();
        }

        public void updateInventory()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject slot = transform.GetChild(i).gameObject;
                for (int j = 0; j < slot.transform.childCount; j++)
                {
                    GameObject item = slot.transform.GetChild(j).gameObject;
                    Destroy(item);
                }
                if (i >= 0 && i < inventoryToRender.items.Count)
                {
                    if (inventoryToRender.items[i] != null)
                    {
                        GameObject item = Instantiate(itemPrefab, slot.transform);
                        item.GetComponent<Image>()
                            .sprite = inventoryToRender.items[i].itemDetails.inventorySprite;
                        item.GetComponentInChildren<TMPro.TMP_Text>().text = ""+inventoryToRender.items[i].stackSize;


                    }

                }
                if (inventoryToRender.selectedSlot == i)
                {
                    Instantiate(SelectorPrefab, slot.transform);
                }

            }
        }
    }
}