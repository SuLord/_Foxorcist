using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float DegreesPerSecond = 1000f; // degrees per second / Flip variables
    private Vector3 currentRot, targetRot;
    public bool rotating { get; private set; } // ENCAPSULATION
    public bool goingUp { get; private set; } // Jump variables
    private float castRange = 10;
    [SerializeField] float jumpForce = 30f;
    private float velocity;
    [SerializeField] float botBound = -1.45f;
    [SerializeField] float height = 4.7f;

    BoxCollider playerCollider; // Collider variables
    private float xSize = 4f;
    private float xStartSize;
    private float ySize = 3f;
    private float yStartSize;
    private float xCenter = 0.6f;
    private float yCenter = 1.6f;

    [SerializeField] float cooldownTime = 0.1f;
    [SerializeField] float lastUsedTime;

    public ParticleSystem hitParticles;
    [SerializeField] ParticleSystem killedParticles;
    public ParticleSystem powerUpParticles;
    [SerializeField] ParticleSystem lostLifeParticles;

    private GameManager gameManager;
    void Start()
    {
        rotating = false;
        goingUp = false;
        currentRot = transform.eulerAngles;
        playerCollider = GetComponent<BoxCollider>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        yStartSize = playerCollider.size.y;
        xStartSize = playerCollider.size.x;
    }
    void Update()
    {
        // ABSTRACTION
        InputRotate();
        InputJump();
    }
    private void InputRotate()
    {
        // Rotates 180° when clicking
        if (Input.GetKeyDown(KeyCode.Mouse0) && velocity == 0 && Time.time > lastUsedTime + cooldownTime)
        {
            StartCoroutine(Rotate());
            playerCollider.size = new Vector3(xSize, ySize, playerCollider.size.z);
            playerCollider.center = new Vector3(xCenter, yCenter, playerCollider.center.z);
            lastUsedTime = Time.time;
        }
    }
    private void InputJump()
    {
        // Raycast
        Vector3 direction = Vector3.up;
        Ray theRay = new Ray(transform.position + transform.up, transform.TransformDirection(direction * castRange));
        Debug.DrawRay(transform.position, transform.TransformDirection(direction * castRange));

        // Jumps when on the upper side and pressing space
        if (Physics.Raycast(theRay, out RaycastHit hit, castRange))
        {
            if (Input.GetKeyDown(KeyCode.Space) && hit.collider.tag == "Roof")
            {
                velocity = jumpForce;
                goingUp = true;
                playerCollider.size = new Vector3(ySize, ySize, playerCollider.size.z);
                playerCollider.center = new Vector3(xCenter, playerCollider.center.y, playerCollider.center.z);
            }
        }

        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        // Falls down after a short time
        if (transform.position.y > height)
        {
            StartCoroutine(Fall());
        }

        // Doesn't fall down to infinity
        if (transform.position.y < botBound)
        {
            transform.position = new Vector3(transform.position.x, botBound, 0f);
            velocity = 0f;
            playerCollider.size = new Vector3(xStartSize, yStartSize, playerCollider.size.z);
            playerCollider.center = new Vector3(0f, 1.3f, playerCollider.center.z); // Collider back to original center
        }
    }
    IEnumerator Rotate()
    {
        if (!rotating)
        {
            rotating = true;
            targetRot.x = currentRot.x + 180;

            while (currentRot.x < targetRot.x)
            {
                currentRot.x = Mathf.MoveTowardsAngle(currentRot.x, targetRot.x, DegreesPerSecond * Time.deltaTime);
                transform.eulerAngles = currentRot;
                yield return null;
            }
            yield return new WaitForSeconds(0.3f); // Cooldown
            rotating = false;
            playerCollider.size = new Vector3(xStartSize, yStartSize, playerCollider.size.z); // Collider back to original size
            playerCollider.center = new Vector3(0f, 1.3f, playerCollider.center.z); // Collider back to original center
        }
    }
    IEnumerator Fall()
    {
        transform.position = new Vector3(transform.position.x, height, 0f);
        yield return new WaitForSeconds(0.4f); // Obligatory CD
        velocity = -jumpForce;
        goingUp = false;
        lastUsedTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Player hit/killed
        if (other.CompareTag("Enemy") && !rotating && !goingUp)
        {
            lostLifeParticles.transform.position = other.gameObject.transform.position;
            lostLifeParticles.Play();
            gameManager.currentLives--;
            gameManager.UpdateLives();

            if(gameManager.currentLives < 1)
            {
                killedParticles.Play();
                gameManager.GameOver();
                Destroy(gameObject);
            }
        }
    }
}
