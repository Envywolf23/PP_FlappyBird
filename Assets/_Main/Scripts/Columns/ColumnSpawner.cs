using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 2f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ColumnPool columnPool;

    [Header("Vertical range for gap position")]
    [SerializeField] private float minGapY = -2f;
    [SerializeField] private float maxGapY = 2f;

    [Header("Gap size range")]
    [SerializeField] private float minGapSize = 2f;
    [SerializeField] private float maxGapSize = 6f;
    [SerializeField] private float initialGapSize = 4f;
    //[SerializeField] private float gapShrinkPerSpawn = 0.05f;

    private float cooldownTimer = Mathf.Infinity;
    private float currentGapSize;

    private void Start()
    {
        currentGapSize = initialGapSize;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= spawnCooldown)
            SpawnColumn();
    }

    /// <summary>
    /// Spawns a new column at the designated spawn point with a randomized vertical gap position and size.
    /// </summary>
    /// <remarks>The spawned column is retrieved from the column pool, positioned at the spawn point, and
    /// activated for gameplay. The vertical gap position and size are randomized within the configured ranges. This
    /// method resets the spawn cooldown timer after spawning.</remarks>
    private void SpawnColumn()
    {
        float gapY = Random.Range(minGapY, maxGapY);

        Column column = columnPool.GetColumnFromPool();
        column.transform.position = spawnPoint.position;
        column.SetGapPosition(gapY);
        column.SetGapSize(currentGapSize);
        column.Activate();

        currentGapSize = Random.Range(minGapSize, maxGapSize);
        cooldownTimer = 0;
    }
}
