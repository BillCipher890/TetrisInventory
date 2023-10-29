using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InventoryModel : ContainerModel, IDropHandler
{
    private void Start()
    {
        StartCoroutine(UseOnStart());
        GlobalEventManager.OnCellUnderData += OnCellUnderData;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnCellUnderData -= OnCellUnderData;
    }

    void OnCellUnderData(Transform transform, DataModel model)
    {
        CellModel cellModel;
        var place1 = int.MinValue;
        var place2 = int.MinValue;
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                cellModel = cells[i, j].GetComponent<CellModel>();
                if (cells[i, j] == transform)
                {
                    place1 = i;
                    place2 = j;
                }
                if (i >= place1 && j >= place2 &&
                    i < Mathf.Min(place1 + model.Size.x, cells.GetLength(0)) &&
                    j < Mathf.Min(place2 + model.Size.y, cells.GetLength(1)))
                {
                    cellModel.MaybeIsBusy = true;
                    cellModel.MaybeDataId = model.Id;
                }
                else
                {
                    if (cellModel.DataId == model.Id || cellModel.DataId == -1)
                    {
                        cellModel.MaybeIsBusy = false;
                        cellModel.MaybeDataId = -1;
                    }
                }
            }
        }
        GlobalEventManager.SendCellBusyChange();
    }

    public void MaybeBecomeRealFields(Transform dataTransform)
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                var cellModel = cells[i, j].GetComponent<CellModel>();
                if (cellModel.DataId == -1 || cellModel.MaybeDataId == -1)
                {
                    cellModel.IsBusy = cellModel.MaybeIsBusy;
                    cellModel.DataId = cellModel.MaybeDataId;
                    continue;
                }

                if(cellModel.DataId != cellModel.MaybeDataId)
                {
                    dataTransform.SetParent(dataTransform.parent.parent.GetChild(0));
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        MaybeBecomeRealFields(eventData.pointerDrag.transform);
        var isFirstBusy = false;
        Vector3 firstPosition = new Vector3();
        Vector3 lastPosition = new Vector3();
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                var cellModel = cells[i, j].GetComponent<CellModel>();
                if (!isFirstBusy && cellModel.DataId == eventData.pointerDrag.GetComponent<DataModel>().Id)
                {
                    isFirstBusy = true;
                    firstPosition = cells[i, j].position;
                }
                if (cellModel.DataId == eventData.pointerDrag.GetComponent<DataModel>().Id)
                {
                    lastPosition = cells[i, j].position;
                }
            }
        }

        eventData.pointerDrag.transform.position = new Vector3(
            (firstPosition.x + lastPosition.x) / 2f,
            (firstPosition.y + lastPosition.y) / 2f,
            0);

        eventData.pointerDrag.transform.SetParent(eventData.pointerDrag.transform.parent.parent.GetChild(1));
        GlobalEventManager.SendCellBusyChange();
    }
}
