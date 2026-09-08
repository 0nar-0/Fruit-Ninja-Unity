using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] GameObject fruitPrefabs;

    float left = -10f;
    float right = 10f;
    float speed;

    float spawn_time = 2f;
    float spawn_timer = 0f;


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
            spawn_timer = spawn_time;
            Instantiate(fruitPrefabs, transform);
        }
        else
        {
            spawn_timer -= Time.deltaTime;
        }
    }
    
    void move()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, right - left) + left, transform.position.y, transform.position.z);
    }
}
