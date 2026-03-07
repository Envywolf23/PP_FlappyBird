using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject columnPrefab;

    [Header("Pool Settings")]
    [SerializeField] private int initialPoolSizePerType = 2;

    // Pools for each column type
    private Queue<Column> columnPool = new Queue<Column>();

    private void Awake()
    {
        for (int i = 0; i < initialPoolSizePerType; i++)
            columnPool.Enqueue(CreateNewColumn());
    }

    /// <summary>
    /// Retrieves an inactive <see cref="Column"/> instance from the pool, or creates a new one if none are available.
    /// </summary>
    /// <remarks>Use this method to efficiently reuse <see cref="Column"/> instances and minimize object
    /// instantiation overhead. The returned column will be inactive; it is the caller's responsibility to activate and
    /// configure it as needed.</remarks>
    /// <returns>A <see cref="Column"/> object that is not currently active in the scene. If all pooled columns are active, a new
    /// instance is created, added to the pool, and returned.</returns>
    public Column GetColumnFromPool()
    {
        foreach (var column in columnPool)
        {
            if (!column.gameObject.activeInHierarchy)
            {
                return column;
            }
        }

        // If all columns are active, create a new one
        Column newColumn = CreateNewColumn();
        columnPool.Enqueue(newColumn);
        //Debug.Log($"[ColumnPool] Pool expanded to {columnPool.Count} instances");
        return newColumn;
    }

    /// <summary>
    /// Creates a new inactive <see cref="Column"/> instance using the configured column prefab.
    /// </summary>
    /// <remarks>The returned <see cref="Column"/> is instantiated but not active in the scene. Callers are
    /// responsible for activating and positioning the column as needed.</remarks>
    /// <returns>A new <see cref="Column"/> component attached to the instantiated column prefab, initially inactive.</returns>
    private Column CreateNewColumn()
    {
        GameObject newColumn = Instantiate(columnPrefab);
        newColumn.SetActive(false);
        return newColumn.GetComponent<Column>();
    }
}
