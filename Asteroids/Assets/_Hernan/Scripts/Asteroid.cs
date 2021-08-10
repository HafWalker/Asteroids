using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AsteroidType {
    Big,
    Medium,
    Small
}

public class Asteroid : MonoBehaviour {

    public AsteroidType type;
    public GameManager gameManager;
    public List<Sprite> sprites;
    public float minSpeed;
    public float maxSpeed;

    private SpriteRenderer m_spriteRender;

    private void Awake() {
        m_spriteRender = GetComponent<SpriteRenderer>();
    }

    public void Initialize(AsteroidType t, GameManager gameMgr) {
        this.gameManager = gameMgr;
        type = t;
        SetRandomSprite();
        SetRandomMovement();
    }

    public void SetRandomMovement() {
        Vector2 randVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        randVector.Normalize();
        randVector *= Random.Range(minSpeed, maxSpeed);
        GetComponent<Rigidbody2D>().AddForce(randVector, ForceMode2D.Impulse);
    }

    public void SetRandomSprite() {
        switch ((int)type)
        {
            case 0:
                m_spriteRender.sprite = sprites[Random.Range(0, 4)];
                break;
            case 1:
                m_spriteRender.sprite = sprites[Random.Range(4, 8)];
                break;
            case 2:
                m_spriteRender.sprite = sprites[Random.Range(8, 12)];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet")) {
            collision.gameObject.SetActive(false);

            // Seria posible agregar una variable de "puntaje" para cada tipo de Asteroide
            gameManager.playerStatus.AddScore(10);
            gameManager.CheckHighScore();

            if (type != AsteroidType.Small)
            {
                gameManager.asteroidManager.CreateNewAsteroid(transform.position, type + 1);
                gameManager.asteroidManager.CreateNewAsteroid(transform.position, type + 1);
            }

            gameManager.asteroidManager.RemoveAsteroid(this.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player")) {
            gameManager.playerStatus.RemoveLife();
        }
    }
}