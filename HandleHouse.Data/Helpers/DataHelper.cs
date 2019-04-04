using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandleHouse.Data.Models;

namespace HandleHouse.Data.Helpers
{
    public class DataHelper
    {
        private readonly Settlement[] _settlementsArray;
        private readonly House[] _housesArray;

        private readonly Random _rnd = new Random();
        private readonly DateTime _lowBirthDate = new DateTime(1943, 1, 1);
        private readonly DateTime _lowSetDate = new DateTime(2000, 1, 1);
        private readonly int _daysFromLowBirthDate;
        private readonly int _daysFromLowSetDate;

        //Settlement parameters
        //ctor (TypeHelper.SettlementType type, string name, string region, string district)
        private readonly string[] _settlementNames = new string[] { "Arizona", "New Mexico", "Florida", "Maine", "Virginia", "New York", "Massachusetts" };
        private readonly string[] _settlementRegions = new string[] { "Arizona", "New Mexico", "Florida", "Maine", "Virginia", "New York", "Massachusetts" };
        private readonly string[] _settlementDistricts = new string[] { "Alabama", "Arizona", "California", "Connecticut", "Florida", "Kansas", "Michigan", "Nevada", "Ohio", "Texas"};

        //House parameters
        //ctor (int number, string passNum, int roomNumber, int area, string street, Settlement settlement)
        private readonly string[] _houseStreets = new string[]
        {
            "Lenore Ln", "James Dr", "Timothy Pl", "Fort Lyon St", "Kings Hwy", "Wagon Dr", "Paul Mill Rd", "Don Mill Rd",
            "Walt Mill St", "Garnet St", "Gray Rock Dr", "Henhawk St", "Eagles Wing St", "Blue Barrow Ride", "Chatham St"
        };

        //Work parameters
        //ctor (TypeHelper.WorkType type, int cost, DateTime start, DateTime end, House house, string description)
        private readonly string[] _workDescriptions = new string[]
        {
            "Wallpaper", "Floor", "Roof", "Walls", "Windows", "Door", "Electricity", "Carpet", "Sofa", "Armchair",
            "Chair", "Bed", "Table"
        };

        //Furniture parameters
        //ctor (string name, int cost, DateTime setDate, House house)
        private readonly string[] _furnitureNames = new string[] {"Table", "Chair", "Shelf", "Sofa"};

        //Person parameters
        //ctor ((string first, string last, string patronymic, DateTime birthday, House house, TypeHelper.Sex sex)
        private readonly string[] _personMaleFirstNames = new string[] {"Bill"};
        private readonly string[] _personFemaleFirstNames = new string[] { "Lora"};
        private readonly string[] _personMaleLastNames = new string[] {"Jones"};
        private readonly string[] _personFemaleLastNames = new string[] { "Bond"};
        private readonly string[] _personMalePatronymics = new string[] { "J."};
        private readonly string[] _personFemalePatronymics = new string[] {"K." };

        //Owner parameters
        //ctor (string passport, string idNumber, string phone, string email, Person person)
        private readonly string[] _ownerPassportNumbers = new string[] {"DY1245HA"};
        private readonly string[] _ownerEmails = new string[] {"crematory@gmai.com"};

        public House[] Houses
        {
            get { return _housesArray; }
        }

        public Settlement[] Settlements
        {
            get { return _settlementsArray; }
        }

        public DataHelper()
        {
            _settlementsArray = GetSettlements().ToArray();
            _housesArray = GetHouses().ToArray();
            _daysFromLowBirthDate = (DateTime.Today - _lowBirthDate).Days;
            _daysFromLowSetDate = (DateTime.Today - _lowSetDate).Days;
        }

        private T GetRandomItem<T>(T[] data)
        {
            return data[_rnd.Next(0, data.Length)];
        }

        private static bool Swap<T>(ref T firstItem, ref T secondItem)
        {
            try
            {
                T temp = firstItem;
                firstItem = secondItem;
                secondItem = temp;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private DateTime GetRandomDate(DateTime lowDate, int daysFromLow)
        {
            return lowDate.AddDays(_rnd.Next(daysFromLow));
        }

        private List<Settlement> GetSettlements(int total = 5)
        {
            List<Settlement> outList = new List<Settlement>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetSettlement());
            }

            return outList;
        }

