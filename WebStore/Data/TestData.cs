using System;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Data
{
    public class TestData
    {
        public static List<Employee> Employees { get;  } = new List<Employee>() 
        {
            new Employee
            {
                Id = 1,
                LastName = "Иванов",
                FirstName = "Александр",
                Patronymic = "Сергеевич",
                BirthDate = new DateTime(1979, 2, 7),
                HireDate = new DateTime(2010, 5, 15),
                City = "Москва"
            },
            new Employee
            {
                Id = 2,
                LastName = "Васечкин",
                FirstName = "Иван",
                Patronymic = "Васильевич",
                BirthDate = new DateTime(1999, 10, 11),
                HireDate = new DateTime(2015, 1, 20),
                City = "Краснодар"
            },
            new Employee
            {
                Id = 3,
                LastName = "Прохоров",
                FirstName = "Михаил",
                Patronymic = "Фёдорович",
                BirthDate = new DateTime(2001, 1, 21),
                HireDate = new DateTime(2020, 6, 10),
                City = "Пермь"
            }
        };
    }
}
