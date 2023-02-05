using Nizu.Util.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public Slider slider;
    public GameObject dangerPrefab;
    private GameObject dangerInstance;
    public UnityEvent loseObjectiveEvent;
    public UnityEvent completeObjectiveEvent;
    private RootState state;
    public FloatReference attackIntervalTimeMinimum;
    public FloatReference attackIntervalTimeMax;
    public FloatReference attackDuration;
    public GameCommand essenceResource;
    public float essenceCost;
    private float timer;


    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        slider = GetComponentInChildren<Slider>();    
        SetDanger();
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            slider.value = timer;
        }
        else if (state == RootState.DANGER)
        {
            SetDamaged();
        }else if(state == RootState.HEALTHY)
        {
            SetDanger();
        }

        if (state == RootState.DANGER && dangerInstance == null)
        {
            SetHealthy();
        }

    }

    public string getStatePlusTimeRemaining()
    {
        return Enum.GetName(typeof(RootState),state).ToString() + ": " + timer;
    }
    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void SetDanger()
    {
        dangerInstance = Instantiate(dangerPrefab);
        state = RootState.DANGER;
        loseObjectiveEvent.Invoke();
        slider.enabled = true;
        timer = attackDuration.Value;
        slider.maxValue = timer;
        spriteRenderer.color = new Color(255,127,0);
    }
    public void SetHealthy()
    {
        state = RootState.HEALTHY;
        completeObjectiveEvent.Invoke();
        slider.enabled = false;
        timer = attackIntervalTimeMinimum.Value + Random.Range(0,attackIntervalTimeMax.Value);
        spriteRenderer.color = Color.white;
    }
    public void SetDamaged()
    {
        state = RootState.DAMAGED;
        slider.enabled = false;
        spriteRenderer.color = Color.red;
    }

    public void Interact(GameObject actor)
    {
        if (state == RootState.DAMAGED)
        {
           if (essenceResource.DecreaseValueUntilZero(essenceCost))
            {
                SetHealthy();
            } else
            {
                //tell not enough essence
            }
        }
        else if (state == RootState.DANGER)
        {
           if (dangerInstance != null)
            {
                //tell to kill bugs
            }
            else
            {
                //do nothing
            }
        }
        else if (state == RootState.HEALTHY)
        {
            //do nothing
        }
    }

    
}
