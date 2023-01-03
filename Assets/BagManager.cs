using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem BagParticles = null;
    [SerializeField]
    private ParticleSystem GlowParticles = null;
    [SerializeField]
    private Animator BagAnimator = null;

    private void Start() {
        GameEvents.current.OnDidMicActivate += AnimateMicActivate;
    }

    private void AnimateMicActivate()
    {
        Debug.Log("BagManager::AnimateMicActivate");
        // Play animation for micphone activation
        BagAnimator.SetBool("Gazing", true);
        BagParticles.Play();
        GlowParticles.Play();
    }

    private void OnDestroy() {
        GameEvents.current.OnDidMicActivate -= AnimateMicActivate;
    }
}
