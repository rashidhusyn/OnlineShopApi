using MediatR;

namespace OnlineShopApi.Resources.Queries
{
    public class DeleteCartCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }

    }
}
