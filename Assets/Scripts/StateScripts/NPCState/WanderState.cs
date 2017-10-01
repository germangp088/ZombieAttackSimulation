using UnityEngine;

public class WanderState : NPCState
{
    private Seek _seek;
    private GameObject _punto;
    private PuntoProvider _puntoProvider;
    public WanderState(StateMachine sm, NPC npc) : base(sm, npc)
    {
    }

    public override void Awake()
    {
        _puntoProvider = Object.FindObjectOfType<PuntoProvider>();
        _npc.Animate("Walk");
        SeekPunto(_puntoProvider.GetRandomGO());
        base.Awake();
    }

    public override void Execute()
    {
        Transform transform = _npc.GetTransform();
        if (_seek.GetDistance(transform) < 2)
        {
            SeekPunto(_puntoProvider.getDiferentPunto(_punto));
        }
        _seek.Execute(transform);
        base.Execute();
    }

    public override void Sleep()
    {
        base.Sleep();
    }

    private void SeekPunto(GameObject punto)
    {
        _punto = punto;
        _seek = new Seek(_npc.GetSpeed(), _punto);
    }
}