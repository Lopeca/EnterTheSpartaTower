using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDash
{
    PlayerController Player { get; }
    PlayerStatHandler StatHandler {  get; }

    void Init();
    void TriggerDash();
}
