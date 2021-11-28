using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

namespace Sonido
{
    public class EventPlayer : MonoBehaviour
    {
        [EventRef]
        public string Event = "";
        protected private FMOD.Studio.EventInstance instance;
        protected private bool loaded = true;

        public virtual void playEvent()
        {

        }
    }
}
