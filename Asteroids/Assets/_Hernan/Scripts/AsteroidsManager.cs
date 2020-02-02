using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    [SerializeField]
    protected GameManager gameManager;

    [SerializeField]
    protected AudioManager audioManager;

    [SerializeField]
    protected int startAsteroidsAmount = 4;

    [SerializeField]
    protected GameObject bigAsteroid;

    [SerializeField]
    protected GameObject midAsteroid;

    [SerializeField]
    protected GameObject smallAsteroid;

    [SerializeField]
    protected List<GameObject> allAsteroids;

    //Orden de Edge en Sentido Horario inicia a la Izquierda
    private int edge = 0;
    private float bounds_width;
    private float bounds_height;
    private Vector2 randomPos;
    private GameObject asteroidRef;

    private Camera cam;
    private float left_bound;
    private float right_bound;
    private float top_bound;
    private float bottom_bound;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        right_bound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, -1f)).x;
        left_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).x;
        top_bound = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, -1f)).y;
        bottom_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).y;
    }

    public void CreateNewAsteroid(Vector2 pos, AsteroidType t)
    {
        switch ((int)t)
        {
            case 0:
                asteroidRef = bigAsteroid;
                break;
            case 1:
                asteroidRef = midAsteroid;
                break;
            case 2:
                asteroidRef = smallAsteroid;
                break;
        }

        GameObject asteroidClone = Instantiate(asteroidRef, pos, Quaternion.identity, transform);
        asteroidClone.GetComponent<Asteroid>().Initialize(t, pos, gameManager);
        allAsteroids.Add(asteroidClone);
    }

    public void CreateFirstAsteroids()
    {
        for (int i = 0; i < startAsteroidsAmount; i++)
        {
            randomPos = GetRandomPosOnEdge();
            CreateNewAsteroid(randomPos, AsteroidType.Big);
        }
    }

    public void RemoveAsteroid(GameObject asteroid) 
    {
        audioManager.playExplosionClip();
        allAsteroids.Remove(asteroid);

        if (allAsteroids.Count == 0)
        {
            ResetAsteroids();
        }

        for (int i = 0; i < allAsteroids.Count; i++)
        {
            if(allAsteroids[i] == null)
            {
                //print("MISING");
                allAsteroids.Remove(allAsteroids[i]);
            }
        }
    }

    public void ResetAsteroids()
    {
        edge = 0;

        foreach (GameObject asteroid in allAsteroids)
        {
            Destroy(asteroid);
        }

        CreateFirstAsteroids();
    }

    public Vector2 GetRandomPosOnEdge()
    {
        float xPos = 0;
        float yPos = 0;

        switch (edge)
        {
            case 0: //Left
                xPos = left_bound; 
                yPos = Random.Range(bottom_bound, top_bound);
                break;
            case 1: //Top
                xPos = Random.Range(left_bound, right_bound); ;
                yPos = top_bound;
                break;
            case 2: //Right
                xPos = right_bound;
                yPos = Random.Range(bottom_bound, top_bound);
                break;
            case 3: //Bot
                xPos = Random.Range(left_bound, right_bound); ;
                yPos = bottom_bound;
                break;
        }

        edge++;

        if (edge > 3)
        {
            edge = 0;
        }

        return new Vector2(xPos, yPos);
    }
}
