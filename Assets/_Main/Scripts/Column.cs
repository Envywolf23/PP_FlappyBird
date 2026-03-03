using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField][Range(0.1f, 3f)]
    private float speed;

    [Header("Gap")]
    [SerializeField] private Transform topColumn;
    [SerializeField] private Transform bottomColumn;
    [SerializeField] private float gapSize = 3f;

    private bool isMoving = true;
    private bool alreadyDetected = false;

    // Cache references for performance
    private Camera playerCamera;
    private Collider2D columnCollider;

    private void Awake()
    {
        playerCamera = Camera.main;
        columnCollider = GetComponentInChildren<Collider2D>();
        ApplyGap();
    }

    /// <summary>
    /// Sets the vertical position of the gap in world space.
    /// </summary>
    /// <param name="worldY">The Y-coordinate in world space to position the gap. Represents the vertical location where the gap will be
    /// placed.</param>
    public void SetGapPosition(float worldY)
    {
        transform.position = new Vector3(transform.position.x, worldY, 0);
    }

    /// <summary>
    /// Sets the size of the gap between elements.
    /// </summary>
    /// <param name="size">The gap size, in units. Must be a non-negative value.</param>
    public void SetGapSize(float size)
    {
        gapSize = size;
        ApplyGap();
    }

    /// <summary>
    /// Adjusts the positions of the top and bottom columns to create a vertical gap of the specified size between them.
    /// </summary>
    /// <remarks>This method centers the gap by moving the top column upward and the bottom column downward by
    /// half the gap size. The gap size is determined by the value of <c>gapSize</c>.</remarks>
    private void ApplyGap()
    {
        float halfGap = gapSize / 2f;
        topColumn.localPosition = new Vector3(0, halfGap, 0);
        bottomColumn.localPosition = new Vector3(0, -halfGap, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) return;

        transform.Translate(speed * Time.deltaTime * -1f, 0, 0);
        
        if (IsVisible())
        {
            alreadyDetected = true;
        }
        else if (alreadyDetected)
        {
            Deactivate();
        }
    }

    /// <summary>
    /// Determines whether the column's collider is within the view frustum of the player camera.
    /// </summary>
    /// <returns><see langword="true"/> if the column's collider is at least partially visible to the player camera; otherwise,
    /// <see langword="false"/>.</returns>
    private bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);
        return GeometryUtility.TestPlanesAABB(planes, columnCollider.bounds);
    }

    private void Deactivate()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        isMoving = true;
        alreadyDetected = false;
        gameObject.SetActive(true);
    }
}
