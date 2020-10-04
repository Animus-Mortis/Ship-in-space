using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject lazerShot;
    [SerializeField]
    private GameObject lazerGun;

    private List<GameObject> lazers = new List<GameObject>();
    private int lazerCount = 10;
    private bool willGrow = false;

    Rigidbody ship;
    private float moveHorizontal, moveVertical;
    private float speed = 2000;
    private float xMin = -34f, xMax = 34f, zMin = -50f, zMax = 30f;
    private float shotDelay = 0.4f;
    private float nextShotTime;

    private void Awake()
    {
        for(int i = 0; i<lazerCount; i++)
        {
            CreateLazer();
        }
    }
    void Start()
    {
        ship = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        ship.velocity = new Vector3(moveHorizontal * speed * Time.deltaTime, 0, moveVertical * speed * Time.deltaTime);

        float clampedX = Mathf.Clamp(ship.position.x, xMin, xMax);
        float clampedZ = Mathf.Clamp(ship.position.z, zMin, zMax);
        ship.position = new Vector3(clampedX, 0, clampedZ);

        ship.rotation = Quaternion.Euler(ship.velocity.z * 0.7f, 0, -ship.velocity.x * 0.7f);

        if (Time.time > nextShotTime && Input.GetButton("Fire1"))
        {
            GameObject newLazer = GetPooledOblect();
            if(newLazer != null)
            {
                newLazer.transform.position = lazerGun.transform.position;
                newLazer.SetActive(true);
            }
            nextShotTime = Time.time + shotDelay;
        }
    }
    GameObject CreateLazer()
    {
        GameObject obj = (GameObject) Instantiate(lazerShot, lazerGun.transform.position, Quaternion.identity);
        obj.SetActive(false);
        lazers.Add(obj);
        return obj;
    }
    GameObject GetPooledOblect()
    {
        for(int i = 0; i< lazers.Count; i++)
        {
            if (!lazers[i].activeSelf)
                return lazers[i];
        }
        if (willGrow)
            return CreateLazer();
        return null;
    }
}
