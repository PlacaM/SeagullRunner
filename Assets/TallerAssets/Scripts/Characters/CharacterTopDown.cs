using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Taller;
using DG.Tweening;


namespace Taller
{
public class CharacterTopDown : Character
{
        // Start is called before the first frame update

        //[Header("MovementOptions")]

        bool bIsMoving;
        public Ease moveType = Ease.Linear; 
        protected override void Awake()
        {
            base.Awake();
            rb.gravityScale = 0;

        }
        void Start()
        {
        
        }
        public virtual void StopForced()
        {
            DOTween.Clear();

        }
        public virtual void MoveTo(Vector3 TargetPosition)
        {
           
            float time = (TargetPosition - transform.position).magnitude / moveSpeed;
            MoveTo(TargetPosition, time); 
        }
        public virtual void MoveTo(Vector3 TargetPosition,float time)
        {
            if(canMove)
            if (moveSpeed <= 0) return;
            if (bIsMoving) return;
            bIsMoving = true;
            //v=d/t;
            //t=d/v
             

            rb.DOMove(TargetPosition, time).SetEase(moveType).OnComplete(() =>
            {
                bIsMoving = false;

            }); ;

        }


    }
}

