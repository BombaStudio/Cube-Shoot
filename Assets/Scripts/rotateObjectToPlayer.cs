using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObjectToPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 cu;
    [SerializeField] [Range(0, 1)] int RotOffX;
    [SerializeField] [Range(0, 1)] int RotOffY;
    [SerializeField] [Range(0, 1)] int RotOffZ;
    //Vector3(90,180,-90)

    void Start()
    {
       
    }

    [System.Obsolete]
    void Update()
    {
        Vector3 bak = player.transform.position - transform.position;
        bak.x *= RotOffX;
        bak.y *= RotOffY;
        bak.z *= RotOffY;
        Vector3 ananýnAmCuuuu = cu + bak;
        //transform.rotation = Quaternion.EulerAngles(ananýnAmCuuuu);
        //döndürme iþlemi yapýlýr
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(ananýnAmCuuuu), 100 * Time.deltaTime);
    }
}
