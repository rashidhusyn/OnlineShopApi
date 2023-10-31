using MediatR;
using OnlineShopApi.Models;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetUserByIdQuery:IRequest<User>
    {
        public string UserId { get; set; }
    }
}
