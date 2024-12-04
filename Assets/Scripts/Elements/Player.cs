using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public Transform bulletsParent;
    public float playerMoveSpeed;
    public float playerXborder;
    public float playerYborder;
    public Bullet bulletPrefab;
    public float PlayerBulletSpeed;
    public float AttackRate;
    public float shootCount;
    public List<Vector3> shootDirections;
    public float startHealth;
    private float _currentHealth;
    public Transform healthBarFillParent;
    public SpriteRenderer healthBarFill;
    private Vector3 _mousePivotPos;
    private Coroutine _shootCoroutine;

    // Start is called before the first frame update
    public void RestartPlayer()
    {
        gameObject.SetActive(true);
        _currentHealth = startHealth;
        transform.position = new Vector3(0, -3.77f, 0);
        StopShooting();
        shootDirections.Clear();
        shootDirections.Add(Vector3.up);
        UpdateHealthBar(1);
    }

    private void StartShooting()
    {
        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    private void StopShooting()
    {
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move Player
        MovePlayer();

        //Clamp Player Position
        ClampPlayerPosition();
        if (Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.CompareTag("Enemy"))
        {
            _currentHealth -= 1;
            UpdateHealthBar(_currentHealth / startHealth); 
            if (_currentHealth <= 0)
            {
                gameObject.SetActive(false);
                gameDirector.LevelFailed();
            }
        }
        if (collision.CompareTag("Coin"))
        {
            gameDirector.coinManager.IncreaseCoinCount(1);
            gameDirector.fxManager.CoinCollectedFX(collision.transform.position);
            collision.gameObject.SetActive(false);
        }
        if (collision.CompareTag("PowerUp"))
        {
            shootDirections.Add(new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0).normalized);
            collision.gameObject.SetActive(false);
            /*shootCount++;
            collision.gameObject.SetActive(false);*/
        }
    }

    void UpdateHealthBar(float ratio)
    {
        healthBarFillParent.transform.localScale = new Vector3(ratio, .1f, 1f);
        healthBarFill.DOColor(Color.red, .1f).SetLoops(2, LoopType.Yoyo);
        if (ratio < .5f)
        {
            healthBarFill.color = Color.red;
        }
        else
        {
            healthBarFill.color = Color.green;
        }
    }

    void MovePlayer()
    {
        Vector3 direction = Vector3.zero;
         /* 
        if (Input.GetMouseButtonDown(0))
        {
            _mousePivotPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            direction = Input.mousePosition - _mousePivotPos;
        }

        transform.position = transform.position + direction * playerMoveSpeed * Time.deltaTime;
      */
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 1f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = worldPosition;
        
    }
    void ClampPlayerPosition()
    {
        //Clamp Player Position
        var pos = transform.position;

        if (transform.position.x < -playerXborder)
        {
            pos.x = -playerXborder;
        }

        if (transform.position.x > playerXborder)
        {
            pos.x = playerXborder;
        }

        if (transform.position.y < -playerYborder)
        {
            pos.y = -playerYborder;
        }

        if (transform.position.y > playerYborder)
        {
            pos.y = playerYborder;
        }
        transform.position = pos;
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(AttackRate);

            for (int i = 0; i < shootDirections.Count; i++)
            {
                Shoot(shootDirections[i]);
            }
        }
    }
    void Shoot(Vector3 dir)
    {
        var newBullet = Instantiate(bulletPrefab, bulletsParent);
        newBullet.transform.position = transform.position;
        newBullet.StartBullet(PlayerBulletSpeed, dir, gameDirector);
    }

   
}
