using System.Linq;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [SerializeField] private DontDestroyObject _object;
    
    void Awake()
    {
        var sameObject = FindObjectsOfType<DontDestroy>().Any(x => x != this && x._object == _object);

        if (sameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

public enum DontDestroyObject
{
    Cheats,
    InputManager,
    GUI,
}
