using System;
using System.Collections.Generic;
using UnityEngine;

public class Humano : MonoBehaviour, NPC
{
    private float _speed;
    private StateMachine _sm;
    private GameObject _zombie;
    private Animator animator;
    private IEnumerable<string> animations;

    void Start () {
        animator = GetComponent<Animator>();
        animations = new string[] { "Walk", "SprintJump" };
        _speed = 5;
        _sm = new StateMachine();
        _sm.AddState(new FleeState(_sm, this));
        _sm.AddState(new WanderState(_sm, this));
        _sm.SetState<WanderState>();
    }
	
	void Update () {
        _sm.Update();
    }

    /// <summary>
    /// Cambio de estado a FleeState.
    /// </summary>
    /// <param name="zombie">Zombie persiguiendo al humano.</param>
    public void Flee(GameObject zombie){
        if (!_sm.IsActualState<FleeState>())
        {
            _zombie = zombie;
            _sm.SetState<FleeState>();
        }
    }

    /// <summary>
    /// Muerte del NPC.
    /// </summary>
    public void Die(Zombie zombie)
    {
        FindObjectOfType<AudioManager>().PlayAudio("Scream_Male_02");
        FindObjectOfType<HumanoProvider>().RemoveGO(gameObject);
        Zombie clone = Instantiate(zombie,transform.position,Quaternion.identity);
        FindObjectOfType<ZombieProvider>().AddGO(clone.gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// Setea al humano con wander state.
    /// </summary>
    public void Wander() {
        if (!_sm.IsActualState<WanderState>())
        {
            _zombie = null;
            _sm.SetState<WanderState>();
        }
    }

    /// <summary>
    /// Devuelve la velocidad del humano.
    /// </summary>
    /// <returns></returns>
    public float GetSpeed(){
        return _speed;
    }

    /// <summary>
    /// Obtiene el zombie que persigue al humano.
    /// </summary>
    /// <returns>Zombie</returns>
    public GameObject GetZombie(){
        return _zombie;
    }

    /// <summary>
    /// Funcion encargada de animar el NPC.
    /// </summary>
    /// <param name="animationName">Nombre de la animacion a reproducir.</param>
    public void Animate(string animationName)
    {
        foreach (var animation in animations)
        {
            animator.SetBool(animation, (animation.ToUpper() == animationName.ToUpper()));
        }
    }

    /// <summary>
    /// Obtiene el transform del humano.
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return transform;
    }
}
