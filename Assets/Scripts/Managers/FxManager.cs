using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : MonoBehaviour
{
    public ParticleSystem coinCollectedPS;
    public ParticleSystem bulletHitPS;

    public void CoinCollectedFX(Vector3 pos)
    {
        var newPS = Instantiate(coinCollectedPS);
        newPS.transform.position = pos;
        newPS.Play();
    }

    public void BulletHitFX(Vector3 pos)
    {
        var newPS = Instantiate(bulletHitPS);
        newPS.transform.position = pos;
        newPS.Play();
    }
}
