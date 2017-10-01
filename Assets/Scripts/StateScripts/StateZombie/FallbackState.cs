using UnityEngine;

public class FallbackState : ZombieState
{

    private float timerExitState;
    private float _speed;
    private Flee flee;
    public FallbackState(StateMachine sm, Zombie n) : base(sm, n)
    {

    }

    /// <summary>
    /// Obtiene la velocidad del zombie y la aumenta para correr.
    /// </summary>
    public override void Awake()
    {
        zombie.Animate("hit");
        flee = new Flee(zombie.GetSpeed(), zombie.GetHumano());
        timerExitState = 0;
        base.Awake();
    }

    /// <summary>
    /// Ejecuta el estado.
    /// </summary>
    public override void Execute()
    {
        flee.Execute(zombie.transform);
        timerExitState += Time.deltaTime;
        if (timerExitState >= 3)
            zombie.SeekHuman();
        base.Execute();        
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}
