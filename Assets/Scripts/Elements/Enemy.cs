using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startHealth;
    private int _currentHealth;
    public float EnemyMoveSpeed;
    public TextMeshPro healthTMP;
    public SpriteRenderer spriteRenderer;
    public PowerUp powerUpPrefab;
    public Coin coinPrefab;
    private bool _didSpawnCoin;
    private Player _player;
    public bool isBoss;

   
    public void StartEnemy(Player player)
    {
        _player = player;
        startHealth += Random.Range(1, 10);
        startHealth += 10 * (player.shootDirections.Count - 1);
        _currentHealth = startHealth;
        healthTMP.text = _currentHealth.ToString();
    }

    void Update()
        {
           transform.position += Vector3.down * Time.deltaTime * EnemyMoveSpeed; 
        }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthTMP.text = _currentHealth.ToString();

        transform.DOKill();
        transform.localScale = Vector3.one;
        transform.DOScale(1.1f,.2f).SetLoops(2, LoopType.Yoyo);

        spriteRenderer.DOKill();
        spriteRenderer.color = Color.red;
        spriteRenderer.DOColor(Color.white, .1f).SetLoops(2, LoopType.Yoyo);

        if (_currentHealth <= 0)
        {
            if (!_didSpawnCoin)
            {
                if (Random.value < .5f)
                {
                   var newCoin = Instantiate(coinPrefab);
                   newCoin.transform.position = transform.position + Vector3.forward * .8f;
                   newCoin.StartCoin(); 
                }
                else
                {
                    var newPowerUp = Instantiate(powerUpPrefab);
                    newPowerUp.transform.position = transform.position + Vector3.forward * .8f;
                    newPowerUp.StartPowerUp();
                }

                _didSpawnCoin = true;
            if (isBoss)
            {
                _player.gameDirector.levelCompleted();
            }
            gameObject.SetActive(false);
            
            }
        }
        

    }

}
