using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Gravity variables")]
    [Space(2)]
    [SerializeField] private float gravityScale;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float maxFallSpeed;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    private void FixedUpdate()
    {
        #region Gravity Logic

        if (rb.linearVelocity.y < 0f)
        {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
            //transform.Rotate(Vector3.forward * 10f * Time.fixedDeltaTime);
        }
        #endregion
    }
}
