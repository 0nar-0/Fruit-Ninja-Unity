using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineTrial : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject slashTrailPrefab;
    private GameObject currentTrail;
    private CircleCollider2D circleCollider2D;
    Camera cam;

    Vector2 prevPosition;
    public bool isSlashing = false;
    public float minCuttingVelocity = 0.001f;


    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        } 
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }

        if (isSlashing)
        {
            UpdateCutting();
        }
    }


    void UpdateCutting()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - prevPosition).magnitude * Time.deltaTime;
        if (velocity > minCuttingVelocity)
        {
            circleCollider2D.enabled = true;
        }
        else
        {
            circleCollider2D.enabled = false;
        }
    }

   void StartCutting()
    {
        isSlashing = true;
        circleCollider2D.enabled = false;
        currentTrail = Instantiate(slashTrailPrefab, transform);
    }

    void StopCutting()
    {
        isSlashing = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail,2f);
        circleCollider2D.enabled = false;
    }



}
