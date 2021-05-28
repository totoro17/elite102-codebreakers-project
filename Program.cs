using System;
using System.IO;

namespace hello
{
    class Program
    {
        static string CaeserShiftEncript(string letter)
        {
            string encriptedLetter = rotXencode(letter, 3);
            return encriptedLetter;
        }
          static string CaeserShiftDecript(string letter)
        {
            string decriptedLetter = rotXdecode(letter, 3);
            return decriptedLetter;
        }

        
        //ROTx cipher that shifts down the alphabet by x places, a negavtive x value shifts up the alphabet
        static string rotXencode (string letter, int x)
        {
            string[] Alphabet = new string [] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            int index = Array.IndexOf(Alphabet, letter);
            int newIndex = index + x;

            if (x >= 0)
            {

                if (newIndex >= 26)
                {
                    newIndex = newIndex - 26;
                }

            }

            if (x < 0)
            {
                if (newIndex < 0)
                {
                    newIndex += 26;
                }

            }

            string encriptedLetter = Alphabet[newIndex];

            if (letter == " ")
                {
                encriptedLetter = " ";
                }

            return encriptedLetter;
            
        }

    static string rotXdecode (string letter, int x)
        {
            string[] Alphabet = new string [] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
            int index = Array.IndexOf(Alphabet, letter);
            int newIndex = index - x;

            if (x >= 0)
            {

                if (newIndex < 0)
                {
                    newIndex += 26;
                }

            }

            if (x < 0)
            {
                if (newIndex > 26)
                {
                    newIndex = newIndex - 26;
                }

            }

            string encriptedLetter = Alphabet[newIndex];

            if (letter == " ")
                {
                encriptedLetter = " ";
                }

            return encriptedLetter;
            
        }

    static string vigenereEncode (string contents, string keyword)
    {   
        contents = contents.ToLower();
        contents = contents.Replace(" ", String.Empty);
        keyword = keyword.ToLower();
        string encKey = "";
        int contentLength = contents.Length;
        int keywordLength = keyword.Length;
        int counter = 0;
        string encriptedmessage = "";
        
        for (int i = 0 ; i < contentLength ; i++)
        {   
            if (counter == keywordLength)
            {
                counter = 0;
            }

            encKey = encKey + keyword[counter];
            counter += 1;
            
        }

        for (int i = 0 ; i < contentLength ; i++)
        {
            string[] Alphabet = new string [] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

            var original_letter = contents[i];
            string letter = Char.ToString(original_letter);

            var enc_letter = encKey[i];
            string shift = Char.ToString(enc_letter);

            int index = Array.IndexOf(Alphabet, shift);
            
            string newLetter = rotXencode(letter, index);

            encriptedmessage = encriptedmessage + newLetter;

        }

        return encriptedmessage;

    }

    static string vigenereDecode (string contents, string keyword)
    {   
        contents = contents.ToLower();
        contents = contents.Replace(" ", String.Empty);
        keyword = keyword.ToLower();
        string encKey = "";
        int contentLength = contents.Length;
        int keywordLength = keyword.Length;
        int counter = 0;
        string decriptedmessage = "";
        
        for (int i = 0 ; i < contentLength ; i++)
        {   
            if (counter == keywordLength)
            {
                counter = 0;
            }

            encKey = encKey + keyword[counter];
            counter += 1;
            
        }

        for (int i = 0 ; i < contentLength ; i++)
        {
            string[] Alphabet = new string [] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

            var original_letter = contents[i];
            string letter = Char.ToString(original_letter);

            var enc_letter = encKey[i];
            string shift = Char.ToString(enc_letter);

            int index = Array.IndexOf(Alphabet, shift);
            
            string newLetter = rotXdecode(letter, index);

            decriptedmessage = decriptedmessage + newLetter;

        }

        return decriptedmessage;

    }

        static void Main(string[] args)
        {
            string myFilePath = args[0];
            string task = args[1];
            string value = args[2];
            string cipherType = args[3];
            string keyword = args[4];
            int shiftValue = Convert.ToInt32(value);
            string new_message = "";
            
            
            //check to see if file selected exists before proceeding
            while (!File.Exists(myFilePath))
            {
                Console.WriteLine("Error: The file you have entered does not exist. Please enter in another file option: ");
                myFilePath = Console.ReadLine();
            }

             //check if the keyword is valid ("encode" or "decode")
            while (task != "encode" & task != "decode")
            {
                Console.WriteLine("Error: The task you entered is invalid. Please select either 'encode' or 'decode': ");
                task = Console.ReadLine();
            }

            //check if the cipher type is valid
            while (cipherType != "caesar" & cipherType != "vigenere" & cipherType != "rotx")
            {
                Console.WriteLine("Error: The cipher type you entered is invalid. Please select either 'caeser', 'vigenere', or 'rotx'");
                cipherType = Console.ReadLine();
            }

            string filecontents = File.ReadAllText(myFilePath);
            filecontents = filecontents.Trim('\r','\n');


            if (cipherType == "vigenere")
            {
                filecontents = filecontents.Replace(" ", String.Empty);

                if (task == "encode")
                {
                    new_message = vigenereEncode(filecontents, keyword);
                }
                if (task == "decode")
                {
                    new_message = vigenereDecode(filecontents, keyword);
                }
            }


            if (cipherType == "rotx")
            {
                if (task == "encode")
                {   
                    foreach (char c in filecontents)
                    {
                        string myLetter = c.ToString();
                        string shift = rotXencode(myLetter,shiftValue);
                        new_message = new_message + shift;
                    }
                    
                }
                if (task == "decode")
                {
                    foreach (char c in filecontents)
                    {
                        string myLetter = c.ToString();
                        string shift = rotXdecode(myLetter, shiftValue);
                        new_message = new_message + shift;
                    }
                    
                }

            }

            if (cipherType == "caesar")
            {
                if (task == "encode")
                {   
                    foreach (char c in filecontents)
                    {
                        string myLetter = c.ToString();
                        string shift = CaeserShiftEncript(myLetter);
                        new_message = new_message + shift;
                    }
                }

                if (task == "decode")
                {
                    foreach (char c in filecontents)
                    {
                        string myLetter = c.ToString();
                        string shift = CaeserShiftDecript(myLetter);
                        new_message = new_message + shift;
                    }
                }

            }

            var superSecretFile = new StreamWriter(myFilePath);
            superSecretFile.WriteLine(new_message);
            superSecretFile.Close(); 
            


        }




    }
}


