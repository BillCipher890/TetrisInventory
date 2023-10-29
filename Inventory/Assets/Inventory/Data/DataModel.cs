using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DataModel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public int Id;
    public string Name;
    [Range(0, 1)] public float State;
    public Sprite Sprite;
    public Vector2 Size;

    public Vector3 Position;
    private Vector3 _offset;

    private void Start()
    {
        GlobalEventManager.OnStartDataController += OnStartDataController;
        GlobalEventManager.SendStartDataModel(Sprite);
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnStartDataController -= OnStartDataController;
    }

    void OnStartDataController(Transform sendedTransform)
    {
        Position = sendedTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = Position - Input.mousePosition;
        transform.GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Position = Input.mousePosition + _offset;
        GlobalEventManager.SendModelPositionChanged(Position, Id);

        var RectTransform = transform.GetComponent<RectTransform>();
        var leftUpCornerPosition = new Vector3(
            transform.position.x - RectTransform.sizeDelta.x / 2,
            transform.position.y + RectTransform.sizeDelta.y / 2,
            transform.position.z);

        GlobalEventManager.SendModelPositionChangedForCells(leftUpCornerPosition, transform.GetComponent<DataModel>());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<Image>().raycastTarget = true;

        StartCoroutine(TakeBackIfNotInInventory());
    }

    private IEnumerator TakeBackIfNotInInventory()
    {
        yield return new WaitForEndOfFrame();
        if (transform.parent == transform.parent.parent.GetChild(0))
        {
            transform.parent.parent.parent.GetChild(1).GetComponent<BoxModel>().PlaceItem(transform);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.SetParent(eventData.pointerDrag.transform.parent.parent.GetChild(0));
        StartCoroutine(TakeBackIfNotInInventory());
    }
}
