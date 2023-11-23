using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyOne : MonoBehaviour
{

    public float speed;
    public bool ground = true;
    public Transform GroundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    void Start()
    {
        
    }
void Update()
{
    transform.Translate(Vector2.right * speed * Time.deltaTime);

    if (GroundCheck != null)
    {
        ground = Physics2D.Linecast(GroundCheck.position, transform.position, groundLayer);
        if (ground == false)
        {
            speed *= -1f;
        }
    }

    if (speed < 0 && facingRight)
    {
        Flip();
    }
    else if (speed > 0 && facingRight)
    {
        Flip();
    }
}



    void Flip() 
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }


}