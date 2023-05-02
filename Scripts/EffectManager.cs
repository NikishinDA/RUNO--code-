using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private void Awake()
    {
        EventManager.AddListener<GateInteractedEvent>(OnGateInteracted);
    }
    private void OnDestroy()
    {
        EventManager.RemoveListener<GateInteractedEvent>(OnGateInteracted);
    }
    private void OnGateInteracted(GateInteractedEvent obj)
    {
        if (obj.IsPickUp)
        {
            GetComponent<Cinemachine.CinemachineImpulseSource>().GenerateImpulse();
            Taptic.Failure();
        }
        else
        {
            Taptic.Medium();
        }
    }
}
