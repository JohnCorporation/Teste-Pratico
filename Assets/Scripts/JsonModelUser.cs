using System;
using System.Collections.Generic;

[Serializable]
public class JsonModelUser
{
    public List<userList> data;
}

[Serializable]
public class userList
{
    public string username;
    public string password;
    public int id;
}


