using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

 
namespace Taller
{

    public class Rotator : MonoBehaviour
    {
        [Header("Rotation data")]
        public float  targetRotation;
        [Header("Play Options")]
        public bool autoPlay = true;
        public int loops = -1;
        public LoopType loopType = LoopType.Restart;
        public float time = 1;
        [Header("Rotator style Options")]
        public RotateMode rotateMode = RotateMode.FastBeyond360; 
        public Ease pathStyle = Ease.Linear;

        public bool topDown = true;
        
        
        protected virtual void Start()
        {
            if (autoPlay)
                Begin();
        }
       
         
        public void Begin()
        {

            
            transform.DORotate(Vector3.forward* targetRotation, time, rotateMode) 
                 .SetLoops(loops, loopType).SetEase(pathStyle);


        }


    }


}
