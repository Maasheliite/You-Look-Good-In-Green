using System.Collections;
using UnityEngine;

namespace Nizu.InventorySystem
{
    public class InventoryItem : MonoBehaviour, IInteractable
    {
        public ItemDetails itemDetails;
        public int stackSize;

        private bool canBePickedUp = true;

        public void OnEnable()
        {
            canBePickedUp = true;
            StartCoroutine(FadeIn(0.5f));
        }
        public GameObject getGameObject()
        {
            return gameObject;
        }

        public void Interact(GameObject actor)
        {
            if (canBePickedUp)
            {
                Inventory inventory = actor.GetComponent<Inventory>();
                if (inventory != null)
                {
                    bool itemAddedToInventory = inventory.AddItem(this);
                    if (itemAddedToInventory)
                    {
                        canBePickedUp = false;
                        StartCoroutine(FadeOut(0.5f));
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
            // do nothing
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