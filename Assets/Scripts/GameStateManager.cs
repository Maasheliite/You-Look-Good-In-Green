using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private float completedObjectives;
    public void completeObjective()
    {
        completedObjectives += 1;
    }
    public void loseObjective()
    {
        completedObjectives -= 1;
    }

    public bool checkWin()
    {
        if (completedObjectives == 4)
        {
            return true;
        }
        return false;
    }
}