        private Settlement GetSettlement()
        {
            Array values = Enum.GetValues(typeof(TypeHelper.SettlementType));
            TypeHelper.SettlementType randomtype = (TypeHelper.SettlementType)values.GetValue(_rnd.Next(values.Length));
            string name = GetRandomItem(_settlementNames);
            string region = GetRandomItem((_settlementRegions));
            string district = GetRandomItem((_settlementDistricts));
            return new Settlement(randomtype, name, region, district);
        }

        private List<House> GetHouses(int total = 15)
        {
            List<House> outList = new List<House>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetHouse());
            }

            return outList;
        }

        private House GetHouse()
        {
            int number = _rnd.Next(1, 130);
            string passNum = "";
            for (int i = 0; i < 12; i++)
            {
                passNum += _rnd.Next(0, 10).ToString();
            }

            int roomNumb = _rnd.Next(1, 15);
            int area = _rnd.Next(60, 151);
            string street = GetRandomItem(_houseStreets);
            Settlement settlement = GetRandomItem(_settlementsArray);
            return new House(number, passNum, roomNumb, area, street, settlement);
        }

        public List<Work> GetWorks(int total = 3)
        {
            List<Work> outList = new List<Work>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetWork());
            }

            return outList;
        }

        private Work GetWork()
        {
            Array values = Enum.GetValues(typeof(TypeHelper.WorkType));
            TypeHelper.WorkType randomtype = (TypeHelper.WorkType)values.GetValue(_rnd.Next(values.Length));
            int cost = _rnd.Next(10, 5001);
            DateTime startDate = GetRandomDate(_lowSetDate, _daysFromLowSetDate);
            DateTime endDate = GetRandomDate(_lowSetDate, _daysFromLowSetDate);
            if (endDate < startDate)
                Swap(ref startDate, ref endDate);
            House house = GetRandomItem(_housesArray);
            string description = GetRandomItem(_workDescriptions);
            return new Work(randomtype, cost, startDate, endDate, house, description);
        }

        public List<Furniture> GetAllFurniture(int total = 15)
        {
            List<Furniture> outList = new List<Furniture>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetFurniture());
            }

            return outList;
        }

        private Furniture GetFurniture()
        {
            string name = GetRandomItem(_furnitureNames);
            int cost = _rnd.Next(5, 3001);
            DateTime date = GetRandomDate(_lowSetDate, _daysFromLowSetDate);
            House house = GetRandomItem(_housesArray);
            return new Furniture(name, cost, date, house);
        }

        public List<Person> GetPeople(int maxTotal = 5)
        {
            int total = _rnd.Next(1, maxTotal);
            List<Person> outList = new List<Person>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetPerson());
            }

            return outList;
        }

        private Person GetPerson()
        {
            Array values = Enum.GetValues(typeof(TypeHelper.Sex));
            TypeHelper.Sex randomSex = (TypeHelper.Sex)values.GetValue(_rnd.Next(values.Length));
            string first, last, patronymic;
            if (randomSex == TypeHelper.Sex.Female)
            {
                first = GetRandomItem(_personFemaleFirstNames);
                last = GetRandomItem(_personFemaleLastNames);
                patronymic = GetRandomItem(_personFemalePatronymics);
            }
            else
            {
                first = GetRandomItem(_personMaleFirstNames);
                last = GetRandomItem(_personMaleLastNames);
                patronymic = GetRandomItem(_personMalePatronymics);
            }

            DateTime birthday = GetRandomDate(_lowBirthDate, _daysFromLowBirthDate);
            House house = GetRandomItem(_housesArray);

            return new Person(first, last, patronymic, birthday, house, randomSex);
        }

        public List<Owner> GetOwners()
        {
            int total = _rnd.Next(1, _housesArray.Length);
            List<Owner> outList = new List<Owner>();
            for (int i = 0; i < total; i++)
            {
                outList.Add(GetOwner());
            }

            return outList;
        }

        private Owner GetOwner()
        {
            Person person = GetPerson();
            string passport = GetRandomItem(_ownerPassportNumbers);
            string id = "";
            string phone = "+1";
            for (int i = 0; i < 10; i++)
            {
                id += _rnd.Next(0, 10).ToString();
                phone += _rnd.Next(0, 10).ToString();
            }

            string email = GetRandomItem(_ownerEmails);
            return new Owner(passport, id, phone, email, person);
        }
    }
}
