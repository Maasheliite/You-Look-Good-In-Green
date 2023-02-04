using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Actor : MonoBehaviour
{
    private IInteractable closestInteractable;
    public FloatReference range;

    private void Update()
    {
        closestInteractable = GetClosestInteractable(transform);
        
        if (closestInteractable != null)
        {
            if (Input.GetKeyDown(closestInteractable.interactButton))
            {
                closestInteractable.Interact(gameObject);
            }
        }
    }

    private IInteractable GetClosestInteractable(Transform transform)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range.Value);
        Dictionary<IInteractable, float> interactables = new Dictionary<IInteractable, float>();

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;
            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
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

