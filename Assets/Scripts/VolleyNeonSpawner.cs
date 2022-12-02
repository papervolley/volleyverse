using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyNeonSpawner : MonoBehaviour
{
    private OVRSceneAnchor _sceneAnchor;
    private OVRSemanticClassification _classification;
    private GameObject VolleyNeonRef;

    [SerializeField] private GameObject VolleyNeonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _sceneAnchor = GetComponent<OVRSceneAnchor>();
        _classification = GetComponent<OVRSemanticClassification>();
        Spawn();
    }

    private void Spawn()
    {
        var plane = _sceneAnchor.GetComponent<OVRScenePlane>();
        var volume = _sceneAnchor.GetComponent<OVRSceneVolume>();

        if (_classification.Contains(OVRSceneManager.Classification.WallFace))
        {
            // attach Volley Neon
            VolleyNeonRef = Instantiate(VolleyNeonPrefab, _sceneAnchor.transform);
            VolleyNeonRef.transform.localScale = Vector3.one;
        }
    }
}
