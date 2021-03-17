using System.Collections;
using UnityEngine;

public class Installer : MonoBehaviour
{
    private void Awake()
    {
        var mapGenerator = new ServiceMapGenerator();
        ServiceLocator.Instance.RegisterService<IMapGenerator>(mapGenerator);
    }
}
