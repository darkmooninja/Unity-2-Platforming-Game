using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : collectable
{
    private void Start()
    {
        collectableName = "Coin";
        description = "Increases score by 10";
    }
    public override void Use()
    {
        player.GetComponent<playerManager>().ChangeScore(10);

    }


}