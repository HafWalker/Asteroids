using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    public List<Transform> parts;
    public float explosionForce;
    public float explosionSpeedRange;

    public float lifeTimeMax;
    public float lifeTimeMin;

    public float angularSpeed;
    public float angularSpeedRange;

    public List<Rigidbody2D> partsRigidbody;
    public List<Vector3> partsPositions;

    private void Start()
    {
        foreach (Transform part in parts)
        {
            partsRigidbody.Add(part.GetComponent<Rigidbody2D>());
            partsPositions.Add(part.localPosition);
            part.gameObject.SetActive(false);
        }
    }

    public void Explode(Vector2 position)
    {
        transform.position = position;

        for (int i = 0; i < parts.Count; i++)
        {
            partsRigidbody[i].velocity = Vector2.zero;
            parts[i].gameObject.SetActive(true);
            parts[i].localPosition = partsPositions[i];
        }

        foreach (Transform part in parts)
        {
            // Calculo de vector de Fuerza
            Vector3 dir = transform.position - part.position;
            dir.Normalize();

            // Random de fuerza de explosion
            float expSpeed = Random.Range(explosionForce - explosionSpeedRange, explosionForce + explosionSpeedRange);
            part.GetComponent<Rigidbody2D>().AddForce(dir * -expSpeed, ForceMode2D.Impulse);

            // Random de velocidad de Rotacion
            float randomAngularSpeed = Random.Range(angularSpeed - angularSpeedRange, angularSpeed + angularSpeedRange);
            part.GetComponent<Rigidbody2D>().AddTorque(randomAngularSpeed, ForceMode2D.Impulse);

            // Rando LifeTime
            float randomTime = Random.Range(lifeTimeMin, lifeTimeMax);
            StartCoroutine(TimerToDie(randomTime, part));
        }
    }

    public IEnumerator TimerToDie(float t, Transform part) // Camb a desaparcer
    {
        yield return new WaitForSeconds(t);
        part.gameObject.SetActive(false);
    }
}
