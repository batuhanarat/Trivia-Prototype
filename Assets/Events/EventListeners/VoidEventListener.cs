using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class VoidEvent : UnityEvent
{

}

public class VoidEventListener : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _channel = default;

    public VoidEvent OnEventRaised;
    public void OnEnable()
    {
        _channel.OnEventRaised += Response;
    }
        
    public void OnDisable()
    {
        _channel.OnEventRaised -= Response;
    }

    public void Response()
    {
        if(OnEventRaised != null)
            OnEventRaised.Invoke();
    }
        

}
