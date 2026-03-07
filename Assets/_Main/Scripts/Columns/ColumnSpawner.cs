using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private float spawnCooldown = 2f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ColumnPool columnPool;

    [Space(2)]
    [Header("Vertical range for gap position")]
    [SerializeField] private float minGapY = -2f;
    [SerializeField] private float maxGapY = 2f;

    [Space(2)]
    [Header("Gap size range")]
    [SerializeField] private float minGapSize = 2f;
    [SerializeField] private float maxGapSize = 6f;
    [SerializeField] private float initialGapSize = 4f;
    //[SerializeField] private float gapShrinkPerSpawn = 0.05f;

    private float cooldownTimer = Mathf.Infinity;
    private float currentGapSize;

    [Header("Despawning")]
    [SerializeField] private float boundaryX;
    private List<Column> activeColumns = new List<Column>();

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

        for (int i = activeColumns.Count - 1; i >= 0; i--)
        {
            if (activeColumns[i].transform.position.x < boundaryX)
            {
                activeColumns[i].Deactivate();

                int last = activeColumns.Count - 1;
                activeColumns[i] = activeColumns[last];
                activeColumns.RemoveAt(last);
            }
        }
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

        activeColumns.Add(column);
    }
}
