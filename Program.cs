using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoloLearn_pswd_gen
{
    class Program
    {
        static void Main(string[] args)
        {
            PsdwGen myChars = new PsdwGen();
            Console.Write("password length (from 3 to 99): ");
            Console.WriteLine("your password: {0}", myChars.PswdBuilder());
        }
    }

    //  Rules for password:
    //     *  -> at least 3 chars long
    //     *  -> always contains at least 1 letter, 1 number, 1 special sign

    public class PsdwGen
    {
        //Fields
        private List<char> _fullPot = new List<char>();
        private Random _rnd = new Random();

        //Constructor
        public PsdwGen()
        {
            for (char cH = 'a'; cH <= 'z'; cH++)            
                _fullPot.Add(cH);            
            
            for (char cH = 'A'; cH <= 'Z'; cH++)
                _fullPot.Add(cH);
                
            for (char cH = '!'; cH <= '@'; cH++)
                _fullPot.Add(cH);                            
        }

        //Methods
        public string PswdBuilder()
        {
            int pswdLength; //userInput
            UserInput.Input(out pswdLength);
            StringBuilder pswd = new StringBuilder();            
            bool containLetter = false, containNumber = false, containSpecialChar = false;

            while (containLetter == false || containNumber == false || containSpecialChar == false)
            {
                pswd.Clear();
                containLetter = false;
                containNumber = false;
                containSpecialChar = false;

                // building a new password from the full pot
                for (int i = 0; i < pswdLength; i++)
                {
                    pswd.Append(RandomCharFromTheFullPot()); 
                }

                // checking the type of each char in pswd
                foreach (char cH in pswd.ToString())
                {
                    if (Char.IsLetter(cH)) containLetter = true;
                    else if (Char.IsDigit(cH)) containNumber = true;
                    else containSpecialChar = true;
                }
            }

            return pswd.ToString();
        }

        //random from the full pot
        private char RandomCharFromTheFullPot() 
        {            
            int randomCharPosition = _rnd.Next(0, _fullPot.Count());
            return _fullPot[randomCharPosition];
        }
    }

    // returns password length (user choice)
    public class UserInput
    {
        public static void Input(out int x)
        {
            x = 3;

            try
            {
                x = int.Parse(Console.ReadLine());
                if (x < 3 || x > 100)
                {
                    Console.WriteLine("Wrong input. You should enter an integer number from within the range <3,100>.");
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong input. You should enter an integer number from within the range <3,100>.");
                Console.WriteLine(e);
                Environment.Exit(0);
            }
        }
    }
}
