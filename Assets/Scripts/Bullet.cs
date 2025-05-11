using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 1f;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }
    private void OnTriggerEnter(Collider other)
    {if (other.gameObject.CompareTag("Wall"))
        {
            Pool.Instance.Return(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(lifetime);
        Pool.Instance.Return(gameObject);
    }
}
