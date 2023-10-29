using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxModel : ContainerModel, IDropHandler
{
    private void Start()
    {
        StartCoroutine(UseOnStart());
        StartCoroutine(FillRandomData());
    }

    private IEnumerator FillRandomData()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        var prefubContainer = transform.parent.GetChild(2).GetChild(0);
        var startRow = 0;
        for (int i = 0; i < prefubContainer.childCount; i++)
        {
            CellModel cell;
            var model = prefubContainer.GetChild(i).GetComponent<DataModel>();
            if(model.Size == new Vector2(5, 2))
            {
                for (int j = startRow; j < startRow + 2; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        cell = cells[k, j].GetComponent<CellModel>();
                        cell.IsBusy = true;
                        cell.DataId = model.Id;
                    }
                }
                startRow += 2;
                continue;
            }

            if (model.Size == new Vector2(2, 2))
            {
                for (int j = startRow; j < startRow + 2; j++)
                {
                    int place = Random.Range(0, 4);
                    for (int k = place; k < place + 2; k++)
                    {
                        cell = cells[k, j].GetComponent<CellModel>();
                        cell.IsBusy = true;
                        cell.DataId = model.Id;
                    }
                }
                startRow += 2;
                continue;
            }

            if (model.Size == new Vector2(3, 1))
            {
                for (int j = startRow; j < startRow + 1; j++)
                {
                    int place = Random.Range(0, 3);
                    for (int k = place; k < place + 3; k++)
                    {
                        cell = cells[k, j].GetComponent<CellModel>();
                        cell.IsBusy = true;
                        cell.DataId = model.Id;
                    }
                }
                startRow += 1;
                continue;
            }

            if (model.Size == new Vector2(1, 1))
            {
                for (int j = startRow; j < startRow + 1; j++)
                {
                    int place = Random.Range(0, 5);
                    for (int k = place; k < place + 1; k++)
                    {
                        cell = cells[k, j].GetComponent<CellModel>();
                        cell.IsBusy = true;
                        cell.DataId = model.Id;
                    }
                }
                startRow += 1;
                continue;
            }
        }
        StartCoroutine(BoxStartPlaceItems());
    }

    private IEnumerator BoxStartPlaceItems()
    {
        yield return null;

        var inBoxContainer = transform.parent.GetChild(2).GetChild(0);
        for (int k = 0; k < inBoxContainer.childCount; k++)
        {
            PlaceItem(inBoxContainer.GetChild(k));
        }
    }

    public void PlaceItem(Transform dataTransform)
    {
        var isFirstBusy = false;
        var firstPosition = new Vector3();
        var lastPosition = new Vector3();

        var dataModel = dataTransform.GetComponent<DataModel>();

        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                var cellModel = cells[i, j].GetComponent<CellModel>();
                if (!isFirstBusy && cellModel.DataId == dataModel.Id)
                {
                    isFirstBusy = true;
                    firstPosition = cells[i, j].position;
                }
                if (cellModel.DataId == dataModel.Id)
                {
                    lastPosition = cells[i, j].position;
                }
            }
        }
        dataTransform.position = new Vector3(
            (firstPosition.x + lastPosition.x) / 2f,
            (firstPosition.y + lastPosition.y) / 2f,
            0);
        dataModel.Position = dataTransform.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.parent.GetChild(0).GetComponent<InventoryModel>().MaybeBecomeRealFields(eventData.pointerDrag.transform);
        GlobalEventManager.SendCellBusyChange();
    }
}
