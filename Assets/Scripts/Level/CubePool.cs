using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    public GameObject pool;
    public bool pooled;

    void Start()
    {
        pool = GameObject.Find("Pool");
    }
    private void Update()
    {
        if (!pooled)
        {
            StartCoroutine(sendPool());
            pooled = true;
        }
    }

    public IEnumerator sendPool()
    {
        yield return new WaitForSeconds(1);
        transform.SetParent(pool.transform);
        gameObject.SetActive(false);
    }
}
