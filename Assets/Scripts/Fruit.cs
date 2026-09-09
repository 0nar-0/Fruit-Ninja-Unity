using UnityEngine;
using UnityEngine.Events;

public class Fruit : MonoBehaviour
{
   public UnityEvent onSliced;
   public GameObject slicedFruitPrefab;
   public GameObject holeFruitPrefab;
   public GameObject Fruithole;
   public bool isSliced = false;

   void Start()
   {
      isSliced = false;
      onSliced.AddListener(GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().addScore);
      onSliced.AddListener(() => GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().updateScore(1));
   }

   void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Blade")
      {
         Debug.Log("Fruit Sliced");
         isSliced = true;
         onSliced.Invoke();
         Destroy(gameObject);
         Instantiate(slicedFruitPrefab, transform.position, transform.rotation);
         Destroy(Fruithole);
      }
   }
}
