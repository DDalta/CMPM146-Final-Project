using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 5f;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 rotation = mouse_position - transform.position;

        float rot_z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }
}
