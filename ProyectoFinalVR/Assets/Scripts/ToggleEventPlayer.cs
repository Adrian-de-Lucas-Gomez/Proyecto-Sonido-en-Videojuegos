using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sonido
{
    public class ToggleEventPlayer : EventPlayer
    {
        private FMOD.Studio.PLAYBACK_STATE state;

        public void Update()
        {
            if (loaded)
            {
                instance.getPlaybackState(out state);
                if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                }
            }

        }

        public override void playEvent()
        {
            instance.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                instance = FMODUnity.RuntimeManager.CreateInstance(Event);
                if (loaded = (instance.start() == FMOD.RESULT.OK)) Debug.Log("Couldn't play sound: " + Event);
                else FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            }
            else
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
            }
        }
    }
}
