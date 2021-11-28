using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{
    [SerializeField]
    private string eventPath;
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.PLAYBACK_STATE state;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    public void playEvent()
    {
        instance.getPlaybackState(out state);
        if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            if (instance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + eventPath);
        }
        else instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
