using AutoMapper;
using AutoMapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            Customer customerdetails = new Customer()
            {
                CustomerId = 1,
                CompanyName = "ABC",
                Address = "R. Rocinha",
                Phone = "11",
                FirstName = "Rodrigo",
                MiddleName = "Salino",
                LastName = "Salino",
                City = "São Paulo",
                Country = "Brasil",
                Pincode = "04127030"
            };
            var customerModel = _mapper.Map<CustomerModel>(customerdetails);
            var fullname = customerModel.FullName;
            var address = customerModel.City;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
