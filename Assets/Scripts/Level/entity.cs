using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class entity : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 target;
    Vector3 prevPos;

    GameObject player;

    [SerializeField] GameObject ragrig;

    Vector3 startPos;

    Enemy enemy;

    public bool isLive = true;

    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        target = transform.position + transform.forward;
        player = GameObject.FindGameObjectWithTag("Player");
        rbStatus(ragrig, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLive)
        {
            float distance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            /*
            Debug.Log(player.GetComponent<playerT>().target==this.gameObject && player.GetComponent<playerT>().isShoot && player.GetComponent<playerT>().isAim ? "true":"false");
            if (
                Input.GetKeyDown(KeyCode.Space) &&
                player.GetComponent<playerT>().isShoot && player.GetComponent<playerT>().isAim &&
                player.GetComponent<playerT>().target == this.gameObject
            )
            {
                Debug.Log("aaa");
                transform.GetChild(0).GetComponent<Animator>().enabled = false;
                isLive = false;
                transform.tag = "Untagged";
                player.GetComponent<playerT>().target = null;
            }
            */

            /*
            if (transform.position == prevPos) agent.SetDestination(target);
            else
            {
                if (player != null)
                {
                    target = player.transform.position;
                    agent.speed = 10;
                }
                else
                {
                    target = transform.position + transform.forward;
                }

            }
            */

            target = player.transform.position;
            agent.SetDestination(target);

            prevPos = transform.position;

            if (distance < 1.25f)
            {
                Debug.Log("a");
                player.GetComponent<playerT>().isLive = false;
                target = startPos;
            }


            /*
            for (int i = 0; i < 180; i++)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position - (transform.up * .5f), transform.right * Mathf.Cos(i * Mathf.Deg2Rad) + transform.forward * Mathf.Sin(i * Mathf.Deg2Rad), out hit, 15))
                {
                    if (hit.transform.name == "PlayerCapsule") player = hit.transform.gameObject;
                }
            }
            */
        }
        else
        {
            rbStatus(ragrig, false);
            agent.SetDestination(transform.position);

            Debug.Log("aaa");
            transform.GetChild(0).GetComponent<Animator>().enabled = false;
            transform.tag = "Untagged";
        }


    }

    void rbStatus(GameObject obj, bool stat)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().isKinematic = stat;
            obj.GetComponent<Rigidbody>().useGravity = !stat;
            obj.GetComponent<Rigidbody>().constraints = stat ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
        }
        if (obj.transform.childCount > 0)
        {
            foreach (Transform child in obj.GetComponentInChildren<Transform>())
            {
                rbStatus(child.gameObject, stat);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Entity")
        {
            
        }
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 20);
        for (int i = 0; i < 180; i++)
        {
            //Debug.DrawRay(transform.position, transform.right * Mathf.Cos(i) + transform.forward * Mathf.Sin(i));
            Gizmos.DrawRay(transform.position - (transform.up * .5f), transform.right * Mathf.Cos(i* Mathf.Deg2Rad) * 15 + transform.forward * Mathf.Sin(i* Mathf.Deg2Rad) * 15 );//+ (transform.up * .5f)
        }
        Debug.Log(Mathf.Sin(90* Mathf.Deg2Rad));
    }
    */
}

[System.Serializable]
class Enemy {
    
}