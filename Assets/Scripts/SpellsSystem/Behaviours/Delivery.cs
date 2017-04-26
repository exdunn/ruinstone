using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Delivery : Behaviour {
    public float _range = 0f;

    public Types.Area _areaType = Types.Area.LINEAR;
}
