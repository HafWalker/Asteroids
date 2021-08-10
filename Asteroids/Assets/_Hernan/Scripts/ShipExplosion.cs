using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipExplosion : MonoBehaviour
{
    public List<Transform> parts;
    public float explosionForce;
    public float explosionSpeedRange;
    public float lifeTime;
    public float lifeTimeRange;
    public float angularSpeed;
    public float angularSpeedRange;
    public List<Rigidbody2D> partsRigidbody;
    public List<Vector3> partsPositions;

    private float m_expSpeed;
    private float m_randomAngularSpeed;
    private float m_randomTime;

    private void Start() {
        foreach (Transform part in parts) {
            partsRigidbody.Add(part.GetComponent<Rigidbody2D>());
            partsPositions.Add(part.localPosition);
            part.gameObject.SetActive(false);
        }
    }

    public void Explode(Vector2 position) {
        transform.position = position;

        for (int i = 0; i < parts.Count; i++) {
            partsRigidbody[i].velocity = Vector2.zero;
            parts[i].gameObject.SetActive(true);
            parts[i].localPosition = partsPositions[i];
        }

        foreach (Transform part in parts) {
            // Calculo de vector de Fuerza
            Vector3 dir = part.position - transform.position;
            dir.Normalize();

            // Random de fuerza de explosion
            m_expSpeed = Random.Range(explosionForce - explosionSpeedRange, explosionForce + explosionSpeedRange);
            part.GetComponent<Rigidbody2D>().AddForce(dir * m_expSpeed, ForceMode2D.Impulse);

            // Random de velocidad de Rotacion
            m_randomAngularSpeed = Random.Range(angularSpeed - angularSpeedRange, angularSpeed + angularSpeedRange);
            part.GetComponent<Rigidbody2D>().AddTorque(m_randomAngularSpeed, ForceMode2D.Impulse);

            // Rando LifeTime
            m_randomTime = Random.Range(lifeTime - lifeTimeRange, lifeTime + lifeTimeRange);
            StartCoroutine(TimeToDisable(m_randomTime, part));
        }
    }

    public IEnumerator TimeToDisable(float t, Transform part) {
        yield return new WaitForSeconds(t);
        part.gameObject.SetActive(false);
    }
}