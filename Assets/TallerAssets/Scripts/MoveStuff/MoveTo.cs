using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
 

namespace Taller
{
     
    public class MoveTo : MonoBehaviour
    {
        Rigidbody2D rb;

        [Header("Path data")]
        public Vector2[] wayPoints;
        [Header("Play Options")]
        public bool autoPlay = true;
        public int loops=0;
        public LoopType loopType = LoopType.Yoyo;
        public float time=1;
        [Header("Path style Options")]
        public PathType pathType = PathType.Linear;
        public Ease ease = Ease.Linear;

        public bool topDown = true;
        [HideInInspector]
        public Vector3[] waypointsWorld;


        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            if(rb!=null)
            {
                rb.gravityScale = 0;

            }
        }
        private void OnEnable()
        {
            waypointsWorld = GetWaypoints();
        }
        protected virtual void Start()
        {
            if (autoPlay)
                Begin();
        }
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying) return;
            if (wayPoints.Length== 0) return;
            GetWaypoints();
            Gizmos.color = Color.green;
            for (int i = 0; i < waypointsWorld.Length; i++)
            {
                Gizmos.DrawSphere(waypointsWorld[i] ,.2f );
                if(i>0)
                {
                   
                    Gizmos.DrawLine(waypointsWorld[i], waypointsWorld[i-1]);

                }



            }

        }

        Vector3[] GetWaypoints()
        {
            waypointsWorld = new Vector3[wayPoints.Length];

            for (int i = 0; i < wayPoints.Length; i++)
            {
                waypointsWorld[i]=wayPoints[i] + (Vector2) transform.position;


            }

            return waypointsWorld;
        }
        public void Begin()
        {
           
            transform.DOPath(waypointsWorld, time, pathType, topDown ? PathMode.TopDown2D : PathMode.Sidescroller2D, 10, Color.green)
                .SetLoops(loops, loopType).SetEase(ease);
            

        }


    }

   
}
