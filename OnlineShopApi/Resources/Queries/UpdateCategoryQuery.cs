using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class UpdateCategoryQuery : IRequest<bool>
    {
        public string CategoryId { get; set; }
        public Categorie UpdatedCategory { get; set; }
    }

}

