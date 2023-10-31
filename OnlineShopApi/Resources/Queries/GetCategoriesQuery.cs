using MediatR;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Categorie>>
    {
        // we can add any parameters or filters as need for the query here
    }
}
