using UnityEngine;
using UnityEngine.Events;

public class Fruit : MonoBehaviour
{
   public UnityEvent onSliced;
   public GameObject slicedFruitPrefab;

   public bool isSliced = false;

    void Start()
    {
        onSliced.AddListener(GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().addScore);
    }

    void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Blade")
      {
         Debug.Log("Fruit Sliced");
         isSliced = true;
         onSliced.Invoke();
         Destroy(gameObject);
         Instantiate(slicedFruitPrefab,transform.position,transform.rotation);
         Destroy(this.gameObject);
      }
   }
}
