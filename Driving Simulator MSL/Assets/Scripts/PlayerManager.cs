using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public enum GearState
    {
        Drive,
        Reverse,
        Neutral,
        Park
    }

    [HideInInspector] public GearState currentState = GearState.Park;

    public static PlayerManager Instance;

    [HideInInspector] public bool isEngineStarted = false;

    //public static event Action OnGearShift;
    //public UnityEvent OnGearShiftEvent;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = GearState.Park;
        isEngineStarted = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchGear(true);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGear(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isEngineStarted)
            {
                isEngineStarted = true;
                UIManager.Instance.UpdateText(UIManager.Instance.engineText, "Engine: On");
            } else {
                isEngineStarted = false;
                UIManager.Instance.UpdateText(UIManager.Instance.engineText, "Engine: Off");
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.Instance.ReloadScene();
        }
    }

    private void SwitchGear(bool forwardShift)
    {
        //OnGearShift?.Invoke();
        //OnGearShiftEvent?.Invoke();

        switch (currentState)
        {
            case GearState.Drive:
                if (forwardShift)
                {
                    currentState = GearState.Neutral;
                } else { currentState = GearState.Park;}
                break;
            case GearState.Neutral:
                if (forwardShift)
                {
                    currentState = GearState.Reverse;
                }
                else { currentState = GearState.Drive; }
                break;
            case GearState.Reverse:
                if (forwardShift)
                {
                    currentState = GearState.Park;
                }
                else { currentState = GearState.Neutral; }
                break;
            case GearState.Park:
                if (forwardShift)
                {
                    currentState = GearState.Drive;
                }
                else { currentState = GearState.Reverse; }
                break;
        }

        UIManager.Instance.UpdateText(UIManager.Instance.gearText, "Gear: " + currentState.ToString());
    }


}
