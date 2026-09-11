using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    // the game prefab reference
    [SerializeField] private GameObject bladePrefab;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] public GameObject LevelLoader;
    GameObject Fruit;

    // game veriables 
    [SerializeField] public int score;
    bool isGameOver;
    bool isGameRunning;

    [SerializeField] private int lives = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setupGame();
        spawnerPrefab = GameObject.FindGameObjectWithTag("Spawner");
        LevelLoader = GameObject.FindGameObjectWithTag("LevelLoader");
    }

    void setupGame()
    {
        isGameRunning = true;
        score = 0;
        isGameOver = false;
        Instantiate(bladePrefab, Vector3.zero, Quaternion.identity);
    }

    public void gameOver()
    {
        if (isGameOver) { return; }
        isGameRunning = false; isGameOver = true;
        Debug.Log("Game Over");
        spawnerPrefab.SetActive(false);
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject fruit in fruits)
        {
            Destroy(fruit);
        }
        StartCoroutine(waitForBombSound());
    }

    IEnumerator waitForBombSound()
    {
        yield return new WaitForSeconds(4f);
        LevelLoader.GetComponent<sceneloader>().LoadNextScene();
    }



    public void addScore()
    {
        score += 1;
        //Debug.Log("Score: " + score);
    }

    public void subtractLife()
    {
        lives -= 1;
        Debug.Log("Lives: " + lives);
        if (lives == 0)
        {
            gameOver();
        }
    }
}
