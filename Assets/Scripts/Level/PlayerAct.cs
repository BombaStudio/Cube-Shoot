using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Windows;

public class PlayerAct : MonoBehaviour
{
    [SerializeField] GameObject camera;
    [SerializeField] GameObject target,target2,weapon,ragrig;

    public bool hasGun;
    [SerializeField] bool aim;
    [SerializeField] bool shoot;

    [SerializeField] Animator animator,animator2;

    [SerializeField] List<MeshRenderer> meshRenderers;
    [SerializeField] Texture2D brush;
    [SerializeField] Vector2Int textureArea = new Vector2Int(1024,1024);
    [SerializeField] Texture2D texture,textureEntity;
    [SerializeField] List<Material> materials;

    [SerializeField] GameObject canvas, canvas2;

    int status;

    void Start()
    {

        canvas.SetActive(true);
        canvas2.SetActive(false);
        //camera = transform.Find("PlayerCameraRoot").gameObject;

        //texture = new Texture2D(textureArea.x,textureArea.y, TextureFormat.ARGB32, false);

        rbStatus(ragrig, true);

        /*
        textureEntity = new Texture2D(textureArea.x,textureArea.y, TextureFormat.ARGB32, false);
        
        foreach (GameObject entity in GameObject.FindGameObjectsWithTag("Entity"))
        {
            foreach (Material material in entity.transform.GetChild(0).Find("Body").GetComponent<SkinnedMeshRenderer>().materials)
            {
                setTexture(material, textureEntity);
            }
            foreach (Material material in entity.transform.GetChild(0).Find("Face").GetComponent<SkinnedMeshRenderer>().materials)
            {
                setTexture(material, textureEntity);
            }
        }
        */

        /*
        if (meshRenderers.Count != 0){
            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                meshRenderer.GetMaterials(materials);
                foreach (Material material in materials)
                {
                    Debug.Log(material.shader.name);
                    setTexture(material, texture);
                }
            }
        }
        */
        /*
        foreach (Material material in materials)
        {
            Debug.Log(material.shader.name);
            if (material.shader.name == "Shader Graphs/PaintedPlaster")
            {
                Debug.Log(material.shader.GetPropertyCount());
                material.SetTexture("baseTexture",texture);
                //Debug.Log(material.shader.GetPropertyTextureDefaultName(material.shader.FindPropertyIndex("baseTexture")));
            }
        }
        */
    }

    void rbStatus(GameObject obj,bool stat)
    {
        obj.GetComponent<Rigidbody>().isKinematic = stat;
        obj.GetComponent<Rigidbody>().useGravity = !stat;
        obj.GetComponent<Rigidbody>().constraints =  stat ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
        if (obj.transform.childCount > 0)
        {
            foreach (Transform child in obj.GetComponentInChildren<Transform>())
            {
                if (child.GetComponent<Rigidbody>())
                {
                    rbStatus(child.gameObject, stat);
                }
            }
        }
    }

    void setTexture(Material material,Texture2D texture)
    {
        switch (material.shader.name)
        {
            case "Shader Graphs/PaintedPlaster":
                material.SetTexture("baseTexture", texture);
                break;
            case "Universal Render Pipeline/Lit":
                material.mainTexture = texture;
                break;
            default:
                Debug.LogError("Shader not found");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(camera.transform.position,camera.transform.forward * 30);
        target.transform.position = camera.transform.position + (camera.transform.forward * 6) + (camera.transform.up);
        //target2.transform.position = camera.transform.position + (camera.transform.forward * 6) + (-camera.transform.up * 60) + (camera.transform.right);

        status = Input.GetMouseButton(0) && hasGun ? 2
            : Input.GetMouseButton(1) ? 1 : 0;

        anim(
            Input.GetAxis("Horizontal") != 0 ? (Input.GetKey(KeyCode.LeftShift) ? 2 : 1)
            : Input.GetAxis("Vertical") != 0 ? (Input.GetKey(KeyCode.LeftShift) ? 2 : 1) : 0
            ,
            status
        );

        if (Input.GetMouseButton(0) && hasGun)
        {
            RaycastHit hit;



            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 30))
            {

                Transform objectHit = hit.transform;

                //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube)).transform.position = objectHit.position;
                Debug.Log(hit.textureCoord);
                switch (hit.transform.tag)
                {
                    case "map":
                        //paint(hit.textureCoord, texture);
                        break;
                    case "Entity":
                        //paint(hit.textureCoord, textureEntity,1);
                        hit.transform.GetComponent<Animator>().enabled = false;
                        break;
                    default: break;
                }
                
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.X))
        {
            byte[] bytes = texture.EncodeToPNG();
            var dir = Application.dataPath + "/../imgs/";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllBytes(dir + "i.png", bytes);
            Debug.Log(dir);

        }
        */
    }

    public void anim(float h, float v)
    {

        animator.SetFloat(Animator.StringToHash("x"), h, 0.1f, Time.deltaTime);
        animator.SetFloat(Animator.StringToHash("y"), v, 0.1f, Time.deltaTime);
        animator2.SetFloat(Animator.StringToHash("x"), h, 0.1f, Time.deltaTime);
        animator2.SetFloat(Animator.StringToHash("y"), v, 0.1f, Time.deltaTime);
    }
    Texture2D generateTexture(Vector2Int textureArea)
    {
        return new Texture2D(textureArea.x, textureArea.y, TextureFormat.ARGB32, false);
    }

    void paint(Vector2 coors,Texture2D texture,int radius = 5)
    {
        Color color = new Color(
            1,0,0
        );

        
        coors.x *= texture.width;
        coors.y *= texture.height;
        

        //Vector2Int halfBrush = new Vector2Int(brush.width/2,brush.height/2);

        Color32[] pixels = texture.GetPixels32();
        int point =
            (int)coors.x +
            ((int)coors.y * texture.width);


        
        for (int x = 0; x < radius*2; x++)
        {
            int posX = x - radius + (int)coors.x;
            if (posX < 0 || posX >= texture.height) continue;

            for (int y = 0; y < radius*2; y++)
            {
                int posY = y - radius + (int)coors.y;
                int posT =
                    posX +
                    (texture.width * posY);
                if (posX < 0 || posX >= texture.height) continue;
                //if (posY < 0 || posY >= texture.height) continue;

                pixels[posT] = color;

                /*
                texture.SetPixel(
                    (int)coors.x + (radius -x),
                    (int)coors.y + (radius -y),
                    new Color(
                        Random.Range(0,255),
                        Random.Range(0,255),
                        Random.Range(0,255)
                    )
                );
                */
            }
        }
        

        //pixels[point] = color;
        texture.SetPixels32(pixels);
        texture.Apply();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Entity")
        {
            rbStatus(ragrig, false);
            animator.enabled = false;
            animator2.enabled = false;
            GetComponent<player>().enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Screen.lockCursor = false;

            canvas.SetActive(false);
            canvas2.SetActive(true);

            this.enabled = false;
        }
    }
}
