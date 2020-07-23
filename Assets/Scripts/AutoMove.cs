using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    void Move()
    {
        transform.position += Vector3.back * GameManager.Instance.playerSpeed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
