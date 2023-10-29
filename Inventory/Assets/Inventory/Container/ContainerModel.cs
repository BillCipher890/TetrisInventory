using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerModel : MonoBehaviour
{
    public int rowsCount;
    public int columnsCount;
    public Transform[,] cells;

    public IEnumerator UseOnStart()
    {
        yield return new WaitForEndOfFrame();
        cells = new Transform[columnsCount, rowsCount];
        for(int i = 0; i < rowsCount; i++)
        {
            for (int j = 0; j < columnsCount; j++)
            {
                cells[j, i] = transform.GetChild(j + i * columnsCount);
            }
        }
    }
}
