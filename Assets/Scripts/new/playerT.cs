using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class playerT : MonoBehaviour
{
    CharacterController controller;
    public GameObject target,rag;

    [SerializeField] Animator animator;

    public bool isShoot = true;
    public bool isLive = true;
    public bool isAim = false;
    void Start()
    {
        
        rbStatus(rag, true);
        controller = GetComponent<CharacterController>();
        isShoot = true;
        isLive = true;
    }

    private void Update()
    {
        if (isLive)
        {
            if (target && Vector3.Distance(transform.position, target.transform.position) < 7.5f)
            {
                transform.GetChild(0).LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
                isAim = true;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Destroy(target);
                    target.GetComponent<entity>().isLive = false;
                    target = null;
                    GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                transform.GetChild(0).LookAt(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
                isAim = false;
            }
        }
        else
        {

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().level < 5)
            {
                SceneManager.LoadScene("Level" + (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().level).ToString());
            }
            else
            {
                SceneManager.LoadScene("Level0");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("31 "+Mathf.Cos(Camera.main.transform.rotation.y * Mathf.Deg2Rad));
        if (isLive)
        {
            anim(
                Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ? 1 : 0,
                target && Input.GetKey(KeyCode.Space) ? 2
                : target ? 1
                : 0
            );
            /*
            Debug.Log((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ? 1 : 0,
                target && Input.GetKey(KeyCode.Space) ? 2
                : target ? 1
                : 0));
            */
            controller.Move(new Vector3(Input.GetAxis("Horizontal"), -9.8f, Input.GetAxis("Vertical")) * Time.fixedDeltaTime * 10);
            
            /*
            controller.Move(
                new Vector3(
                    Input.GetAxis("Horizontal") * Mathf.Cos(Camera.main.transform.rotation.y * Mathf.Deg2Rad),
                    -9.8f, 
                    Input.GetAxis("Vertical") * Mathf.Cos(Camera.main.transform.rotation.z * Mathf.Deg2Rad)
                ) * Time.fixedDeltaTime * 10);
            */

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float midPoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;

            //transform.GetChild(0).LookAt(mouseRay.origin + mouseRay.direction * midPoint);

            /*
            Vector3 cu = mouseRay.direction;
            transform.LookAt(new Vector3(cu.x,transform.position.y,cu.z));
            Debug.DrawRay(transform.position, new Vector3(cu.x, transform.position.y, cu.z));
            */
            //transform.LookAt(new Vector3(target.x,0,target.z));

            float distance;
            float prewdistance = -1;

            foreach (GameObject entity in GameObject.FindGameObjectsWithTag("Entity"))
            {
                distance = Vector3.Distance(transform.position, entity.transform.position);
                if (distance < prewdistance || prewdistance == -1) target = entity;
                prewdistance = distance;
            }
            
        }
        else
        {
            animator.enabled = false;
            
        }

        rbStatus(rag, isLive);
    }
    void anim(float h, float v)
    {
        animator.SetFloat(Animator.StringToHash("x"), h, 0.1f, Time.deltaTime);
        animator.SetFloat(Animator.StringToHash("y"), v, 0.1f, Time.deltaTime);
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Exit")
        {
            Debug.Log("exit");
            if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().level < 5)
            {
                SceneManager.LoadScene("Level" + (GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().level + 1).ToString());
            }
            else
            {
                SceneManager.LoadScene("LevelR");
            }
        }
    }
    
}
