using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnObject
    {
        public GameObject prefab;
        public float weight = 1f;
    }

    float left = -5f;
    float right = 5f;
    float speed;

    float spawn_time = 5f;
    float spawn_timer = 0f;

    float progressScale = 0.01f;
    float scale;

    public SpawnObject[] spawnObjects;

    public AudioSource fruitSpawnSound;

    void Start()
    {
        speed = left;
        spawn_timer = spawn_time;
    }

    void Update()
    {
        // move();

        if (spawn_timer <= 0)
        {
            spawn_timer = spawn_time;

            int fruitCount = checkScale();

            for (int i = 0; i < fruitCount; i++)
            {
                GameObject spawnObject = getSpawnObject();

                GameObject fruit = Instantiate(
                    spawnObject,
                    new Vector3(Random.Range(left, right), -10f, 0f),
                    Quaternion.identity
                );

                fruit.GetComponent<Rigidbody2D>().AddForce(
                    Vector2.up * Random.Range(14f, 16f),
                    ForceMode2D.Impulse
                );

                Debug.Log("Spawned object " + fruit.name);
            }

            updateScale();
            fruitSpawnSound.Play();
        }
        else
        {
            spawn_timer -= Time.deltaTime;
        }
    }

    GameObject getSpawnObject()
    {
        float totalWeight = 0f;

        foreach (SpawnObject spawnObject in spawnObjects)
        {
            totalWeight += spawnObject.weight;
        }

        float randomWeight = Random.Range(0f, totalWeight);

        foreach (SpawnObject spawnObject in spawnObjects)
        {
            randomWeight -= spawnObject.weight;

            if (randomWeight <= 0)
            {
                return spawnObject.prefab;
            }
        }

        return spawnObjects[spawnObjects.Length - 1].prefab;
    }

    void updateScale()
    {
        scale += progressScale * 2;
        spawn_time = Mathf.Clamp(spawn_time - scale, 2f, 5f);
    }

    int checkScale()
    {
        if (scale < 0.01f)
        {
            return 1;
        }
        else if (scale < 0.05f)
        {
            return Random.Range(1, 3);
        }
        else if (scale < 1f)
        {
            return Random.Range(1, 4);
        }
        else
        {
            return Random.Range(2, 4);
        }
    }
}

