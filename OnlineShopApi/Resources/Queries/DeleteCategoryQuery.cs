using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class DeleteCategoryQuery : IRequest<bool>
    {
        public string CategoryId { get; set; }
    }

}

