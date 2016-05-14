using UnityEngine;
using System.Collections;

public class ShieldItem : ItemBase {

    public override void BeginUseItem() {
        base.BeginUseItem();

        owner.GetComponent<AnimatorHandler>().RaiseShield();
    }


    public override void EndUseItem() {
        base.EndUseItem();

        owner.GetComponent<AnimatorHandler>().LowerShield();
    }
}
