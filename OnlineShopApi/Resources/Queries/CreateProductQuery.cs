using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class CreateProductQuery:IRequest<string>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public decimal Price { get; set; }
        public string CategoryId { get; set; }
    }
}
