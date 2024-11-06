using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action TriedToInteract;
    public static event Action SitEvent;

    public static void OnTriedToInteract()
    {
        TriedToInteract?.Invoke();
    }

    public static void OnSit()
    {
        SitEvent?.Invoke();
    }
}
