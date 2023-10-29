using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
                    cells[i, j].GetComponent<Image>().color = Color.red;
                    cellModel.MaybeIsBusy = true;
                    cellModel.MaybeDataId = model.Id;
                }
                else
                {
                    cells[i, j].GetComponent<Image>().color = Color.white;
                    cellModel.MaybeIsBusy = false;
                    cellModel.MaybeDataId = -1;
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var isFirstBusy = false;
        Vector3 firstPosition = new Vector3();
        Vector3 lastPosition = new Vector3();
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                var cellModel = cells[i, j].GetComponent<CellModel>();
                cellModel.IsBusy = cellModel.MaybeIsBusy;
                cellModel.DataId = cellModel.MaybeDataId;
                if(!isFirstBusy && cellModel.IsBusy)
                {
                    isFirstBusy = true;
                    firstPosition = cells[i, j].position;
                }
                if (cellModel.IsBusy)
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
    }
}
