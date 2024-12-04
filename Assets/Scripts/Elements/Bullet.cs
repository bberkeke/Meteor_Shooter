using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameDirector _gameDirector;
    private float _speed;
    private Vector3 _dir;

    public void StartBullet(float BulletSpeed, Vector3 direction, GameDirector gameDirector)
    {
        _speed = BulletSpeed;
        _dir = direction;
        _gameDirector = gameDirector;
    }
    
    void Update()
    {
        transform.position += _dir * Time.deltaTime * _speed; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
            collision.GetComponent<Enemy>().GetHit(1);
            _gameDirector.fxManager.BulletHitFX(transform.position);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
    }
}

