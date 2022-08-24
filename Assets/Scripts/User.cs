public class User
{
    public string username;
    public string password;
    public int id;

    public User() { }

    public User(string username, string password, int id)
    {
        this.username = username;
        this.password = password;
        this.id = id;
    }
}