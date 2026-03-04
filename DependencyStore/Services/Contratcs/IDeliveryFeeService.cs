namespace DependencyStore.Services.Contratcs
{
    public interface IDeliveryFeeService
    {
        Task<decimal> GetDeliveryFeeAsync(string zipCode);
    }
}
