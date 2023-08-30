using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/VoidEvent", fileName = "VoidEvent")]
public class VoidEventChannelSO : ScriptableObject
{

    public UnityAction OnEventRaised;

    public void Invoke()
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke();
        }
    }
}
