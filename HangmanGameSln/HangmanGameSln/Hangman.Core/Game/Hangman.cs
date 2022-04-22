using System;
using HangmanRenderer.Renderer;

namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;
        private string[] _hangmanWords = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
                                           "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", "twenty" };
        private string _mysteryWord = "";
        private string _nextGuess;
        private string _currentGuess = "";
        private string _noMatch = "";

        Random rnd = new Random();
        public HangmanGame() //constructor
        {
            _renderer = new GallowsRenderer();
        }

        private void CurrentWord()
        {
            int currentWord = rnd.Next(_hangmanWords.Length);
            _mysteryWord = _hangmanWords[currentWord];
        }

        private void StartHangman()
        {
            Console.SetCursorPosition(0, 13);
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < _mysteryWord.Length; i++) //loop - to draw correct # of dashes for mysteryword
            {
                _currentGuess += "-";
            }
            Console.Write("Your current guess: " + _currentGuess);

            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Letters guessed that are not a match:" + _noMatch);
        }

        private void PlayerGuess()
        {
            bool correctGuess = false;
            char[] currentGuessArray = _currentGuess.ToCharArray();
            while (!correctGuess)
            {
                Console.SetCursorPosition(0, 17);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("What is your next guess: ");
                _nextGuess = Console.ReadLine();
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("                                                   "); //clears exception messages

                
                try
                {

                    if (_currentGuess.Contains(_nextGuess) || _noMatch.Contains(_nextGuess))
                    {
                        throw new DuplicateGuessException("");
                    }
                    for (int i = 0; i < _mysteryWord.Length; i++) //loops through each letter of the mysteryword
                    {
                        if (_mysteryWord[i] == char.Parse(_nextGuess)) //char.parse converts string into char
                        {

                            currentGuessArray[i] = char.Parse(_nextGuess);
                        }
                    }

                    if (_mysteryWord.Contains(_nextGuess))
                    {
                        throw new ReplayRoundException("");
                    }
                    correctGuess = true;

                    if (_currentGuess.Contains(char.Parse(_nextGuess))) //checks if currentguess contains nextGuess user input letter
                    {
                        _noMatch = _noMatch; // if it does - noMatch remains the same
                    }
                    else
                    {
                        _noMatch = _noMatch + _nextGuess; //if it does not, then nextGuess is added to noMatch string - indicates to user that the letter is not in the mysteryword
                    }

                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Letters guessed that are not a match:" + _noMatch);

                    /*if (_currentGuess.Contains(_nextGuess) || _noMatch.Contains(_nextGuess))
                    {
                        throw new DuplicateGuessException("");
                    }*/
                }


                catch (ReplayRoundException ex)
                {
                    _currentGuess = new string(currentGuessArray); //converts currentGuess array into string
                    Console.SetCursorPosition(0, 13);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Your current guess: " + _currentGuess);
                    correctGuess = false;

                }
                catch (FormatException ex) //format exception due to char conversion of nextGuess above, wont allow more than one letter at a time
                {
                    Console.SetCursorPosition(0, 20);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please enter only one letter per guess");
                    correctGuess = false;
                }
                catch (DuplicateGuessException ex)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("You have already guessed this letter. Try again!");
                }
            }
        }

        public void Run()
        {
            CurrentWord(); //chooses current mysterWord
            StartHangman(); //this code only runs once when the game is started

           for (int i = 6; i > 0; i--) //loops through hangman for each round using i
           {
                _renderer.Render(5, 5, i); //draws gallows at each respective round
                PlayerGuess();

                if (_currentGuess == _mysteryWord)
                {
                    break;
                }
           }
            
            if (_currentGuess != _mysteryWord)
            {
                _renderer.Render(5, 5, 0);
                Console.SetCursorPosition(0, 20);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("YOU DIED!");
                Console.WriteLine($"The correct word is {_mysteryWord}");
            }
            else
            {
                Console.SetCursorPosition(0, 20);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("YOU SURVIVED!");
            }
        }
        
    }
}
