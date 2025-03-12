using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
public class ConstructionController : Entity
{
    
    public int costGold;
    protected override void Die()
    {
        base.Die();
    }


    private List<Enemy> FindEnemiesInRange()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None) // Récupère tous les ennemis
            .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= statistics.attackRange) // Filtre ceux à portée
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)) // Trie par distance croissante
            .ToList();
    }

    protected override void AttackRoutine()
    {
        List<Enemy> enemiesInRange = FindEnemiesInRange();

        if (enemiesInRange.Count > 0)
        {
            
            bool canAttack = !isAttacking || attackPeriod >= 10 / statistics.attackSpeed;
            if (canAttack)
            {
                isAttacking = true;
                for (int i = 0; i < statistics.projectileNumber; i++)
                {
                    if (i < enemiesInRange.Count)
                    {
                        Shoot(enemiesInRange[i]);
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
