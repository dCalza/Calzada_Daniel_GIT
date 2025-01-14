using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{

    public GameObject flashlight;
    private bool flashlightActive = false;

    public float speed = 5f;
    public float sprintMultiplier = 2f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    private bool isGrounded; // Para verificar si el personaje está en el suelo.

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        flashlight.SetActive(false);
    }

    private void Update()
    {
        // Movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= sprintMultiplier;
        }

        rb.MovePosition(transform.position + movement * Time.deltaTime);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.F) && flashlightActive == false)
        {
            flashlight.SetActive(true);
            flashlightActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && flashlightActive == true)
        {
            flashlight.SetActive(false);
            flashlightActive = false;
        }
    }

    // Detecta si está en el suelo utilizando colisiones
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
