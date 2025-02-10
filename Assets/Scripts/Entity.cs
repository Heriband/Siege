using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject projectile;

    public Entity target;
    public Statistics statistics;

    protected bool isAttacking = false;
    protected float attackPeriod = 0.0f;
    protected float regenPeriod = 0.0f;

    protected virtual void Start()
    {
        statistics.health = statistics.healthMax;

    }

    protected virtual void Update()
    {
        AttackRoutine();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }


    public virtual void MoveRoutine(Entity target)
    {
        if (statistics.moveSpeed > 0 && target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) > statistics.attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, statistics.moveSpeed * Time.deltaTime);
            }
        }
    }

    public virtual void TakeDamage(float damage){
        statistics.health -=  Mathf.Max(damage - statistics.defense, 0);

        if (statistics.health <= 0)
        {
            Die();
        }
    }

    
    protected virtual void Shoot(Entity target)
    {
        attackPeriod = 0;
        GameObject projectileGO = Instantiate(projectile, transform.position, Quaternion.identity);
        projectileGO.GetComponent<Projectile>().Initialize(target, statistics.projectileSpeed, statistics.attackPower);
    }


 protected virtual void AttackRoutine()
    {
        if (target != null)
        {
            bool canAttack = !isAttacking || attackPeriod >= 1 / statistics.attackSpeed;
            if (canAttack)
            {
                bool atRange = Vector3.Distance(transform.position, target.transform.position) <= statistics.attackRange;
                if (atRange)
                {

                    isAttacking = true;
                    if (statistics.projectileNumber >= 1)
                    {
                        Shoot(target);
                    }
                }
            }
        }
        else
        {
            isAttacking = false;
        }
        attackPeriod += Time.deltaTime;
    }


}
