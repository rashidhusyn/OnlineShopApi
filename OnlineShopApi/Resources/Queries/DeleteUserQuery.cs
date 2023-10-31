using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class DeleteUserQuery: IRequest<bool>
    {
        public string UserId { get; set; }
    }
}
