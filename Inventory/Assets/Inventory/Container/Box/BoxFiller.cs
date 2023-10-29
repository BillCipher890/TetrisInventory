using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoxFiller : MonoBehaviour
{
    public GameObject[] DataPrefubs;

    private void Start()
    {
        StartCoroutine(Fill());
    }

    private IEnumerator Fill()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        //yield return new WaitForEndOfFrame();

        var id = transform.parent.parent.GetChild(0).GetComponent<InventoryLoader>().createdDatas.Max() + 1;

        var prefub = Instantiate(DataPrefubs[0], transform);
        prefub.GetComponent<DataModel>().Id = id;
        prefub.GetComponent<Image>().overrideSprite = prefub.GetComponent<DataModel>().Sprite;
        id++;

        for (int i = 1; i < DataPrefubs.Length; i++)
        {
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                prefub = Instantiate(DataPrefubs[i], transform);
                prefub.GetComponent<DataModel>().Id = id;
                prefub.GetComponent<Image>().overrideSprite = prefub.GetComponent<DataModel>().Sprite;
                id++;
            }
        }
    }
}
