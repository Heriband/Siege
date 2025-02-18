using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Entity target;
    private float speed;
    private float damage;
    private float lifetime = 2f;

    public Vector3 lastEnemyPosition;


    public void Initialize(Entity newTarget, float projectileSpeed, float projectileDamage)
    {
        target = newTarget;
        lastEnemyPosition = target.transform.position;
        speed = projectileSpeed;
        damage = projectileDamage;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null) {
            transform.position = Vector3.MoveTowards(transform.position, lastEnemyPosition, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, lastEnemyPosition) < 0.1f )
            {
                Destroy(gameObject);
                return;
            }
        }
        else{
            // Déplacement direct vers la cible
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            lastEnemyPosition = target.transform.position;
            // Vérification si le projectile a atteint la cible
            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                HitTarget();
            }
        }
    }

    private void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
