using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Entity target;
    private float speed;
    private float damage;
    private float lifetime = 2f;


    public void Initialize(Entity newTarget, float projectileSpeed, float projectileDamage)
    {
        target = newTarget;
        speed = projectileSpeed;
        damage = projectileDamage;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }
        // Déplacement direct vers la cible
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        // Vérification si le projectile a atteint la cible
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
