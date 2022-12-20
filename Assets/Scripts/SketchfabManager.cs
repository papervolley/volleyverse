using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string Email;
    private string Password;
    void Start()
    {
        Email = GetEnvVar("SKETCHFAB_ACCOUNT");
        Password = GetEnvVar("SKETCHFAB_ACCOUNT");
        
        if (Email != null && Password != null)
        {
            Authorize();
        }
        else
        {
            Debug.LogError("Failed to get access tokens");
        }
    }

    string GetEnvVar(string name)
    {
        if (env.TryParseEnvironmentVariable("SKETCHFAB_ACCOUNT", out string email))
        {
            return email;
        } else {
            return null;
        }
    }

    void Authorize()
    {
        SketchfabAPI.GetAccessToken(Email, Password, (SketchfabResponse<SketchfabAccessToken> answer) =>
        {
            if(answer.Success)
            {
                AccessToken = answer.Object.AccessToken;
                SketchfabAPI.AuthorizeWithAccessToken(answer.Object);
            }
            else
            {
                Debug.LogError(answer.ErrorMessage);
            }

        });
    }

    void DownloadModel(string modelUID, bool enableCache = true)
    {
        // This first call will get the model information
        SketchfabAPI.GetModel(_uid, (resp) =>
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

    void Logout()
    {
        SketchfabAPI.Logout();
        //that is equivalent to setting the access token to String.Empty
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
