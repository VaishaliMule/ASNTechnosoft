using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnosoftModel;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            string status = "Trial";
            List<Institute> institutes = InstituteBL.GetInastituteByStatus(status);
            foreach (Institute item in institutes)
            {
                DateTime endDate = item.RegistrationDate.AddDays(7);
                if(endDate < DateTime.Today)
                {
                    item.IsActive = false;
                    item.Status = "Disable";
                    InstituteBL.Edit(item);
                  
                }
            }
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
