using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject startTextObject;

    private Rigidbody rb;
    private AudioSource omnomnom;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        omnomnom = GetComponent<AudioSource>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        startTextObject.SetActive(true);
    }

    private void OnMovement(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 30)
        {
            winTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 upwardMovement = new Vector3(0, 1, 0);
        rb.AddForce(movement * speed);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(upwardMovement * jumpHeight);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            omnomnom.Play();

            SetCountText();
            startTextObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Death"))
        {
            loseTextObject.SetActive(true);
            Death();
        }
    }

    private void Death()
    {
        Time.timeScale = 0;   
    }
}
