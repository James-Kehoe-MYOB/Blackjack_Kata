using System;
using System.Collections.Generic;

namespace Blackjack {
    class Program {

        public enum Suit {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }
        public static bool currentPlayer = true;
        static List<Card> deck = new List<Card>();
        static Random random = new Random();
        
        static void Main(string[] args) {
            
            Player player = new Player("player");
            Player dealer = new Player("dealer");
            
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("Dealing Hand...");
            
            //Initialise game - deal player 2 cards

            gameInit(player);
            DisplayStatus(player);
            while (currentPlayer) {
                //Prompt hit or stay
                HitorStay(player);
            }

            if (CheckBust(player)) {
                Console.WriteLine("Dealer Wins!");
            }
            else {
                DealersTurn(dealer); 
                CheckWin(player, dealer);
            }
        }
        
        public static void gameInit(Player player) {
                    
                    //initialise new deck
                    
                    int count = 0;

                    foreach (Suit val in Enum.GetValues(typeof(Suit))) {
                        for (int i = 0; i < 13; i++) {
                            Card myCard = new Card(Suit.Hearts, 1);
                            myCard.setSuit(val);
                            myCard.setValue(i+1);
                            count++;
                            deck.Add(myCard);
                        }
                    }
                    
                    //deal 2 cards to the player
                    dealCard(player);
                    dealCard(player);
                    
                }
        
        public static void DisplayStatus(Player player) {
            //print individual cards
            if (CheckBust(player)) {
                if (player.getName() == "player") {
                    Console.WriteLine("You are currently at Bust!");
                }
                else if (player.getName() == "dealer") {
                    Console.WriteLine("The dealer is at Bust!");
                }
            }
            else {
                if (player.getName() == "player") {
                    Console.WriteLine("You are currently at {0}", player.getTotal());
                } else if (player.getName() == "dealer") {
                    Console.WriteLine("The dealer is at {0}", player.getTotal());
                }
            }
            Console.Write("with the hand [");
            for (int i = 0; i < player.getCards().Count; i++) {
                Console.Write("[{0}, {1}]", player.getCards()[i].getName(),  player.getCards()[i].getSuit());
            }
            Console.WriteLine("\n");
        }

        public static void HitorStay(Player player) {
            Console.Write("\nHit or Stay? (Hit = 1, Stay = 0) ");
            string response = Console.ReadLine();

            if (response.Equals("hit", StringComparison.OrdinalIgnoreCase) || response.Equals("1")) {
                dealCard(player);
                if (CheckBust(player)) {
                    currentPlayer = false;
                }
                DisplayStatus(player);
            } else if (response.Equals("stay", StringComparison.CurrentCultureIgnoreCase) || response.Equals("0")) {
                currentPlayer = false;
                Console.WriteLine("it is now the dealer's turn.");
            } else {
                Console.WriteLine("Please input a valid answer.");
                HitorStay(player);
            }
        }

        public static bool CheckBust(Player player) {
            if (player.getTotal() > 21) {
                return true;
            }
            else {
                return false;
            }
        }

        public static void ResolveAce(Player player) {
            int total = player.getTotal();
            for (int i = 0; i < player.getCards().Count; i++) {
                if (player.getCards()[i].getName().Equals("Ace")) {
                    if ((total - 11) < 11) {
                        player.getCards()[i].setPoints(11);
                    }
                    else {
                        player.getCards()[i].setPoints(1);
                    }
                }
            }
        }

        public static void dealCard(Player player) {
            int card_id = random.Next(0, deck.Count);
            player.cardAdd(deck[card_id]);
            deck.Remove(deck[card_id]);
            ResolveAce(player);
            player.setTotal();

        }

        public static void DealersTurn(Player dealer) {

            dealCard(dealer);
            dealCard(dealer);
            DisplayStatus(dealer);
            DealerLogic(dealer);
        }

        public static void DealerLogic(Player dealer) {
            while (dealer.getTotal() < 17) {
                dealCard(dealer);
                DisplayStatus(dealer);
            }
        }

        public static void CheckWin(Player player, Player dealer) {
            if (player.getTotal().CompareTo(dealer.getTotal()) < 0) {
                Console.WriteLine("Dealer Wins!");
            }
            else if (player.getTotal().CompareTo(dealer.getTotal()) > 0) {
                Console.WriteLine("You Win!");
            }
            else {
                Console.WriteLine("It's a tie!");
            }
        }
        
        //---------------------------------------------------
        //Player Class
        //---------------------------------------------------

        public class Player {
            string name;
            int total;
            List<Card> cards = new List<Card>();

            public Player(string name) {
                this.name = name;
            }

            public void setTotal() {
                total = 0;
                for (int i = 0; i < cards.Count; i++) {
                    total = total + cards[i].getPoints();
                }
            }

            public int getTotal() {
                return total;
            }

            public List<Card> getCards() {
                return cards;
            }

            public void cardAdd(Card card) {
                cards.Add(card);
            }

            public string getName() {
                return name;
            }

            /*public int calc_value(List<Card> cards) {
                
            }*/

        }
        
        //---------------------------------------------------
        //Card Class
        //---------------------------------------------------

        public class Card {
            Suit suit;
            int value;
            string name;
            int points;

            public Card(Suit suit, int value) {
                this.suit = suit;
                this.value = value;
            }

            public void setValue(int myValue) {
                value = myValue;
                setName(value);
            }

            private void setName(int value) {
                switch (value) {
                    case 1:
                        name = "Ace";
                        points = 1;
                        break;
                    case 2:
                        name = "2";
                        points = 2;
                        break;
                    case 3:
                        name = "3";
                        points = 3;
                        break;
                    case 4:
                        name = "4";
                        points = 4;
                        break;
                    case 5:
                        name = "5";
                        points = 5;
                        break;
                    case 6:
                        name = "6";
                        points = 6;
                        break;
                    case 7:
                        name = "7";
                        points = 7;
                        break;
                    case 8:
                        name = "8";
                        points = 8;
                        break;
                    case 9:
                        name = "9";
                        points = 9;
                        break;
                    case 10:
                        name = "10";
                        points = 10;
                        break;
                    case 11:
                        name = "Jack";
                        points = 10;
                        break;
                    case 12:
                        name = "Queen";
                        points = 10;
                        break;
                    case 13:
                        name = "King";
                        points = 10;
                        break;
                    default:
                        name = "null";
                        break;
                }
            }

            public string getName() {
                return name;
            }

            public void setPoints(int myPoints) {
                points = myPoints;
            }

            public int getPoints() {
                return points;
            }

            public void setSuit(Suit mySuit) {
                suit = mySuit;
            }

            public int getValue() {
                return value;
            }

            public Suit getSuit() {
                return suit;
            }
        }
    }
}