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
    private bool exeecedTreshold = false;
    private Rigidbody rb = null;
    [SerializeField]
    private float velocityForSound = 0.5f;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void dropFromHand()
    {
        //hasBeenDropped = true;


        //lol
    }

    public void Update()
    {
        if (!exeecedTreshold) exeecedTreshold = rb.velocity.magnitude > velocityForSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (exeecedTreshold)
        {
            hasBeenDropped = false;
            exeecedTreshold = false;
            instance = FMODUnity.RuntimeManager.CreateInstance(Event);
            if (instance.start() != FMOD.RESULT.OK) Debug.Log("Couldn't play sound: " + Event);
            else FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
        }
    }
}