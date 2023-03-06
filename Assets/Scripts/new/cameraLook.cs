using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerT>().isShoot)
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameObject.FindGameObjectWithTag("Player").GetComponent<playerT>().target != null)
            {
                StartCoroutine(shake());
            }
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        }
        else
        {
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)));
        }
    }
    IEnumerator shake()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerT>().isShoot = false;
        
        yield return new WaitForSeconds(0.25f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<playerT>().isShoot = true;
    }
}
