using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 1.5f)]
    private float speed;

    private bool isMoving = true;
    private bool AlreadyDetected = false;

    public bool columnVisible = false;

    [SerializeField]
    private Camera playerCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime * -1;
        if(isMoving)
        {
            transform.Translate(movementSpeed, 0, 0);
        }
        if (isVisible())
        {
            AlreadyDetected = true;
        }
        else
        {
            Deactivate();
        }
    }

    private bool isVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        return GeometryUtility.TestPlanesAABB(planes, this.GetComponentInChildren<Collider2D>().bounds);
    }

    private void Deactivate()
    {
        if(AlreadyDetected)
        {
            isMoving = false;
            gameObject.SetActive(false);
        }
    }

    public void Activate()
    {
        isMoving = true;
        AlreadyDetected = false;
        gameObject.SetActive(true);
    }
}
