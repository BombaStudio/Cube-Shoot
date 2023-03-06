using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.InputSystem;

public class firstDoor : MonoBehaviour
{
    [SerializeField] GameObject pool;
    [SerializeField] GameObject player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 5)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                foreach(Vertex v in gameObject.GetComponent<ProBuilderMesh>().GetVertices())
                {
                    GameObject cube;
                    if (pool.transform.GetChildCount() > 0) cube = pool.transform.GetChild(0).gameObject;
                    else cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.SetActive(true);

                    cube.AddComponent<Rigidbody>();
                    cube.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
                    
                    cube.transform.parent = null;
                    cube.transform.position = transform.position + v.position;
                    cube.transform.localScale = new Vector3(.25f, .25f, .25f);

                    if (!cube.GetComponent<CubePool>()) cube.AddComponent<CubePool>();
                    //cube.GetComponent<CubePool>().pool = pool;
                    //cube.GetComponent<CubePool>().pooled = true;

                }
                Destroy(this.gameObject);
            }
            
        }
    }
    
}
