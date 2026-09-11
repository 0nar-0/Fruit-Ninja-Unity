using UnityEngine;
using UnityEngine.Events;

public class deadzone : MonoBehaviour
{
    public UnityEvent onFruitMissed;
    private GameObject fruit;

    void Start()
    {
        onFruitMissed.AddListener(GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().subtractLife);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            fruit = other.gameObject;
            if (fruit.GetComponent<Fruit>())
            {
                if (fruit.GetComponent<Fruit>().isSliced == false)
                {
                    onFruitMissed.Invoke();
                }
            }

            Destroy(other.gameObject);
        } else if (other.tag == "Bomb")
        {
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Fruit Missed");
        }
    }

}
