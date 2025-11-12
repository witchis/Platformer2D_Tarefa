using System.Collections;
using System.Collections.Generic;
using EBAC.Core.Singleton;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    public enum VFXType
    {
        JUMP,
        DUST,
        COIN,
        FIREFLIES
    }

    public List<VFXManagerSetup> vFXSetup;

    public void PlayVFXByType(VFXType vFXType, Vector3 position)
    {
        foreach (var i in vFXSetup)
        {
            if (i.vFXType == vFXType)
            {
                var item = Instantiate(i.prefab);
                item.transform.position = position;
                Destroy(item.gameObject, 5f);
                break;
            }
        }
    }
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vFXType;
    public GameObject prefab;
}
