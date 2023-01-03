using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CandyCoded.env;
using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;

public class SketchfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string Email;
    private string Password;
    private bool Authorized = false;

    [SerializeField]
    private TextMeshPro GiftContentText;
    [SerializeField]
    public float GiftScale = 1.0f;
    [SerializeField]
    public float MinScaleFactor = 0.1f;
    [SerializeField]
    public float MaxScaleFactor = 3.0f;
    [SerializeField]
    private Transform TreasureBag;
    [SerializeField]
    private Vector3 Offset;
    [SerializeField]
    private GameManager GameManager;
    private void Start()
    {
        Email = GetEnvVar("SKETCHFAB_ACCOUNT");
        Password = GetEnvVar("SKETCHFAB_PASSWORD");
        
        if (Email != null && Password != null)
        {
            Authorize();
        }
        else
        {
            Debug.LogError("Failed to get access tokens");
        }
    }

    private string GetEnvVar(string name)
    {
        if (env.TryParseEnvironmentVariable(name, out string value))
        {
            //Debug.Log($"get var: {name}:{value}");
            return value;
        } else {
            return null;
        }
    }

    private void Authorize()
    {
        SketchfabAPI.GetAccessToken(Email, Password, (SketchfabResponse<SketchfabAccessToken> answer) =>
        {
            if(answer.Success)
            {
                // Skip saving AccessToken for now
                //AccessToken = answer.Object.AccessToken;
                SketchfabAPI.AuthorizeWithAccessToken(answer.Object);
                Authorized = true;
            }
            else
            {
                Debug.LogError(answer.ErrorMessage);
            }

        });
    }

    void ImportModel(SketchfabModel model, bool enableCache = true)
    {
        SketchfabModelImporter.Import(model, (obj) =>
        {
            if(obj != null)
            {
                // Here you can do anything you like to obj (A unity game object containing the sketchfab model)
                //BoxCollider collider = obj.AddComponent<BoxCollider>();
                //collider.size = Vector3.one * 100.0f;
                AddColliders(obj);
                
                Rigidbody rigidbody = obj.AddComponent<Rigidbody>();
                rigidbody.isKinematic = true;
                rigidbody.useGravity = false;

                Grabbable grab = obj.AddComponent<Grabbable>();
                //Collider[] newGrabPoints = new Collider[1];
                //newGrabPoints[0] = boxCollider;
                grab.enabled = true;
                grab.TransferOnSecondSelection = false;
                
                //grab.grabPoints = newGrabPoints;
                
                HandGrabInteractable grabInteractable = obj.AddComponent<HandGrabInteractable>();
                grabInteractable.InjectOptionalPointableElement(grab);
                //grabInteractable.ResetGrabOnGrabsUpdated = false;
                grabInteractable.InjectSupportedGrabTypes(GrabTypeFlags.Pinch);
                
                OneGrabFreeTransformer oneGrabTransformer = obj.AddComponent<OneGrabFreeTransformer>();
                grab.InjectOptionalOneGrabTransformer(oneGrabTransformer);

                TwoGrabFreeTransformer twoGrabTransformer = obj.AddComponent<TwoGrabFreeTransformer>();
                
                // To make two grab scale work, you have to assign the constraints to TwoGrabFreeTransformer
                TwoGrabFreeTransformer.TwoGrabFreeConstraints twoGrabConstraints = new TwoGrabFreeTransformer.TwoGrabFreeConstraints();
                twoGrabConstraints.ConstraintsAreRelative = true;
                FloatConstraint minScaleConstraint = new FloatConstraint();
                minScaleConstraint.Constrain = true;
                minScaleConstraint.Value = MinScaleFactor;
                twoGrabConstraints.MinScale = minScaleConstraint;
                FloatConstraint maxScaleConstraint = new FloatConstraint();
                maxScaleConstraint.Constrain = true;
                maxScaleConstraint.Value = MaxScaleFactor;
                twoGrabConstraints.MaxScale = maxScaleConstraint;
                twoGrabTransformer.Constraints = twoGrabConstraints;
                
                grab.InjectOptionalTwoGrabTransformer(twoGrabTransformer);

                obj.transform.localScale = Vector3.one * GiftScale;
                obj.transform.localPosition = TreasureBag.position + Offset;
            }
        }, enableCache);
    }

    void AddColliders(GameObject obj)
    {
        Component[] renderers = obj.GetComponentsInChildren(typeof(MeshRenderer));
        if (renderers != null)
        {
            Debug.Log($"There are {renderers.Length} meshes");
            foreach (MeshRenderer r in renderers)
            {
                GameObject child = r.gameObject;
                child.AddComponent<BoxCollider>();
                //HandGrabInteractable grabInteractable = child.AddComponent<HandGrabInteractable>();
                //grabInteractable.Rigidbody = obj.GetComponent<Rigidbody>();
            }
        }

    }

    void DownloadModel(string modelUID, bool enableCache = true)
    {
        // This first call will get the model information
        SketchfabAPI.GetModel(modelUID, (resp) =>
        {
            // This second call will get the model information, download it and instantiate it
            SketchfabModelImporter.Import(resp.Object, (obj) =>
            {
                if(obj != null)
                {
                    // Here you can do anything you like to obj (A unity game object containing the sketchfab model)
                }
            }, enableCache);
        }, enableCache);
    }

    void SearchAndDownloadModel(string keyword)
    {
        UnityWebRequestSketchfabModelList.Parameters p = new UnityWebRequestSketchfabModelList.Parameters();
        p.downloadable = true;
        SketchfabAPI.ModelSearch(((SketchfabResponse<SketchfabModelList> _answer) =>
        {
            SketchfabResponse<SketchfabModelList> ans = _answer;
            // TODO: Get the first match and display
            SketchfabModel firstMatchModel = ans.Object.Models[0];
            if (firstMatchModel != null)
            {
                ImportModel(firstMatchModel);
            } else {
                Debug.Log($"There's no match for the keyword {keyword}");
            }


        }), p, keyword);
    }

    void Logout()
    {
        SketchfabAPI.Logout();
        //that is equivalent to setting the access token to String.Empty
    }

    public void GenerateGift(string[] values)
    {
        if (values.Length <= 0 && values[0] == null)
        {
            return;
        }
        string wish = values[0];
        Debug.Log($"Wish: {wish}");
        GiftContentText.text = wish;

        SearchAndDownloadModel(wish);
    }
}
