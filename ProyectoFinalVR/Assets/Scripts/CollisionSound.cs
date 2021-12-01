using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CollisionSound : MonoBehaviour
{
    [EventRef]
    public string Event = "";
    protected private FMOD.Studio.EventInstance instance;
    private bool hasBeenDropped = false;

    public void dropFromHand()
    {
        hasBeenDropped = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasBeenDropped)
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(Event);
            if (instance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + Event);
            else FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        }
    }
}