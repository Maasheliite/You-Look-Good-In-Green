using UnityEngine;

public class ChildScript : MonoBehaviour
{
    public void TriggerAnimationAndDestroy()
    {
        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("ThornRetract");
        }
;
        Destroy(gameObject, 0.5f);
    }
}