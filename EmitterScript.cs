using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{
    [SerializeField]
    GameObject asteroid;
    private float minDelay = 0.3f, maxDelay = 0.5f, nextlaunchTime;

    private List<GameObject> asteroides = new List<GameObject>();
    private int asteroidCount = 12;
    private bool willGrow = true;

    private void Awake()
    {

        for (int i = 0; i < asteroidCount; i++)
        {
            AsteroidCreat();
        }
    }

    void Update()
    {
        float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
        float yPosition = 0;
        float zPosition = transform.position.z;
        Vector3 newPosition = new Vector3(xPosition, yPosition, zPosition);

        if (Time.time > nextlaunchTime)
        {
            GameObject newAsteroid = GetPooledOblect();
            if (newAsteroid != null)
            {
                newAsteroid.transform.position = newPosition;
                newAsteroid.SetActive(true);
            }
            nextlaunchTime = Time.time + Random.Range(minDelay, maxDelay);
        }
    }
    GameObject AsteroidCreat()
    {
        float xPosition = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
        float yPosition = 0;
        float zPosition = transform.position.z;
        Vector3 newPosition = new Vector3(xPosition, yPosition, zPosition);

        GameObject obj = (GameObject)Instantiate(asteroid, newPosition, Quaternion.identity);
        obj.SetActive(false);
        asteroides.Add(obj);
        return obj;
    }
    GameObject GetPooledOblect()
    {
        for (int i = 0; i < asteroides.Count; i++)
        {
            if (!asteroides[i].activeSelf)
                return asteroides[i];
        }
        if (willGrow)
            return AsteroidCreat();
        return null;
    }

}
