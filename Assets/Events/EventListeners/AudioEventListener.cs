using Events;
using UnityEngine;

public class AudioEventListener : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO audioEventChannel = default;

    private void OnEnable()
    {
        if (audioEventChannel != null)
            audioEventChannel.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        if (audioEventChannel != null)
            audioEventChannel.OnEventRaised -= Respond;
    }

    private void Respond(AudioEvent audioEvent)
    {
        SoundManager.Instance.PlayAudioEffect(audioEvent.audioClip);
    }
}