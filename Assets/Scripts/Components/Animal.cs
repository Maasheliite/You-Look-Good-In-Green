using Nizu.Util.ScriptableObjects;
using UnityEngine;
using Nizu.InventorySystem;

public class Animal : MonoBehaviour, IInteractable
{


    public Dialogue Greeting;
    public Dialogue Waiting;
    public Dialogue Healed;

    public Transform Player;


    public ItemDetails BerryResource;
    public ItemDetails FireflyResource;
    public ItemDetails stickResource;
    public ItemDetails WaterResourse;
    public ItemDetails SapResource;

    public Inventory inventory;

    public Animator animator;

    enum AnimalList
    {
        Bear,
        Deer,
        Snail,
        Owl,
        Eagle,
        Bunny
    }

    enum QuestState
    {
        None,
        Start,
        Finish
    }

    QuestState questState = QuestState.None;
    [field: SerializeField] AnimalList AnimalFromList;

    //bear variables
    private bool BearHeal;
    private bool BearFed;

    //bunny variables
    private bool BunnyLit;

    //deer variables
    private bool DeerStick;
    private bool DeerSap;

    //eagle variable
    private bool WingFixed;
    private bool EagleHeal;

    //owl variable
    private bool OwlHeal;

    //snail variables
    private bool SnailWater;
    private bool SnailHeal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
        if (questState == QuestState.Start)
        {
            if (IsMissionFinished(AnimalFromList))
            {
                questState = QuestState.Finish;
                PlayerMovement.SkillPoints++;
            }
            else
            {
                FindObjectOfType<DialogueManager>().StartDialogue(Waiting);
            }
        }
        else if (questState == QuestState.Finish)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(Healed);

        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(Greeting);
            questState = QuestState.Start;
        }
    }

    public void onHighlight()
    {
        if (questState == QuestState.Start)
        {
            if (AnimalFromList == Animal.AnimalList.Bear)
            {
                if (Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanHeal && Input.GetKeyDown(KeyCode.H) && !BearHeal)
                {
                    BearHeal = true;
                }
                if (inventory.removeItemOfType(BerryResource, 1) && !BearFed)
                {

                    BearFed = true;
                }
                //requirements - healing + food
            }
            else if (AnimalFromList == Animal.AnimalList.Bunny)
            {
                if (inventory.removeItemOfType(FireflyResource, 3) && !BunnyLit)
                {
                    BunnyLit = true;
                }
                //requirements - light
            }
            else if (AnimalFromList == Animal.AnimalList.Deer)
            {
                if (inventory.removeItemOfType(stickResource, 2) && !DeerStick)
                {
                    DeerStick = true;
                }
                if (inventory.removeItemOfType(SapResource, 1) && !DeerSap)
                {
                    DeerSap = true;
                }
                //requirements - branches
            }
            else if (AnimalFromList == Animal.AnimalList.Eagle)
            {
                if (Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanShoot && Input.GetButtonDown("Fire1") && !WingFixed)
                {
                    WingFixed = true;
                }
                if (Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanHeal && Input.GetKeyDown(KeyCode.H) && !EagleHeal)
                {
                    EagleHeal = true;
                }
                //requirements - grass blades
            }
            else if (AnimalFromList == Animal.AnimalList.Owl)
            {
                if (Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanHeal && Input.GetKeyDown(KeyCode.H) && !OwlHeal)
                {
                    OwlHeal = true;
                }
                //requirements - healing
            }
            else if (AnimalFromList == Animal.AnimalList.Snail)
            {

                if (inventory.removeItemOfType(WaterResourse, 1) && !SnailWater)
                {
                    SnailWater = true;
                }
                if (Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanHeal && Input.GetKeyDown(KeyCode.H) && !SnailHeal)
                {
                    SnailHeal = true;
                }
                //requirements - water
            }
        }
    }


    private bool IsMissionFinished(Animal.AnimalList AnimalName)
    {
        bool success = false;
        if (AnimalName == Animal.AnimalList.Bear)
        {
            //requirements - healing + food
            if (BearHeal && BearFed)
            {
                success = true;
                animator.Play("BearMid");

            }
        }
        else if (AnimalName == Animal.AnimalList.Bunny)
        {
            //requirements - light
            if (BunnyLit)
            {
                success = true;
                animator.Play("BunnyMid");
            }
        }
        else if (AnimalName == Animal.AnimalList.Deer)
        {
            if (DeerStick && DeerSap)
            {
                success = true;
                animator.Play("DeerMid");

            }
            //requirements - branches
        }
        else if (AnimalName == Animal.AnimalList.Eagle)
        {
            if (WingFixed && EagleHeal)
            {
                success = true;
                animator.Play("EagleMid");

            }

            //requirements - grass blades
        }
        else if (AnimalName == Animal.AnimalList.Owl)
        {
            if (OwlHeal)
            {
                success = true;
                animator.Play("OwlMid");

            }

            //requirements - healing
        }
        else if (AnimalName == Animal.AnimalList.Snail)
        {
            if (SnailWater && SnailHeal)
            {
                success = true;
                animator.Play("SnailMid");

            }
            //requirements - water
        }

        return success;
    }
}
