using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDll
{
    public static class Constants
    {



        public const string AdminEmail = "admin@test.rs";
        public const string AdminAddress = "AdminAddress";

        public const string HrEmail = "hr@test.rs";
        public const string HrAddress = "HrAddress";

        public const string User1FirstName = "User1FirstName";
        public const string User1LastName = "User1LastName";
        public const string User1Email = "user1@test.rs";
        public const string User1Address = "User1Address";

        public const string Password = "Sifra.1234";

        public const string PositionCaption = "Developer";
        public const string PositionDescription = "Description";

        public const string CommentEmployee = "Leaving and never coming back";
        public const string CommentHR = "Goodbye!";

        public const string UserPosisitonCreateErrorMessage = "There was an error while creating the user position. Please try again!";
        public const string UserPosisitonCUpdateErrorMessage = "There was an error while updating the user position. Please try again!";

        public const string UserRequestCreateErrorMessage = "There was an error while creating the user request. Please try again!";

        public const string UserCreateErrorMessage = "There was an error while creating the user. Please try again!";
        public const string UserUpdateErrorMessage = "There was an error while updating the user. Please try again!";

        public const string LoginError = "Invalid credidentials";

        public const string CaptionErrorMessage = "Caption can't be longer than 50 characters.";
        public const string CaptionRequired = "Caption is required.";

        public const string DescRequired = "Description is required.";
        public const string DescErrorMessage = "Description can't be longer than 50 characters.";

        public const string FirstNameRequired = "First Name is required.";
        public const string FirstNameErrorMessage = "First Name can't be longer than 50 characters.";

        public const string LastNameRequired = "Last Name is required.";
        public const string LastNameErrorMessage = "Last Name can't be longer than 50 characters.";

        public const string AddressRequired = "Address is required.";
        public const string AddressErrorMessage = "Address can't be longer than 100 characters.";

        public const string IdNumberRequired = "ID Number is required.";
        public const string IdNumberErrorMessage = "ID Number can't be longer than 20 characters.";

        public const string PositionIdRequired = "Position ID is required.";
        public const string PositionIdRangeErrorMessage = "Position ID must be a positive integer.";

        public const string EmailRequired = "Email is required.";
        public const string EmailErrorMessage = "Invalid email format.";

        public const string PasswordRequired = "Password is required.";
        public const string PasswordErrorMessage = "Password must be at least 6 characters long and no more than 100 characters.";

        public const string IdRequired = "ID is required.";
        public const string IdRangeErrorMessage = "ID must be a positive integer.";

        public const string DaysOffNumberRequired = "Days Off Number is required.";
        public const string DaysOffNumberRangeErrorMessage = "Days Off Number must be between 0 and 365.";

        public const string DateStartRequiered = "Start date is requiered";
        public const string DateEndRequiered = "End date is requiered";



    }
}
