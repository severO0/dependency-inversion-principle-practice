using DependencyStore.Models;
using DependencyStore.Repositories.Contracts;
using DependencyStore.Services.Contratcs;
using Microsoft.AspNetCore.Mvc;

namespace DependencyStore.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly ICustomerRepository _costumerRepository;
        private readonly IDeliveryFeeService _deliveryFeeService;
        private readonly IPromoCodeRepository _promoCodeRepository;
        public OrderController(
            ICustomerRepository customerRepository,
            IDeliveryFeeService deliveryFeeService,
            IPromoCodeRepository promoCodeRepository)
        {
            _deliveryFeeService = deliveryFeeService;
            _costumerRepository = customerRepository;
            _promoCodeRepository = promoCodeRepository;
        }

        [Route("v1/orders")]
        [HttpPost]
        public async Task<IActionResult> Place(string customerId, string zipCode, string promoCode, int[] products)
        {
            var customer = await _costumerRepository.GetByIdAsync(customerId);
            if (customer == null)
                return NotFound();

            var deliveryFee = await _deliveryFeeService.GetDeliveryFeeAsync(zipCode);

            var cupon = await _promoCodeRepository.GetPromoCodeAsync(promoCode);

            var discount = cupon?.Value ?? 0M;

            var order = new Order(deliveryFee, discount, new List<Product>());

            return Ok($"Pedido {order.Code} gerado com sucesso!");

        }
    }
}
