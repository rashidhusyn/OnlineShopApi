using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class CreateCategoryQuery : IRequest<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
