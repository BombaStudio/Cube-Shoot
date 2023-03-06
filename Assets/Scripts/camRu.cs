using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class camRu : MonoBehaviour
{
    private float m_Timer;
    private float m_Speed = 6;
    private float m_Speed_2 = 12;
    private Vector3 Offset = new Vector3(3, 3, 1);
    private Vector3 Offset_2 = new Vector3(0, 0.0025f, 0);
    private Vector3 Offset_3 = new Vector3(15, 15, 15);
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            m_Timer += Time.deltaTime * m_Speed;
            transform.position += Offset_2 * Mathf.Sin(m_Timer);
        }
        //else
        //{
        //    transform.position = new Vector3(0,0.5f,0);
        //}
    }
}
