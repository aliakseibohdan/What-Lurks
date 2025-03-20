using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        var hitTransform = collision.transform;
        if (hitTransform.CompareTag("Player"))
        {
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
