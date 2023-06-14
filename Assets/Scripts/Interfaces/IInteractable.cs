using UnityEngine;
internal interface IInteractable
{
    GameObject getGameObject();
    void Interact(GameObject actor);
    void onHighlight();
}