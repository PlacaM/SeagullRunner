using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Taller
{

   [System.Serializable]
    public class DieEvent : UnityEvent { };

    [System.Serializable]
    public class DamagedEvent : UnityEvent<float> { };


    [RequireComponent( typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AudioSource))]

    public class Character : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected CapsuleCollider2D capsuleCollider2D;
        protected SpriteRenderer spriteRenderer;
        protected AudioSource audioSource;

        [Header("Default Properties")]
        public bool modifyDefaults=false;


        public bool canMove = true;
        public float moveSpeed = 5f;
        public bool lockRotation = true;

        private bool bCanMove;

        private Vector2 moveDir;
        private Vector2 gravity ;
       
        Vector2 finalVelocity;

       [Header("Character Events")]
        public DieEvent onCharacterDie;
        public DamagedEvent onGetDamage;


        protected virtual void Awake()
        {

            gravity = Physics2D.gravity;
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            rb = GetComponent<Rigidbody2D>(); 

            if(lockRotation)
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();

            
        }

        public void PlaySound(AudioClip clip)
        {

        }
         
        public void AddMovementInput(Vector2 direction)
        {
            moveDir = direction;
        }
        
        public void ApplyDamage(float damage,GameObject instigator=null)
        {

            onGetDamage?.Invoke(damage);

        }
        ///player events
        ///
         public void KillCharacter(bool destroyObject=false)
        {
            
           onCharacterDie?.Invoke();


            if (destroyObject)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);


        }
    }
}