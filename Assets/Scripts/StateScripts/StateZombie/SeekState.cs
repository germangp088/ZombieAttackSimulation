using UnityEngine;

public class SeekState : ZombieState
{
    private Seek _seek;
    public SeekState(StateMachine sm, Zombie z) : base(sm, z)
    {
    }

    public override void Awake()
    {
        zombie.Animate("Walk");
        HumanoProvider humanoProvider = GameObject.FindObjectOfType<HumanoProvider>();
        if (humanoProvider.IsEmpty())
            return;
        GameObject humano = humanoProvider.GetRandomGO();
        zombie.SetHuman(humano);
        _seek = new Seek(zombie.GetSpeed(), humano);
    }

    public override void Execute()
    {
        _seek.Execute(zombie.transform);

        if (_seek.GetDistance(zombie.transform) < 5)
            zombie.ScareHuman();
    }

    public override void Sleep()
    {
        base.Sleep();
    }
}