using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class FutureDate :  ValidationAttribute
    {
        //Prevent Users from adding Gigs with past dates
        //Also use Javascript for this with date picker

        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                            "d MMM yyyy", 
                            CultureInfo.CurrentCulture,
                            DateTimeStyles.None, 
                            out dateTime);

            return(isValid && dateTime > DateTime.Now);
        }
    }
}