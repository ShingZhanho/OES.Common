using Newtonsoft.Json;

namespace OES;

/// <summary>
/// A request to create a new Admin User in the database.
/// </summary>
public class CreateAdminUser
{
    /// <summary>
    /// Construct a request for creating an Admin User. Password hash and salt are automatically generated.
    /// </summary>
    /// <param name="id">The login ID of the user.</param>
    /// <param name="userDisplayName">The display name of the user.</param>
    /// <param name="password">The login password of the user.</param>
    public CreateAdminUser(string id, string userDisplayName, string password)
    {
        Id = id;
        UserDisplayName = userDisplayName;
        _password = password;
        Salt = PasswordHash.GetSalt();
        PasswordHashed = PasswordHash.Hash(Password, Salt);
    }

    /// <summary>
    /// The ID of the Admin user.
    /// </summary>
    [JsonProperty("admin_id")]
    public string Id { get; set; }
    
    /// <summary>
    /// The display name of the Admin user.
    /// </summary>
    public string UserDisplayName { get; set; }
    
    /// <summary>
    /// The login password of the user.
    /// </summary>
    [JsonIgnore]
    public string Password {
        get => _password;
        set
        {
            _password = value;
            PasswordHashed = PasswordHash.Hash(_password, Salt);
        } 
    }
    private string _password;
    
    /// <summary>
    /// The hashed password. Generated upon assignment of <see cref="Password"/> property.
    /// </summary>
    [JsonProperty("login_hash")]
    public string PasswordHashed { get; internal set; }
    
    /// <summary>
    /// The login salt for the user. Generated upon instance creation.
    /// </summary>
    [JsonProperty("login_salt")]
    public string Salt { get; }
}