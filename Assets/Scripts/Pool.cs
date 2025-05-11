using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public static Pool Instance { get; private set; }
    public GameObject bulletPrefab;
    public int poolSize = 10;
    public List<GameObject> bulletPool = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        FillPool();
    }
    void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
            bullet.transform.SetParent(transform);
        }
    }

    public GameObject Get( Transform rotation)
    {
        GameObject ret;
        //pasa rotación al objeto con 90 grados de offset
        this.transform.rotation = rotation.rotation * Quaternion.Euler(0, 0, 0);
        if (bulletPool.Count > 0)
        {
            ret = bulletPool[bulletPool.Count - 1];
            bulletPool.RemoveAt(bulletPool.Count - 1);
        }
        else
        {
            ret = Instantiate(bulletPrefab);
            ret.transform.SetParent(transform);
        }
        ret.SetActive(true);
        return ret;
    }

    public void Return(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Add(bullet);
    }
}