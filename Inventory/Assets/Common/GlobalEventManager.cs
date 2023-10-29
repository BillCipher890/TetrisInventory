using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalEventManager : MonoBehaviour
{
    public static event Action<Vector3> OnModelPositionChanged;

    public static event Action<Sprite> OnStartDataModel;
    public static event Action<Transform> OnStartDataController;

    public static event Action<Vector3, DataModel> OnModelPositionChangedForCells;
    public static event Action<Transform, DataModel> OnCellUnderData;

    public static void SendModelPositionChanged(Vector3 position)
    {
        OnModelPositionChanged?.Invoke(position);
    }

    // ���������
    public static void SendModelPositionChangedForCells(Vector3 position, DataModel model)
    {
        OnModelPositionChangedForCells?.Invoke(position, model);
    }

    public static void SendStartDataModel(Sprite sprite)
    {
        OnStartDataModel?.Invoke(sprite);
    }

    public static void SendStartDataController(Transform transform)
    {
        OnStartDataController?.Invoke(transform);
    }

    public static void SendCellUnderData(Transform transform, DataModel model)
    {
        OnCellUnderData?.Invoke(transform, model);
    }
}
