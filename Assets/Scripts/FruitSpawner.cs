using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject fruitPrefabs;

    float left = -5f;
    float right = 5f;
    float speed;

    float spawn_time = 5f;
    float spawn_timer = 0f;

    float progressScale = 0.01f;
    float scale;

    [SerializeField] float fruitForce = 5f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = left;
        spawn_timer = spawn_time;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if(spawn_timer <= 0)
        {
            // check scale and to get how many fruits need to be spawned
            // spawn fruits based on the scale value
            spawn_timer = spawn_time;
            int fruitCount = checkScale();
            for (int i = 0; i < fruitCount; i++)
            {
                GameObject fruit = Instantiate(fruitPrefabs, new Vector3(Random.Range(left, right), -10f, 0f), Quaternion.identity);
                fruit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(14f, 16f), ForceMode2D.Impulse);
            }
            updateScale();
            Debug.Log("Spawned " + fruitCount + " fruits");
        }
        else
        {
            spawn_timer -= Time.deltaTime;
        }

        Debug.Log("Scale: " + scale);
    }
    
    void move()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, right - left) + left, transform.position.y, transform.position.z);
    }

    void updateScale()
    {
        scale += progressScale * 2;
        print("Scale: " + scale);
        spawn_time = Mathf.Clamp(spawn_time - scale, 2f, 5f);
    }
    
    int checkScale()
    {
        if (scale < 0.01f)
        {
            return 1;
        }
        else if (scale < .03f)
        {
            return Random.Range(1, 3);
        }
        else if (scale < 1f)
        {
            return Random.Range(3, 5);
        }
        else
        {
            return Random.Range(1, 5);
        }
    }

}
