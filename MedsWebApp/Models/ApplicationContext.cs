using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MedsWebApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<MedicineInPharmacy> MedicinesInParmacies { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (!Database.EnsureCreated()) return;
            InitDb();
        }

        private void InitDb()
        {
            var manufacturers = new Manufacturer[]
            {
                new Manufacturer {Name = "Енкьюб Етикалз ЛТД", Address = "пр.Индез 21", Country = "Индия", Email = "ind@com.ua", Phone = "+7083454556"},
                new Manufacturer {Name = "Кенди ЛТД", Address = "ул.Боунта 1", Country = "Болгария", Email = "bolg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Э.И.П.И.Ко", Address = "ул.Индустриальная 1", Country = "Египет", Email = "egipt@com.ua", Phone = "+7083454558"},
                new Manufacturer {Name = "ФармаВижн", Address = "ул. Ататурк 2", Country = "Турция", Email = "turk@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "5Уорлд Медицин", Address = "ул.Траст 3", Country = "Турция", Email = "tur2k@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "Фармак", Address = "ул.Стуся 3", Country = "Украина", Email = "ukr2k@com.ua", Phone = "+80443454559"},
                new Manufacturer {Name = "Вертекс", Address = "ул.Поля 3", Country = "Украина", Email = "ukr1k@com.ua", Phone = "+80443454559"},
                new Manufacturer {Name = "Абби Ибрахим", Address = "ул.Траст 87", Country = "Турция", Email = "tur3k@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "ПЛІВА Хрватска", Address = "ул.Траст 87", Country = "Республіка Хорватія", Email = "tur3k@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "Тева Оперейшнз Поланд ", Address = "ул.Траст 87", Country = "Польща", Email = "polk@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "Санофі С.п.А Італія", Address = "ул.Грос 17", Country = " Італія", Email = "it3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Київський вітамінний завод", Address = "ул.Грушевького 17", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Мефар Ілач Сан. А.Ш", Address = "ул.Тукиас 77", Country = "Туреччина", Email = "tur3k@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Здоров'я", Address = "ул.Грушевького 87", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Д-р Редді'с", Address = "ул.Склон 12", Country = "Індія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Кусум Фарм", Address = "ул.Гончара 13", Country = "Україна", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Лабор.Нормон С.А.", Address = "ул.Іспан 13", Country = "Іспанія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "АстраЗенека АБ", Address = "ул.Швейц 11", Country = "Швеція", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Сан Фарм. Індастріз Лтд", Address = "ул.Інд 11", Country = "Індія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Астеллас Фарма Юроп", Address = "ул.Нидерс 1", Country = "Нідерланди", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Дарниця", Address = "ул.Нидерс 1", Country = "Україна", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Хіноїн", Address = "ул.Угорщ 21", Country = "Україна", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Берлін Хемі АГ", Address = "ул.Німечина 21", Country = "Німеччина", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Борщагівський ХФЗ", Address = "ул.Грушевького 37", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Лекхім - Харків", Address = "ул.Грушевького 17", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Юрія-Фарм", Address = "ул.Грушевького 11", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Др.Фальк Фарма ГмбХ", Address = "ул.Грос 27", Country = " Німеччина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Софарма АД", Address = "ул.Боунта 1", Country = "Болгария", Email = "bolg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "ІнтерХім", Address = "ул.Центральная 2", Country = "Україна", Email = "uklg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Істітуто де Анжелі С.р.л.", Address = "ул.Грос 1", Country = "Італія", Email = "uklg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Абботт Біолоджикалз Б.В.", Address = "ул.Нидерс 1", Country = "Нідерланди", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "АВС Фармачеутічі", Address = "ул.Грос 1", Country = "Італія", Email = "uklg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Орісіл-фарм", Address = "ул.Грушевького 12", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Креома-Фарм", Address = "ул.Грушевького 47", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "ФДС Лімітед", Address = "ул.Склон 12", Country = "Індія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Оріон Корпорейшн", Address = "ул.Склон 2", Country = "Фінляндія", Email = "findk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Лек Фарм.компания д.д.", Address = "ул.Боунта 1", Country = "Словенія", Email = "bolg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Індар", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Лек Фарм.компания д.д.", Address = "ул.Боунта 1", Country = "Словенія", Email = "bolg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Такеда Фарма", Address = "ул.Траст 87", Country = "Польща", Email = "polk@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "Біомед-Люблін", Address = "ул.Траст 87", Country = "Польща", Email = "polk@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "Глаксо Веллком С.А./Глаксо Оперейшнс ЮК", Address = "ул.Іспан 13", Country = "Іспанія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Др.Фальк Фарма ГмбХ", Address = "ул.Грос 27", Country = " Німеччина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Гедеон Ріхтер", Address = "ул.Грос 27", Country = "Угорщина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Лабор.Серв’є Індастрі,", Address = "ул.Грос 27", Country = "Франція", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Теммлер Італіа С.р.л", Address = "ул.Грос 1", Country = "Італія", Email = "uklg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Лубнифарм", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Сандоз ГмбХ-ТехОпс", Address = "ул.Гросдер 27", Country = " Австрія", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Гленмарк Фарм. Лтд", Address = "ул.Склон 2", Country = "Індія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Меркле ГмбХ,", Address = "ул.Грос 27", Country = " Німеччина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Грензах Прод. ГмбХ,", Address = "ул.Грос 7", Country = " Німеччина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Біофарма", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Фармекс Груп", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Софартекс", Address = "ул.Іспан 13", Country = "Франція", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Пфайзер Меньюфекчюрін Бельгія Н.В", Address = "ул.Грос 27", Country = "Бельгія", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Такеда Австрія ГмбХ", Address = "ул.Гросдер 27", Country = " Австрія", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Байєр Оу", Address = "ул.Гросдер 27", Country = "Фінляндія", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Сперко", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Іннотера Шузі", Address = "ул.Іспан 13", Country = "Франція", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "ТЕВА Чех Індастріз с.р.о", Address = "ул.Іспан 13", Country = "Чеська Республіка", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Дельфарм Лілль С.А.С", Address = "ул.Іспан 13", Country = "Франція", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Феррінг-Лечива а.с.", Address = "ул.Іспан 13", Country = "Чеська Республіка", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Гріндекс", Address = "ул.Іспан 13", Country = "Латвія", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Іпсен Фарма Біотек", Address = "ул.Іспан 13", Country = "Франція", Email = "indk@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Адамед Фарма С.А.", Address = "ул.Траст 87", Country = "Польща", Email = "polk@com.ua", Phone = "+7083454559"},
                new Manufacturer {Name = "КРКА", Address = "ул.Боунта 1", Country = "Словенія", Email = "bolg@com.ua", Phone = "+7083454557"},
                new Manufacturer {Name = "Ліндофарм ГмбХ,", Address = "ул.Грос 7", Country = " Німеччина", Email = "n3k@com.ua", Phone = "+7088454559"},
                new Manufacturer {Name = "Монфарм", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Уорлд Медици", Address = "ул.Тукиас 77", Country = "Туреччина", Email = "tur3k@com.ua", Phone = "+7083454559" },
                new Manufacturer {Name = "Фармекс Груп", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Галичфарм/Київмедпрепарат", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Астрафарм", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Специфар", Address = "ул.Грушевького 4", Country = "Греция", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Ф. Хоффманн-Ля Рош Лтд", Address = "ул.Грушевького 4", Country = "Швейцария", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "ИнтерХим", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Стома", Address = "ул.Грушевького 4", Country = "Україна", Email = "it3k@com.ua", Phone = "+7088454559" },
                new Manufacturer {Name = "Ромфарм Компані,", Address = "ул.Грушевького 4", Country = "Румунія", Email = "it3k@com.ua", Phone = "+7088454559" },

            };
            for (int i = 0; i < manufacturers.Length; i++)
            {
                Manufacturer manufacturer = manufacturers[i];
                manufacturers[i] = Manufacturers.Add(manufacturer).Entity;
            }
            SaveChanges();

            var rootCategoryGroups = new Category[]
            {
                new Category{ Description = "Засоби, що впливають на травну систему і метаболізм", Name = "Засоби, що впливають на травну систему і метаболізм.", ParentId = null },
                new Category{ Description = "Засоби, що впливають на систему крові і кровотворення.", Name = "Засоби, що впливають на систему крові і кровотворення.", ParentId = null },
                new Category{ Description = "Засоби, що впливають на серцево-судинну систему.", Name = "Засоби, що впливають на серцево-судинну систему.", ParentId = null },
                new Category{ Description = "Дерматологічні засоби.", Name = "Дерматологічні засоби.", ParentId = null },
                new Category{ Description = "Засоби, що впливають на сечостатеву систему й статевіі гормони.", Name = "Засоби, що впливають на сечостатеву систему й статевіі гормони.", ParentId = null },
                new Category{ Description = "Гормональні засоби для системного застосування (крім статевих гормонів і інсулінів).", Name = "Гормональні засоби для системного застосування.", ParentId = null },
                new Category{ Description = "Протимікробні, протигрибкові, противірусні, протипаразитарні засоби для системного застосування; інсектициди, репеленти.", Name = "Протимікробні, протигрибкові, противірусні, протипаразитарні засоби для системного застосування; інсектициди, репеленти.", ParentId = null },
                new Category{ Description = "Засоби, що впливають на опорно-руховий апарат.", Name = "Засоби, що впливають на опорно-руховий апарат.", ParentId = null },
                new Category{ Description = "Засоби, що діють на нервову систему.", Name = "Засоби, що діють на нервову систему.", ParentId = null },
                new Category{ Description = "Засоби, що діють на респіраторну систему.", Name = "Засоби, що діють на респіраторну систему.", ParentId = null },

            };
            for (int i = 0; i < rootCategoryGroups.Length; i++)
            {
                var rootCategoryGroup = rootCategoryGroups[i];
                rootCategoryGroups[i] = Categories.Add(rootCategoryGroup).Entity;
            }
            SaveChanges();

            var subCategoryGroups = new Category[]
            {
                new Category{ Description = "Засоби для застосування в стоматології.", Name = " Засоби для застосування в стоматології.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Засоби для лікування кислотозалежних захворювань шлунково-кишкового тракту.", Name = "Засоби для лікування кислотозалежних захворювань шлунково-кишкового тракту.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Засоби, що застосовуються при функціональних шлунково-кишкових розладах.", Name = "Засоби, що застосовуються при функціональних шлунково-кишкових розладах.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Протиблювотні засоби та засоби, що усувають нудоту.", Name = "Протиблювотні засоби та засоби, що усувають нудоту.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Засоби, що застосовуються при захворюваннях печінки і жовчовивідних шляхів.", Name = "Засоби, що застосовуються при захворюваннях печінки і жовчовивідних шляхів.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Проносні засоби.", Name = "Проносні засоби.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Протидіарейні засоби. Засоби, що застосовуються для лікування інфекційно-запальних захворювань кишківника.", Name = "Протидіарейні засоби.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Засоби замісної терапії, що поліпшують травлення.", Name = "Засоби замісної терапії, що поліпшують травлення.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Засоби, що застосовуються при цукровому діабеті.", Name = "Засоби, що застосовуються при цукровому діабеті.", ParentId = rootCategoryGroups[0].Id },
                new Category{ Description = "Вітаміни, полівітаміни, мікроелементи.", Name = "Вітаміни, полівітаміни, мікроелементи.", ParentId = rootCategoryGroups[0].Id },

                new Category{ Description = "Антитромботичні засоби.", Name = "Антитромботичні засоби.", ParentId = rootCategoryGroups[1].Id },
                new Category{ Description = "Антигеморагічні та гемостатичні засоби.", Name = "Антигеморагічні та гемостатичні засоби.", ParentId = rootCategoryGroups[1].Id },
                new Category{ Description = "Антианемічні засоби; засоби, що стимулюють кровотворення.", Name = "Антианемічні засоби; засоби, що стимулюють кровотворення.", ParentId = rootCategoryGroups[1].Id },
                new Category{ Description = "Кровозамінники та перфузійні розчини.", Name = "Кровозамінники та перфузійні розчини.", ParentId = rootCategoryGroups[1].Id },

                new Category{ Description = "Кардіологічні та ангіологічні засоби.", Name = "Кардіологічні та ангіологічні засоби.", ParentId = rootCategoryGroups[2].Id },
                new Category{ Description = "Гіпотензивні засоби.", Name = "Гіпотензивні засоби.", ParentId = rootCategoryGroups[2].Id },
                new Category{ Description = "Сечогінні засоби.", Name = "Сечогінні засоби.", ParentId = rootCategoryGroups[2].Id },
                new Category{ Description = "Бета-адреноблокатори.", Name = "Бета-адреноблокатори.", ParentId = rootCategoryGroups[2].Id },
                new Category{ Description = "Блокатори кальцієвих каналів.", Name = "Блокатори кальцієвих каналів.", ParentId = rootCategoryGroups[2].Id },
               
                new Category{ Description = "Протигрибкові засоби для застосування в дерматології.", Name = "Протигрибкові засоби для застосування в дерматології.", ParentId = rootCategoryGroups[3].Id },
                new Category{ Description = "Засоби для лікування ран і виразок.", Name = "Засоби для лікування ран і виразок.", ParentId = rootCategoryGroups[3].Id },
                new Category{ Description = "Протисвербіжні засоби.", Name = "Протисвербіжні засоби.", ParentId = rootCategoryGroups[3].Id },
                new Category{ Description = "Кортикостероїди для застосування в дерматології.", Name = "Кортикостероїди для застосування в дерматології.", ParentId = rootCategoryGroups[3].Id },
                new Category{ Description = "Антисептичні та дезінфікуючі засоби.", Name = "Антисептичні та дезінфікуючі засоби.", ParentId = rootCategoryGroups[3].Id },

                new Category{ Description = "Протимікробні та антисептичні засоби, що застосовуються в гінекології.", Name = "Протимікробні та антисептичні засоби, що застосовуються в гінекології.", ParentId = rootCategoryGroups[4].Id },
                new Category{ Description = "Інші гінекологічні засоби.", Name = "Інші гінекологічні засоби.", ParentId = rootCategoryGroups[4].Id },
                new Category{ Description = "Препарати статевих гормонів та препарати, що застосовуються при патології статевої сфери.", Name = "Препарати статевих гормонів та препарати, що застосовуються при патології статевої сфери.", ParentId = rootCategoryGroups[4].Id },
                new Category{ Description = "Засоби, що застосовуються в урології.", Name = "Засоби, що застосовуються в урології.", ParentId = rootCategoryGroups[4].Id },

                new Category{ Description = "Гормони гіпофіза, гіпоталамуса і їх аналоги.", Name = "Гормони гіпофіза, гіпоталамуса і їх аналоги.", ParentId = rootCategoryGroups[5].Id },
                new Category{ Description = "Препарати гормонів кори надниркових залоз.", Name = "Препарати гормонів кори надниркових залоз.", ParentId = rootCategoryGroups[5].Id },
                new Category{ Description = "Засоби, що застосовуються при захворюваннях щитовидної залози.", Name = "Засоби, що застосовуються при захворюваннях щитовидної залози.", ParentId = rootCategoryGroups[5].Id },
                new Category{ Description = "Препарати, що регулюють кальцієвий обмін у кістковій тканині.", Name = "Препарати, що регулюють кальцієвий обмін у кістковій тканині.", ParentId = rootCategoryGroups[5].Id },
                new Category{ Description = "Панкреатичні гормони.", Name = "Панкреатичні гормони", ParentId = rootCategoryGroups[5].Id },

                new Category{ Description = "Антибактеріальні засоби для системного застосування.", Name = "Антибактеріальні засоби для системного застосування.", ParentId = rootCategoryGroups[6].Id },
                new Category{ Description = "Протигрибкові засоби для системного застосування.", Name = "Протигрибкові засоби для системного застосування.", ParentId = rootCategoryGroups[6].Id },
                new Category{ Description = "Протитуберкульозні засоби.", Name = "Протитуберкульозні засоби.", ParentId = rootCategoryGroups[6].Id },
                new Category{ Description = "Противірусні засоби для системного застосування.", Name = "Противірусні засоби для системного застосування.", ParentId = rootCategoryGroups[6].Id },
                new Category{ Description = "Протигельмінтні засоби.", Name = "Протигельмінтні засоби.", ParentId = rootCategoryGroups[6].Id },
                new Category{ Description = "Засоби, що діють на ектопаразитів, включаючи засоби, що застосовуються при корості, і репеленти.", Name = "Засоби, що діють на ектопаразитів, включаючи засоби, що застосовуються при корості, і репеленти.", ParentId = rootCategoryGroups[6].Id },

                new Category{ Description = "Нестероїдні протизапальні та протиревматичні засоби.", Name = "Нестероїдні протизапальні та протиревматичні засоби.", ParentId = rootCategoryGroups[7].Id },
                new Category{ Description = "Специфічні протиревматичні засоби.", Name = "Специфічні протиревматичні засоби.", ParentId = rootCategoryGroups[7].Id },
                new Category{ Description = "Засоби, що застосовуються місцево при суглобовому та м'язовому болю.", Name = "Засоби, що застосовуються місцево при суглобовому та м'язовому болю.", ParentId = rootCategoryGroups[7].Id },
                new Category{ Description = "Міорелаксанти.", Name = "Міорелаксанти.", ParentId = rootCategoryGroups[7].Id },

                new Category{ Description = "Анестетики.", Name = "Анестетики.", ParentId = rootCategoryGroups[8].Id },
                new Category{ Description = "Анальгетики.", Name = "Анальгетики.", ParentId = rootCategoryGroups[8].Id },
                new Category{ Description = "Протиепілептичні засоби.", Name = "Протиепілептичні засоби.", ParentId = rootCategoryGroups[8].Id },
                new Category{ Description = "Протипаркінсонічні засоби.", Name = "Протипаркінсонічні засоби.", ParentId = rootCategoryGroups[8].Id },
                new Category{ Description = "Психолептичні засоби.", Name = "Психолептичні засоби.", ParentId = rootCategoryGroups[8].Id },
                new Category{ Description = "Психоаналептики.", Name = "Психоаналептики.", ParentId = rootCategoryGroups[8].Id },

                new Category{ Description = "Засоби, що застосовуються при захворюваннях порожнини носа.", Name = "Засоби, що застосовуються при захворюваннях порожнини носа.", ParentId = rootCategoryGroups[9].Id },
                new Category{ Description = "Засоби, що застосовуються при захворюваннях горла.", Name = "Засоби, що застосовуються при захворюваннях горла.", ParentId = rootCategoryGroups[9].Id },
                new Category{ Description = "Засоби, що застосовуються при обструктивних захворюваннях дихальних шляхів.", Name = "Засоби, що застосовуються при обструктивних захворюваннях дихальних шляхів.", ParentId = rootCategoryGroups[9].Id },
                new Category{ Description = "Засоби, що застосовуються при кашлі та застудних захворюваннях.", Name = "Засоби, що застосовуються при кашлі та застудних захворюваннях.", ParentId = rootCategoryGroups[9].Id },
            };

            for (int i = 0; i < subCategoryGroups.Length; i++)
            {
                var subCategoryGroup = subCategoryGroups[i];
                subCategoryGroups[i] = Categories.Add(subCategoryGroup).Entity;
            }
            SaveChanges();

            var subSubCategoryGroups = new Category[]
            {
                new Category{ Description = "Протимікробні та антисептичні препарати для місцевого застосування в стоматології.", Name = "Протимікробні та антисептичні препарати для місцевого застосування в стоматології.", ParentId = subCategoryGroups[0].Id },
                new Category{ Description = "Препарати для місцевої анестезії в стоматології.", Name = "Препарати для місцевої анестезії в стоматології.", ParentId = subCategoryGroups[0].Id },
                new Category{ Description = "Інші засоби для місцевого застосування в стоматології.", Name = "Інші засоби для місцевого застосування в стоматології.", ParentId = subCategoryGroups[0].Id },
                 
                new Category{ Description = "Антациди.", Name = "Антациди.", ParentId = subCategoryGroups[1].Id },
                new Category{ Description = "Препарати для лікування пептичної виразки та гастроезофагеальної рефлюксної хвороби.", Name = "Препарати для лікування пептичної виразки та гастроезофагеальної рефлюксної хвороби.", ParentId = subCategoryGroups[1].Id },
                new Category{ Description = "Препарати, що знижують тонус і моторику шлунково-кишкового тракту", Name = "Препарати, що знижують тонус і моторику шлунково-кишкового тракту.", ParentId = subCategoryGroups[2].Id },
                new Category{ Description = "Препарати, що регулюють моторну функцію шлунково-кишкового тракту (стимулятори перистальтики (пропульсанти).", Name = "Препарати, що регулюють моторну функцію шлунково-кишкового тракту (стимулятори перистальтики (пропульсанти).", ParentId = subCategoryGroups[2].Id },
                new Category{ Description = "Інші препарати, що застосовуються при функціональних кишкових розладах", Name = "Інші препарати, що застосовуються при функціональних кишкових розладах", ParentId = subCategoryGroups[2].Id },
                new Category{ Description = "Протиблювотні засоби та засоби, що усувають нудоту.", Name = "Протиблювотні засоби та засоби, що усувають нудоту.", ParentId = subCategoryGroups[3].Id },
                new Category{ Description = "Препарати, що застосовуються при біліарній патології.", Name = "Препарати, що застосовуються при біліарній патології.", ParentId = subCategoryGroups[4].Id },
                new Category{ Description = "Препарати, що сприяють розм'якшенню калових мас.", Name = "Препарати, що сприяють розм'якшенню калових мас.", ParentId = subCategoryGroups[5].Id },
                new Category{ Description = "Протидіарейні засоби. Засоби, що застосовуються для лікування інфекційно-запальних захворювань кишківника.", Name = "Протидіарейні засоби. Засоби, що застосовуються для лікування інфекційно-запальних захворювань кишківника.", ParentId = subCategoryGroups[6].Id },
                new Category{ Description = "Засоби замісної терапії, що поліпшують травлення.", Name = "Засоби замісної терапії, що поліпшують травлення.", ParentId = subCategoryGroups[7].Id },
                new Category{ Description = "Засоби, що застосовуються при цукровому діабеті.", Name = "Засоби, що застосовуються при цукровому діабеті.", ParentId = subCategoryGroups[8].Id },
                new Category{ Description = "Полівітамінні комплекси без добавок.", Name = "Полівітамінні комплекси без добавок.", ParentId = subCategoryGroups[9].Id },
                new Category{ Description = "Антикоагулянти прямої дії.", Name = "Антикоагулянти прямої дії.", ParentId = subCategoryGroups[10].Id },
                new Category{ Description = "Антикоагулянти непрямої дії.", Name = "Антикоагулянти непрямої дії.", ParentId = subCategoryGroups[10].Id },
                new Category{ Description = "Фібринолітики - активатори плазміногену.", Name = "Фібринолітики - активатори плазміногену.", ParentId = subCategoryGroups[10].Id },
                new Category{ Description = "Антиагреганти.", Name = "Антиагреганти.", ParentId = subCategoryGroups[10].Id },
                new Category{ Description = "Антигеморагічні та гемостатичні засоби.", Name = "Антигеморагічні та гемостатичні засоби.", ParentId = subCategoryGroups[11].Id },
                new Category{ Description = "Антианемічні засоби; засоби, що стимулюють кровотворення.", Name = "Антианемічні засоби; засоби, що стимулюють кровотворення.", ParentId = subCategoryGroups[12].Id },
                new Category{ Description = "Кровозамінники та перфузійні розчини.", Name = "Кровозамінники та перфузійні розчини.", ParentId = subCategoryGroups[13].Id },
                new Category{ Description = "Кардіотонічні препарати.", Name = "Кардіотонічні препарати.", ParentId = subCategoryGroups[14].Id },
                new Category{ Description = "Препарати, які пригнічують активність судинного центру.", Name = "Препарати, які пригнічують активність судинного центру.", ParentId = subCategoryGroups[15].Id },
                new Category{ Description = "Діуретики з помірно вираженою активністю.", Name = "Діуретики з помірно вираженою активністю.", ParentId = subCategoryGroups[16].Id },
                new Category{ Description = "Блокатори бета-адренорецепторів (монопрепарати).", Name="Блокатори бета-адренорецепторів (монопрепарати).", ParentId = subCategoryGroups[17].Id },
                new Category{ Description = "Блокатори бета-адренорецепторів в комбінації з діуретиками.", Name="Блокатори бета-адренорецепторів в комбінації з діуретиками.", ParentId = subCategoryGroups[17].Id },
                new Category{ Description = "Антагоністи кальцію з переважною дією на судини.",Name="Антагоністи кальцію з переважною дією на судини.", ParentId = subCategoryGroups[18].Id },
                new Category{ Description = "Антагоністи кальцію в комбінації з діуретиками.", Name="Антагоністи кальцію в комбінації з діуретиками.",ParentId = subCategoryGroups[18].Id },
                new Category{ Description = "Антагоністи кальцію з переважною дією на серце.", Name = "Антагоністи кальцію з переважною дією на серце.", ParentId = subCategoryGroups[18].Id },
                new Category{ Description = "Протигрибкові препарати для місцевого застосування.", Name = "Протигрибкові препарати для місцевого застосування.", ParentId = subCategoryGroups[19].Id },
                new Category{ Description = "Протигрибкові препарати для системного застосування в дерматології.", Name = "Протигрибкові препарати для місцевого застосування.", ParentId = subCategoryGroups[19].Id },
                new Category{ Description = "Препарати, що сприяють загоєнню (рубцюванню) ран.", Name = "Препарати, що сприяють загоєнню (рубцюванню) ран.", ParentId = subCategoryGroups[20].Id }, 
                new Category{ Description = "Протеолітичні ферменти.", Name = "Протигрибкові препарати для місцевого застосування.", ParentId = subCategoryGroups[20].Id },
                new Category{ Description = "Антигістамінні препарати для місцевого застосування.", Name = "Антигістамінні препарати для місцевого застосування.", ParentId = subCategoryGroups[21].Id },
                new Category{ Description = "Місцевоанестезуючі препарати.", Name = "Місцевоанестезуючі препарати.", ParentId = subCategoryGroups[21].Id },
                new Category{ Description = "Прості препарати кортикостероїдів.", Name = "Прості препарати кортикостероїдів.", ParentId = subCategoryGroups[22].Id },
                new Category{ Description = "Кортикостероїди в комбінації з антисептиками.", Name = "Кортикостероїди в комбінації з антисептиками.", ParentId = subCategoryGroups[22].Id },
                new Category{ Description = "Кортикостероїди в комбінації з антибіотиками.", Name = "Кортикостероїди в комбінації з антибіотиками.", ParentId = subCategoryGroups[22].Id },
                new Category{ Description = "Лікарські засоби з антисептичною і дезинфікуючою дією.", Name = "Лікарські засоби з антисептичною і дезинфікуючою дією.", ParentId = subCategoryGroups[23].Id },
                new Category{ Description = "Засоби дезінфекції для шкіри рук, тіла; поверхонь і ВМП.", Name = "Засоби дезінфекції для шкіри рук, тіла; поверхонь і ВМП.", ParentId = subCategoryGroups[23].Id },
               
                new Category{ Description = "Препарати з антибактеріальною дією для місцевого застосування.", Name = "Препарати з антибактеріальною дією для місцевого застосування.", ParentId = subCategoryGroups[24].Id },
                new Category{ Description = "Препарати з протипротозойною і антибактеріальною дією для місцевого застосування.", Name = "Препарати з протипротозойною і антибактеріальною дією для місцевого застосування.", ParentId = subCategoryGroups[24].Id },
                new Category{ Description = "Препарати з протигрибковою дією для місцевого застосування.", Name = "Препарати з протигрибковою дією для місцевого застосування.", ParentId = subCategoryGroups[24].Id },
                new Category{ Description = "Препарати з протигрибковою і антибактеріальною дією для місцевого застосування.", Name = "Препарати з протигрибковою і антибактеріальною дією для місцевого застосування.", ParentId = subCategoryGroups[24].Id },
                new Category{ Description = "Препарати з протипротозойною, протигрибковою і антибактеріальною дією для місцевого застосування.", Name = "Препарати з протипротозойною, протигрибковою і антибактеріальною дією для місцевого застосування.", ParentId = subCategoryGroups[24].Id },
                new Category{ Description = "Препарати, що підвищують тонус і скоротливу активність міометрію.", Name = "Препарати, що підвищують тонус і скоротливу активність міометрію.", ParentId = subCategoryGroups[25].Id },
                new Category{ Description = "Препарати, що знижують тонус і скоротливу активність міометрію.", Name = "Препарати, що знижують тонус і скоротливу активність міометрію.", ParentId = subCategoryGroups[25].Id },
                new Category{ Description = "Контрацептиви для місцевого застосування.", Name = "Контрацептиви для місцевого застосування.", ParentId = subCategoryGroups[25].Id },
                new Category{ Description = "Інгібітори секреції пролактину.", Name = "Інгібітори секреції пролактину.", ParentId = subCategoryGroups[25].Id },
                new Category{ Description = "Гормональні контрацептиви для системного застосування.", Name = "Гормональні контрацептиви для системного застосування.", ParentId = subCategoryGroups[26].Id },
                new Category{ Description = "Андрогенні препарати.", Name = "Андрогенні препарати.", ParentId = subCategoryGroups[26].Id },
                new Category{ Description = "Естрогенні препарати.", Name = "Естрогенні препарати.", ParentId = subCategoryGroups[26].Id },
                new Category{ Description = "Гестагенні препарати.", Name = "Гестагенні препарати.", ParentId = subCategoryGroups[26].Id },
                new Category{ Description = "Засоби, що застосовуються при сечокам'яній хворобі.", Name = "Засоби, що застосовуються при сечокам'яній хворобі.", ParentId = subCategoryGroups[27].Id },
                new Category{ Description = "Препарати, що знижують тонус гладкої мускулатури сечовивідних шляхів.", Name = "Препарати, що знижують тонус гладкої мускулатури сечовивідних шляхів..", ParentId = subCategoryGroups[27].Id },
                new Category{ Description = "Препарати рекомбінантного соматотропного гормону.", Name = "Препарати рекомбінантного соматотропного гормону.", ParentId = subCategoryGroups[28].Id },
                new Category{ Description = "Препарати вазопресину і його аналогів.", Name = "Препарати вазопресину і його аналогів.", ParentId = subCategoryGroups[28].Id },
                new Category{ Description = "Препарати окситоцину і його аналогів.", Name = "Препарати окситоцину і його аналогів.", ParentId = subCategoryGroups[28].Id },
                new Category{ Description = "Препарати аналогів соматостатину.", Name = "Препарати аналогів соматостатину.", ParentId = subCategoryGroups[28].Id },
                new Category{ Description = "Антигонадотропін рилізинг-гормони", Name = "Антигонадотропін рилізинг-гормони", ParentId = subCategoryGroups[28].Id },
                new Category{ Description = "Прості препарати кортикостероїдів для системного застосування.", Name = "Прості препарати кортикостероїдів для системного застосування.", ParentId = subCategoryGroups[29].Id },
                new Category{ Description = "Препарати гормонів щитовидної залози.", Name = "Препарати гормонів щитовидної залози.", ParentId = subCategoryGroups[30].Id },
                new Category{ Description = "Антитиреоїдні препарати.", Name = "Антитиреоїдні препарати.", ParentId = subCategoryGroups[30].Id },
                new Category{ Description = "Препарати йоду, що застосовуються при захворюваннях щитовидної залози.", Name = "Препарати йоду, що застосовуються при захворюваннях щитовидної залози.", ParentId = subCategoryGroups[30].Id },
                new Category{ Description = "Антипаратіреоїдні гормони", Name = "Антипаратіреоїдні гормони", ParentId = subCategoryGroups[31].Id },
                new Category{ Description = "Інші антипаратіреоїдні засоби", Name = "Інші антипаратіреоїдні засоби", ParentId = subCategoryGroups[31].Id },
                new Category{ Description = "Панкреатичні гормони", Name = "Панкреатичні гормони", ParentId = subCategoryGroups[32].Id },

                new Category{ Description = "Препарати тетрациклінів.", Name = "Препарати тетрациклінів.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Препарати амфеніколів.", Name = "Препарати амфеніколів.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Бета-лактамні антибіотики, пеніциліни.", Name = "Бета-лактамні антибіотики, пеніциліни.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Сульфаніламіди і триметоприм.", Name = "Сульфаніламіди і триметоприм.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Макроліди і лінкозаміди.", Name = "Макроліди і лінкозаміди.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Аміноглікозиди.", Name = "Аміноглікозиди.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Препарати групи хінолонів.", Name = "Препарати групи хінолонів.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Комбіновані антибактеріальні препарати.", Name = "Комбіновані антибактеріальні препарати.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Глікопептидні антибіотики.", Name = "Глікопептидні антибіотики.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Препарати групи нітрофурану.", Name = "Препарати групи нітрофурану.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Антибактеріальні препарати; Препарати для лікування протозойних інфекцій.", Name = "Антибактеріальні препарати; Препарати для лікування протозойних інфекцій.", ParentId = subCategoryGroups[33].Id },
                new Category{ Description = "Протигрибкові засоби для системного застосування.", Name = "Протигрибкові засоби для системного застосування.", ParentId = subCategoryGroups[34].Id },
                new Category{ Description = "Препараты аминосалициловой кислоты и ее производных", Name = "Препараты аминосалициловой кислоты и ее производных", ParentId = subCategoryGroups[35].Id },
                new Category{ Description = "Антибиотики.", Name = "Антибиотики.", ParentId = subCategoryGroups[35].Id },
                new Category{ Description = "Гидразиды.", Name = "Гидразиды.", ParentId = subCategoryGroups[35].Id },
                new Category{ Description = "Інші протитуберкульозні препарати.", Name = "Інші протитуберкульозні препарати.", ParentId = subCategoryGroups[35].Id },
                new Category{ Description = "Нуклеозіди и нуклеотіди, за виключенням інгібіторів зворотньої транскриптази.", Name = "Нуклеозіди и нуклеотіди, за виключенням інгібіторів зворотньої транскриптази.", ParentId = subCategoryGroups[36].Id },
                new Category{ Description = "Препарати групи циклічних амінов.", Name = "Препарати групи циклічних амінов.", ParentId = subCategoryGroups[36].Id },
                new Category{ Description = "Нуклеозидні и нуклеотидні інгібітори зворотньої транскриптази", Name = "Нуклеозидні и нуклеотидні інгібітори зворотньої транскриптази", ParentId = subCategoryGroups[36].Id },
                new Category{ Description = "Інгібітори нейроамінідази", Name = "Інгібітори нейроамінідази", ParentId = subCategoryGroups[36].Id },
                new Category{ Description = "Інші противірусні препарати.", Name = "Інші противірусні препарати.", ParentId = subCategoryGroups[36].Id },
                
                new Category{ Description = "Противірусні засоби для лікування ВІЧ-інфекції, комбінації", Name = "Противірусні засоби для лікування ВІЧ-інфекції, комбінації", ParentId = subCategoryGroups[36].Id },
                new Category{ Description = "Препарати, що застосовують при трематодозах", Name = "Препарати, що застосовують при трематодозах", ParentId = subCategoryGroups[37].Id },
                new Category{ Description = "Препарати, що застосовують при нематодозах.", Name = "Препарати, що застосовують при нематодозах.", ParentId = subCategoryGroups[37].Id },
                new Category{ Description = "Засоби, що діють на ектопаразитів, включаючи засоби, що застосовуються при корості, і репеленти.", Name = "Засоби, що діють на ектопаразитів, включаючи засоби, що застосовуються при корості, і репеленти.", ParentId = subCategoryGroups[38].Id },
                new Category{ Description = "Інші препарати, що застосовуються при ектопаразитозах, включаючи коросту.", Name = "Інші препарати, що застосовуються при ектопаразитозах, включаючи коросту.", ParentId = subCategoryGroups[38].Id },
                new Category{ Description = "Препарати-похідні оцтової кислоти і споріднені сполуки.", Name = "Препарати-похідні оцтової кислоти і споріднені сполуки.", ParentId = subCategoryGroups[39].Id },
                new Category{ Description = "Оксиками, НПЗЗ та протиревматичні.", Name = "Оксиками, НПЗЗ та протиревматичні.", ParentId = subCategoryGroups[39].Id },
                new Category{ Description = "Препарати-похідні пропіонової кислоти.", Name = "Препарати-похідні пропіонової кислоти.", ParentId = subCategoryGroups[39].Id },
                new Category{ Description = "Пеніциламін і аналоги.", Name = "Пеніциламін і аналоги.", ParentId = subCategoryGroups[40].Id },
                new Category{ Description = "Нестероїдні протизапальні препарати для місцевого застосування.", Name = "Нестероїдні протизапальні препарати для місцевого застосування.", ParentId = subCategoryGroups[41].Id },
                new Category{ Description = "Міорелаксанти з центральним механізмом дії.", Name = "Міорелаксанти з центральним механізмом дії.", ParentId = subCategoryGroups[42].Id },
                new Category{ Description = "Міорелаксанти з периферічним механізмом дії", Name = "Міорелаксанти з периферічним механізмом дії", ParentId = subCategoryGroups[42].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
                //new Category{ Description = "", Name = "", ParentId = subCategoryGroups[35].Id },
            };
            for (int i = 0; i < subSubCategoryGroups.Length; i++)
            {
                var subSubCategoryGroup = subSubCategoryGroups[i];
                subSubCategoryGroups[i] = Categories.Add(subSubCategoryGroup).Entity;
            }
            SaveChanges();

            var categories = new Category[]
            {
                new Category{ Name = "Гексетидин", Description = "CategoryDesc1", ParentId = subSubCategoryGroups[0].Id },
                new Category{ Name = "Хлоргексидин, комбінації", Description = "CategoryDesc2", ParentId = subSubCategoryGroups[0].Id },
                new Category{ Name = "Метронідазол, комбінації", Description = "CategoryDesc3", ParentId = subSubCategoryGroups[0].Id },
                new Category{ Name = "Клотримазол", Description = "CategoryDesc3", ParentId = subSubCategoryGroups[0].Id },
                new Category{ Name = "Інші протимікробні та антисептичні препарати для місцевого застосування в стоматології", Description = "CategoryDesc4", ParentId = subSubCategoryGroups[0].Id },
                new Category{ Name = "Артикаїн, комбінації", Description = "CategoryDesc5", ParentId = subSubCategoryGroups[1].Id },
                new Category{ Name = "Бензидамін", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[2].Id },
                new Category{ Name = "Комбіновані препарати та комплексні сполуки алюмінію, кальцію і магнію", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[3].Id },
                new Category{ Name = "Антагоністи H2-рецепторів", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[4].Id },
                new Category{ Name = "Інгібітори протонного насоса", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[4].Id },
                new Category{ Name = "Комбінації для ерадикації Helicobacter pylori.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[4].Id },
                new Category{ Name = "Засоби, які надають захисну дію на слизову оболонку шлунка і кишківника.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[4].Id },
                new Category{ Name = "Міотропні спазмолітики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[5].Id },
                new Category{ Name = "Антагоністи дофамінових рецепторів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[6].Id },
                new Category{ Name = "Препарати, що зменшують метеоризм (силікони)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[7].Id },
                new Category{ Name = "Антагоністи серотонінових рецепторів", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[8].Id },
                new Category{ Name = "Препарати жовчних кислот, які сприяють розчиненню холестеринових жовчних каменів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[9].Id },
                new Category{ Name = "Різні препарати з жовчогінною та спазмолітичною дією, включаючи комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[9].Id },
                new Category{ Name = "Гепатопротектори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[9].Id },
                new Category{ Name = "Препарати, що сприяють розм'якшенню калових мас.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[10].Id },
                new Category{ Name = "Препарати, що стимулюють перистальтику кишківника.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[10].Id },
                new Category{ Name = "Препарати з осмотичними властивостями.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[10].Id },
                new Category{ Name = "Протимікробні препарати, що застосовуються для лікування кишкових інфекцій.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[11].Id },
                new Category{ Name = "Ентеросорбенти.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[11].Id },
                new Category{ Name = "Сольові склади для пероральної регідратації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[11].Id },
                new Category{ Name = "Препарати, що зменшують моторику шлунково-кишкового тракту (антиперистальтичні).", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[11].Id },
                new Category{ Name = "Протидіарейні мікробні препарати, що регулюють рівновагу кишкової мікрофлори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[11].Id },
                new Category{ Name = "Препарати ферментів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[12].Id },
                new Category{ Name = "Інсуліни та аналоги для ін'єкцій, швидкої дії.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[13].Id },
                new Category{ Name = "Гіпоглікемізуючі препарати, за винятком інсулінів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[13].Id },
                new Category{ Name = "Полівітамінні комплекси без добавок.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[14].Id },
                new Category{ Name = "Полівітамінні комплекси з різними добавками (мінерали, амінокислоти, біо і фітокомпоненти та ін.)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[14].Id },
                new Category{ Name = "Мінеральні речовини.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[14].Id },
                new Category{ Name = "Група гепарину.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[15].Id },
                new Category{ Name = "Антагоністи вітаміну K.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[16].Id },
                new Category{ Name = "Ферменти.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[17].Id },
                new Category{ Name = "Клопідогрел (Clopidogrelum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[18].Id },
                new Category{ Name = "Кислота ацетилсаліцилова (Acidum аcetylsalicylicum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[16].Id },
                new Category{ Name = "Інгібітори фібринолізу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[19].Id },
                new Category{ Name = "Гемостатичні препарати.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[19].Id },
                new Category{ Name = "Препарати заліза, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[20].Id },
                new Category{ Name = "Препарати вітаміну B12 і фолієвої кислоти.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[20].Id },
                new Category{ Name = "Кровозамінники та білкові фракції плазми крові.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[21].Id },
                new Category{ Name = "Розчини електролітів, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[21].Id },
                new Category{ Name = "Серцеві глікозиди.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[22].Id },
                new Category{ Name = "Антиаритмічні препарати I і III класу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[22].Id },
                new Category{ Name = "Вазодилататори, що застосовуються в кардіології.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[22].Id },
                new Category{ Name = "Препарати, що стимулюють центральні альфа2-адренорецептори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[25].Id },
                new Category{ Name = "Селективні агоністи імідазолінових рецепторів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[25].Id },
                new Category{ Name = "Тіазидні діуретики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[24].Id },
                new Category{ Name = "Високоактивні діуретики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[24].Id },
                new Category{ Name = "Калійзберігаючі діуретики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[24].Id },
                new Category{ Name = "Селективні бета-блокатори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[23].Id },
                new Category{ Name = "Неселективні бета-блокатори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[25].Id },
                new Category{ Name = "Селективні блокатори бета-адренорецепторів в комбінації з діуретиками.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[26].Id },
                new Category{ Name = "Неселективні бета-блокатори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[26].Id },
                new Category{ Name = "Амлодипін (Amlodipinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[27].Id },
                new Category{ Name = "Ніфедипін (Nifedipinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[27].Id },
                new Category{ Name = "Амлодипін і діуретики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[28].Id },
                new Category{ Name = "Верапаміл (Verapamilum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[29].Id },

                new Category{ Name = "Протигрибкові антибіотики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[30].Id },
                new Category{ Name = "Препарати групи азолів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[30].Id }, 
                new Category{ Name = "Препарати групи аліламінів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[30].Id },
                new Category{ Name = "Препарати групи азолів, комбінації з протизапальними та іншими препаратами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[30].Id },
                new Category{ Name = "Препарати групи аліламінів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[31].Id },
                new Category{ Name = "Протигрибкові антибіотики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[31].Id },
                new Category{ Name = "Препарати що поліпшують трофіку і регенерацію тканин.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[32].Id },
                new Category{ Name = "Інші препарати, включаючи комбінації, що сприяють загоєнню (рубцюванню) ран.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[32].Id },
                new Category{ Name = "Протеолітичні ферменти.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[33].Id },
                new Category{ Name = "Диметинден (Dimetindenum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[34].Id },
                new Category{ Name = "Дифенгідрамін (Diphenhydraminum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[34].Id },
                new Category{ Name = "Лідокаїн (Lidocainum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[35].Id },
                new Category{ Name = "Кортикостероїди з низькою активністю (група I).", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[36].Id },
                new Category{ Name = "Помірно активні кортикостероїди (група II).", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[36].Id },
                new Category{ Name = "Активні кортикостероїди (група III).", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[36].Id },
                new Category{ Name = "Високоактивні кортикостероїди (група IV).", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[36].Id },
                new Category{ Name = "Помірно активні кортикостероїди в комбінації з антисептиками.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[37].Id },
                new Category{ Name = "Активні кортикостероїди в комбінації з антисептиками.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[37].Id },
                new Category{ Name = "Низькоактивні кортикостероїди в комбінації з антибіотиками.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[38].Id },
                new Category{ Name = "Помірно активні кортикостероїди в комбінації з антибіотиками", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[38].Id },
                new Category{ Name = "Активні кортикостероїди в комбінації з антибіотиками.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[38].Id },
                new Category{ Name = "Лікарські засоби з антисептичною і дезинфікуючою дією.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[39].Id },
                new Category{ Name = "Засоби дезінфекції для шкіри рук, тіла; поверхонь і ВМП.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[40].Id },

                new Category{ Name = "Антибіотики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[41].Id },
                new Category{ Name = "Похідні імідазолу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[42].Id },
                new Category{ Name = "Протигрибкові антибіотики.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[43].Id },
                new Category{ Name = "Похідні імідазолу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[43].Id },
                new Category{ Name = "Комбінації антибіотиків.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[44].Id },
                new Category{ Name = "Похідні імідазолу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[44].Id },
                new Category{ Name = "Похідні хіноліну.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[45].Id },
                new Category{ Name = "Похідні імідазолу, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[45].Id },
                new Category{ Name = "Похідні імідазолу в комбінації з антибіотиками і кортикостероїдами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[45].Id },
                new Category{ Name = "Препарати простагландинів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[46].Id },
                new Category{ Name = "Алкалоїди ріжків.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[46].Id },
                new Category{ Name = "Стимулятори бета 2 -адренорецепторів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[47].Id },
                new Category{ Name = "Внутрішньоматкові протизаплідні засоби з прогестагенами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[48].Id },
                new Category{ Name = "Вагінальні кільця з прогестагеном і естрогеном.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[48].Id },
                new Category{ Name = "Внутрішньоматкові контрацептиви.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[48].Id },
                new Category{ Name = "Бромокриптин (Bromocriptinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[49].Id },
                new Category{ Name = "Каберголін (Cabergolinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[49].Id },
                new Category{ Name = "Препарати, що містять естрогени і гестагени у фіксованих комбінаціях.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[50].Id },
                new Category{ Name = "Препарати, що містять гестагени та естрогени для послідовного застосування.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[50].Id },
                new Category{ Name = "Гестагенні препарати.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[50].Id },
                new Category{ Name = "Препарати для екстреної контрацепції.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[50].Id },
                new Category{ Name = "Тестостерон (Testosteronum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[51].Id },
                new Category{ Name = "Местеролон (Mesterolonum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[51].Id },
                new Category{ Name = "Прості препарати естрогенів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[52].Id },
                new Category{ Name = "Комбінації естрогенів з іншими препаратами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[52].Id },
                new Category{ Name = "Похідні прегнену.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[53].Id },
                new Category{ Name = "Похідні прегнадієну.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[53].Id },
                new Category{ Name = "Похідні естрену", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[53].Id },
                new Category{ Name = "Препарати, що сприяють розчиненню сечових конкрементів.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[54].Id },
                new Category{ Name = "Дієтичні добавки, що підтримують функцію сечовивідної системи.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[54].Id },
                new Category{ Name = "Спазмолітики з м-холіноблокуючою та міотропною дією.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[55].Id },
                new Category{ Name = "М-холіноблокатори.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[55].Id },

                new Category{ Name = "Соматропін (Somatropinum)", Description = "Соматропін (Somatropinum)", ParentId = subSubCategoryGroups[56].Id },
                new Category{ Name = "Десмопресин (Desmopressinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[57].Id },
                new Category{ Name = "Терліпресин (Terlipressinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[57].Id },
                new Category{ Name = "Демокситоцин (Demoxytocinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[58].Id },
                new Category{ Name = "Окситоцин (Oxytocinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[58].Id },
                new Category{ Name = "Карбетоцин (Carbetocinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[58].Id },
                new Category{ Name = "Октреотид (Octreotidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[59].Id },
                new Category{ Name = "Ланреотид (Lanreotidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[59].Id },
                new Category{ Name = "Мінералокортикоїди.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[61].Id },
                new Category{ Name = "Глюкокортикоїди.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[61].Id },
                new Category{ Name = "Левотироксин натрій (Levothyroxinum natricum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[62].Id },
                new Category{ Name = "Карбімазол (Carbimazolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[63].Id },
                new Category{ Name = "Тіамазол (Thiamazolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[63].Id },
                new Category{ Name = "Препарати йоду, що застосовуються при захворюваннях щитовидної залози.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[64].Id },
                new Category{ Name = "Препарати кальцитоніну", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[65].Id },
                new Category{ Name = "Інші антипаратіреоїдні засоби", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[66].Id },
                new Category{ Name = "Глікогенолітичні гормони", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[67].Id },
                
                new Category{ Name = "Доксициклін (Doxycyclinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[68].Id },
                new Category{ Name = "Тетрациклін (Tetracyclinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[68].Id },
                new Category{ Name = "Хлорамфенікол (Chloramphenicolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[69].Id },
                new Category{ Name = "Пеніциліни широкого спектру дії, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[70].Id },
                new Category{ Name = "Пеніциліни, чутливі до дії бета-лактамаз, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[70].Id },
                new Category{ Name = "Пеніциліни, стійкі до дії бета-лактамаз", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[70].Id },
                new Category{ Name = "Комбінації пеніцилінів, у тому числі з інгібіторами бета-лактамаз.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[70].Id },
                new Category{ Name = "Сульфаніламіди короткої дії.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[71].Id },
                new Category{ Name = "Сульфаніламіди тривалої дії.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[71].Id },
                new Category{ Name = "Макроліди.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[72].Id },
                new Category{ Name = "Лінкозаміди.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[72].Id },
                new Category{ Name = "Стрептоміцин (Streptomycinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[73].Id },
                new Category{ Name = "Тобраміцин (Tobramycinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[73].Id },
                new Category{ Name = "Гентаміцин (Gentamicinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[73].Id },
                new Category{ Name = "Канаміцин (Kanamycinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[73].Id },
                new Category{ Name = "Амікацин (Amikacinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[73].Id },
                new Category{ Name = "Фторхінолони.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[74].Id },
                new Category{ Name = "Фторхінолони в комбінації з іншими антибактеріальними засобами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[75].Id },
                new Category{ Name = "Тетрацикліни в комбінації з іншими протимікробними засобами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[75].Id },
                new Category{ Name = "Макроліди в комбінації з іншими протимікробними засобами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[75].Id },
                new Category{ Name = "Ванкоміцин (Vancomycinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[76].Id },
                new Category{ Name = "Тейкопланін (Teicoplaninum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[76].Id },
                new Category{ Name = "Нітрофурантоїн (Nitrofurantoinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[77].Id },
                new Category{ Name = "Фураздин (Furazidinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[77].Id },
                new Category{ Name = "Препарати групи нітроімідазолу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[78].Id },
                new Category{ Name = "Препарати групи імідазолу", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[79].Id },
                new Category{ Name = "Препарати групи триазолу.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[79].Id },
                new Category{ Name = "Протигрибкові антибіотики", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[79].Id },
                new Category{ Name = "Рифампіцин (Rifampicinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[82].Id },
                new Category{ Name = "Изоніазид (Isoniazidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[83].Id },
                new Category{ Name = "Пиразінамид (Pyrazinamidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[83].Id },
                new Category{ Name = "Ацикловір (Aciclovirum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[84].Id },
                new Category{ Name = "Рибавірин (Ribavirinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[84].Id },
                new Category{ Name = "Фамцикловір (Famciclovirum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[84].Id },
                new Category{ Name = "Валацикловір (Valaciclovirum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[84].Id },
                new Category{ Name = "Римантадін (Rimantadinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[85].Id },
                new Category{ Name = "Осельтамивір (Oseltamivirum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[87].Id },
                new Category{ Name = "Інозін пранобекс (Inosinum pranobex)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[88].Id },
                new Category{ Name = "Энісаміума йодид (ENISAMIUM IODIDE)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[88].Id },
                new Category{ Name = "Уміфеновір (UMIFENOVIRUM)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[88].Id },
                new Category{ Name = "Тилорон (TILORONUM)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[88].Id },
                new Category{ Name = "Фавипиравір (FAVIPIRAVIRUM)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[88].Id },

                new Category{ Name = "Мебендазол (Mebendazolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[91].Id },
                new Category{ Name = "Альбендазол (Albendazolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[91].Id },
                new Category{ Name = "Пірантел (Pyrantelum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[91].Id },
                new Category{ Name = "Левамізол (Levamisolum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[91].Id },
                new Category{ Name = "Препарати групи піретринів, комбінації.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[92].Id },
                new Category{ Name = "Бензилбензоат (Benzylii benzoas)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[93].Id },
                
                new Category{ Name = "Монопрепарати НПЗЗ і протиревматичні.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[94].Id },
                new Category{ Name = "Комбінації диклофенаку з іншими препаратами.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[94].Id },
                new Category{ Name = "Теноксикам (Tenoxicamum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[95].Id },
                new Category{ Name = "Лорноксикам (Lornoxicamum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[95].Id },
                new Category{ Name = "Мелоксикам (Meloxicamum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[95].Id },
                new Category{ Name = "Монопрепарати. НПЗЗ та протиревматичні.", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[96].Id },
                new Category{ Name = "Пеніциламін (Penicillaminum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[97].Id },
                new Category{ Name = "Кеторолак (Ketorolacum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[98].Id },
                new Category{ Name = "Кетопрофен (Ketoprofenum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[98].Id },
                new Category{ Name = "Диклофенак (Diclofenacum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[98].Id },
                new Category{ Name = "Німесулід (Nimesulidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[98].Id },
                new Category{ Name = "Баклофен (Baclofenum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[99].Id },
                new Category{ Name = "Тизанидин (Tizanidinum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[99].Id },
                new Category{ Name = "Толперизон (Tolperisonum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[99].Id },
                new Category{ Name = "Тиоколхикозид (Thiocolchicosidum)", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[99].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },
                //new Category{ Name = "", Description = "CategoryDesc6", ParentId = subSubCategoryGroups[81].Id },


            };
            for (int i = 0; i < categories.Length; i++)
            {
                var category = categories[i];
                categories[i] = Categories.Add(category).Entity;
            }
            SaveChanges();
            var execDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var medicines = new Medicine[]
            {
                new Medicine{ Name = "Гексосепт Tabula Vita, спрей для ротової порожнини 0,2% 25г балон (Табула Віта)", Description = "Desc5", Category = categories[0], Manufacturer = manufacturers[1], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},
                new Medicine{ Name = "Гексорал спрей для ротової порожнини 0,2% 40мл балон", Description = "Desc2", Category = categories[0], Manufacturer = manufacturers[3], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\3574661066110.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\3574661066110.jpg")))}"},
                new Medicine{ Name = "Стоматидин р-н д/рот. порожнини 0,1% 200мл фл.", Description = "Desc3", Category = categories[0], Manufacturer = manufacturers[1],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\3574661066110.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\3574661066110.jpg")))}"},
                new Medicine{ Name = "Стомолік р-н д/рот. порожнини 0,1% 125мл фл.", Description = "Desc4", Category = categories[0], Manufacturer = manufacturers[2],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\stomolik-r-r-d-rot-polosti-0-1-banka-125ml-prat-tehnolog-prod-400x400-6d0a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\stomolik-r-r-d-rot-polosti-0-1-banka-125ml-prat-tehnolog-prod-400x400-6d0a.jpg")))}"},

                new Medicine{ Name = "Септалор табл. для розсмоктування №20", Description = "Desc1", Category = categories[1], Manufacturer = manufacturers[0],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\46149-4971-small-300x300-b166.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\46149-4971-small-300x300-b166.jpg")))}"},

                new Medicine{ Name = "Дентагель гель д/ясен 20г", Description = "Desc6", Category = categories[2], Manufacturer = manufacturers[2],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0 (1).jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0 (1).jpg")))}"},
                new Medicine{ Name = "Метрогіл дента, гель д/ясен 20г", Description = "Desc7", Category = categories[2], Manufacturer = manufacturers[2],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0 (2).jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0 (2).jpg")))}"},

                new Medicine{ Name = "Кандід р-н д/рот. порожнини 1% 15мл фл.", Description = "Desc8", Category = categories[3], Manufacturer = manufacturers[1],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kandid-r-r-d-rot-polosti-1-fl-15ml-glenmark-farmasyutikalz-ltd-prod-400x400-e4eb.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kandid-r-r-d-rot-polosti-1-fl-15ml-glenmark-farmasyutikalz-ltd-prod-400x400-e4eb.jpg")))}"},

                new Medicine{ Name = "Пропосол аерозоль 50г балон (Стома)", Description = "Desc9", Category = categories[4], Manufacturer = manufacturers[3],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\34648-13982-big-1500x1500-5a57.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\34648-13982-big-1500x1500-5a57.jpg")))}"},
                new Medicine{ Name = "Целіста р-н для ротової порожнини 0,1мг/мл, 100мл фл.", Description = "Desc10", Category = categories[4], Manufacturer = manufacturers[3],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tselista-r-r-d-rotov-polosti-0-1mg-ml-fl-100ml-darnitsa-chao-farm-firma-prod-400x400-29e4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tselista-r-r-d-rotov-polosti-0-1mg-ml-fl-100ml-darnitsa-chao-farm-firma-prod-400x400-29e4.jpg")))}"},

                new Medicine{ Name = "Ультракаїн Д-С форте, р-н д/ін. 1,7мл картридж №100", Description = "Desc11", Category = categories[5], Manufacturer = manufacturers[2],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\add.ua-aventis-pharma-deutschland-(nimechchina)-ul-trakain-forte-ds-rozchin-dlja-in-ekcij-1,-7-ml-kartridzh-№100-30.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\add.ua-aventis-pharma-deutschland-(nimechchina)-ul-trakain-forte-ds-rozchin-dlja-in-ekcij-1,-7-ml-kartridzh-№100-30.jpg")))}"},
                new Medicine{ Name = "Ультракаїн Д-С форте, р-н д/ін. 2мл амп. №10", Description = "Desc12", Category = categories[5], Manufacturer = manufacturers[1],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\39221-4079-big-1500x1500-2aa9.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\39221-4079-big-1500x1500-2aa9.jpg")))}"},

                new Medicine{ Name = "Зіпелор спрей д/рот. порожнини 1,5мг/мл 30мл фл.", Description = "Desc13", Category = categories[6], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\zipelor-sprey-d-rotov-pol-1-5mg-ml-fl-30ml-pao-farmak-list-250x250-e3b6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\zipelor-sprey-d-rotov-pol-1-5mg-ml-fl-30ml-pao-farmak-list-250x250-e3b6.jpg")))}"},
                new Medicine{ Name = "Тантіверт табл. зі смаком апельсину 3мг №10", Description = "Desc14", Category = categories[6], Manufacturer = manufacturers[6],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tantivert-tabl-so-vkusom-apelsina-3mg-20-farmatsevticheskaya-firma-verteks-ooo-prod-400x400-0b4a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tantivert-tabl-so-vkusom-apelsina-3mg-20-farmatsevticheskaya-firma-verteks-ooo-prod-400x400-0b4a.jpg")))}"},
                new Medicine{ Name = "Фортеза р-н для ротової порожнини 0,15% 120мл фл.", Description = "Desc15", Category = categories[6], Manufacturer = manufacturers[7],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\forteza-r-r-d-rotov-pol-0-15-fl-120ml-abdi-ibrahim-ilach-sanai-ve-tidzharet-cart-120x120-26f5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\forteza-r-r-d-rotov-pol-0-15-fl-120ml-abdi-ibrahim-ilach-sanai-ve-tidzharet-cart-120x120-26f5.jpg")))}"},

                new Medicine{ Name = "Алмагель суспензія оральна 10мл пакет №20", Description = "Desc16", Category = categories[7], Manufacturer = manufacturers[8],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\almagel-a-susp-oraln-paket-10ml-20-balkanfarma-troyan-at-cart-120x120-6993.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\almagel-a-susp-oraln-paket-10ml-20-balkanfarma-troyan-at-cart-120x120-6993.jpg")))}"},
                new Medicine{ Name = "Гастро-Тева, табл. для смоктання №60", Description = "Desc17", Category = categories[7], Manufacturer = manufacturers[9],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\teva_gastro.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\teva_gastro.jpg")))}"},
                new Medicine{ Name = "Маалокс міні, суспензія оральна 4,3мл (6г) саше №20", Description = "Desc17", Category = categories[7], Manufacturer = manufacturers[10],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\maaloks-mini-susp-oraln-sashe-4-3ml-6g-limon-20-sanofi-s-p-a-cart-120x120-60d1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\maaloks-mini-susp-oraln-sashe-4-3ml-6g-limon-20-sanofi-s-p-a-cart-120x120-60d1.jpg")))}"},
                new Medicine{ Name = "РемМакс-КВ, табл. жувальні з малиновим смаком №18", Description = "Desc17", Category = categories[7], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},

                new Medicine{ Name = "Ранітидин табл. в/о 150мг №10 (Здоров'я)", Description = "Desc17", Category = categories[8], Manufacturer = manufacturers[13], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\140658_115_51_18_21.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\140658_115_51_18_21.jpg")))}"},

                new Medicine{ Name = "Улсепан табл. 40мг №28", Description = "Desc16", Category = categories[9], Manufacturer = manufacturers[4], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ulsepan-tabl-40mg-28-biofarma-ilach-san-ve-tidzh-a-sh-cart-120x120-9b9f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ulsepan-tabl-40mg-28-biofarma-ilach-san-ve-tidzh-a-sh-cart-120x120-9b9f.jpg")))}"},
                new Medicine{ Name = "Улсепан ліофілізат д/р-ну д/ін. 40мг фл. №1", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[4], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ulsepan-liofil-d-r-ra-d-in-40mg-fl-1-mefar-ilach-san-a-sh-cart-120x120-e83f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ulsepan-liofil-d-r-ra-d-in-40mg-fl-1-mefar-ilach-san-a-sh-cart-120x120-e83f.jpg")))}"},
                new Medicine{ Name = "Омез капс. 40мг №28", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[14], ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\omez-kaps-40mg-28-d-r-reddis-laboratoris-ltd-cart-120x120-9a92.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\omez-kaps-40mg-28-d-r-reddis-laboratoris-ltd-cart-120x120-9a92.jpg")))}"},
                new Medicine{ Name = "Золопент табл. в/о 20мг №14", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\zolopent-tabl-p-o-20mg-14-kusum-farm-ooo-cart-120x120-2e8a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\zolopent-tabl-p-o-20mg-14-kusum-farm-ooo-cart-120x120-2e8a.jpg")))}"},

                new Medicine{ Name = "Ранітидин табл. в/о 150мг №20 (Здоров'я)", Description = "Desc17", Category = categories[8], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\140658_115_51_18_21.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\140658_115_51_18_21.jpg")))}"},
                new Medicine{ Name = "Проксіум пор. д/р-ну д/ін. 40мг фл. №1 (без розчинника)", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[16],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\proksium-por-d-r-ra-d-in-fl-40mg-1-laboratorios-normon-s-a-cart-120x120-9aac.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\proksium-por-d-r-ra-d-in-fl-40mg-1-laboratorios-normon-s-a-cart-120x120-9aac.jpg")))}"},
                new Medicine{ Name = "Нексіум пор. д/р-ну д/ін/інф. 40мг фл. №10", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[17],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\neksium-por-d-r-ra-d-in-i-inf-40mg-fl-10-astra-zeneka-ab-cart-120x120-ebb0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\neksium-por-d-r-ra-d-in-i-inf-40mg-fl-10-astra-zeneka-ab-cart-120x120-ebb0.jpg")))}"},
                new Medicine{ Name = "Помпезо, ліофілізат д/р-ну д/ін. 40мг фл. №1", Description = "Desc17", Category = categories[9], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pompezo-liof-d-r-ra-d-in-40mg-fl-1-mefar-ilach-san-a-sh-prod-400x400-2537.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pompezo-liof-d-r-ra-d-in-40mg-fl-1-mefar-ilach-san-a-sh-prod-400x400-2537.jpg")))}"},
                new Medicine{ Name = "Нексіум табл. в/о 20мг №14", Description = "Desc17", Category = categories[10], Manufacturer = manufacturers[17],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\neksium-tabl-p-o-20mg-14-astra-zeneka-ab-cart-120x120-ff85.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\neksium-tabl-p-o-20mg-14-astra-zeneka-ab-cart-120x120-ff85.jpg")))}"},
                new Medicine{ Name = "Нексіум табл. в/о 40мг №14", Description = "Desc17", Category = categories[10], Manufacturer = manufacturers[17],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\neksium-tabl-p-o-40mg-14-astra-zeneka-ab-cart-120x120-1943.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\neksium-tabl-p-o-40mg-14-astra-zeneka-ab-cart-120x120-1943.jpg")))}"},

                new Medicine{ Name = "Віс-нол капс. 120мг №100", Description = "Desc17", Category = categories[11], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\vis-nol-kaps-120mg-100-pao-farmak-cart-120x120-3d16.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\vis-nol-kaps-120mg-100-pao-farmak-cart-120x120-3d16.jpg")))}"},
                new Medicine{ Name = "Де-нол табл. в/о 120мг №112", Description = "Desc17", Category = categories[11], Manufacturer = manufacturers[19],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\de-nol-tabl-p-o-120mg-112-astellas-farma-yurop-b-v-cart-120x120-20e2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\de-nol-tabl-p-o-120mg-112-astellas-farma-yurop-b-v-cart-120x120-20e2.jpg")))}"},

                new Medicine{ Name = "Дротаверин-Дарниця, р-н д/ін. 20мг/мл 2мл амп. №5", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\main.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\main.jpg")))}"},
                new Medicine{ Name = "Дротаверин-Дарниця, табл. 40мг №30", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\193845_88_35_17_13.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\193845_88_35_17_13.jpg")))}"},
                new Medicine{ Name = "Но-шпа р-н д/ін. 20мг/мл 2мл амп. №25", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[21],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\no-shpa-r-r-d-in-20mg-ml-amp-2ml-25-hinoin-prayvit-ko-ltd-cart-120x120-0a76.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\no-shpa-r-r-d-in-20mg-ml-amp-2ml-25-hinoin-prayvit-ko-ltd-cart-120x120-0a76.jpg")))}"},
                new Medicine{ Name = "Но-шпа комфорт, табл. в/о 40мг №24", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[21],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\no-shpa-tabl-40mg-24-hinoin-prayvit-ko-ltd-cart-120x120-059e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\no-shpa-tabl-40mg-24-hinoin-prayvit-ko-ltd-cart-120x120-059e.jpg")))}"},
                new Medicine{ Name = "Бесалол табл. №6", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\besalol-tabl-6-tov-farm-kompaniya-zdorove-cart-120x120-98d7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\besalol-tabl-6-tov-farm-kompaniya-zdorove-cart-120x120-98d7.jpg")))}"},
                new Medicine{ Name = "Новіган табл. в/о №10", Description = "Desc17", Category = categories[12], Manufacturer = manufacturers[14],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\novigan-tabl-p-o-10-d-r-reddis-laboratoris-ltd-cart-120x120-db7f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\novigan-tabl-p-o-10-d-r-reddis-laboratoris-ltd-cart-120x120-db7f.jpg")))}"},

                new Medicine{ Name = "Метоклопрамід-Дарниця, табл. 10мг №50", Description = "Desc17", Category = categories[13], Manufacturer = manufacturers[19],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\metoklopramid-darnitsa-tabl-10mg-50-darnitsa-chao-farm-firma-cart-120x120-57d9.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\metoklopramid-darnitsa-tabl-10mg-50-darnitsa-chao-farm-firma-cart-120x120-57d9.jpg")))}"},
                new Medicine{ Name = "Домрид SR, табл. 30мг №30", Description = "Desc17", Category = categories[13], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\domrid-sr-tabl-30mg-30-kusum-farm-ooo-cart-120x120-0686.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\domrid-sr-tabl-30mg-30-kusum-farm-ooo-cart-120x120-0686.jpg")))}"},
                new Medicine{ Name = "Допрокін 10мг табл. №20", Description = "Desc17", Category = categories[13], Manufacturer = manufacturers[4],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\doprokin-tabl-10mg-20-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-0589.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\doprokin-tabl-10mg-20-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-0589.jpg")))}"},

                new Medicine{ Name = "Колікід табл. в/о 125мг №30", Description = "Desc17", Category = categories[14], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kolikid-tabl-p-o-125mg-30-kusum-farm-ooo-cart-120x120-6915.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kolikid-tabl-p-o-125mg-30-kusum-farm-ooo-cart-120x120-6915.jpg")))}"},
                new Medicine{ Name = "Еспумізан Бебі, краплі оральні, емульсія 100мг/мл 30мл фл.", Description = "Desc17", Category = categories[14], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\espumizan-bebi-kap-oral-emuls-100mg-ml-fl-30ml-berlin-hemi-ag-cart-120x120-157b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\espumizan-bebi-kap-oral-emuls-100mg-ml-fl-30ml-berlin-hemi-ag-cart-120x120-157b.jpg")))}"},
                new Medicine{ Name = "Еспумізан капс. 40мг №50", Description = "Desc17", Category = categories[14], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\espumizan-kaps-40mg-50-berlin-hemi-ag-cart-120x120-747b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\espumizan-kaps-40mg-50-berlin-hemi-ag-cart-120x120-747b.jpg")))}"},

                new Medicine{ Name = "Ондансетрон р-н д/ін. 2мг/мл 2мл амп. №5 (БХФЗ)", Description = "Desc17", Category = categories[15], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ddfc8c2cc896fce58dab1372ff049690.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ddfc8c2cc896fce58dab1372ff049690.jpg")))}"},
                new Medicine{ Name = "Ондансетрон р-н д/ін. 2мг/мл 2мл амп. №5 (Лекхім)", Description = "Desc17", Category = categories[15], Manufacturer = manufacturers[24],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ondansetron-r-r-d-in-2mg-ml-amp-2ml-5-at-lekhim-harkov-cart-120x120-b800.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ondansetron-r-r-d-in-2mg-ml-amp-2ml-5-at-lekhim-harkov-cart-120x120-b800.jpg")))}"},
                new Medicine{ Name = "Осетрон р-н д/ін. 2мг/мл 2мл амп. №5", Description = "Desc17", Category = categories[15], Manufacturer = manufacturers[14],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\osetron-r-r-d-in-2mg-ml-amp-2ml-4mg-5-d-r-reddis-laboratoris-ltd-cart-120x120-ea4b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\osetron-r-r-d-in-2mg-ml-amp-2ml-4mg-5-d-r-reddis-laboratoris-ltd-cart-120x120-ea4b.jpg")))}"},
                new Medicine{ Name = "Юнорм р-н д/ін. 2мг/мл 2мл амп. №5", Description = "Desc17", Category = categories[15], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\yunorm-r-r-d-in-2mg-ml-aml-2ml-5-yuriya-farm-ooo-cart-120x120-877b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\yunorm-r-r-d-in-2mg-ml-aml-2ml-5-yuriya-farm-ooo-cart-120x120-877b.jpg")))}"},

                new Medicine{ Name = "Укрлів табл. 500мг №100", Description = "Desc17", Category = categories[16], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ukrliv-tabl-500mg-100-kusum-farm-ooo-cart-120x120-3980.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ukrliv-tabl-500mg-100-kusum-farm-ooo-cart-120x120-3980.jpg")))}"},
                new Medicine{ Name = "Укрлів, суспензія оральна 250мг/5мл 200мл фл.", Description = "Desc17", Category = categories[16], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kusum_ukrliv_susp.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kusum_ukrliv_susp.jpg")))}"},
                new Medicine{ Name = "Холудексан капс. 300мг №20", Description = "Desc17", Category = categories[16], Manufacturer = manufacturers[7],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\fb4dd69ac4bce1d58a9776f112e6875b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\fb4dd69ac4bce1d58a9776f112e6875b.jpg")))}"},
                new Medicine{ Name = "Урсофальк капс. 250мг №10", Description = "Desc17", Category = categories[16], Manufacturer = manufacturers[26],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ursofalk-kaps-250mg-10-dr-falk-farma-gmbh-cart-120x120-9fd3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ursofalk-kaps-250mg-10-dr-falk-farma-gmbh-cart-120x120-9fd3.jpg")))}"},

                new Medicine{ Name = "Алохол табл. в/о №50", Description = "Desc17", Category = categories[17], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\allohol-tabl-p-o-50-pao-nvts-borschagovskiy-hfz-cart-120x120-9bad.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\allohol-tabl-p-o-50-pao-nvts-borschagovskiy-hfz-cart-120x120-9bad.jpg")))}"},
                new Medicine{ Name = "Артихол табл. в/о 200мг №30", Description = "Desc17", Category = categories[17], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\artihol-tabl-p-o-200mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-0970.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\artihol-tabl-p-o-200mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-0970.jpg")))}"},
                new Medicine{ Name = "Артишока екстракт-Здоров'я, капс. 100мг №60", Description = "Desc17", Category = categories[17], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\artishoka-ekstrakt-zdorove-kaps-100mg-60-tov-farm-kompaniya-zdorove-cart-120x120-922f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\artishoka-ekstrakt-zdorove-kaps-100mg-60-tov-farm-kompaniya-zdorove-cart-120x120-922f.jpg")))}"},

                new Medicine{ Name = "Дарсіл табл. в/о №100", Description = "Desc17", Category = categories[18], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\darsil-tabl-p-o-22-5mg-100-darnitsa-chao-farm-firma-cart-120x120-406b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\darsil-tabl-p-o-22-5mg-100-darnitsa-chao-farm-firma-cart-120x120-406b.jpg")))}"},
                new Medicine{ Name = "Силібор Форте, капс. 70мг №20", Description = "Desc17", Category = categories[18], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\silibor-forte-kaps-70mg-20-tov-farm-kompaniya-zdorove-cart-120x120-9c93.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\silibor-forte-kaps-70mg-20-tov-farm-kompaniya-zdorove-cart-120x120-9c93.jpg")))}"},
                new Medicine{ Name = "Карсил Форте, капс. 90мг №30", Description = "Desc17", Category = categories[18], Manufacturer = manufacturers[27],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\karsil-forte-kaps-tverd-90mg-30-at-sofarma-cart-120x120-b91e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\karsil-forte-kaps-tverd-90mg-30-at-sofarma-cart-120x120-b91e.jpg")))}"},

                new Medicine{ Name = "Докулак ІС табл. в/о 100мг №10", Description = "Desc17", Category = categories[19], Manufacturer = manufacturers[28],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dokulak-is-tabl-p-o-100mg-10-interhim-odo-cart-120x120-3577.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dokulak-is-tabl-p-o-100mg-10-interhim-odo-cart-120x120-3577.jpg")))}"},

                new Medicine{ Name = "Бисакодил супозиторії ректальні 10мг, №10 (Лекхім)", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[24],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bisakodil-supp-rektal-10mg-10-at-lekhim-harkov-cart-120x120-ca80.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bisakodil-supp-rektal-10mg-10-at-lekhim-harkov-cart-120x120-ca80.jpg")))}"},
                new Medicine{ Name = "Бісакодил-Дарниця, табл. в/о 5мг №30", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bisakodil-darnitsa-tabl-p-o-5mg-30-darnitsa-chao-farm-firma-cart-120x120-deb3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bisakodil-darnitsa-tabl-p-o-5mg-30-darnitsa-chao-farm-firma-cart-120x120-deb3.jpg")))}"},
                new Medicine{ Name = "Піколакс краплі оральні 0,75% 15мл фл.", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pikolaks-kap-oral-0-75-fl-15ml-pao-farmak-cart-120x120-85bf.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pikolaks-kap-oral-0-75-fl-15ml-pao-farmak-cart-120x120-85bf.jpg")))}"},
                new Medicine{ Name = "Піколакс табл. 5мг №10", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pikolaks-tabl-5mg-10-pao-farmak-cart-120x120-259b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pikolaks-tabl-5mg-10-pao-farmak-cart-120x120-259b.jpg")))}"},
                new Medicine{ Name = "Гутталакс краплі оральні 7,5мг/мл 30мл фл.", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[29],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-30ml-instituto-de-anzheli-s-r-l-cart-120x120-0b0a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-30ml-instituto-de-anzheli-s-r-l-cart-120x120-0b0a.jpg")))}"},
                new Medicine{ Name = "Гутталакс краплі оральні 7,5мг/мл 15мл фл.", Description = "Desc17", Category = categories[20], Manufacturer = manufacturers[29],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-15ml-instituto-de-anzheli-s-r-l-cart-120x120-fe2c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-15ml-instituto-de-anzheli-s-r-l-cart-120x120-fe2c.jpg")))}"},

                new Medicine{ Name = "Дуфалак сироп 667мг/1мл 15мл пак. №10", Description = "Desc17", Category = categories[21], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dufalak-sirop-pak-15ml-10-abbott-biolodzhikalz-b-v-cart-120x120-da75.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dufalak-sirop-pak-15ml-10-abbott-biolodzhikalz-b-v-cart-120x120-da75.jpg")))}"},
                new Medicine{ Name = "Медулак сироп 667мг/мл 180мл фл.", Description = "Desc17", Category = categories[21], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\medulak-sirop-667-0mg-ml-fl-180ml-avs-farmacheutichi-s-p-a-cart-120x120-78e3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\medulak-sirop-667-0mg-ml-fl-180ml-avs-farmacheutichi-s-p-a-cart-120x120-78e3.jpg")))}"},
                new Medicine{ Name = "Піколакс табл. 5мг №10", Description = "Desc17", Category = categories[21], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-30ml-instituto-de-anzheli-s-r-l-cart-120x120-0b0a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\guttalaks-pikosulfat-kap-fl-30ml-instituto-de-anzheli-s-r-l-cart-120x120-0b0a.jpg")))}"},

                new Medicine{ Name = "Ністатин табл. в/о 500000ОД №20 (БХФЗ)", Description = "Desc17", Category = categories[22], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nistatin-tabl-p-o-500-000-ed-20-pao-nvts-borschagovskiy-hfz-cart-120x120-6cfe.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nistatin-tabl-p-o-500-000-ed-20-pao-nvts-borschagovskiy-hfz-cart-120x120-6cfe.jpg")))}"},
                new Medicine{ Name = "Фталазол-Дарниця, табл. 500мг №10", Description = "Desc17", Category = categories[22], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\21771_95_40_5_7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\21771_95_40_5_7.jpg")))}"},
                new Medicine{ Name = "Ніфурозид-Здоров'я, капс. 200мг №20", Description = "Desc17", Category = categories[22], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\187749_85_81_20_18.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\187749_85_81_20_18.jpg")))}"},

                new Medicine{ Name = "Атоксил порошок для оральної суспезії 2г пак. №20", Description = "Desc17", Category = categories[23], Manufacturer = manufacturers[32],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\atoksil-por-d-p-susp-2g-pak-20-orisil-farm-ooo-cart-120x120-123c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\atoksil-por-d-p-susp-2g-pak-20-orisil-farm-ooo-cart-120x120-123c.jpg")))}"},
                new Medicine{ Name = "Вугілля активоване, табл. 250мг №10 (БХФЗ)", Description = "Desc17", Category = categories[23], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ugol-aktivirovannyiy-tabl-250mg-10-solution-pharm-tov-vtf-farmakom-cart-120x120-b3d1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ugol-aktivirovannyiy-tabl-250mg-10-solution-pharm-tov-vtf-farmakom-cart-120x120-b3d1.jpg")))}"},
                new Medicine{ Name = "Ентеросгель ЕкстраКапс, капс. 0,32г №14", Description = "Desc17", Category = categories[23], Manufacturer = manufacturers[33],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\enterosgel-ekstrakaps-kaps-0-32g-14-kreoma-farm-eof-chao-cart-120x120-79cd.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\enterosgel-ekstrakaps-kaps-0-32g-14-kreoma-farm-eof-chao-cart-120x120-79cd.jpg")))}"},

                new Medicine{ Name = "Іоніка порошок для орального розчину 4,4г пак. №20", Description = "Desc17", Category = categories[24], Manufacturer = manufacturers[34],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ionika-por-d-oral-r-ra-4-4g-paket-20-fds-limited-cart-120x120-41ea.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ionika-por-d-oral-r-ra-4-4g-paket-20-fds-limited-cart-120x120-41ea.jpg")))}"},
                new Medicine{ Name = "Регідрон Оптім, порошок для орального р-ну по 10,7г пак. №20", Description = "Desc17", Category = categories[24], Manufacturer = manufacturers[35],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\regidron-optim-por-d-oraln-r-ra-10-7g-paket-20-orion-korporeyshn-cart-120x120-b47c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\regidron-optim-por-d-oraln-r-ra-10-7g-paket-20-orion-korporeyshn-cart-120x120-b47c.jpg")))}"},
                new Medicine{ Name = "Регідрон порошок по 18,9г пак. №20", Description = "Desc17", Category = categories[24], Manufacturer = manufacturers[35],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\regidron-por-dozir-18-9g-paket-20-orion-korporeyshn-cart-120x120-9ced.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\regidron-por-dozir-18-9g-paket-20-orion-korporeyshn-cart-120x120-9ced.jpg")))}"},

                new Medicine{ Name = "Лопераміду гідрохлорид , табл. 2мг №20", Description = "Desc17", Category = categories[25], Manufacturer = manufacturers[24],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\99348_86_57_20_16.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\99348_86_57_20_16.jpg")))}"},

                new Medicine{ Name = "Ентерожерміна Форте, суспензія оральна 5мл фл. №10", Description = "Desc17", Category = categories[26], Manufacturer = manufacturers[10],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\enterozhermina-forte-susp-oral-fl-5ml-10-sanofi-s-p-a-cart-120x120-de75.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\enterozhermina-forte-susp-oral-fl-5ml-10-sanofi-s-p-a-cart-120x120-de75.jpg")))}"},
                new Medicine{ Name = "Лінекс форте капс. №14 + Лінекс форте капс. №7", Description = "Desc17", Category = categories[26], Manufacturer = manufacturers[36],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lineks-forte-kaps-14-kaps-7-lek-farmatsevticheskaya-kompaniya-d-d-cart-120x120-4792.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lineks-forte-kaps-14-kaps-7-lek-farmatsevticheskaya-kompaniya-d-d-cart-120x120-4792.jpg")))}"},

                new Medicine{ Name = "Креон 10000, капс. 150мг №50", Description = "Desc17", Category = categories[27], Manufacturer = manufacturers[30],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kreon-10000-kaps-tv-150mg-20-abbott-laboratoriz-gmbh-cart-120x120-a795.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kreon-10000-kaps-tv-150mg-20-abbott-laboratoriz-gmbh-cart-120x120-a795.jpg")))}"},
                new Medicine{ Name = "Мезим капсули 25000, капс. №20", Description = "Desc17", Category = categories[27], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\mezim-forte-20000-tabl-20-berlin-hemi-ag-cart-120x120-4972.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\mezim-forte-20000-tabl-20-berlin-hemi-ag-cart-120x120-4972.jpg")))}"},

                new Medicine{ Name = "Фармасулін Н, р-н д/ін. 100 МО/мл 10мл фл.", Description = "Desc17", Category = categories[28], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\farmasulin-h-np-susp-d-in-100me-ml-fl-10ml-1-pao-farmak-cart-120x120-28d4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\farmasulin-h-np-susp-d-in-100me-ml-fl-10ml-1-pao-farmak-cart-120x120-28d4.jpg")))}"},
                new Medicine{ Name = "Хумодар Р 100Р, р-н д/ін. 100 МО/мл 10мл фл. №1", Description = "Desc17", Category = categories[28], Manufacturer = manufacturers[37],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\humodar-b100r-susp-d-in-100me-ml-fl-10ml-1-indar-prat-cart-120x120-dff4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\humodar-b100r-susp-d-in-100me-ml-fl-10ml-1-indar-prat-cart-120x120-dff4.jpg")))}"},

                new Medicine{ Name = "Діаформін SR, табл. 1000мг №60", Description = "Desc17", Category = categories[29], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\diaformin-tabl-p-plen-obol-1000mg-60-pao-farmak-cart-120x120-2bd1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\diaformin-tabl-p-plen-obol-1000mg-60-pao-farmak-cart-120x120-2bd1.jpg")))}"},
                new Medicine{ Name = "Метамін SR табл. пролонг. 500мг №30", Description = "Desc17", Category = categories[29], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\metamin-tabl-p-o-500mg-100-kusum-farm-ooo-cart-120x120-a9a8.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\metamin-tabl-p-o-500mg-100-kusum-farm-ooo-cart-120x120-a9a8.jpg")))}"},

                new Medicine{ Name = "Ревіт драже №80 (КВЗ)", Description = "Desc17", Category = categories[30], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\revit-dr-konteyner-80-pao-vitaminyi-cart-120x120-0a55.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\revit-dr-konteyner-80-pao-vitaminyi-cart-120x120-0a55.jpg")))}"},
                new Medicine{ Name = "Гексавіт драже №50 (КВЗ)", Description = "Desc17", Category = categories[30], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\geksavit-dr-konteyner-50-pao-vitaminyi-cart-120x120-c159.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\geksavit-dr-konteyner-50-pao-vitaminyi-cart-120x120-c159.jpg")))}"},
                new Medicine{ Name = "Піковіт форте, табл. в/о №30", Description = "Desc17", Category = categories[30], Manufacturer = manufacturers[38],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pikovit-tabl-p-o-30-krka-d-d-cart-120x120-bd11.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pikovit-tabl-p-o-30-krka-d-d-cart-120x120-bd11.jpg")))}"},

                new Medicine{ Name = "Квадевіт таблетки в/о №60", Description = "Desc17", Category = categories[31], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kvadevit-tabl-p-o-60-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-a1cf.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kvadevit-tabl-p-o-60-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-a1cf.jpg")))}"},
                new Medicine{ Name = "Оптикс табл. в/о №30", Description = "Desc17", Category = categories[31], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\optiks-tabl-p-o-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-5506.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\optiks-tabl-p-o-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-5506.jpg")))}"},
                new Medicine{ Name = "Дуовіт табл. в/о №40", Description = "Desc17", Category = categories[31], Manufacturer = manufacturers[38],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\duovit-tabl-p-o-kombi-upakovka-40-krka-d-d-cart-120x120-0ade.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\duovit-tabl-p-o-kombi-upakovka-40-krka-d-d-cart-120x120-0ade.jpg")))}"},

                new Medicine{ Name = "Кальцію глюконат стабілізований, р-н д/ін. 100мг/мл 10мл амп. №10", Description = "Desc17", Category = categories[32], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kaltsiya-hlorid-darnitsa-r-r-d-in-100mg-ml-amp-10ml-10-darnitsa-chao-farm-firma-cart-120x120-f4a0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kaltsiya-hlorid-darnitsa-r-r-d-in-100mg-ml-amp-10ml-10-darnitsa-chao-farm-firma-cart-120x120-f4a0.jpg")))}"},
                new Medicine{ Name = "Аспаркам-Здоров'я, табл. №50", Description = "Desc17", Category = categories[32], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\4854_140_93_8_30.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\4854_140_93_8_30.jpg")))}"},

                new Medicine{ Name = "Гепарин-Індар, р-н д/ін. 5000 МО/мл 5мл фл. №5", Description = "Desc17", Category = categories[33], Manufacturer = manufacturers[37],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\201898_67_87_27_18.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\201898_67_87_27_18.jpg")))}"},
                new Medicine{ Name = "Клексан р-н д/ін. (10000 анти-Ха МО/мл) по 2000 анти-Ха МО/0,2мл шприц-доза №10", Description = "Desc17", Category = categories[33], Manufacturer = manufacturers[10],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kleksan-r-r-d-in-2000-anti-ha-me-0-2ml-shpr-doza-10-sanofi-vintrop-indastria-cart-120x120-fdba.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kleksan-r-r-d-in-2000-anti-ha-me-0-2ml-shpr-doza-10-sanofi-vintrop-indastria-cart-120x120-fdba.jpg")))}"},
                new Medicine{ Name = "Фленокс р-н д/ін. 2000 анти-Ха МО/0,2мл шприц №10", Description = "Desc17", Category = categories[3], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\flenoks-r-r-d-in-2000-anti-ha-me-0-2ml-shprits-0-2ml-10-pao-farmak-cart-120x120-abc2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\flenoks-r-r-d-in-2000-anti-ha-me-0-2ml-shprits-0-2ml-10-pao-farmak-cart-120x120-abc2.jpg")))}"},

                new Medicine{ Name = "Варфарин Нікомед табл. 2,5мг №100", Description = "Desc17", Category = categories[34], Manufacturer = manufacturers[39],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\varfarin-nikomed-tabl-2-5mg-100-takeda-farma-sp-cart-120x120-5b12.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\varfarin-nikomed-tabl-2-5mg-100-takeda-farma-sp-cart-120x120-5b12.jpg")))}"},
                new Medicine{ Name = "Фенілін-Здоров'я, табл. 30мг №20", Description = "Desc17", Category = categories[34], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\44490_11_50_20_12.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\44490_11_50_20_12.jpg")))}"},

                new Medicine{ Name = "Дістрептаза Дістрепт супозиторії ректальні 15000МО + 1250МО, №6", Description = "Desc17", Category = categories[35], Manufacturer = manufacturers[40],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\alpen_pharma_distreptaza.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\alpen_pharma_distreptaza.jpg")))}"},

                new Medicine{ Name = "Атерокард табл. в/о 75мг №10", Description = "Desc17", Category = categories[36], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\aterokard-tabl-p-o-75mg-10-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-dbc6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\aterokard-tabl-p-o-75mg-10-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-dbc6.jpg")))}"},
                new Medicine{ Name = "Плавікс табл. в/о 300мг №10", Description = "Desc17", Category = categories[36], Manufacturer = manufacturers[10],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\plaviks-tabl-p-o-300mg-10-sanofi-vintrop-indastria-cart-120x120-65d6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\plaviks-tabl-p-o-300mg-10-sanofi-vintrop-indastria-cart-120x120-65d6.jpg")))}"},
                new Medicine{ Name = "Тромбонет-Фармак, табл. в/о 75мг №30", Description = "Desc17", Category = categories[36], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\562148_140_62_40_35.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\562148_140_62_40_35.jpg")))}"},

                new Medicine{ Name = "Кардіомагніл форте, табл. в/о 150мг №100", Description = "Desc17", Category = categories[37], Manufacturer = manufacturers[10],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kardiomagnil-forte-tabl-p-o-150mg-100-takeda-gmbh-cart-120x120-6b86.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kardiomagnil-forte-tabl-p-o-150mg-100-takeda-gmbh-cart-120x120-6b86.jpg")))}"},
                new Medicine{ Name = "Лоспирин табл. в/о 75мг №120", Description = "Desc17", Category = categories[37], Manufacturer = manufacturers[39],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lospirin-tabl-p-o-75mg-120-kusum-farm-ooo-cart-120x120-61c7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lospirin-tabl-p-o-75mg-120-kusum-farm-ooo-cart-120x120-61c7.jpg")))}"},

                new Medicine{ Name = "АКК розчин 50мг/мл 2мл контейнер №10 (Юрія-Фарм)", Description = "Desc17", Category = categories[38], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\akk-aminokapronovaya-k-ta-r-r-50mg-ml-kont-2ml-10-yuriya-farm-ooo-cart-120x120-fd3c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\akk-aminokapronovaya-k-ta-r-r-50mg-ml-kont-2ml-10-yuriya-farm-ooo-cart-120x120-fd3c.jpg")))}"},
                new Medicine{ Name = "Амінокапронова кислота, порошок оральний 1г пак. №10 (Здоров'я)", Description = "Desc17", Category = categories[38], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\aminokapronovaya-k-ta-por-d-oral-prim-1g-10-tov-farm-kompaniya-zdorove-cart-120x120-2dd7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\aminokapronovaya-k-ta-por-d-oral-prim-1g-10-tov-farm-kompaniya-zdorove-cart-120x120-2dd7.jpg")))}"},
                new Medicine{ Name = "Гемотран р-н д/ін. 100мг/мл 10мл амп. №5", Description = "Desc17", Category = categories[38], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gemotran-r-r-d-in-100mg-ml-amp-10ml-5-pao-farmak-cart-120x120-9b96.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gemotran-r-r-d-in-100mg-ml-amp-10ml-5-pao-farmak-cart-120x120-9b96.jpg")))}"},

                new Medicine{ Name = "Вікасол-Дарниця, р-н д/ін. 10мг/мл 1мл амп. №10", Description = "Desc17", Category = categories[39], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\7373_85_78_33_42.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\7373_85_78_33_42.jpg")))}"},
                new Medicine{ Name = "Дицинон р-н д/ін. 250мг 2мл амп. №50", Description = "Desc17", Category = categories[39], Manufacturer = manufacturers[36],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ditsinon-r-r-d-in-250mg-2ml-amp-2ml-50-lek-farmatsevticheskaya-kompaniya-d-d-cart-120x120-6d79.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ditsinon-r-r-d-in-250mg-2ml-amp-2ml-50-lek-farmatsevticheskaya-kompaniya-d-d-cart-120x120-6d79.jpg")))}"},
                new Medicine{ Name = "Револад табл. в/о 25мг N28", Description = "Desc17", Category = categories[39], Manufacturer = manufacturers[41],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\revolad-tabl-p-o-25mg-28-glakso-vellkom-s-a-cart-120x120-9bea.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\revolad-tabl-p-o-25mg-28-glakso-vellkom-s-a-cart-120x120-9bea.jpg")))}"},

                new Medicine{ Name = "Суфер р-н д/ін. 20мг/мл 5мл амп. №5", Description = "Desc17", Category = categories[40], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\sufer-r-r-dlya-v-v-in-20mg-ml-amp-5ml-5-yuriya-farm-ooo-cart-120x120-183b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\sufer-r-r-dlya-v-v-in-20mg-ml-amp-5ml-5-yuriya-farm-ooo-cart-120x120-183b.jpg")))}"},
                new Medicine{ Name = "Феррум-лек, р-н д/ін. 100мг/2мл 2мл амп. №5", Description = "Desc17", Category = categories[40], Manufacturer = manufacturers[36],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\sandoz_ferrymlek.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\sandoz_ferrymlek.jpg")))}"},
                new Medicine{ Name = "Ферсінол р-н д/ін. 100мг/2мл 2мл амп. №5", Description = "Desc17", Category = categories[40], Manufacturer = manufacturers[3],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\174438_85_80_18_27.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\174438_85_80_18_27.jpg")))}"},

                new Medicine{ Name = "Ціанокобаламін-Дарниця (Витамін В12-Дарниця), р-н д/ін. 0,2мг/мл 1мл амп. №10", Description = "Desc17", Category = categories[41], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\7157_82_78_32_42.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\7157_82_78_32_42.jpg")))}"},
                new Medicine{ Name = "Фолієва кислота табл. 1мг №30 (КВЗ)", Description = "Desc17", Category = categories[41], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\folievaya-k-ta-tabl-1mg-50-pao-vitaminyi-cart-120x120-7cb6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\folievaya-k-ta-tabl-1mg-50-pao-vitaminyi-cart-120x120-7cb6.jpg")))}"},

                new Medicine{ Name = "Рінгера р-н д/інф. 400мл фл. (Юрія-Фарм)", Description = "Desc17", Category = categories[42], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ringera-r-r-d-inf-but-200ml-yuriya-farm-ooo-cart-120x120-f002.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ringera-r-r-d-inf-but-200ml-yuriya-farm-ooo-cart-120x120-f002.jpg")))}"},
                new Medicine{ Name = "Глюкоза р-н д/інф. 10% 200мл фл. (Юрія-Фарм)", Description = "Desc17", Category = categories[42], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\glyukoza-r-r-d-inf-10-but-200ml-yuriya-farm-ooo-cart-120x120-9c5f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\glyukoza-r-r-d-inf-10-but-200ml-yuriya-farm-ooo-cart-120x120-9c5f.jpg")))}"},

                new Medicine{ Name = "Калію хлорид, концентрат д/р-ну д/інф. 7,5% 20мл фл. (Юрія-Фарм)", Description = "Desc17", Category = categories[43], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kaliya-hlorid-konts-d-inf-7-5-fl-20ml-yuriya-farm-ooo-cart-120x120-8423.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kaliya-hlorid-konts-d-inf-7-5-fl-20ml-yuriya-farm-ooo-cart-120x120-8423.jpg")))}"},
                new Medicine{ Name = "Натрію гідрокарбонат, р-н д/інф. 40мг/мл 100мл фл.", Description = "Desc17", Category = categories[43], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\natriya-gidrokarbonat-r-r-d-inf-40mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-1674.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\natriya-gidrokarbonat-r-r-d-inf-40mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-1674.jpg")))}"},
                new Medicine{ Name = "Сода-буфер р-н д/інф. 42мг/мл 200мл фл. (Юрія-Фарм)", Description = "Desc17", Category = categories[43], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\soda-bufer-r-r-d-inf-42mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-d474.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\soda-bufer-r-r-d-inf-42mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-d474.jpg")))}"},
                new Medicine{ Name = "Натрію хлорид, р-н д/ін. 0,9% 2мл контейнер №10 (Юрія-Фарм)", Description = "Desc17", Category = categories[43], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\natriya-hlorid-fiz-rastvor-r-r-d-in-0-9-amp-10ml-10-darnitsa-chao-farm-firma-cart-120x120-d6d3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\natriya-hlorid-fiz-rastvor-r-r-d-in-0-9-amp-10ml-10-darnitsa-chao-farm-firma-cart-120x120-d6d3.jpg")))}"},
                new Medicine{ Name = "Магнію сульфат, р-н д/ін. 250мг/мл 5мл амп. №10 (Юрія-Фарм)", Description = "Desc17", Category = categories[43], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\magniya-sulfat-r-r-d-in-250mg-ml-amp-5ml-10-yuriya-farm-ooo-cart-120x120-3952.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\magniya-sulfat-r-r-d-in-250mg-ml-amp-5ml-10-yuriya-farm-ooo-cart-120x120-3952.jpg")))}"},

                new Medicine{ Name = "Дигоксин-Здоров'я, табл. 0,25мг №50", Description = "Desc17", Category = categories[44], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\23751_90_80_25_18.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\23751_90_80_25_18.jpg")))}"},
                new Medicine{ Name = "Адреналін-Дарниця, р-н д/ін. 1,8мг/мл 1мл амп. №10", Description = "Desc17", Category = categories[44], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\38223_83_75_33_43.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\38223_83_75_33_43.jpg")))}"},

                new Medicine{ Name = "Мексаритм капс. 200мг №20", Description = "Desc17", Category = categories[45], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\meksaritm-kaps-200mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-efbe.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\meksaritm-kaps-200mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-efbe.jpg")))}"},
                new Medicine{ Name = "Аритміл табл. 200мг №50", Description = "Desc17", Category = categories[45], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\aritmil-tabl-200mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-eb76.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\aritmil-tabl-200mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-eb76.jpg")))}"},
                new Medicine{ Name = "Кардіодарон-Здоров'я табл. 200мг №30", Description = "Desc17", Category = categories[45], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kardiodaron-zdorove-tabl-200mg-30-tov-farm-kompaniya-zdorove-cart-120x120-d862.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kardiodaron-zdorove-tabl-200mg-30-tov-farm-kompaniya-zdorove-cart-120x120-d862.jpg")))}"},
                new Medicine{ Name = "Ротаритміл табл. 200мг №30", Description = "Desc17", Category = categories[45], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\rotaritmil-tabl-200mg-30-rivofarm-sa-cart-120x120-f9b2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\rotaritmil-tabl-200mg-30-rivofarm-sa-cart-120x120-f9b2.jpg")))}"},

                new Medicine{ Name = "Мононітросид табл. 40мг №40", Description = "Desc17", Category = categories[46], Manufacturer = manufacturers[24],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\mononitrosid-tabl-40mg-40-pao-nvts-borschagovskiy-hfz-cart-120x120-44be.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\mononitrosid-tabl-40mg-40-pao-nvts-borschagovskiy-hfz-cart-120x120-44be.jpg")))}"},
                new Medicine{ Name = "Сидокард табл. 2мг №30", Description = "Desc17", Category = categories[46], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\sidokard-tabl-2mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-bb64.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},

                new Medicine{ Name = "Клофелін-Дарниця табл. 0,15мг №50", Description = "Desc17", Category = categories[48], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\21642_90_35_28_22 (1).jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\21642_90_35_28_22 (1).jpg")))}"},
                  
                new Medicine{ Name = "Моксогама табл. в/о 0,2мг №30", Description = "Desc17", Category = categories[50], Manufacturer = manufacturers[42],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\moksogamma-tabl-p-o-0-2mg-30-artezan-farma-gmbh-i-ko-cart-120x120-85bc.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\moksogamma-tabl-p-o-0-2mg-30-artezan-farma-gmbh-i-ko-cart-120x120-85bc.jpg")))}"},
                new Medicine{ Name = "Моксогама табл. в/о 0,4мг №30", Description = "Desc17", Category = categories[50], Manufacturer = manufacturers[42],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\moksogamma-tabl-p-o-0-4mg-30-artezan-farma-gmbh-i-ko-cart-120x120-7e67.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\moksogamma-tabl-p-o-0-4mg-30-artezan-farma-gmbh-i-ko-cart-120x120-7e67.jpg")))}"},

                new Medicine{ Name = "Гідрохлортіазид табл. 25мг №20 (БХФЗ)", Description = "Desc17", Category = categories[49], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gidrohlortiazid-tabl-25mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-d823.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gidrohlortiazid-tabl-25mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-d823.jpg")))}"},
                new Medicine{ Name = "Тиурекс табл. 12,5мг №30", Description = "Desc17", Category = categories[49], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tiureks-tabl-12-5mg-30-kusum-farm-ooo-cart-120x120-adc0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tiureks-tabl-12-5mg-30-kusum-farm-ooo-cart-120x120-adc0.jpg")))}"},

                new Medicine{ Name = "Фуросемід-Дарниця, табл. 40мг №50", Description = "Desc17", Category = categories[50], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\28479_90_35_29_18.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\28479_90_35_29_18.jpg")))}"},
                new Medicine{ Name = "Фуросемід-Дарниця, р-н д/ін. 1% 2мл амп. №10", Description = "Desc17", Category = categories[50], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\30986_83_78_33_57.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\30986_83_78_33_57.jpg")))}"},

                new Medicine{ Name = "Верошпірон капс. 100мг №30", Description = "Desc17", Category = categories[51], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\veroshpiron-kaps-100mg-30-gedeon-rihter-oao-cart-120x120-b32c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\veroshpiron-kaps-100mg-30-gedeon-rihter-oao-cart-120x120-b32c.jpg")))}"},
                new Medicine{ Name = "Спіронолактон-Дарниця, табл. 100мг №30", Description = "Desc17", Category = categories[51], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\spironolakton-darnitsa-tabl-100mg-30-darnitsa-chao-farm-firma-cart-120x120-7370.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\spironolakton-darnitsa-tabl-100mg-30-darnitsa-chao-farm-firma-cart-120x120-7370.jpg")))}"},

                new Medicine{ Name = "Анаприлін-Здоров'я, табл. 10мг №50", Description = "Desc17", Category = categories[52], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\26915_92_40_34_21.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\26915_92_40_34_21.jpg")))}"},
                new Medicine{ Name = "Біпролол табл. 10мг №30", Description = "Desc17", Category = categories[52], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\biprolol-tabl-10mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-01bf.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\biprolol-tabl-10mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-01bf.jpg")))}"},
                new Medicine{ Name = "Бісопролол-КВ, табл. 10мг №30", Description = "Desc17", Category = categories[52], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kvz_bisoprolol_kv.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kvz_bisoprolol_kv.jpg")))}"},

                new Medicine{ Name = "Анаприлін-Здоров'я, табл. 10мг №50", Description = "Desc17", Category = categories[55], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\26915_92_40_34_21.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\26915_92_40_34_21.jpg")))}"},
                new Medicine{ Name = "Соритмік табл. 80мг №20", Description = "Desc17", Category = categories[53], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\soritmik-tabl-80mg-20-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-d58e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\soritmik-tabl-80mg-20-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-d58e.jpg")))}"},

                new Medicine{ Name = "Небілет Плюс 5/12,5, табл. в/о №28", Description = "Desc17", Category = categories[54], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nebilet-plyus-5-12-5-tabl-p-o-28-berlin-hemi-ag-cart-120x120-9436.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nebilet-plyus-5-12-5-tabl-p-o-28-berlin-hemi-ag-cart-120x120-9436.jpg")))}"},
                new Medicine{ Name = "Динорик-Дарниця табл. в/о №10", Description = "Desc17", Category = categories[54], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dinorik-darnitsa-tabl-p-o-10-darnitsa-chao-farm-firma-cart-120x120-0ad0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dinorik-darnitsa-tabl-p-o-10-darnitsa-chao-farm-firma-cart-120x120-0ad0.jpg")))}"},

                new Medicine{ Name = "Аладин-Фармак табл. 10мг №50", Description = "Desc17", Category = categories[56], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\aladin-farmak-tabl-10mg-50-pao-farmak-cart-120x120-a0db.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\aladin-farmak-tabl-10mg-50-pao-farmak-cart-120x120-a0db.jpg")))}"},
                new Medicine{ Name = "Семлопін табл. 5мг №28", Description = "Desc17", Category = categories[56], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\semlopin-tabl-5mg-28-kusum-farm-ooo-cart-120x120-9f33.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\semlopin-tabl-5mg-28-kusum-farm-ooo-cart-120x120-9f33.jpg")))}"},

                new Medicine{ Name = "Фармадипін краплі оральні, р-н 2% 25мл фл.", Description = "Desc17", Category = categories[57], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\farmadipin-kap-oral-2-fl-25ml-pao-farmak-cart-120x120-8356.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\farmadipin-kap-oral-2-fl-25ml-pao-farmak-cart-120x120-8356.jpg")))}"},
 
                new Medicine{ Name = "Арифам табл. 1,5мг/5мг №30", Description = "Desc17", Category = categories[58], Manufacturer = manufacturers[44],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\arifam-tabl-1-5mg-5mg-30-laboratorii-serve-indastri-cart-120x120-9cf7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\arifam-tabl-1-5mg-5mg-30-laboratorii-serve-indastri-cart-120x120-9cf7.jpg")))}"},

                new Medicine{ Name = "Верапамілу гідрохлорид, табл. в/о 80мг №50 (БХФЗ)", Description = "Desc17", Category = categories[59], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\verapamila-gidrohlorid-tabl-p-o-80mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-b02d.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\verapamila-gidrohlorid-tabl-p-o-80mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-b02d.jpg")))}"},
                new Medicine{ Name = "Вератард 180, капс. 180мг №30", Description = "Desc17", Category = categories[59], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\veratard-kaps-180mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-bbb5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\veratard-kaps-180mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-bbb5.jpg")))}"},
                new Medicine{ Name = "Ізоптин SR, табл. 240мг №30", Description = "Desc17", Category = categories[59], Manufacturer = manufacturers[30],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\izoptin-sr-tabl-240mg-30-famar-a-v-e-avlon-plant-cart-120x120-bcd2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\izoptin-sr-tabl-240mg-30-famar-a-v-e-avlon-plant-cart-120x120-bcd2.jpg")))}"},
                
                new Medicine{ Name = "Ністатинова мазь 100000 ОД/г 15г (Лубнифарм)", Description = "Desc17", Category = categories[60], Manufacturer = manufacturers[46],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nistatinovaya-maz-100-000-ed-g-tuba-15g-lubnyifarm-chao-cart-120x120-01f0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nistatinovaya-maz-100-000-ed-g-tuba-15g-lubnyifarm-chao-cart-120x120-01f0.jpg")))}"},
                new Medicine{ Name = "Пімафуцин крем 20мг/г 30г", Description = "Desc17", Category = categories[60], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pimafutsin-krem-20mg-g-tuba-30g-temmler-italia-s-r-l-cart-120x120-c4a2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pimafutsin-krem-20mg-g-tuba-30g-temmler-italia-s-r-l-cart-120x120-c4a2.jpg")))}"},

                new Medicine{ Name = "Клотримазол р-н зовнішній 1% 25мл фл. (БХФЗ)", Description = "Desc17", Category = categories[61], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klotrimazol-maz-1-tuba-25g-pao-nvts-borschagovskiy-hfz-cart-120x120-3510.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klotrimazol-maz-1-tuba-25g-pao-nvts-borschagovskiy-hfz-cart-120x120-3510.jpg")))}"},
                new Medicine{ Name = "Міконазол-Дарниця, крем 2% 15г", Description = "Desc17", Category = categories[61], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\23469_120_32_22_60.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\23469_120_32_22_60.jpg")))}"},
                new Medicine{ Name = "Дермазол крем 20 мг/г 15г", Description = "Desc17", Category = categories[61], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dermazol-krem-20mg-g-tuba-15g-kusum-helthker-pvt-ltd-cart-120x120-354f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dermazol-krem-20mg-g-tuba-15g-kusum-helthker-pvt-ltd-cart-120x120-354f.jpg")))}"},
                new Medicine{ Name = "Дермазол Плюс шампунь, 100мл фл.", Description = "Desc17", Category = categories[61], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dermazol-shampun-2-fl-100ml-kusum-helthker-pvt-ltd-cart-120x120-7c8c.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dermazol-shampun-2-fl-100ml-kusum-helthker-pvt-ltd-cart-120x120-7c8c.jpg")))}"},

                new Medicine{ Name = "Екзифін гель 1% 15г", Description = "Desc17", Category = categories[62], Manufacturer = manufacturers[14],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ekzifin-gel-1-tuba-15g-d-r-reddis-laboratoris-ltd-cart-120x120-14b2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ekzifin-gel-1-tuba-15g-d-r-reddis-laboratoris-ltd-cart-120x120-14b2.jpg")))}"},
                new Medicine{ Name = "Ламікон крем 1% 15г", Description = "Desc17", Category = categories[62], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lamikon-krem-1-tuba-15g-pao-farmak-cart-120x120-b945.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lamikon-krem-1-tuba-15g-pao-farmak-cart-120x120-b945.jpg")))}"},
                new Medicine{ Name = "Екзодерил р-н нашкірний 1% 10мл фл.", Description = "Desc17", Category = categories[62], Manufacturer = manufacturers[47],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ekzoderil-r-r-nakozh-1-fl-10ml-sandoz-gmbh-tehops-cart-120x120-32ef.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ekzoderil-r-r-nakozh-1-fl-10ml-sandoz-gmbh-tehops-cart-120x120-32ef.jpg")))}"},
                new Medicine{ Name = "Екзодерил р-н нашкірний 1% 20мл фл. з пилкою д/нігтів (подарунок)", Description = "Desc17", Category = categories[62], Manufacturer = manufacturers[47],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ekzoderil-r-r-nakozh-1-fl-10ml-pilka-d-nogtey-sandoz-gmbh-tehops-cart-120x120-f623.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ekzoderil-r-r-nakozh-1-fl-10ml-pilka-d-nogtey-sandoz-gmbh-tehops-cart-120x120-f623.jpg")))}"},

                new Medicine{ Name = "Кандід-Б крем 15г", Description = "Desc17", Category = categories[63], Manufacturer = manufacturers[48],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kandid-r-r-d-rot-polosti-1-fl-15ml-glenmark-farmasyutikalz-ltd-prod-400x400-e4eb.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kandid-r-r-d-rot-polosti-1-fl-15ml-glenmark-farmasyutikalz-ltd-prod-400x400-e4eb.jpg")))}"},

                new Medicine{ Name = "Тербінафін-КВ, табл. 250мг №14", Description = "Desc17", Category = categories[64], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kvz_terbinafin_kv.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kvz_terbinafin_kv.jpg")))}"},
                new Medicine{ Name = "Ламікон табл. 250мг №14", Description = "Desc17", Category = categories[64], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lamikon-tabl-0-25g-14-pao-farmak-cart-120x120-6507.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lamikon-tabl-0-25g-14-pao-farmak-cart-120x120-6507.jpg")))}"},

                new Medicine{ Name = "Гризеофульвін табл. 125мг №40", Description = "Desc17", Category = categories[65], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\grizeofulvin-tabl-125mg-40-pao-nvts-borschagovskiy-hfz-cart-120x120-8dab.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\grizeofulvin-tabl-125mg-40-pao-nvts-borschagovskiy-hfz-cart-120x120-8dab.jpg")))}"},
                
                new Medicine{ Name = "Пантенол-Тева, мазь 5% 35г", Description = "Desc17", Category = categories[66], Manufacturer = manufacturers[49],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\439483_130_35_35_56.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\439483_130_35_35_56.jpg")))}"},
                new Medicine{ Name = "ХепіДерм Плюс, крем 20г", Description = "Desc17", Category = categories[66], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\heppiderm-plyus-krem-tuba-20g-tov-farm-kompaniya-zdorove-cart-120x120-c3ca.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\heppiderm-plyus-krem-tuba-20g-tov-farm-kompaniya-zdorove-cart-120x120-c3ca.jpg")))}"},
                new Medicine{ Name = "Бепантен крем 5% 100г", Description = "Desc17", Category = categories[66], Manufacturer = manufacturers[50],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bepanten-krem-5-tuba-100g-gp-grenzah-produktions-gmbh-cart-120x120-0ddd.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bepanten-krem-5-tuba-100g-gp-grenzah-produktions-gmbh-cart-120x120-0ddd.jpg")))}"},
                 
                new Medicine{ Name = "Діоксизоль-Дарниця, розчин 100г фл.", Description = "Desc17", Category = categories[67], Manufacturer = manufacturers[49],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\21882_115_46_46_129.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\21882_115_46_46_129.jpg")))}"},
                new Medicine{ Name = "Діоксизоль-Дарниця, розчин 50г фл.", Description = "Desc17", Category = categories[67], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\21881_96_40_40_69.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\21881_96_40_40_69.jpg")))}"},
                new Medicine{ Name = "Левомеколь мазь 40г (Фармак)", Description = "Desc17", Category = categories[67], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\levomekol-maz-tuba-40g-prat-farmats-fabrika-viola-cart-120x120-08c9.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\levomekol-maz-tuba-40g-prat-farmats-fabrika-viola-cart-120x120-08c9.jpg")))}"},
                 
                new Medicine{ Name = "Хімотрипсин кристалічний, ліофілізат д/р-ну д/ін. 0,01г фл. №10 (Біофарма)", Description = "Desc17", Category = categories[68], Manufacturer = manufacturers[51],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\himotripsin-kristallicheskiy-liofil-d-r-ra-d-in-0-01g-fl-10-biofarma-fz-ooo-cart-120x120-6a70.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\himotripsin-kristallicheskiy-liofil-d-r-ra-d-in-0-01g-fl-10-biofarma-fz-ooo-cart-120x120-6a70.jpg")))}"},

                new Medicine{ Name = "Едермік гель 0,1% 30г", Description = "Desc17", Category = categories[69], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\edermik-gel-0-1-tuba-30g-pao-farmak-cart-120x120-6b14.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\edermik-gel-0-1-tuba-30g-pao-farmak-cart-120x120-6b14.jpg")))}"},
                 
                new Medicine{ Name = "Псило-бальзам, гель 1% 20г", Description = "Desc17", Category = categories[70], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0 (3).jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0 (3).jpg")))}"},

                new Medicine{ Name = "Лідокаїн-Здоров'я, спрей 10% 38г фл.", Description = "Desc17", Category = categories[71], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\19265_50_120_50_112.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\19265_50_120_50_112.jpg")))}"},

                new Medicine{ Name = "Гідрокортизон мазь 1% 10г (Нижфарм)", Description = "Desc17", Category = categories[72], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gidrokortizonovaya-maz-1-tuba-10g-nizhfarm-ao-cart-120x120-d905.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gidrokortizonovaya-maz-1-tuba-10g-nizhfarm-ao-cart-120x120-d905.jpg")))}"},

                new Medicine{ Name = "Кортидерм крем 1мг/г 15г", Description = "Desc17", Category = categories[73], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kortiderm-krem-1mg-g-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-5a81.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kortiderm-krem-1mg-g-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-5a81.jpg")))}"},
                new Medicine{ Name = "Фокорт-Дарниця, крем 1мг/г 15г", Description = "Desc17", Category = categories[73], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\36222_122_32_21_25.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\36222_122_32_21_25.jpg")))}"},
                new Medicine{ Name = "Фторокорт мазь 1мг/г 15г", Description = "Desc17", Category = categories[73], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ftorokort-maz-tuba-15g-gedeon-rihter-oao-cart-120x120-9ea2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ftorokort-maz-tuba-15g-gedeon-rihter-oao-cart-120x120-9ea2.jpg")))}"},

                new Medicine{ Name = "Мезодерм крем 0,1% 30г", Description = "Desc17", Category = categories[74], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\mezoderm-krem-0-1-tuba-30g-pao-nvts-borschagovskiy-hfz-cart-120x120-815e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\mezoderm-krem-0-1-tuba-30g-pao-nvts-borschagovskiy-hfz-cart-120x120-815e.jpg")))}"},
                new Medicine{ Name = "Алергодерм мазь 0,25мг/г 15г", Description = "Desc17", Category = categories[74], Manufacturer = manufacturers[13]},
                new Medicine{ Name = "Флуцар-Дарниця, крем 1мг/г 15г", Description = "Desc17", Category = categories[74], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\44137_122_32_22_24.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\44137_122_32_22_24.jpg")))}"},

                new Medicine{ Name = "Дермовейт крем 0,05% 25г", Description = "Desc17", Category = categories[75], Manufacturer = manufacturers[41],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dermoveyt-krem-0-05-tuba-25g-glakso-smit-klyayn-farmasyutikalz-cart-120x120-977e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dermoveyt-krem-0-05-tuba-25g-glakso-smit-klyayn-farmasyutikalz-cart-120x120-977e.jpg")))}"},
                new Medicine{ Name = "Клобескін мазь 0,05% 25г", Description = "Desc17", Category = categories[75], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klobeskin-maz-0-05-tuba-25g-tov-farm-kompaniya-zdorove-cart-120x120-b293.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klobeskin-maz-0-05-tuba-25g-tov-farm-kompaniya-zdorove-cart-120x120-b293.jpg")))}"},
                new Medicine{ Name = "Поверкорт крем 15г", Description = "Desc17", Category = categories[75], Manufacturer = manufacturers[48],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\poverkort-krem-0-05-tuba-15g-glenmark-farmasyutikalz-ltd-cart-120x120-3a30.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\poverkort-krem-0-05-tuba-15g-glenmark-farmasyutikalz-ltd-cart-120x120-3a30.jpg")))}"},
                 
                new Medicine{ Name = "Тримістин-Дарниця, мазь 14г", Description = "Desc17", Category = categories[76], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\21839_115_33_22_27.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\21839_115_33_22_27.jpg")))}"},

                new Medicine{ Name = "Бетаметазон-Дарниця, крем 15г", Description = "Desc17", Category = categories[77], Manufacturer = manufacturers[48],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\27436_116_33_21_24.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\27436_116_33_21_24.jpg")))}"},

                new Medicine{ Name = "Пімафукорт мазь 15г", Description = "Desc17", Category = categories[78], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pimafukort-maz-tuba-15g-temmler-italia-s-r-l-cart-120x120-a71a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pimafukort-maz-tuba-15g-temmler-italia-s-r-l-cart-120x120-a71a.jpg")))}"},
                new Medicine{ Name = "Пімафукорт крем 15г", Description = "Desc17", Category = categories[78], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pimafukort-krem-tuba-15g-temmler-italia-s-r-l-cart-120x120-cb33.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pimafukort-krem-tuba-15g-temmler-italia-s-r-l-cart-120x120-cb33.jpg")))}"},

                new Medicine{ Name = "Бетазон ультра, крем 15г", Description = "Desc17", Category = categories[80], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\betazon-ultra-krem-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-a954.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\betazon-ultra-krem-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-a954.jpg")))}"},
                new Medicine{ Name = "Бетазон ультра, мазь 15г", Description = "Desc17", Category = categories[80], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\betazon-ultra-maz-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-07f6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\betazon-ultra-maz-tuba-15g-tov-farm-kompaniya-zdorove-cart-120x120-07f6.jpg")))}"},
                new Medicine{ Name = "Кандідерм крем 15г", Description = "Desc17", Category = categories[80], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kandiderm-krem-tuba-15g-glenmark-farmasyutikalz-ltd-cart-120x120-a3c3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kandiderm-krem-tuba-15g-glenmark-farmasyutikalz-ltd-cart-120x120-a3c3.jpg")))}"},

                new Medicine{ Name = "Бепантен плюс, крем 30г", Description = "Desc17", Category = categories[81], Manufacturer = manufacturers[50],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bepanten-plyus-krem-tuba-30g-gp-grenzah-produktions-gmbh-cart-120x120-2ce3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bepanten-plyus-krem-tuba-30g-gp-grenzah-produktions-gmbh-cart-120x120-2ce3.jpg")))}"},
                new Medicine{ Name = "Бетайод-Здоров'я, р-н нашкірний 100мг/мл 50мл фл.", Description = "Desc17", Category = categories[81], Manufacturer = manufacturers[13]},
                new Medicine{ Name = "Бетайод-Здоров'я, р-н нашкірний 100мг/мл 1000мл фл.", Description = "Desc17", Category = categories[81], Manufacturer = manufacturers[13]},

                new Medicine{ Name = "Дарсепт, антисептик для рук з декспантенолом (75% Етанол), 50мл фл.", Description = "Desc17", Category = categories[82], Manufacturer = manufacturers[20]},

                new Medicine{ Name = "Банбакт супозиторії вагінальні 100мг №3", Description = "Desc17", Category = categories[83], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\banbakt-supp-vaginal-100mg-3-kusum-helthker-pvt-ltd-cart-120x120-8295.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\banbakt-supp-vaginal-100mg-3-kusum-helthker-pvt-ltd-cart-120x120-8295.jpg")))}"},
                new Medicine{ Name = "Вагіцин-Здоров'я, крем вагінальний 2% 20г", Description = "Desc17", Category = categories[83], Manufacturer = manufacturers[13]},
                
                new Medicine{ Name = "Містол супозиторії вагінальні 500мг №10", Description = "Desc17", Category = categories[84], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\mistol-supp-vaginal-500mg-10-kusum-helthker-pvt-ltd-cart-120x120-1932.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\mistol-supp-vaginal-500mg-10-kusum-helthker-pvt-ltd-cart-120x120-1932.jpg")))}"},

                new Medicine{ Name = "Пімафуцин супозиторії вагінальні 100мг №3", Description = "Desc17", Category = categories[85], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pimafutsin-supp-vaginal-100mg-3-temmler-italia-s-r-l-cart-120x120-0c9a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pimafutsin-supp-vaginal-100mg-3-temmler-italia-s-r-l-cart-120x120-0c9a.jpg")))}"},
                new Medicine{ Name = "Пімафуцин супозиторії вагінальні 100мг №6", Description = "Desc17", Category = categories[85], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pimafutsin-supp-vaginal-100mg-6-temmler-italia-s-r-l-cart-120x120-4a0e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pimafutsin-supp-vaginal-100mg-6-temmler-italia-s-r-l-cart-120x120-4a0e.jpg")))}"},
                 
                new Medicine{ Name = "Клотримазол таблетки вагінальні 100мг №10 (БХФЗ)", Description = "Desc17", Category = categories[86], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klotrimazol-tabl-vaginal-100mg-10-pao-nvts-borschagovskiy-hfz-cart-120x120-433f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klotrimazol-tabl-vaginal-100mg-10-pao-nvts-borschagovskiy-hfz-cart-120x120-433f.jpg")))}"},
                new Medicine{ Name = "Клофан крем вагінальний 10% 7г", Description = "Desc17", Category = categories[86], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klofan-krem-vaginalnyiy-10-tuba-7g-kusum-helthker-pvt-ltd-cart-120x120-64a7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klofan-krem-vaginalnyiy-10-tuba-7g-kusum-helthker-pvt-ltd-cart-120x120-64a7.jpg")))}"},

                new Medicine{ Name = "Вагіцин Нео, таблетки вагінальні №10", Description = "Desc17", Category = categories[87], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\vagitsin-neo-tabl-vagin-10-tov-farm-kompaniya-zdorove-cart-120x120-5ddb.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\vagitsin-neo-tabl-vagin-10-tov-farm-kompaniya-zdorove-cart-120x120-5ddb.jpg")))}"},

                new Medicine{ Name = "Сертаконазол-Фармекс, песарій 300мг №1", Description = "Desc17", Category = categories[88], Manufacturer = manufacturers[52],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\297701_70_38_23_10.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\297701_70_38_23_10.jpg")))}"},

                new Medicine{ Name = "Хінофуцин супозиторії вагінальні 0,015г №10", Description = "Desc17", Category = categories[89], Manufacturer = manufacturers[24],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\hinofutsin-supp-vaginal-0-015g-10-at-lekhim-harkov-cart-120x120-67b5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\hinofutsin-supp-vaginal-0-015g-10-at-lekhim-harkov-cart-120x120-67b5.jpg")))}"},
                new Medicine{ Name = "Хінофуцін супозиторії вагінальні 0,015г №5", Description = "Desc17", Category = categories[89], Manufacturer = manufacturers[24]},
                new Medicine{ Name = "Феміклін таблетки вагінальні 10мг №6", Description = "Desc17", Category = categories[89], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\femiklin-tabl-vaginal-10mg-6-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-ee89.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\femiklin-tabl-vaginal-10mg-6-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-ee89.jpg")))}"},
                
                new Medicine{ Name = "Гайнекс супозиторії вагінальні №14", Description = "Desc17", Category = categories[90], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gayneks-supp-vaginal-14-kusum-helthker-pvt-ltd-cart-120x120-06bd.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gayneks-supp-vaginal-14-kusum-helthker-pvt-ltd-cart-120x120-06bd.jpg")))}"},
                new Medicine{ Name = "Гайнекс Форте, супозиторії вагінальні №7", Description = "Desc17", Category = categories[90], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gayneks-forte-supp-vaginal-7-kusum-helthker-pvt-ltd-cart-120x120-a2fa.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gayneks-forte-supp-vaginal-7-kusum-helthker-pvt-ltd-cart-120x120-a2fa.jpg")))}"},

                new Medicine{ Name = "Тержинан таблетки вагінальні №10", Description = "Desc17", Category = categories[91], Manufacturer = manufacturers[53],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\terzhinan-tabl-vaginal-10-sofarteks-cart-120x120-9940.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\terzhinan-tabl-vaginal-10-sofarteks-cart-120x120-9940.jpg")))}"},
                new Medicine{ Name = "Тержинан таблетки вагінальні №6", Description = "Desc17", Category = categories[91], Manufacturer = manufacturers[53],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\terzhinan-tabl-vaginal-6-sofarteks-cart-120x120-a265.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\terzhinan-tabl-vaginal-6-sofarteks-cart-120x120-a265.jpg")))}"},

                new Medicine{ Name = "Препідил гель ендоцервікальний 0,5мг/3г шприц №1", Description = "Desc17", Category = categories[92], Manufacturer = manufacturers[54],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},

                new Medicine{ Name = "Гініпрал концентрат д/р-ну д/інф. 25мкг/5мл амп. №5", Description = "Desc17", Category = categories[94], Manufacturer = manufacturers[55],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},
                new Medicine{ Name = "Гініпрал р-н д/ін. 10мкг/2мл амп. №5", Description = "Desc17", Category = categories[94], Manufacturer = manufacturers[55],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},

                new Medicine{ Name = "Джайдес внутрішньоматкова система з левоноргестрелом по 13,5мг", Description = "Desc17", Category = categories[95], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dzhaydes-vnutrimat-s-ma-s-levonorgest-13-5mg-1-bayer-ou-cart-120x120-820d.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dzhaydes-vnutrimat-s-ma-s-levonorgest-13-5mg-1-bayer-ou-cart-120x120-820d.jpg")))}"},
                new Medicine{ Name = "Мірена внутрішньоматкова система з левоноргестрелом 52мг (20мкг/24години)", Description = "Desc17", Category = categories[95], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\spiral-mirena-bayer-ou-cart-120x120-9274.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\spiral-mirena-bayer-ou-cart-120x120-9274.jpg")))}"},

                new Medicine{ Name = "Еротекс, супозиторії вагінальні із запахом троянди №10", Description = "Desc17", Category = categories[96], Manufacturer = manufacturers[57],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\eroteks-p-z-supp-10-roza-sperko-ukraina-cart-120x120-45d5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\eroteks-p-z-supp-10-roza-sperko-ukraina-cart-120x120-45d5.jpg")))}"},
                new Medicine{ Name = "Фарматекс супозиторії вагінальні 18,9мг №10", Description = "Desc17", Category = categories[96], Manufacturer = manufacturers[58],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\farmateks-supp-vaginal-10-innotera-shuzi-cart-120x120-088a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\farmateks-supp-vaginal-10-innotera-shuzi-cart-120x120-088a.jpg")))}"},
                new Medicine{ Name = "Фарматекс таблетки вагінальні 20мг №12", Description = "Desc17", Category = categories[97], Manufacturer = manufacturers[58],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\farmateks-tabl-vaginal-20mg-12-innotera-shuzi-cart-120x120-d3e2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\farmateks-tabl-vaginal-20mg-12-innotera-shuzi-cart-120x120-d3e2.jpg")))}"},

                new Medicine{ Name = "Бромкриптин-КВ табл. 2,5мг №30", Description = "Desc17", Category = categories[98], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bromkriptin-kv-tabl-2-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-65f8.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bromkriptin-kv-tabl-2-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-65f8.jpg")))}"},
                new Medicine{ Name = "Бромокриптин-Ріхтер табл. 2,5мг №30", Description = "Desc17", Category = categories[98], Manufacturer = manufacturers[45],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\bromokriptin-rihter-tabl-2-5mg-30-gedeon-rihter-oao-cart-120x120-94ee.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\bromokriptin-rihter-tabl-2-5mg-30-gedeon-rihter-oao-cart-120x120-94ee.jpg")))}"},
               
                new Medicine{ Name = "Алактин табл. 0,5мг №2", Description = "Desc17", Category = categories[99], Manufacturer = manufacturers[59],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\alaktin-tabl-0-5mg-2-teva-cheh-indastriz-s-r-o-cart-120x120-bc40.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\alaktin-tabl-0-5mg-2-teva-cheh-indastriz-s-r-o-cart-120x120-bc40.jpg")))}"},
                new Medicine{ Name = "Достинекс табл. 0,5мг №2", Description = "Desc17", Category = categories[99], Manufacturer = manufacturers[54],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dostineks-tabl-0-5mg-2-pfayzer-italiya-s-r-l-cart-120x120-6bb3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dostineks-tabl-0-5mg-2-pfayzer-italiya-s-r-l-cart-120x120-6bb3.jpg")))}"},

                new Medicine{ Name = "БеларРигевідон табл. в/о №21а табл. в/о 2мг/0,03мг №63", Description = "Desc17", Category = categories[100], Manufacturer = manufacturers[43]},
                new Medicine{ Name = "Достинекс табл. 0,5мг №2", Description = "Desc17", Category = categories[100], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dostineks-tabl-0-5mg-2-pfayzer-italiya-s-r-l-cart-120x120-6bb3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dostineks-tabl-0-5mg-2-pfayzer-italiya-s-r-l-cart-120x120-6bb3.jpg")))}"},
                new Medicine{ Name = "Регулон табл. в/о 0,15мг/0,03мг №63", Description = "Desc17", Category = categories[100], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\regulon-tabl-p-o-0-15-mg-0-03-mg-63-gedeon-rihter-oao-cart-120x120-b3db.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\regulon-tabl-p-o-0-15-mg-0-03-mg-63-gedeon-rihter-oao-cart-120x120-b3db.jpg")))}"},
                new Medicine{ Name = "Вендіол табл. в/о 0,06мг/0,015мг №28", Description = "Desc17", Category = categories[100], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\img_0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\img_0.jpg")))}"},
                new Medicine{ Name = "Джаз табл. в/о №28", Description = "Desc17", Category = categories[100], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dzhaz-tabl-p-o-28-bayer-vaymar-gmbh-cart-120x120-977f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dzhaz-tabl-p-o-28-bayer-vaymar-gmbh-cart-120x120-977f.jpg")))}"},

                new Medicine{ Name = "Три-регол, табл. в/о №21", Description = "Desc17", Category = categories[101], Manufacturer = manufacturers[43]},

                new Medicine{ Name = "Депо-провера, суспензія д/ін. 150мг/мл 1мл фл.", Description = "Desc17", Category = categories[102], Manufacturer = manufacturers[54],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\82127_73_34_34_19.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\82127_73_34_34_19.jpg")))}"},
                new Medicine{ Name = "Лактинет-Ріхтер, табл. в/о 0,075мг №28", Description = "Desc17", Category = categories[102], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\117166_110_67_19_12.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\117166_110_67_19_12.jpg")))}"},

                new Medicine{ Name = "Постинор табл. 0,75мг №2", Description = "Desc17", Category = categories[103], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\postinor-tabl-0-75mg-2-gedeon-rihter-oao-cart-120x120-254a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\postinor-tabl-0-75mg-2-gedeon-rihter-oao-cart-120x120-254a.jpg")))}"},
                new Medicine{ Name = "Ескапел табл. 1,5мг №1", Description = "Desc17", Category = categories[103], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\eskapel-tabl-1-5mg-1-gedeon-rihter-oao-cart-120x120-e765.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\eskapel-tabl-1-5mg-1-gedeon-rihter-oao-cart-120x120-e765.jpg")))}"},

                new Medicine{ Name = "Небідо р-н д/ін. 250мг/мл 4мл амп. №1", Description = "Desc17", Category = categories[104], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nebido-r-r-d-in-250mg-ml-fl-4ml-1-bayer-ag-cart-120x120-119d.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nebido-r-r-d-in-250mg-ml-fl-4ml-1-bayer-ag-cart-120x120-119d.jpg")))}"},
                new Medicine{ Name = "Тестостерону пропіонат р-н д/ін. 5% 1мл амп. №5 (Фармак)", Description = "Desc17", Category = categories[104], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\testosterona-propionat-r-r-d-in-5-amp-1ml-5-pao-farmak-cart-120x120-8e62.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\testosterona-propionat-r-r-d-in-5-amp-1ml-5-pao-farmak-cart-120x120-8e62.jpg")))}"},
                
                new Medicine{ Name = "Провірон табл. 25мг №20", Description = "Desc17", Category = categories[105], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\proviron-tabl-25mg-20-bayer-vaymar-gmbh-cart-120x120-86c9.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\proviron-tabl-25mg-20-bayer-vaymar-gmbh-cart-120x120-86c9.jpg")))}"},
                
                new Medicine{ Name = "Лензетто спрей трансдермальний, р-н 1,53мг/дозу (56доз) фл.", Description = "Desc17", Category = categories[106], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lenzetto-sprey-transderm-r-r-1-53mg-doza-fl-8-1ml-56doz-gedeon-rihter-rumyiniya-a-t-cart-120x120-e125.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lenzetto-sprey-transderm-r-r-1-53mg-doza-fl-8-1ml-56doz-gedeon-rihter-rumyiniya-a-t-cart-120x120-e125.jpg")))}"},
                
                new Medicine{ Name = "Клімен табл. в/о №21", Description = "Desc17", Category = categories[107], Manufacturer = manufacturers[60],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klimen-tabl-p-o-21-delfarm-lill-s-a-s-cart-120x120-3028.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klimen-tabl-p-o-21-delfarm-lill-s-a-s-cart-120x120-3028.jpg")))}"},

                new Medicine{ Name = "Інжеста капс. 100мг №30", Description = "Desc17", Category = categories[108], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\inzhesta-kaps-myagkie-100mg-30-pao-farmak-cart-120x120-e3ac.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\inzhesta-kaps-myagkie-100mg-30-pao-farmak-cart-120x120-e3ac.jpg")))}"},
                new Medicine{ Name = "Інжеста капс. 200мг №20", Description = "Desc17", Category = categories[108], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\inzhesta-kaps-myagkie-200mg-20-pao-farmak-cart-120x120-db98.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\inzhesta-kaps-myagkie-200mg-20-pao-farmak-cart-120x120-db98.jpg")))}"},

                new Medicine{ Name = "Дуфастон табл. в/о 10мг №14", Description = "Desc17", Category = categories[109], Manufacturer = manufacturers[30],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dufaston-tabl-p-o-10mg-14-abbott-biolodzhikalz-b-v-cart-120x120-433b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dufaston-tabl-p-o-10mg-14-abbott-biolodzhikalz-b-v-cart-120x120-433b.jpg")))}"},
                new Medicine{ Name = "Візан табл. 2мг №28", Description = "Desc17", Category = categories[109], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\vizan-tabl-2mg-28-bayer-vaymar-gmbh-cart-120x120-e547.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\vizan-tabl-2mg-28-bayer-vaymar-gmbh-cart-120x120-e547.jpg")))}"},

                new Medicine{ Name = "Норколут табл. 5мг №20", Description = "Desc17", Category = categories[110], Manufacturer = manufacturers[30],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\norkolut-tabl-5mg-20-gedeon-rihter-oao-cart-120x120-f4b0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\norkolut-tabl-5mg-20-gedeon-rihter-oao-cart-120x120-f4b0.jpg")))}"},
                new Medicine{ Name = "Примолют-Нор, табл. 5мг №30", Description = "Desc17", Category = categories[110], Manufacturer = manufacturers[56],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\97487_86_36_33_20.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\97487_86_36_33_20.jpg")))}"},

                new Medicine{ Name = "Уронефрон табл. в/о №60", Description = "Desc17", Category = categories[111], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\uronefron-tabl-p-o-60-pao-farmak-cart-120x120-97f1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\uronefron-tabl-p-o-60-pao-farmak-cart-120x120-97f1.jpg")))}"},
                new Medicine{ Name = "Фітоліт капс. №60", Description = "Desc17", Category = categories[111], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\fitolit-kaps-60-tov-farm-kompaniya-zdorove-cart-120x120-ed67.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\fitolit-kaps-60-tov-farm-kompaniya-zdorove-cart-120x120-ed67.jpg")))}"},

                new Medicine{ Name = "Нефрокеа табл. №20", Description = "Desc17", Category = categories[112], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tabletki-dlya-profilaktiki-i-kompleksnogo-lecheniya-zabolevaniy-mochepolovoy-sistemyi-nefrokea-2-blistera-po-10-sht-eckhart-corp-cart-120x120-36ea.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tabletki-dlya-profilaktiki-i-kompleksnogo-lecheniya-zabolevaniy-mochepolovoy-sistemyi-nefrokea-2-blistera-po-10-sht-eckhart-corp-cart-120x120-36ea.jpg")))}"},
               
                new Medicine{ Name = "Сибутін табл. 5мг №30", Description = "Desc17", Category = categories[113], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\sibutin-tabl-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-b5db.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\sibutin-tabl-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-b5db.jpg")))}"},

                new Medicine{ Name = "Нігісем табл. в/о 5мг №30", Description = "Desc17", Category = categories[114], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nigisem-tabl-p-o-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-ddbd.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nigisem-tabl-p-o-5mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-ddbd.jpg")))}"},
                new Medicine{ Name = "Соліцин табл. в/о 5мг №30", Description = "Desc17", Category = categories[114], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\solitsin-tabl-p-o-5mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-df3a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\solitsin-tabl-p-o-5mg-30-pao-nvts-borschagovskiy-hfz-cart-120x120-df3a.jpg")))}"},

                new Medicine{ Name = "Генотропін порошок ліофілізований д/р-ру д/ін. 36 МО (12мг) з розчинником в картриджі, шприц-ручка №1", Description = "Desc17", Category = categories[115], Manufacturer = manufacturers[54],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\genotropin-por-i-rastv-d-r-ra-d-in-36me-12mg-katr-v-ruchke-1-pfayzer-menyufekchuring-belgiya-nv-cart-120x120-ec85.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\genotropin-por-i-rastv-d-r-ra-d-in-36me-12mg-katr-v-ruchke-1-pfayzer-menyufekchuring-belgiya-nv-cart-120x120-ec85.jpg")))}"},
               
                new Medicine{ Name = "Уропрес краплі назальні 0,1мг/мл 2,5мл фл.", Description = "Desc17", Category = categories[116], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\uropres-kap-nazal-0-1mg-ml-fl-2-5ml-pao-farmak-cart-120x120-0832.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\uropres-kap-nazal-0-1mg-ml-fl-2-5ml-pao-farmak-cart-120x120-0832.jpg")))}"},

                new Medicine{ Name = "Реместип р-н д/ін. 0,1мг/мл 2мл амп. №5", Description = "Desc17", Category = categories[117], Manufacturer = manufacturers[61],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\remestip-r-r-d-in-0-1mg-ml-amp-2ml-5-zentiva-ooo-cart-120x120-c39b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\remestip-r-r-d-in-0-1mg-ml-amp-2ml-5-zentiva-ooo-cart-120x120-c39b.jpg")))}"},

                new Medicine{ Name = "Дезаміноокситоцин табл. 50 МЕ №10", Description = "Desc17", Category = categories[118], Manufacturer = manufacturers[62],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dezaminooksitotsin-tabl-50me-10-grindeks-ao-cart-120x120-3803.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dezaminooksitotsin-tabl-50me-10-grindeks-ao-cart-120x120-3803.jpg")))}"},
                new Medicine{ Name = "Окситоцин р-н д/ін. 5 МО/мл 1мл амп. №5 (Гедеон)", Description = "Desc17", Category = categories[119], Manufacturer = manufacturers[62],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\oksitotsin-r-r-d-in-5-me-ml-amp-1ml-5-gedeon-rihter-oao-cart-120x120-c5a0.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\oksitotsin-r-r-d-in-5-me-ml-amp-1ml-5-gedeon-rihter-oao-cart-120x120-c5a0.jpg")))}"},
                new Medicine{ Name = "Пабал р-н д/ін. 100мкг/мл 1мл амп. №5", Description = "Desc17", Category = categories[120], Manufacturer = manufacturers[61]},

                new Medicine{ Name = "Октра р-н д/ін. 0,1мг/мл 1мл амп. №5", Description = "Desc17", Category = categories[121], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\oktra-r-r-d-in-0-1mg-ml-amp-1ml-5-pao-farmak-cart-120x120-db23.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\oktra-r-r-d-in-0-1mg-ml-amp-1ml-5-pao-farmak-cart-120x120-db23.jpg")))}"},
                new Medicine{ Name = "Соматулін Аутожель 120мг, р-н д/ін. 120мг/0,5мл шприц №1", Description = "Desc17", Category = categories[122], Manufacturer = manufacturers[63],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\somatulin-autozhel-120mg-r-r-d-in-prol-120mg-shprits-0-5ml-1-igla-ipsen-farma-biotek-cart-120x120-35ae.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\somatulin-autozhel-120mg-r-r-d-in-prol-120mg-shprits-0-5ml-1-igla-ipsen-farma-biotek-cart-120x120-35ae.jpg")))}"},
                new Medicine{ Name = "Кортінефф табл. 0,1мг №20 фл.", Description = "Desc17", Category = categories[123], Manufacturer = manufacturers[64],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kortineff-tabl-0-1mg-20-adamed-farma-s-a-cart-120x120-0b62.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kortineff-tabl-0-1mg-20-adamed-farma-s-a-cart-120x120-0b62.jpg")))}"},

                new Medicine{ Name = "Бетаспан р-н д/ін. 4мг/мл 1мл амп. №5", Description = "Desc17", Category = categories[124], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\betaspan-r-r-d-in-4mg-ml-amp-1ml-5-pao-farmak-cart-120x120-b29f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\betaspan-r-r-d-in-4mg-ml-amp-1ml-5-pao-farmak-cart-120x120-b29f.jpg")))}"},
                new Medicine{ Name = "Депос суспензія д/ін. 1мл шприц №1", Description = "Desc17", Category = categories[124], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\depos-susp-d-in-shprits-1ml-1-pao-farmak-cart-120x120-5397.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\depos-susp-d-in-shprits-1ml-1-pao-farmak-cart-120x120-5397.jpg")))}"},
                new Medicine{ Name = "Флостерон суспензія д/ін. 1мл амп. №5", Description = "Desc17", Category = categories[124], Manufacturer = manufacturers[65],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\flosteron-susp-d-in-amp-1ml-5-krka-d-d-cart-120x120-0efb.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\flosteron-susp-d-in-amp-1ml-5-krka-d-d-cart-120x120-0efb.jpg")))}"},
                new Medicine{ Name = "Дексаметазон р-н д/ін. 4мг/1мл 1мл амп. №25", Description = "Desc17", Category = categories[124], Manufacturer = manufacturers[65],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\deksametazon-r-r-d-in-4mg-ml-amp-1ml-25-krka-d-d-cart-120x120-2149.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\deksametazon-r-r-d-in-4mg-ml-amp-1ml-25-krka-d-d-cart-120x120-2149.jpg")))}"},
                new Medicine{ Name = "Дексаметазону фосфат, р-н д/ін. 4мг/мл 1мл амп. №10 (Фармак)", Description = "Desc17", Category = categories[124], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\deksametazona-fosfat-r-r-d-in-4mg-ml-amp-1ml-10-pao-farmak-cart-120x120-4dbd.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\deksametazona-fosfat-r-r-d-in-4mg-ml-amp-1ml-10-pao-farmak-cart-120x120-4dbd.jpg")))}"},
                new Medicine{ Name = "L-тироксин 100 Берлін-Хемі табл. 100мкг №50", Description = "Desc17", Category = categories[125], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg")))}"},
                new Medicine{ Name = "L-тироксин 150 Берлін-Хемі табл. 150мкг №50", Description = "Desc17", Category = categories[125], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg")))}"},
                new Medicine{ Name = "L-тироксин 50 Берлін-Хемі, табл. 50мкг №50", Description = "Desc17", Category = categories[125], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\berlin_chemie_l_tiroksin.jpg")))}"},
                new Medicine{ Name = "Еспа-карб табл. 10мг №50", Description = "Desc17", Category = categories[126], Manufacturer = manufacturers[66],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\espa-karb-tabl-10mg-50-lindofarm-gmbh-cart-120x120-4742.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\espa-karb-tabl-10mg-50-lindofarm-gmbh-cart-120x120-4742.jpg")))}"},
                new Medicine{ Name = "Мерказоліл-Здоров'я, табл. 5мг №100", Description = "Desc17", Category = categories[127], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\37494_60_37_37_26.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\37494_60_37_37_26.jpg")))}"},
                new Medicine{ Name = "Йодомарин 200, табл. 200мкг №50", Description = "Desc17", Category = categories[128], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\yodomarin-tabl-200mkg-50-berlin-hemi-ag-cart-120x120-cf5b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\yodomarin-tabl-200mkg-50-berlin-hemi-ag-cart-120x120-cf5b.jpg")))}"},

                new Medicine{ Name = "Доксициклін-БХФЗ, капс. 100мг №10", Description = "Desc17", Category = categories[132], Manufacturer = manufacturers[23]},
                new Medicine{ Name = "Доксициклін-Дарниця, капс. 100мг №10", Description = "Desc17", Category = categories[132], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\49932_80_60_25_17.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\49932_80_60_25_17.jpg")))}"},
                new Medicine{ Name = "Тетрацикліну гідрохлорид табл. в/о 100мг №20 (БХФЗ)", Description = "Desc17", Category = categories[133], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tetratsiklina-g-h-tabl-p-o-100mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-fc1a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tetratsiklina-g-h-tabl-p-o-100mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-fc1a.jpg")))}"},
                new Medicine{ Name = "Левоміцетин-Дарниця, табл. 500мг №10", Description = "Desc17", Category = categories[134], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\7401_95_40_7_3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\7401_95_40_7_3.jpg")))}"},
                new Medicine{ Name = "Оспамокс ДТ, табл. 1000мг №20", Description = "Desc17", Category = categories[135], Manufacturer = manufacturers[47],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ospamoks-dt-tabl-disperg-1000mg-20-sandoz-gmbh-tehops-cart-120x120-3536.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ospamoks-dt-tabl-disperg-1000mg-20-sandoz-gmbh-tehops-cart-120x120-3536.jpg")))}"},
                new Medicine{ Name = "Пеніцилін G натрієва сіль Сандоз, порошок д/р-ну д/ін. 1000000МО фл. №100", Description = "Desc17", Category = categories[136], Manufacturer = manufacturers[47],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\penitsillin-g-na-sol-sandoz-por-d-r-ra-d-in-1mln-me-100-sandoz-gmbh-tehops-cart-120x120-b18e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\penitsillin-g-na-sol-sandoz-por-d-r-ra-d-in-1mln-me-100-sandoz-gmbh-tehops-cart-120x120-b18e.jpg")))}"},
                new Medicine{ Name = "Амоксиклав порошок д/р-ну д/ін. 1000мг/200мг фл. №5", Description = "Desc17", Category = categories[138], Manufacturer = manufacturers[47],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\amoksiklav-por-d-r-ra-d-in-1000mg-200mg-fl-5-sandoz-gmbh-tehops-cart-120x120-0634.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\amoksiklav-por-d-r-ra-d-in-1000mg-200mg-fl-5-sandoz-gmbh-tehops-cart-120x120-0634.jpg")))}"},
                new Medicine{ Name = "Стрептоцид табл. 0,5г №20 (Монфарм)", Description = "Desc17", Category = categories[139], Manufacturer = manufacturers[67],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\streptotsid-tabl-0-5g-10-monfarm-chao-cart-120x120-0a0f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\streptotsid-tabl-0-5g-10-monfarm-chao-cart-120x120-0a0f.jpg")))}"},
                new Medicine{ Name = "Еритроміцин табл. 100мг №20 (БХФЗ)", Description = "Desc17", Category = categories[141], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\eritromitsin-tabl-100mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-aa11.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\eritromitsin-tabl-100mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-aa11.jpg")))}"},
                new Medicine{ Name = "Дораміцин табл. в/о 3000000 МО №10 (10 х1)", Description = "Desc17", Category = categories[141], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\doramitsin-tabl-p-o-3-000-000-me-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-e25e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\doramitsin-tabl-p-o-3-000-000-me-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-e25e.jpg")))}"},
                new Medicine{ Name = "Далацин Ц Фосфат, р-н д/ін. 150мг/мл 4мл (600мг) амп. №1", Description = "Desc17", Category = categories[142], Manufacturer = manufacturers[54],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dalatsin-ts-fosfat-r-r-d-in-150mg-ml-amp-4ml-1-pfayzer-menyufekchuring-belgiya-nv-cart-120x120-8235.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dalatsin-ts-fosfat-r-r-d-in-150mg-ml-amp-4ml-1-pfayzer-menyufekchuring-belgiya-nv-cart-120x120-8235.jpg")))}"},
                new Medicine{ Name = "Браксон р-н д/ін. 40мг/мл 2мл амп. №10", Description = "Desc17", Category = categories[144], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\brakson-r-r-d-in-40mg-ml-amp-2ml-10-yuriya-farm-ooo-cart-120x120-d01b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\brakson-r-r-d-in-40mg-ml-amp-2ml-10-yuriya-farm-ooo-cart-120x120-d01b.jpg")))}"},
                new Medicine{ Name = "Гентаміцину сульфат-Дарниця, р-н д/ін. 40мг/мл 2мл амп. №10", Description = "Desc17", Category = categories[145], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gentamitsina-sulfat-darnitsa-r-r-d-in-40mg-ml-amp-2ml-10-darnitsa-chao-farm-firma-cart-120x120-8a0f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gentamitsina-sulfat-darnitsa-r-r-d-in-40mg-ml-amp-2ml-10-darnitsa-chao-farm-firma-cart-120x120-8a0f.jpg")))}"},
                new Medicine{ Name = "Флапрокс табл. в/о 500мг №10", Description = "Desc17", Category = categories[148], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\flaproks-tabl-p-o-500mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-eae4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\flaproks-tabl-p-o-500mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-eae4.jpg")))}"},
                new Medicine{ Name = "Офлоксацин-Дарниця, табл. 200мг №10", Description = "Desc17", Category = categories[148], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\20944_102_44_16_11.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\20944_102_44_16_11.jpg")))}"},

                new Medicine{ Name = "Орципол табл. в/о №10", Description = "Desc17", Category = categories[149], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ortsipol-tabl-p-o-10-laboratoriya-beyli-kreat-vernuye-cart-120x120-cc9e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ortsipol-tabl-p-o-10-laboratoriya-beyli-kreat-vernuye-cart-120x120-cc9e.jpg")))}"},
                new Medicine{ Name = "Грандазол р-н д/інф. 5мг/2,5мг/мл (1000мг/500мг) 200мл фл.", Description = "Desc17", Category = categories[150], Manufacturer = manufacturers[25],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\grandazol-r-r-d-inf-5mg-2-5-mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-5562.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\grandazol-r-r-d-inf-5mg-2-5-mg-ml-but-200ml-yuriya-farm-ooo-cart-120x120-5562.jpg")))}"},
                new Medicine{ Name = "Ванкоміцин-Фармекс, ліофілізат д/р-ну д/інф. 1000мг фл.", Description = "Desc17", Category = categories[152], Manufacturer = manufacturers[69],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\272986_83_55_50_38.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\272986_83_55_50_38.jpg")))}"},
                new Medicine{ Name = "Фурагін табл. 50мг №30 (Артеріум)", Description = "Desc17", Category = categories[155], Manufacturer = manufacturers[70],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\3599_105_44_24_13.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\3599_105_44_24_13.jpg")))}"},
                new Medicine{ Name = "Метронідазол-Здоров'я табл. 250мг №20", Description = "Desc17", Category = categories[157], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\metronidazol-zdorove-tabl-250mg-20-tov-farm-kompaniya-zdorove-cart-120x120-e881.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\metronidazol-zdorove-tabl-250mg-20-tov-farm-kompaniya-zdorove-cart-120x120-e881.jpg")))}"},
                 
                
                new Medicine{ Name = "Дифлазон капс. 50мг №7", Description = "Desc17", Category = categories[158], Manufacturer = manufacturers[65],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\diflazon-kaps-50mg-7-krka-d-d-cart-120x120-8ddc.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\diflazon-kaps-50mg-7-krka-d-d-cart-120x120-8ddc.jpg")))}"},
                new Medicine{ Name = "Флузамед капс. 150мг №1 (Уорлд)", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\fluzamed-kaps-150mg-1-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-5fd5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\fluzamed-kaps-150mg-1-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-5fd5.jpg")))}"},
                new Medicine{ Name = "Рифампицин капс. 150мг №20 (БХФЗ)", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\rifampitsin-kaps-150mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-e8d4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\rifampitsin-kaps-150mg-20-pao-nvts-borschagovskiy-hfz-cart-120x120-e8d4.jpg")))}"},
                new Medicine{ Name = "Изониазид табл. 200мг №50 (БХФЗ)", Description = "Desc17", Category = categories[161], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\izoniazid-tabl-200mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-a1a5.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\izoniazid-tabl-200mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-a1a5.jpg")))}"},
                new Medicine{ Name = "Изониазид-Дарниця, табл. 300мг №50", Description = "Desc17", Category = categories[161], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\29313_103_43_30_32.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\29313_103_43_30_32.jpg")))}"},
                new Medicine{ Name = "Пиразинамід табл. 500мг №50 (БХФЗ)", Description = "Desc17", Category = categories[162], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pirazinamid-tabl-500mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-b5ff.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pirazinamid-tabl-500mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-b5ff.jpg")))}"},
                new Medicine{ Name = "Этамбутол табл. 400мг №50 (БХФЗ)", Description = "Desc17", Category = categories[162], Manufacturer = manufacturers[23],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\etambutol-tabl-400mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-2e55.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\etambutol-tabl-400mg-50-pao-nvts-borschagovskiy-hfz-cart-120x120-2e55.jpg")))}"},
               
                new Medicine{ Name = "Ацикловір-фармак табл. 200мг №20", Description = "Desc17", Category = categories[163], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\atsiklovir-farmak-tabl-200mg-20-pao-farmak-cart-120x120-4ae6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\atsiklovir-farmak-tabl-200mg-20-pao-farmak-cart-120x120-4ae6.jpg")))}"},
                new Medicine{ Name = "Рибавирин-Астрафарм, капс. 200мг №60", Description = "Desc17", Category = categories[164], Manufacturer = manufacturers[71],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\94727_100_60_53_48.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\94727_100_60_53_48.jpg")))}"},
                new Medicine{ Name = "Виракса в табл. п/о 250мг №21", Description = "Desc17", Category = categories[165], Manufacturer = manufacturers[72],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\viraksa-tabl-p-o-250mg-21-spesifar-s-a-cart-120x120-c7a3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\viraksa-tabl-p-o-250mg-21-spesifar-s-a-cart-120x120-c7a3.jpg")))}"},
                new Medicine{ Name = "Валавір табл. п/о 500мг №42", Description = "Desc17", Category = categories[166], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\valavir-tabl-p-o-500mg-42-pao-farmak-cart-120x120-1474.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\valavir-tabl-p-o-500mg-42-pao-farmak-cart-120x120-1474.jpg")))}"},
                
                new Medicine{ Name = "Римантадин-Дарница, табл. 50мг №20", Description = "Desc17", Category = categories[167], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\47981_102_45_16_12.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\47981_102_45_16_12.jpg")))}"},
                new Medicine{ Name = "Тамифлю капс. 75мг №10", Description = "Desc17", Category = categories[168], Manufacturer = manufacturers[73],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tamiflyu-kaps-75mg-10-f-hoffmann-lya-rosh-ltd-cart-120x120-3f55.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tamiflyu-kaps-75mg-10-f-hoffmann-lya-rosh-ltd-cart-120x120-3f55.jpg")))}"},
                new Medicine{ Name = "Тамифлю порошок для оральной суспензии 6мг/мл 13г фл.", Description = "Desc17", Category = categories[168], Manufacturer = manufacturers[73],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tamiflyu-por-d-oral-susp-6mg-ml-but-13g-f-hoffmann-lya-rosh-ltd-cart-120x120-1e95.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tamiflyu-por-d-oral-susp-6mg-ml-but-13g-f-hoffmann-lya-rosh-ltd-cart-120x120-1e95.jpg")))}"},
                new Medicine{ Name = "Гропивирин табл. 500мг №50", Description = "Desc17", Category = categories[169], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\gropivirin-tabl-500mg-50-pao-farmak-cart-120x120-a7c1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\gropivirin-tabl-500mg-50-pao-farmak-cart-120x120-a7c1.jpg")))}"},
                new Medicine{ Name = "Гропринозин табл. 500мг №20", Description = "Desc17", Category = categories[169], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\groprinozin-tabl-500mg-50-gedeon-rihter-polsha-ooo-cart-120x120-531a.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\groprinozin-tabl-500mg-50-gedeon-rihter-polsha-ooo-cart-120x120-531a.jpg")))}"},
                new Medicine{ Name = "Амізон табл. п/о 0,25г №20", Description = "Desc17", Category = categories[170], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\amizon-tabl-p-o-0-25g-20-pao-farmak-cart-120x120-fc68.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\amizon-tabl-p-o-0-25g-20-pao-farmak-cart-120x120-fc68.jpg")))}"},
                new Medicine{ Name = "Амізончик сироп 10мг/мл, 100мл фл.", Description = "Desc17", Category = categories[170], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\amizonchik-sirop-10mg-ml-fl-100ml-pao-farmak-cart-120x120-ed93.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\amizonchik-sirop-10mg-ml-fl-100ml-pao-farmak-cart-120x120-ed93.jpg")))}"},
                new Medicine{ Name = "Арбивір-Здоров'я Форте, капс. 200мг №10", Description = "Desc17", Category = categories[171], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\arbivir-zdorove-forte-kaps-200mg-10-tov-farm-kompaniya-zdorove-cart-120x120-8d45.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\arbivir-zdorove-forte-kaps-200mg-10-tov-farm-kompaniya-zdorove-cart-120x120-8d45.jpg")))}"},
                new Medicine{ Name = "Іммустат табл. п/о 100мг №10", Description = "Desc17", Category = categories[171], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\immustat-tabl-p-o-100mg-10-darnitsa-chao-farm-firma-cart-120x120-9990.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\immustat-tabl-p-o-100mg-10-darnitsa-chao-farm-firma-cart-120x120-9990.jpg")))}"},
                new Medicine{ Name = "Аміксин ИС, табл. п/о 0,125г №10", Description = "Desc17", Category = categories[172], Manufacturer = manufacturers[74],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\amiksin-ic-tabl-p-o-0-125g-10-interhim-odo-cart-120x120-299b.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\amiksin-ic-tabl-p-o-0-125g-10-interhim-odo-cart-120x120-299b.jpg")))}"},
                new Medicine{ Name = "Ковіфлу табл. п/о 200мг №34", Description = "Desc17", Category = categories[173], Manufacturer = manufacturers[48],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\koviflu-tabl-p-o-200mg-34-glenmark-farmasyutikalz-ltd-cart-120x120-6b1f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\koviflu-tabl-p-o-200mg-34-glenmark-farmasyutikalz-ltd-cart-120x120-6b1f.jpg")))}"},
                new Medicine{ Name = "Вермокс табл. 100мг №6 (Гедеон)", Description = "Desc17", Category = categories[174], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\vermoks-tabl-100mg-6-gedeon-rihter-oao-cart-120x120-cd15.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\vermoks-tabl-100mg-6-gedeon-rihter-oao-cart-120x120-cd15.jpg")))}"},
                new Medicine{ Name = "Альбелия табл. 400мг №3", Description = "Desc17", Category = categories[175], Manufacturer = manufacturers[15]},
                new Medicine{ Name = "Пірантел суспензия оральная 250мг/5мл 15мл фл. (Кусум)", Description = "Desc17", Category = categories[176], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\pirantel-polfarma-susp-oraln-250mg-5ml-fl-15ml-medana-farma-ao-cart-120x120-3fcc.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\pirantel-polfarma-susp-oraln-250mg-5ml-fl-15ml-medana-farma-ao-cart-120x120-3fcc.jpg")))}"},
                new Medicine{ Name = "Левамизол-Здоров'я табл. 150мг №1", Description = "Desc17", Category = categories[177], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\levamizol-zdorove-tabl-150mg-1-tov-farm-kompaniya-zdorove-cart-120x120-e5e6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\levamizol-zdorove-tabl-150mg-1-tov-farm-kompaniya-zdorove-cart-120x120-e5e6.jpg")))}"},
                new Medicine{ Name = "Декарис табл. 150мг №1", Description = "Desc17", Category = categories[177], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\dekaris-tabl-150mg-1-gedeon-rihter-oao-cart-120x120-f44f.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\dekaris-tabl-150mg-1-gedeon-rihter-oao-cart-120x120-f44f.jpg")))}"},
                new Medicine{ Name = "Перметрин спрей 0,5% 50г фл.", Description = "Desc17", Category = categories[178], Manufacturer = manufacturers[75],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\permetrin-sprey-0-5-ballon-50g-stoma-ao-cart-120x120-e7e4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\permetrin-sprey-0-5-ballon-50g-stoma-ao-cart-120x120-e7e4.jpg")))}"},
                new Medicine{ Name = "Бензилбензоат крем 250мг/г 40г", Description = "Desc17", Category = categories[179], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\benzilbenzoat-krem-25-tuba-40g-pao-farmak-cart-120x120-4eed.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\benzilbenzoat-krem-25-tuba-40g-pao-farmak-cart-120x120-4eed.jpg")))}"},
                new Medicine{ Name = "Бензилбензоат-Дарниця, мазь 250мг/г 30г", Description = "Desc17", Category = categories[179], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\benzilbenzoat-darnitsa-maz-250-mg-g-tuba-30g-darnitsa-chao-farm-firma-cart-120x120-1541 (1).jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\benzilbenzoat-darnitsa-maz-250-mg-g-tuba-30g-darnitsa-chao-farm-firma-cart-120x120-1541 (1).jpg")))}"},
               
                
                new Medicine{ Name = "Клодифен р-н д/ін. 25мг/мл 3мл амп. №5", Description = "Desc17", Category = categories[180], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klodifen-r-r-d-in-25mg-ml-amp-3ml-5-mefar-ilach-san-a-sh-cart-120x120-4857.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klodifen-r-r-d-in-25mg-ml-amp-3ml-5-mefar-ilach-san-a-sh-cart-120x120-4857.jpg")))}"},
                new Medicine{ Name = "Медролгін р-н д/ін. 30мг/мл 1мл амп. №5", Description = "Desc17", Category = categories[180], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\medrolgin-r-r-d-in-30mg-ml-amp-1ml-5-farmavizhn-san-ve-tidzh-a-sh-cart-120x120-9103.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\medrolgin-r-r-d-in-30mg-ml-amp-1ml-5-farmavizhn-san-ve-tidzh-a-sh-cart-120x120-9103.jpg")))}"},
                new Medicine{ Name = "Індометацин-Здоров'я табл. в/о 25мг №30", Description = "Desc17", Category = categories[180], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\indometatsin-zdorove-tabl-p-o-25mg-upak-30-tov-farm-kompaniya-zdorove-cart-120x120-d3c8.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\indometatsin-zdorove-tabl-p-o-25mg-upak-30-tov-farm-kompaniya-zdorove-cart-120x120-d3c8.jpg")))}"},
                new Medicine{ Name = "Диклофенак-Дарниця р-н д/ін. 25 мг/мл 3мл амп. №5", Description = "Desc17", Category = categories[180], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\diklofenak-darnitsa-r-r-d-in-25mg-ml-amp-3ml-10-darnitsa-chao-farm-firma-cart-120x120-6190.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\diklofenak-darnitsa-r-r-d-in-25mg-ml-amp-3ml-10-darnitsa-chao-farm-firma-cart-120x120-6190.jpg")))}"},
                new Medicine{ Name = "Диклокаїн р-н д/ін. 2мл амп. №10", Description = "Desc17", Category = categories[181], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\diklokain-r-r-d-in-amp-2ml-10-tov-farm-kompaniya-zdorove-cart-120x120-1052.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\diklokain-r-r-d-in-amp-2ml-10-tov-farm-kompaniya-zdorove-cart-120x120-1052.jpg")))}"},
                new Medicine{ Name = "Фаніган табл. №100", Description = "Desc17", Category = categories[181], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\fanigan-tabl-100-indiya-kusum-helthker-pvt-ltd-cart-120x120-f1a3.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\fanigan-tabl-100-indiya-kusum-helthker-pvt-ltd-cart-120x120-f1a3.jpg")))}"},
                new Medicine{ Name = "Артоксан табл. в/о 20мг №10", Description = "Desc17", Category = categories[182], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\artoksan-tabl-p-o-20mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-d8d7.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\artoksan-tabl-p-o-20mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-d8d7.jpg")))}"},
                new Medicine{ Name = "Артоксан ліофілізат д/р-ну д/ін. 20мг фл. №3", Description = "Desc17", Category = categories[182], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\artoksan-liof-d-r-ra-d-in-20mg-fl-r-l-amp-2ml-3-mefar-ilach-san-a-sh-cart-120x120-45d2.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\artoksan-liof-d-r-ra-d-in-20mg-fl-r-l-amp-2ml-3-mefar-ilach-san-a-sh-cart-120x120-45d2.jpg")))}"},
                new Medicine{ Name = "Ксефокам пор. д/р-ну д/ін. 8мг фл. №5", Description = "Desc17", Category = categories[183], Manufacturer = manufacturers[55],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ksefokam-por-d-r-nu-d-in-8mg-fl-5-takeda-avstriya-gmbh-cart-120x120-9768.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ksefokam-por-d-r-nu-d-in-8mg-fl-5-takeda-avstriya-gmbh-cart-120x120-9768.jpg")))}"},
                new Medicine{ Name = "Лорнадо ліофілізат для р-ну д/ін. по 8мг фл. №3 з розчинником", Description = "Desc17", Category = categories[183], Manufacturer = manufacturers[12],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\lornado-liof-d-r-ra-d-in-8mg-fl-r-l-amp-2ml-3-mefar-ilach-san-a-sh-cart-120x120-12d6.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\lornado-liof-d-r-ra-d-in-8mg-fl-r-l-amp-2ml-3-mefar-ilach-san-a-sh-cart-120x120-12d6.jpg")))}"},
                new Medicine{ Name = "Локсидол р-н д/ін. 15мг/1,5мл 1,5мл амп. №3", Description = "Desc17", Category = categories[184], Manufacturer = manufacturers[76],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\loksidol-r-r-d-in-15mg-1-5ml-amp-1-5ml-3-romfarm-kompani-k-o-cart-120x120-0723.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\loksidol-r-r-d-in-15mg-1-5ml-amp-1-5ml-3-romfarm-kompani-k-o-cart-120x120-0723.jpg")))}"},
                new Medicine{ Name = "Локсидол табл. 15мг №10", Description = "Desc17", Category = categories[184], Manufacturer = manufacturers[76],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\loksidol-tabl-15mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-c585.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\loksidol-tabl-15mg-10-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-c585.jpg")))}"},
                new Medicine{ Name = "Ревмоксикам р-н д/ін. 1% 1,5мл амп. №3", Description = "Desc17", Category = categories[184], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\revmoksikam-r-r-d-in-1-amp-1-5ml-5-pao-farmak-cart-120x120-e919.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\revmoksikam-r-r-d-in-1-amp-1-5ml-5-pao-farmak-cart-120x120-e919.jpg")))}"},
                new Medicine{ Name = "Ревмоксикам табл. 15мг №10", Description = "Desc17", Category = categories[184], Manufacturer = manufacturers[5],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\revmoksikam-tabl-15mg-10-pao-farmak-cart-120x120-34ac.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\revmoksikam-tabl-15mg-10-pao-farmak-cart-120x120-34ac.jpg")))}"},
                new Medicine{ Name = "Ібупрофен-Дарниця табл. 200мг №50", Description = "Desc17", Category = categories[185], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ibuprofen-darnitsa-tabl-200mg-50-darnitsa-chao-farm-firma-cart-120x120-5f74.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ibuprofen-darnitsa-tabl-200mg-50-darnitsa-chao-farm-firma-cart-120x120-5f74.jpg")))}"},
                new Medicine{ Name = "Імет табл. в/о 400мг №20", Description = "Desc17", Category = categories[185], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\imet-tabl-p-o-400mg-20-berlin-hemi-ag-cart-120x120-e869.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\imet-tabl-p-o-400mg-20-berlin-hemi-ag-cart-120x120-e869.jpg")))}"},
                new Medicine{ Name = "Напрофф табл. в/о 550мг №10", Description = "Desc17", Category = categories[185], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\naproff-tabl-p-o-550mg-10-biofarma-ilach-san-ve-tidzh-a-sh-cart-120x120-2b69.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\naproff-tabl-p-o-550mg-10-biofarma-ilach-san-ve-tidzh-a-sh-cart-120x120-2b69.jpg")))}"},
                new Medicine{ Name = "Налгезін форте табл. в/о 550мг №10", Description = "Desc17", Category = categories[185], Manufacturer = manufacturers[65],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nalgezin-forte-tabl-p-o-550mg-10-krka-d-d-cart-120x120-6153.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nalgezin-forte-tabl-p-o-550mg-10-krka-d-d-cart-120x120-6153.jpg")))}"},
                new Medicine{ Name = "Купреніл табл. в/о 250мг №100", Description = "Desc17", Category = categories[186], Manufacturer = manufacturers[9],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\kuprenil-tabl-p-o-250mg-100-teva-opereyshnz-poland-ooo-cart-120x120-4396.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\kuprenil-tabl-p-o-250mg-100-teva-opereyshnz-poland-ooo-cart-120x120-4396.jpg")))}"},
                new Medicine{ Name = "Кеторол Гель, гель 2% 30г", Description = "Desc17", Category = categories[187], Manufacturer = manufacturers[14],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\ketorol-gel-2-tuba-30g-d-r-reddis-laboratoris-ltd-cart-120x120-ca52.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\ketorol-gel-2-tuba-30g-d-r-reddis-laboratoris-ltd-cart-120x120-ca52.jpg")))}"},
                new Medicine{ Name = "Артрокол гель 25мг/г 45г", Description = "Desc17", Category = categories[188], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\artrokol-gel-25mg-g-tuba-45g-k-o-slaviya-farm-s-r-l-cart-120x120-e016.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\artrokol-gel-25mg-g-tuba-45g-k-o-slaviya-farm-s-r-l-cart-120x120-e016.jpg")))}"},
                new Medicine{ Name = "Фастум Гель, гель 2,5% 50г", Description = "Desc17", Category = categories[188], Manufacturer = manufacturers[22],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\fastum-gel-2-5-tuba-50g-menarini-manufakturing-logistiks-end-servises-s-r-l-cart-120x120-f890.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\fastum-gel-2-5-tuba-50g-menarini-manufakturing-logistiks-end-servises-s-r-l-cart-120x120-f890.jpg")))}"},
                new Medicine{ Name = "Диклофенак-Здоров'я Ультра, гель 50мг/г 50г", Description = "Desc17", Category = categories[189], Manufacturer = manufacturers[13],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\diklofenak-zdorove-ultra-gel-50mg-g-tuba-50g-tov-farm-kompaniya-zdorove-cart-120x120-6072.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\diklofenak-zdorove-ultra-gel-50mg-g-tuba-50g-tov-farm-kompaniya-zdorove-cart-120x120-6072.jpg")))}"},
                new Medicine{ Name = "Клодифен гель 50мг/г 45г", Description = "Desc17", Category = categories[189], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\klodifen-gel-50mg-g-tuba-45g-k-o-slaviya-farm-s-r-l-cart-120x120-c40e.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\klodifen-gel-50mg-g-tuba-45g-k-o-slaviya-farm-s-r-l-cart-120x120-c40e.jpg")))}"},
                new Medicine{ Name = "Німедар гель 10мг/г 30г", Description = "Desc17", Category = categories[190], Manufacturer = manufacturers[20],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nimedar-gel-10mg-g-tuba-30g-darnitsa-chao-farm-firma-cart-120x120-99d1.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nimedar-gel-10mg-g-tuba-30g-darnitsa-chao-farm-firma-cart-120x120-99d1.jpg")))}"},
                new Medicine{ Name = "Німід гель 10мг/г 30г", Description = "Desc17", Category = categories[190], Manufacturer = manufacturers[15],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\nimid-gel-d-naruzh-prim-10mg-g-tuba-30g-kusum-helthker-pvt-ltd-cart-120x120-8bde.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\nimid-gel-d-naruzh-prim-10mg-g-tuba-30g-kusum-helthker-pvt-ltd-cart-120x120-8bde.jpg")))}"},
                new Medicine{ Name = "Тизалуд табл. 4мг №30", Description = "Desc17", Category = categories[192], Manufacturer = manufacturers[11],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\tizalud-tabl-4mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-bfeb.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\tizalud-tabl-4mg-30-kievskiy-vitaminnyiy-zavod-ao-cart-120x120-bfeb.jpg")))}"},
                new Medicine{ Name = "Мідокалм табл. в/о 150мг №30", Description = "Desc17", Category = categories[193], Manufacturer = manufacturers[43],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\midokalm-tabl-p-o-150mg-30-gedeon-rihter-oao-cart-120x120-78d4.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\midokalm-tabl-p-o-150mg-30-gedeon-rihter-oao-cart-120x120-78d4.jpg")))}"},
                new Medicine{ Name = "Мускомед капс. 4мг №20", Description = "Desc17", Category = categories[194], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(Path.Combine(execDirectory, @"Med\muskomed-kaps-tverdyie-4mg-20-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-4355.jpg"))};base64,{Convert.ToBase64String(File.ReadAllBytes(Path.Combine(execDirectory, @"Med\muskomed-kaps-tverdyie-4mg-20-uorld-meditsin-ilach-san-ve-tidzh-a-sh-cart-120x120-4355.jpg")))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},
                //new Medicine{ Name = "", Description = "Desc17", Category = categories[160], Manufacturer = manufacturers[68],ImageURL= $"data:image/{Path.GetExtension(@"")};base64,{Convert.ToBase64String(File.ReadAllBytes(@""))}"},



            };
            for (int i = 0; i < medicines.Length; i++)
            {
                var medicine = medicines[i];
                medicines[i] = Medicines.Add(medicine).Entity;
            }
            var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes("1234");
            var passwordHashBytes = sha256.ComputeHash(passwordBytes);
            var passwordHashBase64 = Convert.ToBase64String(passwordHashBytes);


            var pharmacies = new Pharmacy[]
            {
                new Pharmacy{ Name = "Аптека №1", Address = "вул. Соборна, 93",  Phone = "(099) 266-42-56", WorkTime = "Пн-Нд	:	08:00 - 20:00 Online  :	09:00 - 19:00"},
                new Pharmacy{ Name = "Аптека №2", Address = "вул. Центральна, 75",  Phone = "(095) 348-00-48", WorkTime = "Пн-Нд	:	08:00 - 20:00 Online  :	09:00 - 19:00"},
                new Pharmacy{ Name = "Аптека №3", Address = "вул. Західнодонбаська, 3",  Phone = "(095) 819-42-97", WorkTime = "Пн-Нд	:	08:00 - 20:00 Online  :	09:00 - 19:00"},
                new Pharmacy{ Name = "Аптечний пункт №2", Address = "вул. Дніпровська, 541",  Phone = "(095) 819-42-93", WorkTime = "Пн-Нд	:	00:00 - 24:00 Online  :	09:00 - 19:00"},
            };
            for (int i = 0; i < pharmacies.Length; i++)
            {
                var pharmacy = pharmacies[i];
                pharmacies[i] = Pharmacies.Add(pharmacy).Entity;
            }
            SaveChanges();

            var userAdmin = new User
            {
                Email = "ok170519841@gmail.com",
                Name = "Oksana",
                PasswordHash = passwordHashBase64,
                RefreshToken = Guid.NewGuid(),
                RefreshTokenExpire = DateTime.Now.AddDays(30),
                Role = User.Roles.Admin,
                IsEmailConfirmed = true,
            };
            userAdmin = Users.Add(userAdmin).Entity;
            SaveChanges();




            var medicineInPharmacies = new MedicineInPharmacy[]
            {
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[0], Price = 65.58, AvailableCount = 0 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[0], Price = 66.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[0], Price = 64.98, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[0], Price = 65.58, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[1], Price = 65.58, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[1], Price = 123.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[1], Price = 141.98, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[1], Price = 125.58, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[2], Price = 125.2, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[2], Price = 138.8, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[2], Price = 160.1, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[2], Price = 133.18, AvailableCount = 4 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[3], Price = 67.9, AvailableCount = 12},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[3], Price = 80.18, AvailableCount = 20},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[3], Price = 85, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[3], Price = 69.13, AvailableCount = 14 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[4], Price = 59.58, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[4], Price = 68.58, AvailableCount = 5},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[4], Price = 55.8, AvailableCount = 25},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[4], Price = 66.5, AvailableCount = 1},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[5], Price = 55.58, AvailableCount = 5},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[5], Price = 55.58, AvailableCount = 8},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[5], Price = 55.58, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[5], Price = 55.58, AvailableCount = 115},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[6], Price = 95.99, AvailableCount = 5},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[6], Price = 110.58, AvailableCount = 8},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[6], Price = 99.6, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[6], Price = 111, AvailableCount = 11},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[7], Price = 147.99, AvailableCount = 17},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[7], Price = 161.58, AvailableCount = 8},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[7], Price = 180.6, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[7], Price = 181, AvailableCount = 11},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[8], Price = 49.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[8], Price = 68.5, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[8], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[8], Price = 74.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[9], Price = 136.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[9], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[9], Price = 76.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[9], Price = 74.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[10], Price = 2960.55, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[10], Price = 3100.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[10], Price = 3200.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[10], Price = 2990.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[11], Price = 296.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[11], Price = 210.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[11], Price = 200.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[11], Price = 296.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[12], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[12], Price = 150.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[12], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[12], Price = 141.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[13], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[13], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[13], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[13], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[14], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[14], Price = 159.90, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[14], Price = 207.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[14], Price = 201.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[15], Price = 170.58, AvailableCount = 25 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[15], Price = 213.5, AvailableCount = 20 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[15], Price = 199.38, AvailableCount = 25 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[15], Price = 173.8, AvailableCount = 20 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[16], Price = 111.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[16], Price = 106.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[16], Price = 125.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[16], Price = 100.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[17], Price = 171.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[17], Price = 186.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[17], Price = 225.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[17], Price = 212.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[18], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[18], Price = 159.90, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[18], Price = 207.40, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[18], Price = 201.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[19], Price = 111.40, AvailableCount = 110 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[19], Price = 106.5, AvailableCount = 78 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[19], Price = 125.40, AvailableCount = 36 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[19], Price = 100.20, AvailableCount = 87 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[20], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[20], Price = 150.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[20], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[20], Price = 141.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[21], Price = 136.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[21], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[21], Price = 76.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[21], Price = 74.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[22], Price = 136.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[22], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[22], Price = 76.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[22], Price = 74.20, AvailableCount = 8 },
                 
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[23], Price = 296.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[23], Price = 270.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[23], Price = 320.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[23], Price = 296.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[24], Price = 170.58, AvailableCount = 25 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[24], Price = 213.5, AvailableCount = 20 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[24], Price = 199.38, AvailableCount = 25 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[24], Price = 173.8, AvailableCount = 20 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[25], Price = 340.58, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[25], Price = 293.5, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[25], Price = 299.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[25], Price = 313.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[26], Price = 279.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[26], Price = 298.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[26], Price = 320.38, AvailableCount = 15 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[26], Price = 349.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[27], Price = 371.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[27], Price = 386.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[27], Price = 425.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[27], Price = 412.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[28], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[28], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[28], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[28], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[29], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[29], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[29], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[29], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[30], Price = 340.58, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[30], Price = 360.5, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[30], Price = 340.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[30], Price = 313.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[31], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[31], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[31], Price = 76.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[31], Price = 74.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[32], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[32], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[32], Price = 40.2, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[32], Price = 36.55, AvailableCount = 8 },
                 
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[33], Price = 48.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[33], Price = 64.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[33], Price = 49.2, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[33], Price = 56.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[34], Price = 50.8, AvailableCount = 5},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[34], Price = 51.2, AvailableCount = 8},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[34], Price = 54.8, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[34], Price = 59.5, AvailableCount = 15},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[35], Price = 170.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[35], Price = 213.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[35], Price = 199.38, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[35], Price = 173.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[36], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[36], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[36], Price = 86.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[36], Price = 94.20, AvailableCount = 8 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[37], Price = 95.99, AvailableCount = 5},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[37], Price = 110.58, AvailableCount = 8},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[37], Price = 99.6, AvailableCount = 15},
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[37], Price = 111, AvailableCount = 11},

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[38], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[38], Price = 150.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[38], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[38], Price = 141.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[39], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[39], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[39], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[39], Price = 131.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[40], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[40], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[40], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[40], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[41], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[41], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[41], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[41], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[42], Price = 160.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[42], Price = 150.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[42], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[42], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[43], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[43], Price = 179.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[43], Price = 188.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[43], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[44], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[44], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[44], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[44], Price = 36.55, AvailableCount = 8 },


                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[45], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[45], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[45], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[45], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[46], Price = 160.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[46], Price = 150.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[46], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[46], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[47], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[47], Price = 179.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[47], Price = 188.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[47], Price = 210.20, AvailableCount = 2 },


                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[48], Price = 2071.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[48], Price = 2086.5, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[48], Price = 2025.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[48], Price = 2012.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[49], Price = 670.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[49], Price = 606.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[49], Price = 625.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[49], Price = 699.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[50], Price = 296.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[50], Price = 210.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[50], Price = 200.2, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[50], Price = 296.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[51], Price = 680.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[51], Price = 676.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[51], Price = 685.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[51], Price = 689.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[52], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[52], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[52], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[52], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[53], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[53], Price = 84.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[53], Price = 86.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[53], Price = 94.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[54], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[54], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[54], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[54], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[55], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[55], Price = 84.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[55], Price = 86.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[55], Price = 74.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[56], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[56], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[56], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[56], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[57], Price = 170.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[57], Price = 179.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[57], Price = 225.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[57], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[58], Price = 18.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[58], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[58], Price = 20.2, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[58], Price = 16.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[59], Price = 18.55, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[59], Price = 21.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[59], Price = 14.2, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[59], Price = 16.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[60], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[60], Price = 32.19, AvailableCount = 13 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[60], Price = 30.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[60], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[61], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[61], Price = 39.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[61], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[61], Price = 46.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[62], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[62], Price = 30.19, AvailableCount = 13 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[62], Price = 31.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[62], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[63], Price = 110.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[63], Price = 100.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[63], Price = 98.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[63], Price = 101.20, AvailableCount = 5 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[64], Price = 66.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[64], Price = 64.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[64], Price = 66.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[64], Price = 71.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[65], Price = 270.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[65], Price = 253.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[65], Price = 249.38, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[65], Price = 273.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[66], Price = 115.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[66], Price = 140.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[66], Price = 128.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[66], Price = 131.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[67], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[67], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[67], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[67], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[68], Price = 28.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[68], Price = 32.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[68], Price = 30.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[68], Price = 26.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[69], Price = 66.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[69], Price = 64.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[69], Price = 66.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[69], Price = 71.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[70], Price = 230.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[70], Price = 223.5, AvailableCount = 20 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[70], Price = 219.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[70], Price = 193.8, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[71], Price = 4.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[71], Price = 4.19, AvailableCount = 13 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[71], Price = 5.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[71], Price = 4.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[72], Price = 67.9, AvailableCount = 12},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[72], Price = 80.18, AvailableCount = 20},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[72], Price = 85, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[72], Price = 69.13, AvailableCount = 14 },


                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[73], Price = 67.9, AvailableCount = 12},
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[73], Price = 80.18, AvailableCount = 20},
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[73], Price = 85, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[73], Price = 69.13, AvailableCount = 14 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[74], Price = 240.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[74], Price = 199.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[74], Price = 245.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[74], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[75], Price = 250.58, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[75], Price = 260.5, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[75], Price = 310.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[75], Price = 313.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[76], Price = 250.58, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[76], Price = 260.5, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[76], Price = 310.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[76], Price = 313.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[77], Price = 9.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[77], Price = 10.19, AvailableCount = 13 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[77], Price = 10.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[77], Price = 9.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[78], Price = 279.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[78], Price = 298.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[78], Price = 320.38, AvailableCount = 15 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[78], Price = 349.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[79], Price = 359.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[79], Price = 308.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[79], Price = 340.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[79], Price = 339.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[80], Price = 270.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[80], Price = 279.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[80], Price = 288.40, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[80], Price = 310.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[81], Price = 399.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[81], Price = 408.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[81], Price = 440.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[81], Price = 339.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[82], Price = 279.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[82], Price = 298.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[82], Price = 320.38, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[82], Price = 349.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[83], Price = 479.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[83], Price = 498.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[83], Price = 520.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[83], Price = 549.8, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[84], Price = 279.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[84], Price = 298.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[84], Price = 320.38, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[84], Price = 349.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[85], Price = 28.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[85], Price = 32.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[85], Price = 30.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[85], Price = 26.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[86], Price = 28.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[86], Price = 32.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[86], Price = 30.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[86], Price = 26.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[87], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[87], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[87], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[87], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[88], Price = 170.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[88], Price = 179.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[88], Price = 175.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[88], Price = 169.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[89], Price = 160.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[89], Price = 150.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[89], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[89], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[90], Price = 115.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[90], Price = 140.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[90], Price = 128.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[90], Price = 131.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[91], Price = 120.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[91], Price = 129.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[91], Price = 125.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[91], Price = 139.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[92], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[92], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[92], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[92], Price = 36.55, AvailableCount = 8 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[93], Price = 18.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[93], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[93], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[93], Price = 16.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[94], Price = 459.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[94], Price = 498.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[94], Price = 500.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[94], Price = 499.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[95], Price = 970.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[95], Price = 906.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[95], Price = 925.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[95], Price = 899.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[96], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[96], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[96], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[96], Price = 799.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[97], Price = 70.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[97], Price = 70.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[97], Price = 72.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[97], Price = 79.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[98], Price = 41.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[98], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[98], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[98], Price = 66.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[99], Price = 459.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[99], Price = 498.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[99], Price = 500.38, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[99], Price = 499.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[100], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[100], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[100], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[100], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[101], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[101], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[101], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[100], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[102], Price = 499.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[102], Price = 458.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[102], Price = 460.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[102], Price = 489.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[103], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[103], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[103], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[103], Price = 131.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[104], Price = 79.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[104], Price = 80.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[104], Price = 72.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[104], Price = 79.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[105], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[105], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[105], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[105], Price = 131.20, AvailableCount = 11 },
               
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[106], Price = 58.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[106], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[106], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[106], Price = 56.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[107], Price = 519.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[107], Price = 498.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[107], Price = 520.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[107], Price = 549.8, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[108], Price = 89.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[108], Price = 98.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[108], Price = 102.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[108], Price = 109.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[109], Price = 559.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[109], Price = 648.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[109], Price = 690.38, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[109], Price = 599.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[110], Price = 4519.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[110], Price = 4498.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[110], Price = 4520.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[110], Price = 4549.8, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[111], Price = 1171.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[111], Price = 1286.5, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[111], Price = 1225.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[111], Price = 1212.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[112], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[112], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[112], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[112], Price = 131.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[113], Price = 150.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[113], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[113], Price = 148.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[113], Price = 141.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[114], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[114], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[114], Price = 40.2, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[114], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[115], Price = 5.55, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[115], Price = 6.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[115], Price = 6.2, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[115], Price = 6.55, AvailableCount = 4 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[116], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[116], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[116], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[116], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[117], Price = 18.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[117], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[117], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[117], Price = 16.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[118], Price = 28.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[118], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[118], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[118], Price = 26.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[119], Price = 48.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[119], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[119], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[119], Price = 46.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[120], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[120], Price = 110.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[120], Price = 118.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[120], Price = 111.20, AvailableCount = 11 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[121], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[121], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[121], Price = 86.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[121], Price = 94.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[122], Price = 18.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[122], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[122], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[122], Price = 16.5, AvailableCount = 8 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[123], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[123], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[123], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[123], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[124], Price = 46.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[124], Price = 44.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[124], Price = 46.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[124], Price = 44.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[125], Price = 48.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[125], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[125], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[125], Price = 46.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[126], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[126], Price = 94.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[126], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[126], Price = 104.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[127], Price = 48.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[127], Price = 52.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[127], Price = 60.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[127], Price = 56.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[128], Price = 96.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[128], Price = 94.5, AvailableCount =18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[128], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[128], Price = 102.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[129], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[129], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[129], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[129], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[130], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[130], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[130], Price = 40.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[130], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[131], Price = 58.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[131], Price = 52.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[131], Price = 60.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[131], Price = 46.5, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[132], Price = 275.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[132], Price = 279.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[132], Price = 288.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[132], Price = 310.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[133], Price = 375.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[133], Price = 379.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[133], Price = 388.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[133], Price = 310.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[134], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[134], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[134], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[134], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[135], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[135], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[135], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[135], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[136], Price = 11.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[136], Price = 16.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[136], Price = 14.40, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[136], Price = 15.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[137], Price = 19.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[137], Price = 20.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[137], Price = 27.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[137], Price = 25.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[138], Price = 199.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[138], Price = 259.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[138], Price = 258.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[138], Price = 210.20, AvailableCount = 1 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[139], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[139], Price = 94.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[139], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[139], Price = 104.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[140], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[140], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[140], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[140], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[141], Price = 11.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[141], Price = 16.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[141], Price = 14.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[141], Price = 15.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[142], Price = 17.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[142], Price = 16.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[142], Price = 18.40, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[142], Price = 19.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[143], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[143], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[143], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[143], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[144], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[144], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[144], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[144], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[145], Price = 17.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[145], Price = 16.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[145], Price = 18.40, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[145], Price = 19.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[146], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[146], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[146], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[146], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[147], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[147], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[147], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[147], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[148], Price = 17.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[148], Price = 16.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[148], Price = 18.40, AvailableCount = 17 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[148], Price = 19.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[149], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[149], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[149], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[149], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[150], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[150], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[150], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[150], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[151], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[151], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[151], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[151], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[152], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[152], Price = 94.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[152], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[152], Price = 104.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[153], Price = 199.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[153], Price = 259.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[153], Price = 258.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[153], Price = 210.20, AvailableCount = 1 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[154], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[154], Price = 32.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[154], Price = 30.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[154], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[155], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[155], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[155], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[155], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[156], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[156], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[156], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[156], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[157], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[157], Price = 94.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[157], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[157], Price = 104.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[158], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[158], Price = 89.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[158], Price = 78.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[158], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[159], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[159], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[159], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[159], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[160], Price = 76.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[160], Price = 94.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[160], Price = 96.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[160], Price = 104.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[161], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[161], Price = 89.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[161], Price = 78.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[161], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[162], Price = 199.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[162], Price = 259.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[162], Price = 258.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[162], Price = 210.20, AvailableCount = 1 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[163], Price = 275.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[163], Price = 279.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[163], Price = 288.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[163], Price = 310.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[164], Price = 150.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[164], Price = 159.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[164], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[164], Price = 181.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[165], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[165], Price = 159.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[165], Price = 168.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[165], Price = 171.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[166], Price = 199.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[166], Price = 219.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[166], Price = 218.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[166], Price = 210.20, AvailableCount = 1 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[167], Price = 48.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[167], Price = 52.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[167], Price = 50.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[167], Price = 46.55, AvailableCount = 8 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[168], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[168], Price = 159.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[168], Price = 168.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[168], Price = 171.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[169], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[169], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[169], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[169], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[170], Price = 399.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[170], Price = 399.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[170], Price = 418.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[170], Price = 410.20, AvailableCount = 5 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[171], Price = 279.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[171], Price = 249.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[171], Price = 258.40, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[171], Price = 280.20, AvailableCount = 4 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[172], Price = 170.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[172], Price = 159.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[172], Price = 168.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[172], Price = 171.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[173], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[173], Price = 60.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[173], Price = 57.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[173], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[174], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[174], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[174], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[174], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[175], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[175], Price = 89.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[175], Price = 78.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[175], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[176], Price = 100.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[176], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[176], Price = 98.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[176], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[177], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[177], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[177], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[177], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[178], Price = 49.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[178], Price = 60.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[178], Price = 57.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[178], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[179], Price = 47.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[179], Price = 69.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[179], Price = 59.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[179], Price = 58.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[180], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[180], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[180], Price = 51.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[180], Price = 54.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[181], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[181], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[181], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[181], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[182], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[182], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[182], Price = 51.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[182], Price = 54.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[183], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[183], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[183], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[183], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[184], Price = 47.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[184], Price = 69.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[184], Price = 59.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[184], Price = 58.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[185], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[185], Price = 189.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[185], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[185], Price = 211.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[186], Price = 100.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[186], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[186], Price = 98.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[186], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[187], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[187], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[187], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[187], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[188], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[188], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[188], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[188], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[189], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[189], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[189], Price = 51.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[189], Price = 54.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[190], Price = 246.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[190], Price = 210.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[190], Price = 210.2, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[190], Price = 246.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[191], Price = 249.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[191], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[191], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[191], Price = 230.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[192], Price = 115.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[192], Price = 121.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[192], Price = 118.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[192], Price = 120.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[193], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[193], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[193], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[193], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[194], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[194], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[194], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[194], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[195], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[195], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[195], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[195], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[196], Price = 71.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[196], Price = 68.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[196], Price = 71.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[196], Price = 74.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[197], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[197], Price = 279.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[197], Price = 288.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[197], Price = 310.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[198], Price = 58.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[198], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[198], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[198], Price = 56.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[199], Price = 246.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[199], Price = 210.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[199], Price = 210.2, AvailableCount = 9 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[199], Price = 246.55, AvailableCount = 8 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[200], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[200], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[200], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[200], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[201], Price = 115.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[201], Price = 121.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[201], Price = 118.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[201], Price = 120.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[202], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[202], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[202], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[202], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[203], Price = 399.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[203], Price = 399.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[203], Price = 418.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[203], Price = 410.20, AvailableCount = 5 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[204], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[204], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[204], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[204], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[205], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[205], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[205], Price = 51.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[205], Price = 54.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[206], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[206], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[206], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[206], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[207], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[207], Price = 169.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[207], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[207], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[208], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[208], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[208], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[208], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[209], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[209], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[209], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[209], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[210], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[210], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[210], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[210], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[211], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[211], Price = 289.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[211], Price = 338.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[211], Price = 310.20, AvailableCount = 7 },
                 
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[212], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[212], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[212], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[212], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[213], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[213], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[213], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[213], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[214], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[214], Price = 169.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[214], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[214], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[215], Price = 770.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[215], Price = 769.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[215], Price = 688.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[215], Price = 701.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[216], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[216], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[216], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[216], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[217], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[217], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[217], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[217], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[218], Price = 1870.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[218], Price = 1769.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[218], Price = 1988.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[218], Price = 1901.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[219], Price = 2140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[219], Price = 2169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[219], Price = 2178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[219], Price = 2151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[220], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[220], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[220], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[220], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[221], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[221], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[221], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[221], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[222], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[222], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[222], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[222], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[223], Price = 130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[223], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[223], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[223], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[224], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[224], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[224], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[224], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[225], Price = 320.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[225], Price = 349.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[225], Price = 338.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[225], Price = 341.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[226], Price = 440.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[226], Price = 449.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[226], Price = 438.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[226], Price = 441.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[227], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[227], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[227], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[227], Price = 799.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[228], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[228], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[228], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[228], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[229], Price = 570.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[229], Price = 569.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[229], Price = 588.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[229], Price = 501.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[230], Price = 320.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[230], Price = 349.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[230], Price = 338.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[230], Price = 341.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[231], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[231], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[231], Price = 308.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[231], Price = 301.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[232], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[232], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[232], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[232], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[233], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[233], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[233], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[233], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[234], Price = 190.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[234], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[234], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[234], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[235], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[235], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[235], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[235], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[236], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[236], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[236], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[236], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[237], Price = 4570.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[237], Price = 4669.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[237], Price = 4588.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[237], Price = 4701.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[238], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[238], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[238], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[238], Price = 799.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[239], Price = 420.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[239], Price = 349.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[239], Price = 338.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[239], Price = 341.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[240], Price = 400.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[240], Price = 339.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[240], Price = 369.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[240], Price = 380.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[241], Price = 559.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[241], Price = 608.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[241], Price = 620.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[241], Price = 599.8, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[242], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[242], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[242], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[242], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[243], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[243], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[243], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[243], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[244], Price = 400.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[244], Price = 439.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[244], Price = 369.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[244], Price = 380.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[245], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[245], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[245], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[245], Price = 799.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[246], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[246], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[246], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[246], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[247], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[247], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[247], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[247], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[248], Price = 130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[248], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[248], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[248], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[249], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[249], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[249], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[249], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[250], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[250], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[250], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[250], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[251], Price = 190.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[251], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[251], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[251], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[252], Price = 299.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[252], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[252], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[252], Price = 210.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[253], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[253], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[253], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[253], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[254], Price = 6559.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[254], Price = 6608.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[254], Price = 6620.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[254], Price = 6599.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[255], Price = 299.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[255], Price = 259.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[255], Price = 258.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[255], Price = 210.20, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[256], Price = 1130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[256], Price = 1149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[256], Price = 1138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[256], Price = 1141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[257], Price = 299.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[257], Price = 259.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[257], Price = 258.40, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[257], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[258], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[258], Price = 89.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[258], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[258], Price = 78.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[259], Price = 5327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[259], Price = 5329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[259], Price = 5288.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[259], Price = 5291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[260], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[260], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[260], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[260], Price = 799.20, AvailableCount = 1 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[261], Price = 30770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[261], Price = 31506.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[261], Price = 31325.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[261], Price = 30799.20, AvailableCount = 1 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[262], Price = 140.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[262], Price = 119.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[262], Price = 128.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[262], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[263], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[263], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[263], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[263], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[264], Price = 130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[264], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[264], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[264], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[265], Price = 659.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[265], Price = 708.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[265], Price = 720.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[265], Price = 699.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[266], Price = 370.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[266], Price = 339.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[266], Price = 339.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[266], Price = 340.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[267], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[267], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[267], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[267], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[268], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[268], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[268], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[268], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[269], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[269], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[269], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[269], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[270], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[270], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[270], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[270], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[271], Price = 190.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[271], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[271], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[271], Price = 220.20, AvailableCount = 2 },
                 
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[272], Price = 219.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[272], Price = 239.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[272], Price = 248.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[272], Price = 210.20, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[273], Price = 121.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[273], Price = 99.00, AvailableCount = 14 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[273], Price = 129.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[273], Price = 101.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[274], Price = 18.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[274], Price = 12.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[274], Price = 15.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[274], Price = 16.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[275], Price = 13.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[275], Price = 12.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[275], Price = 13.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[275], Price = 15.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[276], Price = 23.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[276], Price = 18.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[276], Price = 22.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[276], Price = 19.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[277], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[277], Price = 32.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[277], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[277], Price = 35.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[278], Price = 180.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[278], Price = 199.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[278], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[278], Price = 211.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[279], Price = 2680.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[279], Price = 2799.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[279], Price = 2778.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[279], Price = 2911.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[280], Price = 470.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[280], Price = 439.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[280], Price = 439.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[280], Price = 440.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[281], Price = 13.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[281], Price = 12.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[281], Price = 13.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[281], Price = 14.55, AvailableCount = 7 },
                
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[282], Price = 23.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[282], Price = 18.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[282], Price = 22.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[282], Price = 19.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[283], Price = 249.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[283], Price = 259.00, AvailableCount = 6 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[283], Price = 258.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[283], Price = 230.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[284], Price = 299.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[284], Price = 259.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[284], Price = 258.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[284], Price = 210.20, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[285], Price = 770.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[285], Price = 769.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[285], Price = 688.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[285], Price = 701.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[286], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[286], Price = 50.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[286], Price = 41.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[286], Price = 44.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[287], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[287], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[287], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[287], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[288], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[288], Price = 32.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[288], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[288], Price = 35.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[289], Price = 190.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[289], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[289], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[289], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[290], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[290], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[290], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[290], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[291], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[291], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[291], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[291], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[292], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[292], Price = 89.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[292], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[292], Price = 78.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[293], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[293], Price = 36.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[293], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[293], Price = 35.55, AvailableCount = 7 },


                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[294], Price = 249.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[294], Price = 249.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[294], Price = 248.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[294], Price = 210.20, AvailableCount = 3 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[295], Price = 71.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[295], Price = 79.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[295], Price = 68.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[295], Price = 78.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[296], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[296], Price = 36.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[296], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[296], Price = 35.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[297], Price = 23.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[297], Price = 18.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[297], Price = 22.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[297], Price = 19.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[298], Price = 33.5, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[298], Price = 34.2, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[298], Price = 31.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[298], Price = 37.5, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[299], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[299], Price = 32.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[299], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[299], Price = 35.55, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[300], Price = 61.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[300], Price = 59.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[300], Price = 58.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[300], Price = 58.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[301], Price = 33.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[301], Price = 36.9, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[301], Price = 33.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[301], Price = 35.55, AvailableCount = 7 },  

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[302], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[302], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[302], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[302], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[303], Price = 670.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[303], Price = 706.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[303], Price = 725.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[303], Price = 699.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[304], Price = 559.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[304], Price = 608.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[304], Price = 620.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[304], Price = 599.8, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[305], Price = 86.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[305], Price = 84.5, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[305], Price = 86.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[305], Price = 74.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[306], Price = 459.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[306], Price = 498.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[306], Price = 500.38, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[306], Price = 499.8, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[307], Price = 770.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[307], Price = 806.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[307], Price = 825.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[307], Price = 799.20, AvailableCount = 1 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[308], Price = 170.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[308], Price = 179.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[308], Price = 215.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[308], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[309], Price = 140.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[309], Price = 169.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[309], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[309], Price = 151.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[310], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[310], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[310], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[310], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[311], Price = 81.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[311], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[311], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[311], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[312], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[312], Price = 150.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[312], Price = 138.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[312], Price = 131.20, AvailableCount = 11 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[313], Price = 91.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[313], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[313], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[313], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[314], Price = 370.00, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[314], Price = 339.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[314], Price = 339.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[314], Price = 340.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[315], Price = 1371.40, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[315], Price = 1486.5, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[315], Price = 1425.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[315], Price = 1412.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[316], Price = 101.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[316], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[316], Price = 98.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[316], Price = 111.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[317], Price = 70.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[317], Price = 70.5, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[317], Price = 72.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[317], Price = 79.20, AvailableCount = 7 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[318], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[318], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[318], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[318], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[319], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[319], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[319], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[319], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[320], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[320], Price = 50.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[320], Price = 41.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[320], Price = 44.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[321], Price = 101.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[321], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[321], Price = 98.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[321], Price = 111.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[322], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[322], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[322], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[322], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[323], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[323], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[323], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[323], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[324], Price = 101.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[324], Price = 99.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[324], Price = 98.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[324], Price = 111.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[325], Price = 100.00, AvailableCount = 8 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[325], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[325], Price = 108.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[325], Price = 101.20, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[326], Price = 28.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[326], Price = 22.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[326], Price = 20.2, AvailableCount = 11 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[326], Price = 26.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[327], Price = 38.55, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[327], Price = 42.19, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[327], Price = 40.2, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[327], Price = 36.55, AvailableCount = 8 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[328], Price = 160.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[328], Price = 150.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[328], Price = 178.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[328], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[329], Price = 170.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[329], Price = 179.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[329], Price = 215.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[329], Price = 210.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[330], Price = 130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[330], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[330], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[330], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[331], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[331], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[331], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[331], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[332], Price = 479.58, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[332], Price = 498.5, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[332], Price = 520.38, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[332], Price = 549.8, AvailableCount = 7 },
                  
                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[333], Price = 327.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[333], Price = 329.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[333], Price = 288.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[333], Price = 291.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[334], Price = 130.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[334], Price = 149.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[334], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[334], Price = 141.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[335], Price = 101.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[335], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[335], Price = 118.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[335], Price = 111.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[336], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[336], Price = 129.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[336], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[336], Price = 131.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[337], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[337], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[337], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[337], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[338], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[338], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[338], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[338], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[339], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[339], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[339], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[339], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[340], Price = 70.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[340], Price = 79.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[340], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[340], Price = 91.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[341], Price = 101.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[341], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[341], Price = 118.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[341], Price = 111.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[342], Price = 1030.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[342], Price = 1049.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[342], Price = 1038.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[342], Price = 1041.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[343], Price = 120.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[343], Price = 129.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[343], Price = 138.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[343], Price = 131.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[344], Price = 90.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[344], Price = 109.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[344], Price = 88.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[344], Price = 101.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[345], Price = 100.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[345], Price = 100.00, AvailableCount = 12 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[345], Price = 108.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[345], Price = 101.20, AvailableCount = 12 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[346], Price = 59.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[346], Price = 60.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[346], Price = 57.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[346], Price = 55.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[347], Price = 90.40, AvailableCount = 10 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[347], Price = 84.5, AvailableCount = 18 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[347], Price = 79.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[347], Price = 84.20, AvailableCount = 8 },


                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[348], Price = 69.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[348], Price = 90.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[348], Price = 77.40, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[348], Price = 85.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[349], Price = 40.00, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[349], Price = 50.00, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[349], Price = 41.40, AvailableCount = 2 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[349], Price = 44.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[350], Price = 190.00, AvailableCount = 4 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[350], Price = 189.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[350], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[350], Price = 220.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[351], Price = 170.00, AvailableCount = 7 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[351], Price = 169.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[351], Price = 188.40, AvailableCount = 5 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[351], Price = 201.20, AvailableCount = 2 },

                new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[352], Price = 227.70, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[352], Price = 229.00, AvailableCount = 1 },
                new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[352], Price = 238.40, AvailableCount = 3 },
                new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[352], Price = 251.20, AvailableCount = 2 },

                //new MedicineInPharmacy { Pharmacy = pharmacies[0], Medicine = medicines[353], Price = 170.00, AvailableCount = 7 },
                //new MedicineInPharmacy { Pharmacy = pharmacies[1], Medicine = medicines[353], Price = 170.00, AvailableCount = 1 },
                //new MedicineInPharmacy { Pharmacy = pharmacies[2], Medicine = medicines[353], Price = 178.40, AvailableCount = 5 },
                //new MedicineInPharmacy { Pharmacy = pharmacies[3], Medicine = medicines[353], Price = 181.20, AvailableCount = 2 },















            };
            for (int i = 0; i < medicineInPharmacies.Length; i++)
            {
                var pharmacy = medicineInPharmacies[i];
                medicineInPharmacies[i] = MedicinesInParmacies.Add(pharmacy).Entity;
            }

            SaveChanges();

            var discount1 = new Discount
            {
                Name = "Discount1",
                DiscountStart = DateTime.Now,
                DiscountEnd = DateTime.Now.AddDays(2),
                Value = 10,
                Description = "Знижка 10% на товари",
                Medicines = new List<MedicineInPharmacy>
                {
                    medicineInPharmacies[78],
                    medicineInPharmacies[2],
                    medicineInPharmacies[4],
                    medicineInPharmacies[5],
                    medicineInPharmacies[8],
                    medicineInPharmacies[22],
                }
            };
            var discount2 = new Discount
            {
                Name = "Discount2",
                DiscountStart = DateTime.Now,
                DiscountEnd = DateTime.Now.AddDays(3),
                Value = 10,
                Description = "Знижка 20% на товари",
                Medicines = new List<MedicineInPharmacy>
                {
                    medicineInPharmacies[11],
                    medicineInPharmacies[12],
                    medicineInPharmacies[9]
                }
            };
            Discounts.AddRange(discount1, discount2);
            
            SaveChanges();
        }
    }
}
