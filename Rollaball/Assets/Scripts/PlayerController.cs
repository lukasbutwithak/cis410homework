using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    private Rigidbody rb;

    private int count;
    private bool grounded;
    private bool canJump;

    private float movementX;
    private float movementY;
    private float movementZ;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        grounded = true;
        canJump = true;
        SetCountText();

        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed && (grounded || canJump))
        {
            movementZ = 25.0f;

            if (canJump)
            {
                canJump = false;
            }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if (rb.transform.position.y <= 0.5f)
        {
            grounded = true;
            canJump = true;
        }

        else
        {
            grounded = false;
        }
        
        Vector3 movement = new Vector3(movementX, movementZ, movementY);
        
        rb.AddForce(movement * speed);

        movementZ = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}
