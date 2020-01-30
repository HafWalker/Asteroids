using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Type
{
    Big,
    Medium,
    Small
}

public class Asteroid : MonoBehaviour
{
    public Type type;
    private AsteroidsManager asteroidMgr;

    private Image image;
    public List<Sprite> sprites;

    public float minSpeed;
    public float maxSpeed;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Initialize(Type t, Vector3 pos, AsteroidsManager asteroidManager)
    {
        asteroidMgr = asteroidManager;
        type = t;
        SetRandomSprite();
        SetRandomMovement();
    }

    public void SetRandomMovement()
    {
        Vector2 randVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randVector.Normalize();
        randVector = randVector * Random.Range(minSpeed, maxSpeed);
        GetComponent<Rigidbody2D>().AddForce(randVector, ForceMode2D.Impulse);
    }

    public void SetRandomSprite()
    {
        switch ((int)type)
        {
            case 0:
                image.sprite = sprites[Random.Range(0, 4)];
                image.SetNativeSize();
                break;
            case 1:
                image.sprite = sprites[Random.Range(4, 8)];
                image.SetNativeSize();
                break;
            case 2:
                image.sprite = sprites[Random.Range(8, 12)];
                image.SetNativeSize();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // Seria posible agregar una variable de "puntaje" para cada tipo de Asteroide
            asteroidMgr.playerStatus.GetComponent<PlayerStatus>().AddScore(10);

            if (type != Type.Small)
            {
                asteroidMgr.CreateNewAsteroid(transform.position, type + 1);
                asteroidMgr.CreateNewAsteroid(transform.position, type + 1);
            }

            asteroidMgr.RemoveAsteroid(this.gameObject);
            Destroy(gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            asteroidMgr.playerStatus.GetComponent<PlayerStatus>().RemoveLife();

            if (!asteroidMgr.playerStatus.GetComponent<PlayerStatus>().haveShield)
            {
                asteroidMgr.ResetAsteroids();
                asteroidMgr.playerStatus.GetComponent<ShipController>().ResetPos();
            }
        }
    }
}
