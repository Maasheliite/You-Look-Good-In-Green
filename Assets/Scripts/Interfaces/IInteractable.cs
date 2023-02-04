using UnityEngine;

internal interface IInteractable
{
    KeyCode interactButton { get; set; }

    void Interact(GameObject actor);
}