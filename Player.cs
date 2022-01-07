using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider; // collision box (object) that covers the player

    private Vector3 moveDelta; // how much has changed between frames

    private RaycastHit2D hit;
    

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Reset moveDelta
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(x, y, 0); //tracks movement of player

        //Swap sprite direction R/L

        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("People", "Blocking"));

        // make sure player can move in y direction by creating new hitbox above
        if (hit.collider == null) // if the collision returns null, move there
        {
            
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("People", "Blocking"));

        // make sure player can move in y direction by creating new hitbox above
        if (hit.collider == null) // if the collision returns null, move there
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }



    }
}