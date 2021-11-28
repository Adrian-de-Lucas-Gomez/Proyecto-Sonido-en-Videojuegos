using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FMODUnity
{
    public class ToggleEventPlayer : MonoBehaviour
    {
        [EventRef]
        public string Event = "";
        private FMOD.Studio.EventInstance instance;
        private FMOD.Studio.PLAYBACK_STATE state;

        public void Update()
        {
            instance.getPlaybackState(out state);
            if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            }
        }

        public void playEvent()
        {
            instance.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                instance = FMODUnity.RuntimeManager.CreateInstance(Event);
                if (instance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + Event);
                else FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            }
            else instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
