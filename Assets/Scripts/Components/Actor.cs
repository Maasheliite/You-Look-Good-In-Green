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
    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    private void OnEnable()
    {
        highlight = Instantiate(highlightPrefab,transform);
        highlight.transform.position += Vector3.up;
        highlight.SetActive(false);
    }
    private void Update()
    {
        closestInteractable = GetClosestInteractable();
        HighlightInteractable();
        if (closestInteractable != null)
        {
            if (Input.GetKeyDown(interactButton))
            {
                closestInteractable.Interact(gameObject);
            }
        }
    }
    private void HighlightInteractable()
    {
        if (closestInteractable == null)
        {
            highlight.transform.position = transform.position;
            highlight.SetActive(false);
        }
        else
        {
            closestInteractable.onHighlight();
            highlight.SetActive(true);
            highlight.transform.position = closestInteractable.getGameObject().transform.position+ Vector3.up;
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

