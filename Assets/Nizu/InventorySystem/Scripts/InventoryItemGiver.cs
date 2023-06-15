using System.Collections;
using UnityEngine;
namespace Nizu.InventorySystem
{
    public class InventoryItemGiver : MonoBehaviour, IInteractable
    {

        public InventoryItem itemToGive;
        //-1 is infinite
        public int itemAmount = 1;
        public bool removedAfterDepletion = false;

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
                    InventoryItem newItem = Instantiate(itemToGive, actor.transform.position, Quaternion.identity);
                    newItem.gameObject.SetActive(false);
                    bool itemAddedToInventory = inventory.AddItem(newItem);
                    if (itemAddedToInventory)
                    {
                        itemAmount--;
                        if (itemAmount == 0)
                        {
                            canBeInteractedWith = false;
                            if (removedAfterDepletion)
                            {
                                StartCoroutine(FadeOut(0.5f));
                            }
                        }
                    }
                    else
                    {
                        Destroy(newItem);
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

        private IEnumerator FadeOut(float fadeOutTime)
        {
            float timer = 0;
            SpriteRenderer[] SpriteRenderers = getSpriteRenderers();
            float step = 0.1f; // Time interval for each step in the fade-out effect
            int steps = Mathf.CeilToInt(fadeOutTime / step); // Number of steps based on fadeOutTime and step size

            for (int i = 0; i <= steps; i++)
            {
                timer += step;
                float val = Mathf.Lerp(1, 0, timer / fadeOutTime);

                foreach (SpriteRenderer renderer in SpriteRenderers)
                {
                    renderer.material.color = new Color(1, 1, 1, val);
                    renderer.color = new Color(1, 1, 1, val);
                }

                yield return new WaitForSeconds(step);
            }

            this.gameObject.SetActive(false);
        }

        private IEnumerator FadeIn(float fadeInTime)
        {
            float timer = 0;
            SpriteRenderer[] SpriteRenderers = getSpriteRenderers();
            float step = 0.1f; // Time interval for each step in the fade-in effect
            int steps = Mathf.CeilToInt(fadeInTime / step); // Number of steps based on fadeInTime and step size

            for (int i = 0; i <= steps; i++)
            {
                timer += step;
                float val = Mathf.Lerp(0, 1, timer / fadeInTime);

                foreach (SpriteRenderer renderer in SpriteRenderers)
                {
                    renderer.material.color = new Color(1, 1, 1, val);
                    renderer.color = new Color(1, 1, 1, val);
                }

                yield return new WaitForSeconds(step);
            }
        }

        private SpriteRenderer[] getSpriteRenderers()
        {
            SpriteRenderer[] spriteRenderers = GetComponents<SpriteRenderer>();
            if (spriteRenderers == null || spriteRenderers.Length == 0)
            {
                spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            }
            return spriteRenderers;
        }

    }
}
