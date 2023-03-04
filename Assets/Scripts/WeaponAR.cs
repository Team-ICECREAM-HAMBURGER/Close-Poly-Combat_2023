using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAR : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 3.18f;
        base.ikHandL.weight = 1;
        base.ikHandL.data.target = base.refIKHandL;
    }

    private void OnEnable() {
        Init();
    }

    private void Update() {
        base.UpdateFire();
        base.UpdateAim();
        base.UpdateReload();
    }

    public override IEnumerator OnReload() {
        base.ikHandL.weight = 0;

        yield return StartCoroutine(base.OnReload());

        base.ikHandL.weight = 1;

        yield return null;
    }
}