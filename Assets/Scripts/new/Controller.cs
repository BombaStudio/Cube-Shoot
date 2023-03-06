using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] GameObject entityPrefab;

    [SerializeField] List<GameObject> maps;
    [SerializeField] List<GameObject> objects;


    public int level = 0;

    public int entityCount;

    GameObject map;

    [SerializeField] List<Transform> entityTransforms;
    [SerializeField] List<Transform> objectTransforms;

    private void Awake()
    {
        light.SetActive(true);
        light.GetComponent<Light>().enabled = true;
    }

    private void Start()
    {
        if (level > 5)
        {
            map = Instantiate(maps[Random.Range(0,maps.Count)]);
            map.GetComponent<NavMeshSurface>().BuildNavMesh();
            entityCount = Random.Range(1, 15);

            foreach (Transform t in map.GetComponentInChildren<Transform>())
            {
                Debug.Log(t.tag);
                if (t.tag == "entitySpawner")
                {
                    Debug.Log("aa");
                    entityTransforms.Add(t);
                }
                if (t.tag == "objectSpawner")
                {
                    Debug.Log("bb");
                    objectTransforms.Add(t);
                }
            }
            while (entityCount > 0)
            {
                GameObject e = Instantiate(entityPrefab, entityTransforms[Random.Range(0,entityTransforms.Count)]);
                //e.transform.SetParent(map.transform);
                entityCount--;
                Debug.Log(entityCount);
            }
            foreach (Transform obj in objectTransforms)
            {
                GameObject g = Instantiate(objects[Random.Range(0,objects.Count)], entityTransforms[Random.Range(0, objectTransforms.Count)]);
                g.transform.Rotate(0,Random.Range(0,360),0);
                //g.transform.SetParent(map.transform);
            }
            
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level0");
    }
}
