namespace Taqm.Data.Requests
{
    public record UpdateUserRolesRequest(int Id, List<string> RolesNames);

}
