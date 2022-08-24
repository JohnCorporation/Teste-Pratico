using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Login : MonoBehaviour
{
    public Web web;

    public GameObject username;
    public GameObject passaword;
    public GameObject texto;
    public GameObject UICadastro;

    public GameObject organizador;
    public GameObject prefab;
    public void Logar()
    {
        StartCoroutine(web.Login(username.GetComponent<Text>().text, passaword.GetComponent<Text>().text));
    }

    public void CriarUsuario()
    {
        User createUser = new User { username = username.GetComponent<Text>().text, password = passaword.GetComponent<Text>().text };
        string postData = JsonConvert.SerializeObject(createUser);
        StartCoroutine(web.Post(postData, texto, UICadastro));
    }

    public void Start()
    {
        StartCoroutine(web.GetUsers(organizador, prefab));
    }
}
