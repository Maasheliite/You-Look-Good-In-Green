using UnityEngine;
namespace Nizu.InventorySystem
{
    public class InventoryItemGiver : MonoBehaviour, IInteractable
    {

        public InventoryItem itemToGive;
        public int itemAmount = 0;

        private bool canBeInteractedWith = true;

        public GameObject getGameObject()
        {
            return gameObject;
        }

        public void Interact(GameObject actor)
        {
            if (canBeInteractedWith)
            {
                Inventory inventory = actor.GetComponent<Inventory>();
                if (inventory != null)
                {
                    bool itemAddedToInventory = inventory.AddItem(itemToGive);
                    if (itemAddedToInventory)
                    {
                        itemAmount--;
                        if (itemAmount <= 0)
                        {
                            canBeInteractedWith = false;
                        }
                    }
                    else
                    {
                        //item cant be picked up, inventory full.
                    }
                }
                else
                {
                    //no ivnentory found
                }
            }
        }

        public void onHighlight()
        {
            if (canBeInteractedWith)
            {
                //highlight?
            }
        }
    }
}
