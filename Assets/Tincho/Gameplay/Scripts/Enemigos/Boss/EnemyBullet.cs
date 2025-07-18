using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;
    public float damage = 10f;
    private float maxLifetime = 3f;
    private float lifeTimer = 0f;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
