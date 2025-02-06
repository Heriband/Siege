using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CastleController : ConstructionController
{

    public static CastleController instance;
    protected override void Start()
    {
        base.Start();
        instance = this;
    }



    private List<Enemy> FindEnemiesInRange()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None) // Récupère tous les ennemis
            .Where(enemy => Vector3.Distance(transform.position, enemy.transform.position) <= statistics.attackRange) // Filtre ceux à portée
            .OrderBy(enemy => Vector3.Distance(transform.position, enemy.transform.position)) // Trie par distance croissante
            .ToList();
    }
}
