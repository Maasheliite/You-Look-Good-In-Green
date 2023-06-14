using UnityEngine;
using UnityEngine.Events;

namespace Nizu.InventorySystem
{
    public class InventoryItemConsumer : MonoBehaviour, IInteractable
    {

        public ItemDetails itemToConsume;
        //-1 is infinite
        public int interactAmount = -1;
        public UnityEvent consumptionEvent;
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
                    if (inventory.removeItemOfType(itemToConsume, 1))
                    {
                        consumptionEvent.Invoke();
                        interactAmount--;
                        if (interactAmount == 0)
                        {
                            canBeInteractedWith = false;
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
