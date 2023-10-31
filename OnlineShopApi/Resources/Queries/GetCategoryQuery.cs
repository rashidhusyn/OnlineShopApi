using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetCategoryQuery : IRequest<Categorie>
    {
        public string CategoryId { get; set; }

    }
}
