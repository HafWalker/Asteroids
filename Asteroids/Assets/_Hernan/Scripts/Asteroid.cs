using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AsteroidType
{
    Big,
    Medium,
    Small
}

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    protected AsteroidType type;

    [SerializeField]
    protected GameManager gameManager;

    [SerializeField]
    protected List<Sprite> sprites;

    [SerializeField]
    protected float minSpeed;

    [SerializeField]
    protected float maxSpeed;

    private SpriteRenderer image;

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
    }

    public void Initialize(AsteroidType t, Vector3 pos, GameManager gameMgr)
    {
        this.gameManager = gameMgr;
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
                break;
            case 1:
                image.sprite = sprites[Random.Range(4, 8)];
                break;
            case 2:
                image.sprite = sprites[Random.Range(8, 12)];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);

            // Seria posible agregar una variable de "puntaje" para cada tipo de Asteroide
            gameManager.playerStatus.AddScore(10);
            gameManager.CheckHighScore();

            if (type != AsteroidType.Small)
            {
                gameManager.asteroidMgr.CreateNewAsteroid(transform.position, type + 1);
                gameManager.asteroidMgr.CreateNewAsteroid(transform.position, type + 1);
            }

            gameManager.asteroidMgr.RemoveAsteroid(this.gameObject);
            Destroy(gameObject);
        }

        if (collision.tag == "Player")
        {
            gameManager.playerStatus.RemoveLife();
        }
    }
}
