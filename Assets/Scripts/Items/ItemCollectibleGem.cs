using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectibleGem : ItemCollectibleBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddGems();
    }
}
