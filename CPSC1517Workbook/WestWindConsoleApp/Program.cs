using WestWindSystem.DAL;

var db = new WestWindContext();

var countCustomers = db.Customers.Count();

Console.WriteLine($"There are {countCustomers} customers in the DB.");

var customer = db.Customers.FirstOrDefault();

Console.WriteLine($"the first customer is {customer.ContactName}.");
