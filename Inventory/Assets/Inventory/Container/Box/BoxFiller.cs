using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxFiller : MonoBehaviour
{
    public GameObject[] DataPrefubs;

    private void Start()
    {
        var id = 0;

        var prefub = Instantiate(DataPrefubs[0], transform);
        prefub.GetComponent<DataModel>().Id = id;
        prefub.GetComponent<Image>().overrideSprite = prefub.GetComponent<DataModel>().Sprite;
        id++;

        for (int i = 1; i < DataPrefubs.Length; i++)
        {
            for (int j = 0; j < Random.Range(1,3); j++)
            {
                prefub = Instantiate(DataPrefubs[i], transform);
                prefub.GetComponent<DataModel>().Id = id;
                prefub.GetComponent<Image>().overrideSprite = prefub.GetComponent<DataModel>().Sprite;
                id++;
            }
        }
    }
}
