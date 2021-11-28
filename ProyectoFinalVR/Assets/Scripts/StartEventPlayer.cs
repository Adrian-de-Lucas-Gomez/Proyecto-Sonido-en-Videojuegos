using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sonido
{
    public class StartEventPlayer : EventPlayer
    {
        public bool canBePickedUp = false;
        public void Start()
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(Event);
            if (loaded = (instance.start() == FMOD.RESULT.OK)) FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            else Debug.Log("Couldn't play sound: " + Event);
        }

        public void Update()
        {
            if (canBePickedUp && loaded) instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }
    }
}