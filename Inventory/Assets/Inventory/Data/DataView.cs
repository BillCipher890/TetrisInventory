using UnityEngine;
using UnityEngine.UI;

public class DataView : MonoBehaviour
{
    void Start()
    {
        GlobalEventManager.OnModelPositionChanged += OnModelPositionChanged;
        GlobalEventManager.OnStartDataModel += OnStartDataModel;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnModelPositionChanged -= OnModelPositionChanged;
        GlobalEventManager.OnStartDataModel -= OnStartDataModel;
    }

    void OnModelPositionChanged(Vector3 position)
    {
        transform.position = position;
    }

    void OnStartDataModel(Sprite sprite)
    {
        transform.GetComponent<Image>().overrideSprite = sprite;
    }
}