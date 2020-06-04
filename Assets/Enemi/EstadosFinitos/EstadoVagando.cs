using UnityEngine;
using System.Collections;
using System;

public class EstadoVagando : EstadosFinitos
{
    public override void Salir()
    {
        base.Start();
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override Type VerficarTransiciones()
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}
