# pswd-generator
// password generator inspired by SoloLearn challenger

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PswdGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            CharsBase myChars = new CharsBase();       
            Console.WriteLine("your password: {0}", myChars.PswdBuilder());
        }
    }
    
    public class CharsBase
    {        
        //Fields
	    List<char> alphabetPot = new List<char>();
        List<char> numbersPot = new List<char>();
        List<char> specialCharsPot = new List<char>();
        List<char> specialCharsWithNumbersPot = new List<char>();
    
        public Random rnd = new Random();

    /*
    I will not random from the full pot, as the probability of drawing e.g. a number is less than a letter (there is only 10 numbers and
    52 numbers in the full pot). To give every char an equall chance, firstly I will random a pot and then chars.
    */

	//Constructor
        public CharsBase()
        {
            for (char cH = 'a'; cH <= 'z'; cH++) alphabetPot.Add(cH); //lower case letters
            for (char cH = 'A'; cH <= 'Z'; cH++) alphabetPot.Add(cH); //upper case letters
            for (char cH = '!'; cH <= '@'; cH++) specialCharsWithNumbersPot.Add(cH); //special characters including numbers
                                    
            foreach (char cH in specialCharsWithNumbersPot)
            {
                if (Char.IsDigit(cH)) numbersPot.Add(cH); //numbers only
                else specialCharsPot.Add(cH); //special characters only
            }            
        }                 
	
	    //Methods
	    public string PswdBuilder()
	    {
            int pswdLength; //userInput
            UserInput.Input(out pswdLength);
            
            StringBuilder pswd = new StringBuilder();
        
            bool containLetter = false, containNumber = false, containSpecialChar = false;
        
            for (int i = 0; i < pswdLength-2; i++)
            {
                pswd.Append(RandomPotRandomChar('a', 'n', 's')); //initially append from the full pot
            }
            
            //checking char types of each pswd char (less than last 2 chars, as they have not yet been appended)
            foreach (char cH in pswd.ToString())
            {
                if (Char.IsLetter(cH)) containLetter = true;
                else if (Char.IsDigit(cH)) containNumber = true;
                else containSpecialChar = true;
            }                
                if (containLetter == true && (containNumber == false && containSpecialChar == false)) //current uncomplete pswd contains only letters
                {
                    pswd.Append(RandomPotRandomChar(0, "sCh")); //as current uncomplete pswd contains only letters, penultimate pswd char must be either a number or special char
                    
                    //check the type of penultimate char and append the last char
                    if (Char.IsDigit(pswd[pswdLength-2])) pswd.Append(RandomPotRandomChar("sCh")); //penultimate is number, so the last char must be here a special char
                    else pswd.Append(RandomPotRandomChar(0));
                    return pswd.ToString();
                }
                else if (containNumber == true && (containLetter == false && containSpecialChar == false)) //current uncomplete pswd contains only numbers
                {
                    pswd.Append(RandomPotRandomChar('a', "sCh")); //as current uncomplete pswd contains only numbers, penultimate pswd char must be either a letter or special char
                    
                    //check the type of penultimate char and append the last char
                    if (Char.IsLetter(pswd[pswdLength-2])) pswd.Append(RandomPotRandomChar("sCh")); //penultimate is letter, so the last char must be here a special char
                    else pswd.Append(RandomPotRandomChar('a'));
                    return pswd.ToString();
                }
                else if (containSpecialChar == true && (containLetter == false && containNumber == false)) //current uncomplete pswd contains only special chars
                {
                    pswd.Append(RandomPotRandomChar('a', 0)); //as current uncomplete pswd contains only sp. chars, penultimate pswd char must be either a letter or number
                    
                    //check the type of penultimate char and append the last char
                    if (Char.IsLetter(pswd[pswdLength-2])) pswd.Append(RandomPotRandomChar(0)); //penultimate is letter, so the last char must be here a number
                    else pswd.Append(RandomPotRandomChar('a'));
                    return pswd.ToString();
                }
                else //current uncomplete pswd contains at least 2 different types of chars
                {
                    pswd.Append(RandomPotRandomChar('a', 'n', 's')); //as current uncomplete pswd contains at least 2 different types of chars, penultimate pswd char can be any
                    
                    //check previous char types (length - 1) and append the last char
                    foreach (char cH in pswd.ToString())
                    {
                        if (Char.IsLetter(cH)) containLetter = true;
                        else if (Char.IsDigit(cH)) containNumber = true;
                        else containSpecialChar = true;
                    }
                        if (containLetter == true && containNumber == true && containSpecialChar == true) //current uncomplete pswd already contains all chars, so the last one can be any
                        {
                            pswd.Append(RandomPotRandomChar('a', 'n', 's'));
                            return pswd.ToString();
                        }
                        else if (containLetter == true && containNumber == true && containSpecialChar == false) //current uncomplete pswd does not contain a special char
                        {
                            pswd.Append(RandomPotRandomChar("sCh"));
                            return pswd.ToString();
                        }
                        else if (containLetter == true && containNumber == false && containSpecialChar == true) //current uncomplete pswd does not contain a number
                        {
                            pswd.Append(RandomPotRandomChar(0));
                            return pswd.ToString();
                        }                    
                        else //current uncomplete pswd does not contain a letter
                        {
                            pswd.Append(RandomPotRandomChar('a'));
                            return pswd.ToString();
                        }                    
                }
	}
	
	    public char RandomPotRandomChar(char a, char n, char s) //random from the full pot
	    {
    	    char pswdCh;
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
                dict.Add(0, alphabetPot);
                dict.Add(1, numbersPot);
                dict.Add(2, specialCharsPot);
            int key, randomCharPosition;
            
            key = rnd.Next(0, 3);            
            randomCharPosition = rnd.Next(0, dict[key].Count());
            pswdCh = dict[key][randomCharPosition];
	    
	        return pswdCh;
	    }
	
        public char RandomPotRandomChar(int n, string sCh) //random form either numbers or special chars
	    {
    	    char pswdCh;
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
                dict.Add(0, numbersPot);
                dict.Add(1, specialCharsPot);
            int key, randomCharPosition;
            
            key = rnd.Next(0, 2);            
            randomCharPosition = rnd.Next(0, dict[key].Count());
            pswdCh = dict[key][randomCharPosition];
	    
	        return pswdCh;
	    }
	
        public char RandomPotRandomChar(char a, string sCh) //random from either letters or special chars
	    {
	        char pswdCh;
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
                dict.Add(0, alphabetPot);
                dict.Add(1, specialCharsPot);
            int key, randomCharPosition;
            
            key = rnd.Next(0, 2);            
            randomCharPosition = rnd.Next(0, dict[key].Count());
            pswdCh = dict[key][randomCharPosition];
	    
	        return pswdCh;
    	}
	
        public char RandomPotRandomChar(char a, int n) //random from either letters or numbers
	    {
	        char pswdCh;
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();
                dict.Add(0, alphabetPot);
                dict.Add(1, numbersPot);
            int key, randomCharPosition;
            
            key = rnd.Next(0, 2);            
            randomCharPosition = rnd.Next(0, dict[key].Count());
            pswdCh = dict[key][randomCharPosition];
	    
	        return pswdCh;
	    }

        public char RandomPotRandomChar(char a) //random letter
	    {
	        char pswdCh;

            int randomCharPosition = rnd.Next(0, alphabetPot.Count());
            pswdCh = alphabetPot[randomCharPosition];
	    
	        return pswdCh;
    	}
    
        public char RandomPotRandomChar(int n) //random digit
	    {
	        char pswdCh;

            int randomCharPosition = rnd.Next(0, numbersPot.Count());
            pswdCh = numbersPot[randomCharPosition];
	    
	        return pswdCh;
	    } 

        public char RandomPotRandomChar(string sCh) //random special char
	    {
	        char pswdCh;

            int randomCharPosition = rnd.Next(0, specialCharsPot.Count());
            pswdCh = specialCharsPot[randomCharPosition];
	    
	        return pswdCh;
    	}                                                    
    }      
    
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
                //Console.WriteLine(e);
                Environment.Exit(0);
            }
        }
    }    
}
