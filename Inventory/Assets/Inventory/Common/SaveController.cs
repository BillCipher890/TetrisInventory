using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public void OnClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
