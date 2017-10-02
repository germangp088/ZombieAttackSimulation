using System.Collections.Generic;
using UnityEngine;

public class SangreProvider : MonoBehaviour, Provider
{
    public List<GameObject> sangres = new List<GameObject>();

    /// <summary>
    /// Obtiene la cantidad de sangre de la lista.
    /// </summary>
    /// <returns></returns>
    public int GetCount()
    {
        return sangres.Count;
    }

    public void AddGO(GameObject go)
    {
    }

    /// <summary>
    /// Obtiene el listado de sangre
    /// </summary>
    /// <returns></returns>
    public List<GameObject> GetList()
    {
        return sangres;
    }

    /// <summary>
    /// Obtiene sangre al azar.
    /// </summary>
    /// <returns></returns>
    public GameObject GetRandomGO()
    {
        return sangres[Random.Range(0, sangres.Count)];
    }

    public void RemoveGO(GameObject go)
    {
    }
}
