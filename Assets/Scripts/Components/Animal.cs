using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nizu.Util.ScriptableObjects;


public class Animal : MonoBehaviour, IInteractable
{


    public Dialogue Greeting;
    public Dialogue Waiting;
    public Dialogue Healed;

    public Transform Player;

    public GameCommand BerryResource;
    public GameCommand FireflyResource;
    public GameCommand stickResource;
    public GameCommand WaterResourse;
    public GameCommand SapResource;


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
        if(questState == QuestState.Start)
        {
            if (AnimalFromList == Animal.AnimalList.Bear)
            {
                if(Vector2.Distance(Player.transform.position, gameObject.transform.position) < 10f && PlayerMovement.CanHeal && Input.GetKeyDown(KeyCode.H) && !BearHeal)
                {
                    BearHeal = true;
                }
                if (BerryResource.DecreaseValueUntilZero(3) && !BearFed)
                {
                    BearFed = true;
                }
                //requirements - healing + food
            }
            else if (AnimalFromList == Animal.AnimalList.Bunny)
            {
                if (FireflyResource.DecreaseValueUntilZero(5) && !BunnyLit)
                {
                    BunnyLit = true;
                }
                //requirements - light
            }
            else if (AnimalFromList == Animal.AnimalList.Deer)
            {
                if (stickResource.DecreaseValueUntilZero(2) && !DeerStick)
                {
                    DeerStick = true;
                }
                if (SapResource.DecreaseValueUntilZero(1) && !DeerSap)
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

                if (WaterResourse.DecreaseValueUntilZero(1) && !SnailWater)
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
            }
        }
        else if (AnimalName == Animal.AnimalList.Bunny)
        {
            //requirements - light
            if (BunnyLit)
            {
                success = true;
            }
        }
        else if (AnimalName == Animal.AnimalList.Deer)
        {
            if (DeerStick && DeerSap)
            {
                success = true;
            }
            //requirements - branches
        }
        else if (AnimalName == Animal.AnimalList.Eagle)
        {
            if (WingFixed && EagleHeal)
            {
                success = true;
            }

            //requirements - grass blades
        }
        else if (AnimalName == Animal.AnimalList.Owl)
        {
            if (OwlHeal)
            {
                success = true;
            }

            //requirements - healing
        }
        else if (AnimalName == Animal.AnimalList.Snail)
        {
            if (SnailWater && SnailHeal)
            {
                success = true;
            }
            //requirements - water
        }

        return success;
    }
}
