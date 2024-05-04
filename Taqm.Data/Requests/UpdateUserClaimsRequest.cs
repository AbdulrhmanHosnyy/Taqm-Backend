namespace Taqm.Data.Requests
{
    public record UpdateUserClaimsRequest(int Id, List<string> ClaimsNames);
}
