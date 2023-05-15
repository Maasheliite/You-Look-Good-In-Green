using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;

    public Dialogue Greeting;
    public Dialogue Waiting;
    public Dialogue Healed;

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
        Start,
        Finish
    }

    QuestState questState;
    [field: SerializeField] AnimalList AnimalFromList;


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

        }
        //show neccessary resource if you've talked to them
    }


    private bool IsMissionFinished(Animal.AnimalList AnimalName)
    {
        bool success = false;
        if (AnimalName == Animal.AnimalList.Bear)
        {
            //requirements - healing + food

            success = false;
        }
        else if (AnimalName == Animal.AnimalList.Bunny)
        {
            //requirements - light
            success = false;

        }
        else if (AnimalName == Animal.AnimalList.Deer)
        {
            //requirements - branches
            success = false;
        }
        else if (AnimalName == Animal.AnimalList.Eagle)
        {
            //requirements - grass blades
            success = false;
        }
        else if (AnimalName == Animal.AnimalList.Owl)
        {
            //requirements - healing
            success = false;
        }
        else if (AnimalName == Animal.AnimalList.Snail)
        {
            //requirements - water
            success = false;
        }

        return success;
    }
}
