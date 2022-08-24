using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class Web : MonoBehaviour
{
 

    void Start()
    {
        // A correct website page.
       //  StartCoroutine(GetRequest("https://avaliacao-entre-lacos.herokuapp.com/user"));

      
    }

    private JsonModelUser processJsonData(string _url)
    {
        JsonModelUser jsnData = JsonUtility.FromJson<JsonModelUser>(_url);

        return jsnData;
    }

    private User processJsonUser(string _url)
    {
        User jsnUser = JsonUtility.FromJson<User>(_url);

        return jsnUser;
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string resp = webRequest.downloadHandler.text; // pega a resposta da internet e coloca em uma String
                    processJsonData(resp); // transforma a string em json e separa as coisas de acordo com o model;

                    JsonModelUser jsnData = processJsonData(resp);
                    foreach (userList user in jsnData.data)
                    {
                        Debug.Log("Nome de Usuario: " + user.username + "\n" + "Senha: " + user.password + "\n" + "ID: " + user.id + "\n");
                    }
                    break;
            }
        }
    }

    public IEnumerator Login(string username, string senha)
    {
        string uri = "https://avaliacao-entre-lacos.herokuapp.com/user";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string resp = webRequest.downloadHandler.text; // pega a resposta da internet e coloca em uma String
                    processJsonData(resp); // transforma a string em json e separa as coisas de acordo com o model;

                    JsonModelUser jsnData = processJsonData(resp);
                    foreach (userList user in jsnData.data)
                    {
                       if(username == user.username)
                        {
                            if(senha == user.password)
                            {
                                Debug.Log("Entrou");
                            }
                            else
                            {
                                Debug.Log("Senha está errada");
                            }
                        }
                        
                    }
                    break;
            }
        }
    }

    public IEnumerator Upload(string username, string senha)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", senha);

        using (UnityWebRequest www = UnityWebRequest.Post("https://avaliacao-entre-lacos.herokuapp.com/user", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("form upload fail");
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    public IEnumerator Post(string postData, GameObject texto, GameObject UICadastro)
    {
        

        var www = new UnityWebRequest("https://avaliacao-entre-lacos.herokuapp.com/user", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(postData);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("form upload fail");
        }
        else
        {
            string username = processJsonUser(postData).username;
            UICadastro.SetActive(false);
            texto.SetActive(true);
            texto.GetComponent<Text>().text = "Olá, seja bem vindo " + username +"!";
        }
    }

    public IEnumerator GetUsers(GameObject organizador, GameObject prefab)
    {
        string uri = "https://avaliacao-entre-lacos.herokuapp.com/user";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:

                    string resp = webRequest.downloadHandler.text; // pega a resposta da internet e coloca em uma String
                    processJsonData(resp); // transforma a string em json e separa as coisas de acordo com o model;

                    JsonModelUser jsnData = processJsonData(resp);
                    foreach (userList user in jsnData.data)
                    {
                       GameObject bloco = Instantiate(prefab, organizador.transform.position, organizador.transform.rotation, organizador.transform);
                        bloco.GetComponentInChildren<Text>().text = "" + user.username;
                    }
                    break;
            }
        }
    }
}