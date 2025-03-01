using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength = 13;
    public Logic logic;
    public bool birdIsAlive = true;
    private float topBoundary = 18.7f;
    private float bottomBoundary = -21f;
    public AudioSource flapSFX; 
    public AudioSource fellSFX; 
    public AudioSource hitPipeSFX; 
    public AudioSource MusicSFX; 
    private Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<Logic>();
        }

        // Debugging: Check if AudioSources are assigned
        if (flapSFX == null)
        {
            Debug.LogError("flapSFX is not assigned in the Inspector!");
        }
        if (fellSFX == null)
        {
            Debug.LogError("fellSFX is not assigned in the Inspector!");
        }
        if (hitPipeSFX == null)
        {
            Debug.LogError("hitPipeSFX is not assigned in the Inspector!");
        }
        if (MusicSFX == null)
        {
            Debug.LogError("MusicSFX is not assigned in the Inspector!");
        }

        MusicSFX.Play();

        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            myRigidBody.linearVelocity = Vector2.up * flapStrength;
            flapSFX.Play();
        }

        CheckIfOffScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (birdIsAlive)
        {
            logic.gameOver();
            birdIsAlive = false;
            hitPipeSFX.Play();
        }
    }

    private void CheckIfOffScreen()
    {
        // Get the bird's Y position
        float birdY = transform.position.y;

        // Check if the bird is above the top boundary or below the bottom boundary
        if (birdY > topBoundary || birdY < bottomBoundary)
        {
            if (birdIsAlive)
            {
                logic.gameOver();
                birdIsAlive = false;
                fellSFX.Play();
            }
        }
    }

    public void OnGameStart()
    {
        // Enable physics simulation
        myRigidBody.simulated = true;
        // Reset position if needed
        transform.position = startPosition;
        // Set bird to alive state
        birdIsAlive = true;
    }
}