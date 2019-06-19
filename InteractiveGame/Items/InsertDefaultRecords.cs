using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGame
{
    public class InsertDefaultRecords
    {
        public static void AddTestRecords()
        {
            //ClearDatabase();
            AddDefaultUsers();
            AddDefaultCategories();
            AddDefaultTopicsForHistory();
            AddDefaultTopicsForGeography();
            AddDefaultTopicsForMath();
        }

        // Add some users
        public static void AddDefaultUsers()
        {
            string[] usernames = new string[] { "dimitri", "pesho", "ceco", "test", "slavi69", "milen", "thebestyet" };
            string[] fullNames = new string[] { "Kanaliev", "Pesho", "Ceco", "Test Test", "Slavi Testov", "Milen", "Stoyan Kolev" };
            const string PASSWORD = "password@2";

            for (int i = 0; i < usernames.Length; i++)
            {
                App.DbManager.GameUser.Add(new GameUser(usernames[i], PASSWORD, fullNames[i]));
            }

            App.DbManager.SaveChanges();
        }

        // Add some categories
        public static void AddDefaultCategories()
        {
            string[] categories = new string[]
            {
                "История", "Математика", "География", "Литература", "Български език", "Биология", "тест"
            };

            foreach (string category in categories)
            {
                App.DbManager.Category.Add(new Category(category));
            }

            App.DbManager.SaveChanges();
        }

        // Add some topics for categories
        public static void AddDefaultTopicsForHistory()
        {
            string[] titles = new string[] { "Цар Калоян", "Турско робство" };
            string[] descriptions = new string[]
            {
                @"Калоян (Йоаница, Йоан II, Ioan II, Kalojan Asen, на латински: Kaloioannes, Caloiohannes, Johannitzes, Ioannica,
гр.: Kaloiōannēs, Iōannēs) – „Ромеоубиеца“ (* ок. 1170; † 1207) [1] е цар на България от 1197 до 1207 г.
от династията Асеневци. Той е най-малкият брат на българските царе Иван Асен I и Теодор Петър.
С цел да получи императорска титла от Светия престол, той влиза в кореспонденция с папа Инокентий III.
Изпратен е кардинал Лъв, който коронясва Калоян за rex Bulgarorum et Blachorum, правейки го единственият
български владетел с католическата титла крал.

Вероятно след обсадата на Ловеч от 1187 г. Калоян е пратен в Константинопол като заложник.
Там прекарва около две години. Калоян успява да избяга от Византия към края на 1189 г. Оттогава цар Асен го
държи постоянно при себе си. След убийството на цар Иван Асен I (Асен), цар Петър го назначава за помощник
в управлението. Когато и той става жертва на заговор, Калоян изпреварва и отстранява заговорниците, и бива коронясан
за български цар в началото на 1198 г.",

                @"Османското владичество[1] или още османска власт и османско господство, назовавано също, предимно в
българската възрожденска литература, като османско робство, османско иго или турско робство, е периодът от края
на 14 до края на 19 век, през който не съществува независима българска държава, а населените с българи земи са
под управлението на Османската империя. Унищожена е и самостоятелната Българска патриаршия, която е подчинена на
както и възстановената през 1557 г. заради сърбите в османските владения Печка патриаршия (до 1766 г.).
Българите подготвят редица въстания срещу местните и централните власти, като борбата за отхвърляне на османската власт
достига своя връх с Априлското въстание от 1876 г. и Руско-турската освободителна война от 1878 г."
            };

            int categoryId = App.DbManager.Category.Where(c => c.CategoryName == "История").Select(c => c.Id).First();

            App.DbManager.Topic.Add(new Topic(categoryId, titles[0], descriptions[0]));
            App.DbManager.Topic.Add(new Topic(categoryId, titles[1], descriptions[1]));

            App.DbManager.SaveChanges();
        }

        public static void AddDefaultTopicsForMath()
        {
            string[] titles = new string[] { "Смятане до 10", "Умножение", "Питагорова теорема" };
            string[] descriptions = new string[] 
            {
                @"1, 2, 3, 4, 5, 6, 7, 8, 9, 10 и т.н.",
                @" a . b = c",
                @"a^2 + b^2 = c^2 за Правоъгълен триъгълник"
            };
            
            int categoryId = App.DbManager.Category.Where(c => c.CategoryName == "Математика").Select(c => c.Id).First();

            for (int i = 0; i < titles.Length; i++)
            {
                App.DbManager.Topic.Add(new Topic(categoryId, titles[i], descriptions[i]));
            }

            App.DbManager.SaveChanges();
        }

        public static void AddDefaultTopicsForGeography()
        {
            string title = "Африка";
            string description = @"Африка е вторият по големина и по население континент в света след Азия.
Със своята площ от около 30 221 532 km² (11 млн. мили²) (включително площта на островите), тя заема 6% от
повърхността и 20% от сушата на Земята, а нейните над 1 001 320 281 обитатели представляват над 15% от населението на света.
Континентът е заобиколен от Средиземно море на север, Суецкия канал и Червено море по протежението на Синайския полуостров
на североизток, Индийския океан на югоизток, и Атлантическия океан на запад. Континентът има 52 суверенни държави, включително
остров Мадагаскар, редица други островни групи и Сахарска арабска демократична република, държава-членка на Африканския съюз,
чиято държавност се оспорва от Мароко.

Африка, по-специално Централна и Източна Африка, е широко разглеждана сред научната общност като мястото, от където произхождат
хората и човекоподобните маймуни, както е видно от откриването на най-ранните хоминиди и техните предци, както и по-късно тези,
които са на около седем милиона години.

Африка лежи от двете страни на екватора, което е предпоставка за обхващането на редица климатични зони. Единственият континент,
който стига от северния умерен до южния умерен пояс. Меридианът Гринуич също преминава по нейната територия.";

            int categoryId = App.DbManager.Category.Where(c => c.CategoryName == "География").Select(c => c.Id).First();

            App.DbManager.Topic.Add(new Topic(categoryId, title, description));
            App.DbManager.SaveChanges();
        }
    }
}
