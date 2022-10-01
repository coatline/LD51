using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class DataLibrary : Singleton<DataLibrary>
{
    public Getter<Gun> Guns { get; private set; }
    public Getter<Boss> EasyBosses { get; private set; }
    public Getter<Boss> MediumBosses { get; private set; }
    public Getter<Boss> HardBosses { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Guns = new Getter<Gun>(Resources.LoadAll<Gun>("Guns"));
        EasyBosses = new Getter<Boss>(Resources.LoadAll<Boss>("Bosses/Easy"));
        MediumBosses = new Getter<Boss>(Resources.LoadAll<Boss>("Bosses/Medium"));
        HardBosses = new Getter<Boss>(Resources.LoadAll<Boss>("Bosses/Hard"));
    }
}
