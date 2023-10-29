using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobalEventManager : MonoBehaviour
{
    public static event Action<Vector3, int> OnModelPositionChanged;

    public static event Action<Sprite, int> OnStartDataModel;
    public static event Action<Transform> OnStartDataController;

    public static event Action<Vector3, DataModel> OnModelPositionChangedForCells;
    public static event Action<Transform, DataModel> OnCellUnderData;
    public static event Action OnCellBusyChange;

    public static void SendModelPositionChanged(Vector3 position, int id)
    {
        OnModelPositionChanged?.Invoke(position, id);
    }

    // Исправить
    public static void SendModelPositionChangedForCells(Vector3 position, DataModel model)
    {
        OnModelPositionChangedForCells?.Invoke(position, model);
    }

    public static void SendStartDataModel(Sprite sprite, int id)
    {
        OnStartDataModel?.Invoke(sprite, id);
    }

    public static void SendStartDataController(Transform transform)
    {
        OnStartDataController?.Invoke(transform);
    }

    public static void SendCellUnderData(Transform transform, DataModel model)
    {
        OnCellUnderData?.Invoke(transform, model);
    }
    public static void SendCellBusyChange()
    {
        OnCellBusyChange?.Invoke();
    }
}
