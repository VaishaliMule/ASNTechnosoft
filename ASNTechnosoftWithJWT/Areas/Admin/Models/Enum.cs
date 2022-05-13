using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class Enum
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info,
            question
        }

        public enum GenderList
        {
            Male,
            Female,
            TransGender
        }

        public enum MaritalStatus
        {
            Married,
            Single,
            Divorcee,
            Widow
        }

        public enum SalaryType
        {
            Fixed,
            Intensive,
            Fixed_and_Intensive
        }
    }
}