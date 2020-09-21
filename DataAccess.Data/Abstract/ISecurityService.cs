namespace DataAccess.Data.Abstract
{
    public interface ISecurityService
    {
        bool AuthenticateTeam(string username, string secretToken);
    }
}