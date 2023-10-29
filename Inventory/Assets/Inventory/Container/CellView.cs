using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    void Start()
    {
        GlobalEventManager.OnCellBusyChange += OnCellModelBusyChange;
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnCellBusyChange -= OnCellModelBusyChange;
    }

    private void OnCellModelBusyChange()
    {
        var model = transform.GetComponent<CellModel>();
        if (model.IsBusy)
        {
            transform.GetComponent<Image>().color = Color.grey;
            return;
        }

        if (model.MaybeIsBusy)
        {
            transform.GetComponent<Image>().color = Color.white;
            return;
        }

        transform.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f);
    }
}
