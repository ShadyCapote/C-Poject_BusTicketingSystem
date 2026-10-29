using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapoteBusTicketSystemFinalTerm
{
    internal class PersonalRecord
    {
        public static int[] id = new int[50];
        public static string[] passenger = new string[50];
        public static string[] route = new string[50];
        public static string[] seatNo = new string[50];
        public static string[] departureTime = new string[50];
        public static int idCounter = 1;
        public static int indexCounter = 0;
        public static int cellClickIDHolder;
        public static bool cellClickIDChecker = false;

      


       


        public static string[] firstNames = new string[MAX];

        public const int MAX = 100;

        public static string[] names = new string[MAX];
        public static string[] lastNames = new string[MAX];
        public static string[] emails = new string[MAX];
        public static string[] userNames = new string[MAX];
        public static string[] passwords = new string[MAX];

        public static int Count = 0;

        public static object Usernames { get; internal set; }

      
    }
}
