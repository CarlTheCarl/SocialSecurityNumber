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
            const string VERSION_NUMBER = "1.1 Socks with sandals Bugfix 1";
            string socialSecurityNumber = null;
            string firstName = null;
            string lastName = null;
            int socialSecurityNumberLength = 0;
            int age;
            string temp;
            string helpOutput = null;
            string versionOutput = null;
            if (args.Length != 0) // check if there is an argument in the cmd / terminal / whatever you use for this
            {
                temp = args[0];
                if (temp.Length < 10)
                {
                    string[] help = new string[] { "h", "H", "help", "Help", "f", "F", "format", "Format", "s", "S", "syntax", "Syntax" };
                    string[] version = new string[] { "v", "V", "version", "Version" };

                    if (Array.IndexOf(help, temp) >= 0)
                    {
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

                        goto Quit;
                    }
                    else if (Array.IndexOf(version, temp) >= 0)
                    {
                        Console.WriteLine($"Version: {VERSION_NUMBER}");
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
                        socialSecurityNumberLength = socialSecurityNumber.Length;
                        break;

                    case 2:
                        socialSecurityNumber = args[0];
                        socialSecurityNumberLength = socialSecurityNumber.Length;
                        firstName = args[1];
                        break;

                    case 3:
                        socialSecurityNumber = args[0];
                        socialSecurityNumberLength = socialSecurityNumber.Length;
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

            int genderNumber = int.Parse(socialSecurityNumber.Substring(socialSecurityNumberLength - 2, 1)); // The second to last number that defines legal gender
            string gender = genderNumber % 2 == 0 ? "Female" : "Male";
            string pronoun = genderNumber % 2 == 0 ? "her" : "his";

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

                //if ((socialSecurityNumberLength == 11 ) 
                //    && socialSecurityNumber.Substring(socialSecurityNumberLength - 5, 1) == "+")
                //{
                //    age += 100;
                //} // Temporarly removing this until I can get it fixed with the whole generation calculation thing, probably gonna rewrite this whole thing though.

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

            } else
            {
                // this code is probably not important but I'm keeping it here incase something messes up further up
                Console.WriteLine($"The social secutrity number you have enter is {socialSecurityNumberLength} characters long which is the wrong ammount");
                Console.WriteLine("Please try again later");
                goto Quit;

            }

            string generation;
            int birthYear = birthDate.Year;
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

            Console.WriteLine($"Social security number: {socialSecurityNumber}");
            Console.WriteLine($"First/given name: {firstName}");
            Console.WriteLine($"Last/family name: {lastName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Generation: {generation}");


        Quit: { } //not sure why this needs to be here but I got an error when I didn't have this

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

