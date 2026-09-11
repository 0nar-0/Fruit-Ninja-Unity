using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public UnityEvent onBombExploded;

    // Audio references
    public AudioSource bombExplodeSound;
    public AudioSource bombSpawnStart;
    public AudioSource bombSpawnLoop;

    [SerializeField] private bool isExploded = false;

    private void Start()
    {
        isExploded = false;

        // Play the spawn/start sound once
        bombSpawnStart.Play();

        onBombExploded.AddListener(
            GameObject.FindGameObjectWithTag("Game")
                .GetComponent<Game>()
                .gameOver
        );
    }

    private void Update()
    {
        // Once the start sound finishes, start the looping sound
        if (!isExploded &&
            !bombSpawnStart.isPlaying &&
            !bombSpawnLoop.isPlaying)
        {
            bombSpawnLoop.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blade") && !isExploded)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Bomb Exploded");

        isExploded = true;

        // Stop the looping sound
        bombSpawnLoop.Stop();

        // Play explosion sound independently of the bomb GameObject
        AudioSource.PlayClipAtPoint(
            bombExplodeSound.clip,
            transform.position,
            3f
        );

        onBombExploded.Invoke();

        // Remove Rigidbody
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Destroy(rb);
        }

        Debug.Log("Start bomb animation");

        // If you eventually want to destroy the bomb:
        // Destroy(gameObject);
    }
}
