using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveInput;

    private InputMaster controls; // Reference to the generated InputMaster class

    private bool codingViable; //Checks if Coding Station can be used

    private bool animationViable; //Checks if first Animation Station can be used

    private bool animationViable1; //Checks if second Animation Station can be used

    private bool artViable; // Checks if first art station is usable

    private bool artViable1; //Checks second art station

    private bool artViable2; //Checks third art station

    private bool artViable3; //checks if the fourth art station can be used

    private bool timerRunning;

    private float timer;

    private bool inRange;

    Coroutine timerRoutine = null;

    [SerializeField ]private GameObject progressBarCodeStation;

    [SerializeField] private GameObject progressBarArtStation;

    [SerializeField] private GameObject progressBarArtStation1;

    [SerializeField] private GameObject progressBarArtStation2;

    [SerializeField] private GameObject progressBarArtStation3;

    [SerializeField] private GameObject progressBarAnimationStation;

    [SerializeField] private GameObject progressBarAnimationStation2;

    private void Awake()
    {
        controls = new InputMaster(); // Initialize the InputMaster

        codingViable = false;

        animationViable = false;

        animationViable1 = false;

        artViable = false;

        artViable1 = false;

        artViable2 = false;

        artViable3 = false;

        timer = 0f;

        timerRunning = false;

        inRange = false;
    }

    private void OnEnable()
    {
        // Subscribe to the Movement action, correctly referenced here
        controls.Player.Movement.performed += OnMove;
        controls.Player.Movement.canceled += OnMove;  // Reset movement on cancel
        controls.Enable(); // Enable controls
    }

    private void OnDisable()
    {
        // Unsubscribe from the Movement action
        controls.Player.Movement.performed -= OnMove;
        controls.Player.Movement.canceled -= OnMove;
        controls.Disable(); // Disable controls
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("CodingStation"))
        {
            codingViable = true;
        }
        else if (collision.gameObject.name.Contains("AnimationStation1"))
        {
            animationViable1 = true;
        }
        else if (collision.gameObject.name.Contains("AnimationStation"))
        {
            animationViable = true;
        }
        else if (collision.gameObject.name.Contains("ArtStation1"))
        {
            artViable1 = true;
        }
        else if (collision.gameObject.name.Contains("ArtStation2.1"))
        {
            artViable3 = true;
        }
        else if (collision.gameObject.name.Contains("ArtStation2"))
        {
            artViable2 = true;
        }
        else if (collision.gameObject.name.Contains("ArtStation"))
        {
            artViable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("CodingStation"))
        {
            codingViable = false;
        }
        else if (collision.gameObject.name.Contains("AnimationStation1"))
        {
            animationViable1 = false;
        }
        else if (collision.gameObject.name.Contains("AnimationStation"))
        {
            animationViable = false;
        }
        else if (collision.gameObject.name.Contains("ArtStation1"))
        {
            artViable1 = false;
        }
        else if (collision.gameObject.name.Contains("ArtStation2.1"))
        {
            artViable3 = false;
        }
        else if (collision.gameObject.name.Contains("ArtStation2"))
        {
            artViable2 = false;
        }
        else if (collision.gameObject.name.Contains("ArtStation"))
        {
            artViable = false;
        }
        UpdateProgressBars(); // Ensure progress bars hide on exit
    }

    private void Update()
    {
        Debug.Log(inRange);

        Debug.Log(timer);

        MovePlayer();

        InteractStation();

        UpdateProgressBars(); // Update visibility every frame
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void miniGame(bool viable, float upperBound, float lowerBound)
    {
        if (viable)
        {
            if (timer < upperBound && timer > lowerBound)
            {
                inRange = true;

                Debug.Log("Ya did it");
            }
            else
            {
                inRange = false;

                Debug.Log("ya fucked up");
            }
            StopCoroutine(timerRoutine);

            timer = 0f;

            timerRunning = false;
        }
    }

    private void InteractStation()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!timerRunning)
            {
                timerRoutine = StartCoroutine(time());
            }
            else
            {
                if (codingViable) miniGame(true, 0.8f, 0.2f);

                else if (animationViable) miniGame(true, 0.8f, 0.4f);

                else if (animationViable1) miniGame(true, 1f, 0.6f);

                else if (artViable) miniGame(true, 1f, 0.4f);

                else if (artViable1) miniGame(true, 1.2f, 0.4f);

                else if (artViable2) miniGame(true, 0.8f, 0.2f);

                else if (artViable3) miniGame(true, 0.6f, 0.2f);
            }
        }
    }

    private void UpdateProgressBars()
    {
        progressBarCodeStation.GetComponent<MeshRenderer>().enabled = codingViable && timerRunning && timer < 0.8f && timer > 0.2f;

        progressBarAnimationStation.GetComponent<MeshRenderer>().enabled = animationViable && timerRunning && timer < 0.8f && timer > 0.4f;

        progressBarAnimationStation2.GetComponent<MeshRenderer>().enabled = animationViable1 && timerRunning && timer < 1f && timer > 0.6f;

        progressBarArtStation.GetComponent<MeshRenderer>().enabled = artViable && timerRunning && timer < 1f && timer > 0.4f;

        progressBarArtStation1.GetComponent<MeshRenderer>().enabled = artViable1 && timerRunning && timer < 1.2f && timer > 0.4f;

        progressBarArtStation2.GetComponent<MeshRenderer>().enabled = artViable2 && timerRunning && timer < 0.8f && timer > 0.2f;

        progressBarArtStation3.GetComponent<MeshRenderer>().enabled = artViable3 && timerRunning && timer < 0.6f && timer > 0.2f;
    }

    private IEnumerator time()
    {
        timerRunning = true;

        while (timer < 1.4f)
        {
            yield return new WaitForSeconds(0.2f);
            timer += 0.2f;
            Debug.Log(timer);
        }

        Debug.Log("Ya fucked up mode 2");
        timerRunning = false;
        timer = 0;
        yield break;
    }

    // Function to move the player based on input
    private void MovePlayer()
    {
        Vector3 move = new Vector3(-moveInput.x, 0, -moveInput.y) * moveSpeed * Time.deltaTime; // Negate the y component
        transform.Translate(move, Space.World); // Move the player
        //Debug.Log("Movement Vector: " + moveInput); // Debug the movement vector
    }
}
