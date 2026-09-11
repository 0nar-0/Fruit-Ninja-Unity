using UnityEngine;
using UnityEngine.Events;

public class Fruit : MonoBehaviour
{
    public UnityEvent onSliced;
    public GameObject slicedFruitPrefab;
    public AudioSource fruitSliceSound;
    public bool isSliced = false;

    void Start()
    {
        isSliced = false;

        onSliced.AddListener(
            GameObject.FindGameObjectWithTag("Game")
                .GetComponent<Game>()
                .addScore
        );

        onSliced.AddListener(
            () => GameObject.FindGameObjectWithTag("UI")
                .GetComponent<UIManager>()
                .updateScore(1)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Blade")
        {
            isSliced = true;
            fruitSliceSound.Play();
            onSliced.Invoke();

            Instantiate(
                slicedFruitPrefab,
                transform.position,
                transform.rotation
            );

            Destroy(gameObject);
        }
    }
}

