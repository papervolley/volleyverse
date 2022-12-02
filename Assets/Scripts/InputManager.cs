using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{

    public enum MicIntention
    {
        Activate,
        Deactivate
    }

    private MicIntention micIntention = MicIntention.Deactivate;

    [SerializeField]
    private Oculus.Voice.AppVoiceExperience VoiceExperience = null;
    [SerializeField]
    private TextMeshPro StateTextMeshPro = null;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnVoiceDeactive()
    {
        micIntention = MicIntention.Deactivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.7)
        {
            // Activate mic
            if (micIntention != MicIntention.Activate)
            {
                micIntention = MicIntention.Activate;
                VoiceExperience.Activate();
                //StateTextMeshPro.text = "Activate";
            }
        } else {
            //micIntention = MicIntention.Deactivate;
            //VoiceExperience.Deactivate();
            //StateTextMeshPro.text = "Deactivate";
        }
    }
}
