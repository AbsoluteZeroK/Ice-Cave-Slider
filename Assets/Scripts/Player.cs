using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nevelson.TerrainSystem
{
    [RequireComponent(typeof(Rigidbody2D))]


    public class Player : MonoBehaviour
    {
        public float defaultMoveSpeed = 6f;
        private Vector2 moveVelocity;
        private Rigidbody2D rigidBody;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private bool isSpriteFlipped = false;

        // Start is called before the first frame update
        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            Vector2 inputRaw = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            SetMovementVelocity(inputRaw);
            SetSpriteRendererDirection(inputRaw.x);
            SetAnimationState(inputRaw);
        }

         private void FixedUpdate()
        {
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + moveVelocity * Time.fixedDeltaTime);
        }

        private void SetMovementVelocity(Vector2 inputRaw)
        {
            moveVelocity = inputRaw * defaultMoveSpeed;
        }

        private void SetSpriteRendererDirection(float inputRawX)
        {
            switch (inputRawX)
            {
                case 0:
                    spriteRenderer.flipX = isSpriteFlipped;
                    break;
                case -1:
                    isSpriteFlipped = false;
                    spriteRenderer.flipX = isSpriteFlipped;
                    break;
                case 1:
                    isSpriteFlipped= true;
                    spriteRenderer.flipX = isSpriteFlipped; 
                    break;
            }
        }

        private void SetAnimationState(Vector2 inputRaw)
        {
            animator.SetBool("isMoving", inputRaw != Vector2.zero);
        }
    }
}
