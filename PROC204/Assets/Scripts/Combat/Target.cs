using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Health
{
    [SerializeField] GameObject brokenVariant;
    [SerializeField] float minExplosionForce = 2000f;
    [SerializeField] float maxExplosionForce = 4000f;
    [SerializeField] float explosionRadius = 4f;

    //CACHE REFERENCES

    //Use to rotate targets to face position of camera
    Transform playerCamera;

    private void Awake()
    {
        playerCamera = FindObjectOfType<CinemachineBrain>().transform; 
    }

    private void Update()
    {
        Vector3 camPos = new Vector3(playerCamera.position.x, transform.position.y, playerCamera.position.z);
        transform.LookAt(camPos); 
    }

    //When target is destroyed the current instance is swapped with a broken
    //variant with small pieces that can have force applied to them
    protected override void Die()
    {
        base.Die();

        GameObject instance = Instantiate(brokenVariant, transform.position, transform.rotation); //broken variant

        Rigidbody[] rigidbodies = instance.GetComponentsInChildren<Rigidbody>(); //Pieces to aply force to

        foreach(Rigidbody rb in rigidbodies)
        {
            float explosionForce = Random.Range(minExplosionForce, maxExplosionForce);
            Vector3 explosionPos = transform.position + transform.forward; //Explosion force from front always
            rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius);
        }

        Destroy(instance, 5f); //Destroy pieces
        Destroy(gameObject); //Remove non-broken instance
    }
}
