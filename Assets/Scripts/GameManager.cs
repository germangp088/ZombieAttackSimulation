using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private HumanoProvider _humanoProvider;
    private ZombieProvider _zombieProvider;
    private WaitForEndOfFrame wait = new WaitForEndOfFrame();
    void Start()
    {
        StartCoroutine(Initialize());
    }
    
    void Update()
    {
        if (_humanoProvider.IsEmpty()) {

        }
    }

    private IEnumerator Initialize()
    {
        ActivateProviders();
        yield return wait;
        ActivateNPC(_humanoProvider.GetList());
        yield return wait;
        ActivateNPC(_zombieProvider.GetList());
    }

    private void ActivateProviders()
    {
        FindObjectOfType<PuntoProvider>().GetComponent<PuntoProvider>().enabled = true;

        _humanoProvider = FindObjectOfType<HumanoProvider>();
        _humanoProvider.GetComponent<HumanoProvider>().enabled = true;

        _zombieProvider = FindObjectOfType<ZombieProvider>();
        _zombieProvider.GetComponent<ZombieProvider>().enabled = true;
    }

    private void ActivateNPC(List<GameObject> list)
    {
        foreach (GameObject item in list)
        {
            item.GetComponent<MonoBehaviour>().enabled = true;
        }
    }
}
