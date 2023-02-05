using UnityEngine;
internal interface IInteractable
{
    KeyCode interactButton { get; set; }
    GameObject getGameObject();
    void Interact(GameObject actor);
}