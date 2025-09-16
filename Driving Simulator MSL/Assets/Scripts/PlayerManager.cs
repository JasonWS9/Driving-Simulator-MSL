using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum GearState
    {
        Drive,
        Reverse,
        Neutral,
        Park
    }

    public GearState currentState = GearState.Park;

    public static PlayerManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwitchGear(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchGear(false);
        }
    }

    private void SwitchGear(bool forwardShift)
    {
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

        Debug.Log(currentState);
    }
}
