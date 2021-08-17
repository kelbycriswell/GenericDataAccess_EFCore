using GenericDataAccess.Context;
using GenericDataAccess.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericDataAccess.App
{
    class Program
    {
        static void Main(string[] args)
        {
            char _choice;
            List<Customer> custs;
            CustomerRepo repo = new CustomerRepo();
            custs = repo.GetAll().ToList();
            Console.WriteLine("Customers in DB: " + custs.Count);
            for (var i = 1; i <= custs.Count; i++)
            {
                var cust = custs[i - 1];
                Console.Write($"{i}:) ");
                Console.Write($"ID: {cust.ID} -- ");
                Console.Write($"{cust.FName} {cust.MI} {cust.LName}{Environment.NewLine}");

            }
            bool _invalidInput = true;
            bool _changeName = false;
            do
            {
                Console.Write("Would you like to edit a Customers Name?(y/n): ");
                _choice = Console.ReadKey().KeyChar;

                switch (_choice)
                {
                    case 'y':
                        {
                            _invalidInput = false;
                            _changeName = true;
                            break;
                        }
                    case 'n':
                        {
                            _invalidInput = false;
                            _changeName = false;
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid input, Try Again.");
                        _invalidInput = true;
                        break;
                }
            }
            while (_invalidInput);

            if (_changeName)
            {
                int custIdx;
                string newName = string.Empty;
                Customer cust;
                do
                {
                    if (_invalidInput)
                    {
                        Console.WriteLine("Must enter a name that is different...");
                    }
                    _invalidInput = true;
                    Console.WriteLine("Which entry do you want to edit (See list above)?: ");
                    if (Int32.TryParse(Console.ReadLine(), out custIdx))
                    {
                        if (custIdx <= custs.Count)
                        {
                            --custIdx;
                            cust = custs[custIdx];
                            Console.WriteLine("What first name do you want to give them? ");
                            newName = Console.ReadLine();
                            _invalidInput = cust.FName.ToLower() == newName.ToLower();
                            custs[custIdx].FName = newName;
                            repo.AddOrUpdate(ref cust);
                            repo.Save();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, Try Again.");
                        _invalidInput = true;
                    }
                } while (_invalidInput);

            }

            custs = repo.GetAll().ToList();
            Console.WriteLine("Customers in DB: " + custs.Count);
            for (var i = 1; i <= custs.Count; i++)
            {
                var cust = custs[i - 1];
                Console.Write($"{i}:) ");
                Console.Write($"ID: {cust.ID} -- ");
                Console.Write($"{cust.FName} {cust.MI} {cust.LName}{Environment.NewLine}");

            }

            Console.ReadLine();


        }

    }
}
