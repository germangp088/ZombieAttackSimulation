using UnityEngine;

public class FleeState : HumanoState
{
    Flee flee;
    public FleeState(StateMachine sm, Humano h) : base(sm, h)
    {
    }

    public override void Awake()
    {
        humano.Animate("SprintJump");
        flee = new Flee(humano.GetSpeed(), humano.GetZombie());
        base.Awake();
    }

    public override void Execute()
    {
        flee.Execute(humano.transform);
        base.Execute();
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}