using MediatR;
using MongoDB.Driver;
using OnlineShopApi.Resources.Queries;
using StoreAPI.Models;

namespace OnlineShopApi.Resources.Commands
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Categorie>>
    {
        private readonly IMongoCollection<Categorie> _categoryCollection;

        public GetCategoriesQueryHandler(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Categorie>("Categories");
        }

        public async Task<IEnumerable<Categorie>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryCollection.Find(_ => true).ToListAsync(cancellationToken);

            var categoryViewModels = categories.Select(category => new Categorie
            {
                Id = category.Id,
                Title = category.Title,
                Descrtiption = category.Descrtiption
            }).ToList();

            return categoryViewModels;
        }
    }
}
