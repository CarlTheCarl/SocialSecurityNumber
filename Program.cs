using System;
using System.Globalization;
using System.Threading;
using System.Xml;

namespace SocialSecurityNumber
{
    class Program
    {
        static void Main(string[] args) // Varför är stringen arg?
        {
            string socialSecurityNumber = null;
            int socialSecurityNumberLength = 0;
            if (args.Length != 0) // check if there is an argument in the cmd / terminal / whatever you use for this
            {
                foreach (Object obj in args)
                {
                    socialSecurityNumber = args[0];
                    socialSecurityNumberLength = socialSecurityNumber.Length;
                }
            }
            else
            {
                Console.Write("Please enter your social security number ((YY)YYMMDD-NNNN): ");
                socialSecurityNumber = Console.ReadLine();
                socialSecurityNumberLength = socialSecurityNumber.Length;
            }
            while (socialSecurityNumberLength != 10
                && socialSecurityNumberLength != 11
                && socialSecurityNumberLength != 12
                && socialSecurityNumberLength != 13)
            {
                Console.Clear();
                Console.WriteLine($"The social security number you entered  had {socialSecurityNumberLength} characters");
                Console.WriteLine("This isn't one of the correct lenghts for the social security number");
                Console.Write("Please enter your social security number ((YY)YYMMDD-NNNN): ");
                socialSecurityNumber = Console.ReadLine();
                socialSecurityNumberLength = socialSecurityNumber.Length;
            }

                // TODO : add check to see if the user isn't inputing the wrong thing

            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumberLength - 2, 1)); // The second to last number that defines legal gender
            string gender = genderNumber % 2 == 0 ? "Female" : "Male";
            string pronoun = genderNumber % 2 == 0 ? "her" : "his";
            if ((socialSecurityNumberLength == 10 ||
                socialSecurityNumberLength == 11))
            {
                DateTime birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);

                int age = DateTime.Now.Year - birthDate.Year;

                if ((birthDate.Month > DateTime.Now.Month)
                    || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
                {
                    age--;
                }

                if ((socialSecurityNumberLength == 11 )
                    && socialSecurityNumber.Substring(socialSecurityNumberLength - 5, 1) == "+")
                {
                    age += 100;
                }

                Console.WriteLine($"This is a {gender}, and {pronoun} age is {age}.");

            } else if  ((socialSecurityNumberLength == 12
                || socialSecurityNumberLength == 13))
            {
                DateTime birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

                int age = DateTime.Now.Year - birthDate.Year;

                if ((birthDate.Month > DateTime.Now.Month)
                    || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
                {
                    age--;
                }

                Console.WriteLine($"This is a {gender}, and {pronoun} age is {age}.");
            } else
            {
                Console.WriteLine($"The social secutrity number you have enter is {socialSecurityNumberLength} characters long which is the wrong ammount");
                Console.WriteLine("Please try again later");

            }
        }
    }
    /*
 $$$$$$\          $$$$$$$$\       $$$$$$$\  $$$$$$\ $$$$$$$$\ 
$$  __$$\         $$  _____|      $$  __$$\ \_$$  _|$$  _____|
$$ /  \__|        $$ |            $$ |  $$ |  $$ |  $$ |      
$$ |      $$$$$$\ $$$$$\          $$$$$$$  |  $$ |  $$$$$\    
$$ |      \______|$$  __|         $$  __$$<   $$ |  $$  __|   
$$ |  $$\         $$ |            $$ |  $$ |  $$ |  $$ |      
\$$$$$$  |        $$ |            $$ |  $$ |$$$$$$\ $$ |      
 \______/         \__|            \__|  \__|\______|\__|  
    */
}

