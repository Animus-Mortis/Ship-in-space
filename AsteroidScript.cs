using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    GameObject asteroidExploision;
    [SerializeField]
    GameObject playerExploision;

    private float rotation = 15;
    private float minSpeed = 20f, maxSpeed = 50f, zSpeed;
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotation;
        zSpeed = Random.Range(minSpeed, maxSpeed);
        asteroid.velocity = new Vector3(0, 0, -zSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameBaundery")
            return;
        Instantiate(asteroidExploision, transform.position, Quaternion.identity);
        if(other.tag == "Player")
        {
            Instantiate(playerExploision, other.transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }
}
