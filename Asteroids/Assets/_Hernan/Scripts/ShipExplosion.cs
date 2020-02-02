using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    [SerializeField]
    protected List<Transform> parts;

    [SerializeField]
    protected float explosionForce;
    [SerializeField]
    protected float explosionSpeedRange;

    [SerializeField]
    protected float lifeTime;
    [SerializeField]
    protected float lifeTimeRange;

    [SerializeField]
    protected float angularSpeed;
    [SerializeField]
    protected float angularSpeedRange;

    [SerializeField]
    protected List<Rigidbody2D> partsRigidbody;
    [SerializeField]
    protected List<Vector3> partsPositions;

    private float expSpeed;
    private float randomAngularSpeed;
    private float randomTime;

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
            Vector3 dir = part.position - transform.position;
            dir.Normalize();

            // Random de fuerza de explosion
            expSpeed = Random.Range(explosionForce - explosionSpeedRange, explosionForce + explosionSpeedRange);
            part.GetComponent<Rigidbody2D>().AddForce(dir * expSpeed, ForceMode2D.Impulse);

            // Random de velocidad de Rotacion
            randomAngularSpeed = Random.Range(angularSpeed - angularSpeedRange, angularSpeed + angularSpeedRange);
            part.GetComponent<Rigidbody2D>().AddTorque(randomAngularSpeed, ForceMode2D.Impulse);

            // Rando LifeTime
            randomTime = Random.Range(lifeTime - lifeTimeRange, lifeTime + lifeTimeRange);
            StartCoroutine(TimeToDisable(randomTime, part));
        }
    }

    public IEnumerator TimeToDisable(float t, Transform part)
    {
        yield return new WaitForSeconds(t);
        part.gameObject.SetActive(false);
    }
}
