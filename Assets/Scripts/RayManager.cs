using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayManager : MonoBehaviour
{
    public enum MicIntention
    {
        Activate,
        Deactivate
    }

    private MicIntention micIntention = MicIntention.Deactivate;

    [SerializeField]
    private Camera Eye;

    [SerializeField]
    private TextMeshPro Debugger;

    [SerializeField]
    private Oculus.Voice.AppVoiceExperience VoiceExperience = null;

    [SerializeField]
    private ParticleSystem bagParticles = null;

    public ParticleSystem glowParticles;

    [SerializeField] private Animator bagAnimator;

    [SerializeField] private LayerMask layermask;

    private void Start() {
        //GameEvents.current.OnWillMicActivate += ActivateMic;
    }

    private void FixedUpdate() {
        Vector3 fwd = Eye.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(Eye.transform.position, fwd, out hit, 10.0f, layermask))
        {
            GameObject hitObj = hit.transform.gameObject;
            Debug.Log("hitting " + hitObj.tag);
            Debugger.text = hitObj.name;
            // activate voice
            if (GameManager.current.State == GameState.Idle)
            {
                GameEvents.current.WillMicActivate();
            }
        } else {
            Debugger.text = "not hitting anything";
            bagAnimator.SetBool("Gazing", false);
        }
    }
}
