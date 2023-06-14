using UnityEngine;
namespace Nizu.InventorySystem
{
    public class InventoryItemTrader : MonoBehaviour, IInteractable
    {

        public ItemDetails requiredItem;
        public InventoryItem itemToGive;
        //-1 is infinite
        public int tradeAmount = -1;

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
                    if (inventory.removeItemOfType(requiredItem, 1))
                    {
                        bool itemAddedToInventory = inventory.AddItem(itemToGive);
                        if (itemAddedToInventory)
                        {
                            tradeAmount--;
                            if (tradeAmount == 0)
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
                        //required item not in inventory.
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
