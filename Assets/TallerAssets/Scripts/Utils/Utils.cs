using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Taller
{
    public class Utils : MonoBehaviour
    {
        public static Vector2 ReflectCollision(Rigidbody2D rb, Collision2D collision)
        {
           return Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        }
        // Start is called before the first frame update
        
    }
}

