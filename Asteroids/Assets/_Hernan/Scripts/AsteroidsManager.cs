using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {

    public GameManager gameManager;
    public AudioManager audioManager;
    public int startAsteroidsAmount = 4;
    public GameObject bigAsteroid;
    public GameObject midAsteroid;
    public GameObject smallAsteroid;
    public List<GameObject> allAsteroids;
    public Camera cam;

    //Orden de Edge en Sentido Horario inicia a la Izquierda
    private int m_edge = 0;
    private Vector2 m_randomPos;
    private GameObject m_asteroidRef;

    private float m_left_bound;
    private float m_right_bound;
    private float m_top_bound;
    private float m_bottom_bound;

    private void Start() {
        m_right_bound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, -1f)).x;
        m_left_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).x;
        m_top_bound = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, -1f)).y;
        m_bottom_bound = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -1f)).y;
    }

    public void CreateNewAsteroid(Vector2 pos, AsteroidType t) {
        switch ((int)t) {
            case 0:
                m_asteroidRef = bigAsteroid;
                break;
            case 1:
                m_asteroidRef = midAsteroid;
                break;
            case 2:
                m_asteroidRef = smallAsteroid;
                break;
        }

        GameObject asteroidClone = Instantiate(m_asteroidRef, pos, Quaternion.identity, transform);
        asteroidClone.GetComponent<Asteroid>().Initialize(t, gameManager);
        allAsteroids.Add(asteroidClone);
    }

    public void CreateAsteroids() {
        for (int i = 0; i < startAsteroidsAmount; i++) {
            m_randomPos = GetRandomPosOnEdge();
            CreateNewAsteroid(m_randomPos, AsteroidType.Big);
        }
    }

    public void RemoveAsteroid(GameObject asteroid) {
        audioManager.playExplosionClip();
        allAsteroids.Remove(asteroid);

        if (allAsteroids.Count == 0) {
            ResetAsteroids();
        }

        for (int i = 0; i < allAsteroids.Count; i++) {
            if(allAsteroids[i] == null) {
                allAsteroids.Remove(allAsteroids[i]);
            }
        }
    }

    public void ResetAsteroids() {
        m_edge = 0;
        foreach (GameObject asteroid in allAsteroids) {
            Destroy(asteroid);
        }
        CreateAsteroids();
    }

    public Vector2 GetRandomPosOnEdge() {
        float xPos = 0;
        float yPos = 0;

        switch (m_edge) {
            case 0: //Left
                xPos = m_left_bound; 
                yPos = Random.Range(m_bottom_bound, m_top_bound);
                break;
            case 1: //Top
                xPos = Random.Range(m_left_bound, m_right_bound); ;
                yPos = m_top_bound;
                break;
            case 2: //Right
                xPos = m_right_bound;
                yPos = Random.Range(m_bottom_bound, m_top_bound);
                break;
            case 3: //Bot
                xPos = Random.Range(m_left_bound, m_right_bound); ;
                yPos = m_bottom_bound;
                break;
        }

        m_edge++;

        if (m_edge > 3) {
            m_edge = 0;
        }

        return new Vector2(xPos, yPos);
    }
}