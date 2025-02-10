using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public Enemy enemyPrefab;
    protected float currentLoop = 0.0f;
    protected float spawnTime = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentLoop);

        if (currentLoop >= spawnTime)
        {
            createEnemy();
            currentLoop = 0f;
        }
        else
        {
            currentLoop += Time.deltaTime;
        }
    }


    public void createEnemy()
    {
        Debug.Log(transform.position);
        Instantiate(enemyPrefab, transform.position , Quaternion.identity);
    }
}
