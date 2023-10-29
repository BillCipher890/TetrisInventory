using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxModel : ContainerModel
{
    private void Start()
    {
        StartCoroutine(UseOnStart());
        StartCoroutine(FillRandomData());
        StartCoroutine(BoxStartPlaceItems());
    }

    private IEnumerator FillRandomData()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        var cell = cells[0, 0].GetComponent<CellModel>();
        cell.IsBusy = true;
        cell.DataId = 0;

        cell = cells[0, 1].GetComponent<CellModel>();
        cell.IsBusy = true;
        cell.DataId = 0;

        cell = cells[1, 0].GetComponent<CellModel>();
        cell.IsBusy = true;
        cell.DataId = 0;

        cell = cells[1, 1].GetComponent<CellModel>();
        cell.IsBusy = true;
        cell.DataId = 0;
    }

    private IEnumerator BoxStartPlaceItems()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

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
}
