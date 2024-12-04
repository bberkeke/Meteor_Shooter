using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameDirector GameDirector;
    public Enemy enemyPrefab;
    public Enemy fastEnemyPrefab;
    public Enemy bossEnemyPrefab;
    public float enemyYSpacing;
    public int enemyCount;
    private int _spawnedEnemyCount;
    public Player player;
    private Coroutine _enemyGenerationCoroutine;
    public List<Enemy> _enemies = new List<Enemy>(); 


    public void RestartEnemyManager()
    {
        DeleteEnemies();
        //SpawnEnemies();
        if (_enemyGenerationCoroutine != null)
        {
            StopCoroutine(_enemyGenerationCoroutine);
        }
        _enemyGenerationCoroutine=StartCoroutine(EnemyGenerationCoroutine());
        _spawnedEnemyCount = 0;
    }

    private void DeleteEnemies()
    {
        foreach(Enemy enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }
        _enemies.Clear();
    }

    IEnumerator EnemyGenerationCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1 + Random.Range(0,2f));
            var enemyCountBonus = (GameDirector.levelNo - 1) * 5;
            enemyCountBonus = Mathf.Min(enemyCountBonus, 95);
            if (_spawnedEnemyCount < 5+ enemyCountBonus)
            {
                if (Random.value < .5f)
                {
                    SpawnEnemy();
                }
                else
                {
                    SpawnTwoEnemies();
                }
            }
            else
            {
                yield return new WaitForSeconds(2);
                SpawnBoss();
                break;
            }

        }
    }
    
      void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var newEnemy = Instantiate(enemyPrefab);
            var enemyXPos = Random.Range(-2.2f, 2.2f);
            var enemyYPos = 5 * enemyYSpacing;
            newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        }
      
    }
    
    void SpawnEnemy()
    {
        var selectedEnemyPrefab = enemyPrefab;
        if (GameDirector.levelNo>3 && Random.value < .33f)
        {
            selectedEnemyPrefab = fastEnemyPrefab;
        }

        var newEnemy = Instantiate(selectedEnemyPrefab);
        var enemyXPos = Random.Range(-2.2f, 2.2f);
        var enemyYPos = 5 * enemyYSpacing;
        newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy);
        newEnemy.StartEnemy(player);
    }
    void SpawnTwoEnemies()
    {
        var newEnemy = Instantiate(enemyPrefab);
        var enemyXPos = Random.Range(1f, 2.2f);
        var enemyYPos = 5 * enemyYSpacing;
        newEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _enemies.Add(newEnemy);
        newEnemy.StartEnemy(player);

        var newEnemy2 = Instantiate(enemyPrefab);
        var enemyXPos2 = Random.Range(-1f, -2.2f);
        var enemyYPos2 = 5 * enemyYSpacing;
        newEnemy2.transform.position = new Vector3(enemyXPos2, enemyYPos2, 0);
        _spawnedEnemyCount++;
        _enemies.Add(newEnemy2);
        newEnemy2.StartEnemy(player);
    }
    void SpawnBoss()
    {
        var newBossEnemy = Instantiate(bossEnemyPrefab);
        var enemyXPos = Random.Range(-2.2f, 2.2f);
        var enemyYPos = 5 * enemyYSpacing;
        newBossEnemy.transform.position = new Vector3(enemyXPos, enemyYPos, 0);
        _enemies.Add(newBossEnemy);
        newBossEnemy.StartEnemy(player);
    }
}
