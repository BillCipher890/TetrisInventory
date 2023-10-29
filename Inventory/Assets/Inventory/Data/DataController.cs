using UnityEngine;
using UnityEngine.EventSystems;

public class DataController : MonoBehaviour
{
    private void Start()
    {
        GlobalEventManager.SendStartDataController(transform);
    }
}
