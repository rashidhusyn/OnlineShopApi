using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class DeleteOrderQuery : IRequest<bool>
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
    }
}
