using CA.ValueObjectsUsingComplexTypes.Data;
using CA.ValueObjectsUsingComplexTypes.Models;

namespace CA.ValueObjectsUsingComplexTypes
{
    internal class Program
    {
        private static void Main()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Database.EnsureCreated();
                var address = Address.Create("123 Main St", "Springfield", "12345", "USA");
                var customer = Customer.Create("John Doe", address);
                context.Customers.Add(customer);
                context.SaveChanges();
                Console.WriteLine($"Customer {customer.Name} with ID {customer.Id} created successfully.");

                //GetCustomers(context);
            }

            Console.ReadKey();
        }

        private static void GetCustomers(ApplicationDbContext context)
        {
            var customers = context.Customers.ToList();

            foreach (var customer in customers)
                Console.WriteLine(customer);
        }
    }
}
