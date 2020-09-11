using System;
using System.Globalization;
using System.Threading;
using System.Xml;

namespace SocialSecurityNumber
{
    class Program
    {
        const string VERSION_NUMBER = "1.2 Salt and Pepper";
        public enum Gender
        {
            Male,
            Female
        }

         public enum Pronoun
        {
            her,
            his
        }

        static void Main(string[] args) // Varför är stringen arg?
        {
            string socialSecurityNumber = null;
            string firstName = null;
            string lastName = null;
            int socialSecurityNumberLength = 0;
            int age = 0;
            string temp;
            Gender gender;
            Pronoun pronoun;
            if (args.Length != 0) // check if there is an argument in the cmd / terminal / whatever you use for this
            {
                temp = args[0];
                if (temp.Length < 10)
                {
                    string[] help = new string[] { "h", "H", "help", "Help", "f", "F", "format", "Format", "s", "S", "syntax", "Syntax" };
                    string[] version = new string[] { "v", "V", "version", "Version" }; ;
                    string helpOutput = null;
                    string versionOutput = null;

                    if (Array.IndexOf(help, temp) >= 0)
                    {
                        GetHelp();
                        goto Quit;
                    }
                    else if (Array.IndexOf(version, temp) >= 0)
                    {
                        GetVersion();
                        goto Quit;
                    }
                    else
                    {
                        Console.WriteLine("syntax error, please type ");
                        foreach (string helpTemp in help)
                        {
                            helpOutput = helpOutput + helpTemp;
                            helpOutput = helpOutput + " ";
                        }
                        Console.WriteLine(helpOutput);
                        Console.WriteLine("for help");

                        goto Quit;
                    }
                }
                switch (args.Length) // checks how many arguments there are
                {
                    case 1:
                        socialSecurityNumber = args[0];
                        break;

                    case 2:
                        socialSecurityNumber = args[0];
                        firstName = args[1];
                        break;

                    case 3:
                        socialSecurityNumber = args[0];
                        firstName = args[1];
                        lastName = args[2];
                        break;

                    default:
                        break;
                }
            }
            else
            {
                Console.Write("Please enter your social security number ((YY)YYMMDD-NNNN): ");
                socialSecurityNumber = Console.ReadLine();
            }

            while (firstName == null)
            {
                Console.Write("Please enter your First/given name: ");
                firstName = Console.ReadLine();
            }

            while (lastName == null)
            {
                Console.Write("Please enter your Last/family name: ");
                lastName = Console.ReadLine();
            }
            gender = GetGender(socialSecurityNumber);
            pronoun = GetPronoun(socialSecurityNumber);
            age = GetAge(socialSecurityNumber);
            while (age == -1)
            {
                Console.Clear();
                Console.WriteLine($"The social security number you entered  had {socialSecurityNumberLength} characters");
                Console.WriteLine("This isn't one of the correct lenghts for the social security number");
                Console.Write("Please enter your social security number ((YY)YYMMDD-NNNN): ");
                socialSecurityNumber = Console.ReadLine();
                age = GetAge(socialSecurityNumber: socialSecurityNumber);
            }
            string generation = GetGeneration(age);

            Console.WriteLine($"Social security number: {socialSecurityNumber}");
            Console.WriteLine($"First/given name: {firstName}");
            Console.WriteLine($"Last/family name: {lastName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Generation: {generation}");


        Quit: { } //not sure why this needs to be here but I got an error when I didn't have this

        }

