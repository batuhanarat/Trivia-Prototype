using Events;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Audio Event Channel")]
public class AudioEventChannelSO : ScriptableObject
{
    public UnityAction<AudioEvent> OnEventRaised;

    public void Raise(AudioEvent audioEvent)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(audioEvent);
        }
    }
}