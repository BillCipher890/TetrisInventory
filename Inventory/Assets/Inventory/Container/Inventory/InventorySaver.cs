using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
    void Start()
    {
        GlobalEventManager.OnCellBusyChange += Save;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnCellBusyChange -= Save;
    }

    private void Save()
    {
        var cells = transform.GetComponent<InventoryModel>().cells;

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                PlayerPrefs.SetInt("DataId:" + i + "," + j, cells[i, j].GetComponent<CellModel>().DataId);
            }
        }
        PlayerPrefs.Save();
    }
}