        private static string GetGeneration(int age)
        {
            string generation;
            int birthYear = DateTime.Now.Year - age;
            // I might update this when a new generation hit the scene but I wouldn't bet on it
            if (birthYear >= 2012) // source for generation data https://en.wikipedia.org/wiki/File:Generation_timeline.svg collected 2020-09-03 16:34 CEST
            {
                generation = "Gen Alpha";
            }
            else if (birthYear <= 2011 && birthYear >= 1997)
            {
                generation = "Gen Z";
            }
            else if (birthYear <= 1996 && birthYear >= 1981)
            {
                generation = "Millenial";
            }
            else if (birthYear <= 1980 && birthYear >= 1965)
            {
                generation = "Gen X";
            }
            else if (birthYear <= 1964 && birthYear >= 1946)
            {
                generation = "Baby Boomer"; // Ok boomer
            }
            else if (birthYear <= 1945 && birthYear >= 1928)
            {
                generation = "Silent Generation";
            }
            else if (birthYear <= 1927 && birthYear >= 1901)
            {
                generation = "Greatest Generation"; // No, this isn't my opinion, it's just what the generation happens to be named
            }
            else if (birthYear <= 1900 && birthYear >= 1883)
            {
                generation = "Lost Generation";
            }
            else
            {
                generation = "N/A";
            }

            return generation;
        }

        protected static int GetAge(string socialSecurityNumber)
        {
            int socialSecurityNumberLength = socialSecurityNumber.Length;
            int age;
            int socialSecurityNumberDashLocation = socialSecurityNumberLength - 5;
            DateTime birthDate;
            if ((socialSecurityNumberLength == 10 ||
                socialSecurityNumberLength == 11))
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 6), "yyMMdd", CultureInfo.InvariantCulture);

                age = DateTime.Now.Year - birthDate.Year;

                if ((birthDate.Month > DateTime.Now.Month)
                    || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
                {
                    age--;
                }
                while (age < 0) // this is to counter any date time shenanigans
                    //I.E. interpeting some number as belonging to the 21th century when they are obviously not
                    //I.E interpeting 25 as 2025 and not 1925;
                {
                    age += 100;
                }
                if (socialSecurityNumber.Substring(socialSecurityNumberDashLocation, 1) == "+")
                {
                    age += 100;
                }
                return age;

            }
            else if ((socialSecurityNumberLength == 12
                || socialSecurityNumberLength == 13))
            {
                birthDate = DateTime.ParseExact(socialSecurityNumber.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture);

                age = DateTime.Now.Year - birthDate.Year;

                if ((birthDate.Month > DateTime.Now.Month)
                    || (birthDate.Month == DateTime.Now.Month && birthDate.Day > DateTime.Now.Day))
                {
                    age--;
                }
                return age;
            }
            return -1;
        }

        protected static Gender GetGender(string socialSecurityNumber)
        {
            int socialSecurityNumberLength = socialSecurityNumber.Length;
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumberLength - 2, 1)); // The second to last number that defines legal gender
            Gender gender = genderNumber % 2 == 0 ? Gender.Female : Gender.Male;

            return gender;
        }
        protected static Pronoun GetPronoun(string socialSecurityNumber)
        {
            int socialSecurityNumberLength = socialSecurityNumber.Length;
            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumberLength - 2, 1));
            Pronoun pronoun = genderNumber % 2 == 0 ? Pronoun.her : Pronoun.his;
            return pronoun;
        }

        protected static void GetHelp()
        {
            string[] help = new string[] { "h", "H", "help", "Help", "f", "F", "format", "Format", "s", "S", "syntax", "Syntax" };
            string[] version = new string[] { "v", "V", "version", "Version" };
            string helpOutput = null;
            string versionOutput = null;

            Console.WriteLine("Syntax");
            Console.WriteLine("    Social-Securty-Number");
            Console.WriteLine("    Social-Securty-Number First/given-name");
            Console.WriteLine("    Social-Securty-Number First/given-name Last/given-name");
            Console.WriteLine("Help");

            foreach (string helpTemp in help)
            {
                helpOutput = helpOutput + helpTemp;
                helpOutput = helpOutput + " ";
            }

            Console.WriteLine($"    {helpOutput}");
            Console.WriteLine("Verison");

            foreach (string versionTemp in version)
            {
                versionOutput = versionOutput + versionTemp;
                versionOutput = versionOutput + " ";
            }
            Console.WriteLine($"    {versionOutput}");
        }

        private static void GetVersion()
        {
            Console.WriteLine($"Version: {VERSION_NUMBER}");
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

