using Playground.Nl.CustomerManagementAPI.Database.Models;
using Playground.Nl.CustomerManagementAPI.Nl.Models;
using Playground.Nl.CustomerManagementAPI.Services.Models;
using Playground.Nl.CustomerManagementAPI.Services.Models.Customer;

namespace Playground.Nl.CustomerManagementAPI.Nl.Mappers
{
    public static class Mappers
    {
        public static CreateCustomerModel MapToCreateCustomerModel(this CreateCustomerInputModel inputModel) => new CreateCustomerModel
        {
            FirstName = inputModel.FirstName,
            Insertion = inputModel.Insertion,
            LastName = inputModel.LastName,
            Email = inputModel.Email,
            Password = inputModel.Password,
            PostalCode = inputModel.PostalCode,
            HouseNumber = inputModel.HouseNumber,
            Address = inputModel.Address,
            City = inputModel.City
        };
    }
}