using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tiramos o Class e colocamos Interface para transformar em Interface
public interface ICollectableBehaviour
{
    // Qualquer classe que implementar essa interface precisar� implementar esse m�todo
    void OnCollected(GameObject player);
}
