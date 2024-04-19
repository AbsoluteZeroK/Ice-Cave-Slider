using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;
    public LayerMask solidObjectsLayer;

    // Update is called once per frame
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal movement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                //Check for forgound collision
                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
    }

    //IEnumerator is a Coroutine, allows us to use yield statement like a return, but it does so differently where it returns but code will resume where it left off in the next frame
    //Great for things that happen over time, reduce the amount of boolean checks if we were to do all in Update function.
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        //below chekcs if there is any position change bigger than 0, it means player has moved.
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            //suspend action for 1 frame, helps break up movement, also has other function eg. 'new WaitForSeconds(1)' 
            yield return null;
        }
        transform.position = targetPos;

        
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }
}
