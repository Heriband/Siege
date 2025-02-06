using UnityEngine;

public class Entity : MonoBehaviour
{

    public Statistics statistics;

    protected virtual void Start()
    {
        statistics.health = statistics.healthMax;

    }

    protected virtual void Update()
    {
        
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
}
