using UnityEngine;

public class LineTrial : MonoBehaviour
{
    [SerializeField] private GameObject slashTrailPrefab;
    [SerializeField] private float minCuttingVelocity = 1f;

    private Rigidbody2D rb;
    private CircleCollider2D circleCollider2D;
    private GameObject currentTrail;
    private Camera cam;

    private Vector2 prevPosition;
    private Vector2 targetPosition;
    public bool isSlashing = false;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartCutting();
        else if (Input.GetMouseButtonUp(0)) StopCutting();

        if (isSlashing)
            targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        if (!isSlashing) return;

        float velocity = (targetPosition - prevPosition).magnitude * Time.fixedDeltaTime;
        circleCollider2D.enabled = velocity > minCuttingVelocity;

        rb.MovePosition(targetPosition);
        prevPosition = targetPosition;
    }

    void StartCutting()
    {
        isSlashing = true;
        circleCollider2D.enabled = false;
        prevPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        targetPosition = prevPosition;
        currentTrail = Instantiate(slashTrailPrefab, transform);
    }

    void StopCutting()
    {
        isSlashing = false;
        circleCollider2D.enabled = false;

        if (currentTrail != null)
        {
            currentTrail.transform.SetParent(null);
            Destroy(currentTrail, 0.05f);
            currentTrail = null;
        }
    }
}