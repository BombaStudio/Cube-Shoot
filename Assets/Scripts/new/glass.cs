using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glass : MonoBehaviour
{
    Controller gc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Entity").Length <= 0)
        {
            foreach (Transform t in transform.GetComponentInChildren<Transform>())
            {
                if (t.gameObject)
                {
                    Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.AddForce(new Vector3(
                        Random.Range(-1, 1),
                        Random.Range(-1, 1),
                        Random.Range(-1, 1)
                    ) * 1000);
                    }
                }
            }
        }
    }
}
