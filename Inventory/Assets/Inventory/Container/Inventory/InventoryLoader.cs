using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoader : MonoBehaviour
{
    public GameObject[] DataPrefubs;
    [HideInInspector]public List<int> createdDatas;


    void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForEndOfFrame();


        var cells = transform.GetComponent<InventoryModel>().cells;
        bool isPrefsExist = false;

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                CellModel cellModel = cells[i, j].GetComponent<CellModel>();
                cellModel.DataId = PlayerPrefs.GetInt("DataId:" + i + "," + j, -1);
                cellModel.IsBusy = cellModel.DataId != -1;

                isPrefsExist = cellModel.IsBusy || isPrefsExist;
            }
        }
        if(isPrefsExist)
            GlobalEventManager.SendCellBusyChange();

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                CellModel cellModel = cells[i, j].GetComponent<CellModel>();
                if (!createdDatas.Contains(cellModel.DataId))
                {
                    createdDatas.Add(cellModel.DataId);
                    int length = 1;
                    int height = 1;
                    while (i + length < cells.GetLength(0) &&
                        cells[i + length, j].GetComponent<CellModel>().DataId == cellModel.DataId)
                    {
                        length++;
                    }
                    while (j + height < cells.GetLength(1) &&
                        cells[i, j + height].GetComponent<CellModel>().DataId == cellModel.DataId)
                    {
                        height++;
                    }
                    foreach (var data in DataPrefubs)
                    {
                        if (data.GetComponent<DataModel>().Size == new Vector2(length, height))
                        {
                            var newDataInInventory = Instantiate(data, transform.parent.GetChild(2).GetChild(1));

                            var position = cells[i + length - 1, j].position + cells[i, j + height - 1].position;

                            newDataInInventory.transform.position =
                                new Vector3(position.x / 2, position.y / 2, position.z / 2);
                            newDataInInventory.GetComponent<DataModel>().Position = newDataInInventory.transform.position;

                            newDataInInventory.GetComponent<DataModel>().Id = cellModel.DataId;
                            newDataInInventory.GetComponent<Image>().overrideSprite =
                                newDataInInventory.GetComponent<DataModel>().Sprite;
                            break;
                        }
                    }
                }
            }
        }
    }
}
