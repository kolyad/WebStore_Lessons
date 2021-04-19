using System;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        /// <summary>
        /// Id сотрудника
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Дата рождения сотрудника
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age
        {
            get
            {
                var now = DateTime.Now;

                var age = now.Year - BirthDate.Year;

                if (age <= 0)
                {
                    return 0;
                }

                if ((now.Month < BirthDate.Month) ||
                    (now.Month == BirthDate.Month && now.Day < BirthDate.Day))
                {
                    age--;
                }

                return age;
            }
        }
        /// <summary>
        /// Дата найма сотрудника
        /// </summary>
        public DateTime HireDate { get; set; }
        /// <summary>
        /// Город проживания сотрудинка
        /// </summary>
        public string City { get; set; }
    }
}
