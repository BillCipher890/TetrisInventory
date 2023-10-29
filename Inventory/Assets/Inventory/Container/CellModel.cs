using UnityEngine;

public class CellModel : MonoBehaviour
{
    public bool IsBusy = false;
    public int DataId = -1;

    public bool MaybeIsBusy = false;
    public int MaybeDataId = -1;

    private void Start()
    {
        GlobalEventManager.OnModelPositionChangedForCells += OnModelPositionChangedForCells;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnModelPositionChangedForCells -= OnModelPositionChangedForCells;
    }

    void OnModelPositionChangedForCells(Vector3 position, DataModel model)
    {
        var sizeDelta = transform.GetComponent<RectTransform>().sizeDelta;
        var leftDown = new Vector3(
            transform.position.x - sizeDelta.x / 2, 
            transform.position.y - sizeDelta.y / 2, 
            transform.position.z);

        if (position.x > leftDown.x && position.x < leftDown.x + sizeDelta.x &&
            position.y > leftDown.y && position.y < leftDown.y + sizeDelta.y)
        {
            GlobalEventManager.SendCellUnderData(transform, model);
        }
    }
}
