using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    public GameManager gameMgr;
    public int startAsteroidsAmount = 4;

    public GameObject asteroidRef;

    public GameObject bigAsteroid;
    public GameObject midAsteroid;
    public GameObject smallAsteroid;

    private Vector2 randomPos;

    public List<GameObject> allAsteroids;

    public PlayerStatus playerStatus;

    //Orden de Edge en Sentido Horario inicia a la Izquierda
    private int edge = 0;

    private float bounds_width;
    private float bounds_height;

    private void Start()
    {
        bounds_width = Screen.width/2;
        bounds_height = Screen.height/2;
    }

    public void CreateNewAsteroid(Vector2 pos, Type t)
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
        asteroidClone.GetComponent<Asteroid>().Initialize(t, pos, this, gameMgr);
        allAsteroids.Add(asteroidClone);
    }

    public void CreateFirstAsteroids()
    {

        for (int i = 0; i < startAsteroidsAmount; i++)
        {
            randomPos = GetRandomPosOnEdge();
            CreateNewAsteroid(randomPos, Type.Big);
        }
    }

    public void RemoveAsteroid(GameObject asteroid) 
    {
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
                xPos = -bounds_width;
                yPos = Random.Range(0, bounds_height);
                break;
            case 1: //Top
                xPos = Random.Range(0, bounds_width); ;
                yPos = bounds_height;
                break;
            case 2: //Right
                xPos = bounds_width;
                yPos = Random.Range(0, bounds_height);
                break;
            case 3: //Bot
                xPos = Random.Range(0, bounds_width); ;
                yPos = -bounds_height;
                break;
        }

        edge++;

        if (edge > 3)
        {
            edge = 0;
        }

        return new Vector2(xPos + bounds_width, yPos + bounds_height);
    }
}
