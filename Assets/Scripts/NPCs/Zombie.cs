﻿using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, NPC
{
    public LayerMask layer;
    private float _speed;
    private Seek _seek;
    private GameObject _humano;
    private StateMachine _sm;
    private HumanoProvider _humanoProvider;
    private Animator animator;
    private IEnumerable<string> animations;

    public void Awake()
    {
        _humanoProvider = FindObjectOfType<HumanoProvider>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 3;
        animations = new string[] { "Walk", "attack", "hit", "Idle" };
        _speed = 7;
        _sm = new StateMachine();
        _sm.AddState(new SeekState(_sm, this));
        _sm.AddState(new WanderState(_sm, this));
        _sm.AddState(new FallbackState(_sm, this));
        _sm.SetState<SeekState>();
    }

    void Update()
    {
        if (Random.Range(0, 2000) < 15)
            FindObjectOfType<AudioManager>().PlayAudio("Zombie_03");

        if (_sm.IsActualState<WanderState>())
        {
            _sm.Update();
            return;
        }
        
        if (_humanoProvider.IsEmpty())
        {
            _sm.CleanState();
            _sm.SetState<WanderState>();
        }
        else
        {
            if (_humano == null)
            {
                SeekHuman();
            }
        }

        _sm.Update();
    }

    /// <summary>
    /// Metodo encargado de asustar al humano.
    /// </summary>
    public void ScareHuman()
    {
        _humano.SendMessage("Flee", gameObject);
    }

    /// <summary>
    /// Activa el estado de busqueda.
    /// </summary>
    public void SeekHuman()
    {
        _humano = null;
        _sm.CleanState();
        _sm.SetState<SeekState>();
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
    /// Detecta si hay un grupo de humano cerca.
    /// </summary>
    public bool DetectarGrupoHumanos(Vector3 centro, float radio)
    {
        Collider[] hitCollider = Physics.OverlapSphere(centro, radio, layer);
        if (hitCollider.Length >= 2)
        {
            return (Random.Range(0, 2) == 0);
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _humano)
        {
            if (_humanoProvider.IsEmpty())
                return;
            if (DetectarGrupoHumanos(transform.position, 4))
            {
                _humano.SendMessage("Wander");
                _sm.CleanState();
                _sm.SetState<FallbackState>();
            }
            else
            {
                Animate("attack");
                FindObjectOfType<AudioManager>().PlayAudio("bloodfx10");
                _humano.SendMessage("Die", this);
                _humano = null;
                _sm.CleanState();
                _sm.SetState<SeekState>();
            }            
        }
        if (other.gameObject.tag == "pared")
        {
            Debug.Log(other.gameObject);
        }
    }

    /// <summary>
    /// Devuelve la velocidad del zombie.
    /// </summary>
    /// <returns></returns>
    public float GetSpeed()
    {
        return _speed;
    }

    /// <summary>
    /// Obtiene el transform del zombie.
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return transform;
    }

    /// <summary>
    /// Obtiene el humano.
    /// </summary>
    public GameObject GetHumano()
    {
        return _humano;
    }

    /// <summary>
    /// Asigna un humano como target;
    /// </summary>
    /// <param name="humano"></param>
    public void SetHuman(GameObject humano)
    {
        _humano = humano;
    }
}
