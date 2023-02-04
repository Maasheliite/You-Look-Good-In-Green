using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Actor : MonoBehaviour
{
    private IInteractable closestInteractable;
    public FloatReference range;
    public GameObject highlightPrefab;
    private GameObject highlight;

    private void OnEnable()
    {
        highlight = Instantiate(highlightPrefab,transform.position+Vector3.up,Quaternion.identity);
        highlight.SetActive(false);
    }
    private void Update()
    {
        closestInteractable = GetClosestInteractable();
        HighlightInteractable();
        if (closestInteractable != null)
        {
            if (Input.GetKeyDown(closestInteractable.interactButton))
            {
                closestInteractable.Interact(gameObject);
            }
        }
    }
    private void HighlightInteractable()
    {
        if (closestInteractable == null)
        {
            highlight.transform.SetParent(transform, false);
            highlight.SetActive(false);
        }
        else
        {
            highlight.SetActive(true);
            highlight.transform.SetParent(closestInteractable.getGameObject().transform, false);
        }
    }
    private IInteractable GetClosestInteractable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range.Value);
       
        Dictionary<IInteractable, float> interactables = new Dictionary<IInteractable, float>();
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log(interactable);
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                interactables[interactable] = distance;
            }
        }

        if (interactables.Count > 0)
        {
            return interactables.OrderBy(x => x.Value).First().Key;
        }
        return null;
    }
}

