namespace WLib.Core.Mobile.Services.Auth
{
    public interface IAppUser
    {
        string Name { get; set; }

        string Email { get; set; }

        bool IsLogged { get; set; }


    }
}
