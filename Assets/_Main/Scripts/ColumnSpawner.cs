using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnCooldown;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject[] columns;


    private float cooldownTimer = Mathf.Infinity;
    private int lastColumnIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= spawnCooldown)
        {
            SpawnColumn();
        }
    }

    private void SpawnColumn()
    {
        int randomColumn = FindColumn();
        Debug.Log(randomColumn);
        //GameObject columnClone = Instantiate(columns[randomColumn], spawnPoint.position, Quaternion.identity);
        //columnClone.SetActive(true);
        columns[randomColumn].transform.position = spawnPoint.position;
        columns[randomColumn].GetComponent<Column>().Activate();
        cooldownTimer = 0;
    }

    private int FindColumn()
    {
        int randomColumn = Random.Range(0, columns.Length);
        while (randomColumn == lastColumnIndex)
        {
            randomColumn = Random.Range(0, columns.Length);
        }
        lastColumnIndex = randomColumn;
        return randomColumn;
    }

    private int FindNextPreviousColumn()
    {
        int randomColumn = 0;

        if(lastColumnIndex != -1)
        {
            if (lastColumnIndex == columns.Length - 1)
            {
                randomColumn = lastColumnIndex - 1;
            }
            else if (lastColumnIndex == 0)
            {
                randomColumn = lastColumnIndex + 1;
            }
            else
            {
                int randomNumber = Random.Range(0, 2);
                randomColumn = randomNumber == 0 ? lastColumnIndex - 1 : lastColumnIndex + 1;
            }

            lastColumnIndex = randomColumn;
        }
        else
        {
            randomColumn = Random.Range(0, columns.Length);
            lastColumnIndex = randomColumn;
        }

        return randomColumn;
    }
}
