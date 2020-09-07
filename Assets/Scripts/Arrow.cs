using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : XRGrabInteractable
{
    [Tooltip("How fast the arrow should fly")]
    public float speed = 2000f;

    [Tooltip("The head of the arrow.")]
    public Transform tip = null;

    [Tooltip("The trail of the arrow.")]
    public TrailRenderer trail;

    [Tooltip("The release sound the arrow makes when shot.")]
    public AudioClip[] audioClips;

    //Is the arrow flying
    private bool inAir = false;

    //Arrows position
    private Vector3 lastPosition = Vector3.zero;

    //Arrows rigidbody
    private Rigidbody rigidBody = null;

    // Audiosource for the arrows release sound.
    AudioSource audioSource;
    

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

            if (trail != null)
            {
                trail.enabled = false; 
            }
    }

    private void FixedUpdate()
    {
        if (inAir)
        {
            CheckForCollision();
            lastPosition = tip.position;
        }
    }

    //Check to see if the arrow has collided with something, then stop it from flying.
    private void CheckForCollision()
    {
        if (Physics.Linecast(lastPosition, tip.position))
        {
            Stop();
        }
    }

    //Stop arrow from flying by setting its physics to false.
    private void Stop()
    {
        inAir = false;
        //SetPhysics(false);

        if (trail != null)
            {
                trail.enabled = false; 
            }
    }

    // Release the arrow from the bow. 
    public void Release(float pullValue)
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.PlayOneShot(audioSource.clip);
        
        inAir = true;
        SetPhysics(true);

        MaskAndFire(pullValue);
        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;


        if (trail != null)
            {
                trail.enabled = true;
            }
    }

    // Update the arrows physics.
    private void SetPhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;

        rigidBody.useGravity = usePhysics;
    }

    // Give the arrow some velocity upon release.
    private void MaskAndFire(float power)
    {
        colliders[0].enabled = false;
        interactionLayerMask = 1 << LayerMask.NameToLayer("Ignore");

        Vector3 force = transform.forward * (power * speed);
        rigidBody.AddForce(force);
    }

    // Rotate the arrow as velocity builds upon release.
    private IEnumerator RotateWithVelocity()
    {

        yield return new WaitForFixedUpdate();

        while (inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    public new void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
    }

    public new void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
    }
}
