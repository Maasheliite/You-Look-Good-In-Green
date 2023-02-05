using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum RootState
{
    DAMAGED,
    DANGER,
    HEALTHY
}

public class Root : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public KeyCode interactButton { get; set; } = KeyCode.None;
    public GameObject dangerPrefab;
    private GameObject dangerInstance;
    public UnityEvent loseObjectiveEvent;
    public UnityEvent completeObjectiveEvent;
    private RootState state;
    public FloatReference attackIntervalTimeMinimum;
    public FloatReference attackIntervalTimeMax;
    public FloatReference attackDuration;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (state == RootState.DANGER)
        {
            SetDamaged();
        }else if(state == RootState.HEALTHY)
        {
            SetDanger();
        }
    }
    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void SetDanger()
    {
        state = RootState.DANGER;
        loseObjectiveEvent.Invoke();
        timer = attackDuration.Value;
    }
    public void SetHealthy()
    {
        state = RootState.HEALTHY;
        completeObjectiveEvent.Invoke();
        timer = attackIntervalTimeMinimum.Value + Random.Range(0,attackIntervalTimeMax.Value);
    }
    public void SetDamaged()
    {
        state = RootState.DAMAGED;
    }

    public void Interact(GameObject actor)
    {
        if (state == RootState.DAMAGED)
        {
            //take essence to heal or not enough warning 
        }
        else if (state == RootState.DANGER)
        {
            //tell to kill bugs
        }
        else if (state == RootState.HEALTHY)
        {
            //do nothing
        }
    }

    
}
