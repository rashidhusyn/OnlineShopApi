using MediatR;
using OnlineShopApi.Models;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class UpdateUserQuery : IRequest<bool>
    {
        public string UserId { get; set; }
        public User UpdatedUser { get; set; }
    }

}

