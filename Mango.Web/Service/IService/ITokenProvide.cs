namespace Mango.Web.Service.IService
{
    public interface ITokenProvide
    {
        void SetToken(string token);
        string? GetToken();

        void ClearToken();
    }
}
