using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args; // для получения типов двнных
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types;
using Microsoft.Ajax.Utilities;
using Telegram.Bot.Requests;
using System.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic;
using System.Net;
using System.IO;

namespace BotStore
{
    class Program
    {
        static ITelegramBotClient botClient;
     // public static string lincBD = ;
        static async Task Main()
        {

           

           
                botClient = new TelegramBotClient("6571440981:AAGw_LZa1Gx_SLFxi-mr9U-bWNlgcIDnYJ0");
                var me = botClient.GetMeAsync().Result; // считывает информацию о нашем боте
                Console.WriteLine(me.FirstName); // выводит информацию (название) бота
                                                 // botClient.OnCallbackQuery += BotClient_OnCallbackQuery;
                botClient.OnMessage += BotClient_OnMessage;// событие и присваеваем ему метод(любое название) Этот метод для работы с сообщениями
                botClient.StartReceiving(); // начинаем прослушивание сообщений
                Console.ReadLine();
                botClient.StopReceiving(); // заканчиваем прослушивание сообщений
                     

        }

     

        private static async void BotClient_OnMessage(object? sender, Telegram.Bot.Args.MessageEventArgs e) //async это значит можем принимать сообщения и обрабатывать их от нескольких пользоваителей одновременно
        {
         

            string connecting = "Data Source=WIN-LK1QRPRQTC6\\SQLEXPRESS;Initial Catalog=TelBot;Integrated Security=True";


                var mesage = e.Message; // присваиваем сообщение которое напишем в чат бота

                if (mesage.Type != MessageType.Text)// если пользователь отправил не текст то выйдет из метода
                {
                    return;
                }
                //   Console.WriteLine( mesage.Text); // Выводим в консоль то что написал пользователь
                string infoID = mesage.From.Id.ToString(); // айди телеграмм акаунта пользователя


                string name = mesage.From.FirstName + " " + mesage.From.LastName; // присвоили в переменную имя и фамилию того кто написал предложение а эта информ ация есть у каждого сообщения (фио,фамилия у сообщения)
                Console.WriteLine("ID: " + infoID + " Отправил: " + name + " Сообщение: " + mesage.Text + " Дата и время: " + DateTime.Now); // вывели сообщение + кто отправил ФИ


                SqlConnection str = new SqlConnection(connecting);
                str.Open();
                SqlCommand command = new SqlCommand($"insert into  Логи(ТелеграммID,UserName,ДатаВремя,Действие)  values('" + infoID + "', '" + name + "', '" + Convert.ToString(DateTime.Now) + "', '" + mesage.Text + "');", str);
                command.ExecuteNonQuery();
                str.Close();





            bool createnewaccount = false;
            string TGID = "";
            SqlConnection str1101 = new SqlConnection(connecting);
            str1101.Open();
            SqlCommand command1101 = new SqlCommand("select TelegramID from Клиенты", str1101);
            SqlDataReader reader1101 = command1101.ExecuteReader();
            while (reader1101.Read())
            {              
                    TGID = reader1101[0].ToString();
                    if (TGID == infoID)
                    {
                        createnewaccount = false;
                    
                        break;
                    }
                    else if (TGID != infoID)
                    {
                        createnewaccount = true;
                       
                    }  
            }




            if (createnewaccount == true)
            {
                SqlConnection str66 = new SqlConnection(connecting);
                str66.Open();
                SqlCommand command66 = new SqlCommand($"insert into  Клиенты(UserName,TelegramID,ID_Доступа) values('" + name + "','" + infoID + "', 1)", str66);
                command66.ExecuteNonQuery();
                str66.Close();
            }




            bool Dostup = false;
            string ResultPrint = "";
            SqlConnection str110 = new SqlConnection(connecting);
            str110.Open();
            SqlCommand command110 = new SqlCommand("select Доступы.Доступ  from Клиенты join Доступы on Доступы.ID_Доступа = Клиенты.ID_Доступа WHERE TelegramID = '" + infoID + "'", str110);
            SqlDataReader reader110 = command110.ExecuteReader();
            while (reader110.Read())
            {
                ResultPrint = reader110[0].ToString();
                if (ResultPrint == "Разрешен")
                {
                    Dostup = true;
                }
                else 
                {
                    Dostup = false;
                }
                  

            }
            reader110.Close();
            str110.Close();






            // обработчик инлайн нажатия 

            if (Dostup == true)
            {

                int IDTovar = 0;
                string Kategory = "";
                string Photo = "";
                string Diskription = "";
                string price = "";
                string IDtovara = "";
                string IDklient = "";
                botClient.OnCallbackQuery += BotClient_OnCallbackQuery;


                async void BotClient_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
                {


               


                    try
                    {
                        CallbackQuery query = e.CallbackQuery;
                        string queryId = query.Id;
                        switch (query.Data)
                        {


                            case "Изменить пункт выдачи":
                                var inlineKeyboard1 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                        {
                    new [] // первый массив кнопок 
                    {
                        InlineKeyboardButton.WithCallbackData("Москва ул. Ильинка 7", "Москва ул. Ильинка 7"),// 1 это назваание 2 это обращение или что то типа того

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("Пермь ул. Петропавловская 56", "Пермь ул. Петропавловская 56"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("Киров ул. Красноармейская 26", "Киров ул. Красноармейская 26"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                       InlineKeyboardButton.WithCallbackData("Кастрома ул. Чайковского 9Б", "Кастрома ул. Чайковского 9Б"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Ярославль ул. Свободы 19", "Ярославль ул. Свободы 19"),

                                }
                                 });
                                await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите пункт выдачи в вашем городе", replyMarkup: inlineKeyboard1); // выводим в переписку созданные кнопки

                                break;







                            case "Пополнить счёт":

                                var inlineKeyboard17 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                      {
                    new [] // первый массив кнопок 
                    {
                        InlineKeyboardButton.WithCallbackData("Перевод на карту", "Перевод на карту"),// 1 это назваание 2 это обращение или что то типа того

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("QIWI", "QIWI"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("USDT (TRC20)", "USDT (TRC20)"),

                                }
                                 });
                                await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите способ оплаты", replyMarkup: inlineKeyboard17); // выводим в переписку созданные кнопки


                                break;

                            case "Перевод на карту":

                                var inlineKeyboard21 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн 
                                  {
                        new [] // первый массив кнопок 
                        {
                            InlineKeyboardButton.WithCallbackData("100р VISA/MASTERCARD", "100р VISA/MASTERCARD"),// 1 это назваание 2 это обращение или что то типа того

                                    },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("500р VISA/MASTERCARD", "500р VISA/MASTERCARD"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("1000р VISA/MASTERCARD", "1000р VISA/MASTERCARD"),

                                }
                                 });


                                await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите сумму пополнения", replyMarkup: inlineKeyboard21); // выводим в переписку созданные кнопки

                                break;
                            case "QIWI":
                                var inlineKeyboard212 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн 
                              {
                        new [] // первый массив кнопок 
                        {
                            InlineKeyboardButton.WithCallbackData("100р QIWI/RUB", "100р QIWI/RUB"),// 1 это назваание 2 это обращение или что то типа того

                                    },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("500р QIWI/RUB", "500р QIWI/RUB"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("1000р QIWI/RUB", "1000р QIWI/RUB"),

                                }
                                 });
                                await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите сумму пополнения", replyMarkup: inlineKeyboard212);

                                break;
                            case "USDT (TRC20)":
                                var inlineKeyboard211 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн 
                           {
                        new [] // первый массив кнопок 
                        {
                            InlineKeyboardButton.WithCallbackData("100р USDT (TRC20)", "100р USDT (TRC20)"),// 1 это назваание 2 это обращение или что то типа того

                                    },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("500р USDT (TRC20)", "500р USDT (TRC20)"),

                                },
                                 new [] // второй массив кнопок 
                                {
                                     InlineKeyboardButton.WithCallbackData("1000р USDT (TRC20)", "1000р USDT (TRC20)"),

                                }
                                 });
                                await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите сумму пополнения", replyMarkup: inlineKeyboard211);
                                break;


                            case "100р VISA/MASTERCARD":


                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 100 рублей на карту: 2938 8237 2874 8344"); // выводим в переписку созданные кнопки

                                break;
                            case "100р QIWI/RUB":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 100 рублей на QIWI: +79536533385");
                                break;
                            case "100р USDT (TRC20)":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 1,75 USDT на кошелёк: f4fGg$Gkm48nffkGHH3kddgl390");
                                break;

                            case "500р VISA/MASTERCARD":


                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 500 рублей на карту: 2938 8237 2874 8344"); // выводим в переписку созданные кнопки

                                break;
                            case "500р QIWI/RUB":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 500 рублей на QIWI: +79536533385");
                                break;
                            case "500р USDT (TRC20)":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 5,23 USDT на кошелёк: f4fGg$Gkm48nffkGHH3kddgl390");
                                break;


                            case "1000р VISA/MASTERCARD":


                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 1000 рублей на карту: 2938 8237 2874 8344"); // выводим в переписку созданные кнопки

                                break;
                            case "1000р QIWI/RUB":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 1000 рублей на QIWI: +79536533385");
                                break;
                            case "1000р USDT (TRC20)":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Переведите 10,3 USDT на кошелёк: f4fGg$Gkm48nffkGHH3kddgl390");
                                break;


                            case "Москва ул. Ильинка 7":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Вы изменили пункт выдачи на: Москва ул. Ильинка 7");


                                string street = "Москва ул.Ильинка 7";
                                SqlConnection str = new SqlConnection(connecting);
                                str.Open();
                                SqlCommand command = new SqlCommand($"update Клиенты set  ПунктВыдачи = '" + street + "' WHERE TelegramID = '" + infoID + "'", str);
                                command.ExecuteNonQuery();
                                str.Close();
                                break;

                            case "Пермь ул. Петропавловская 56":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Вы изменили пункт выдачи на: Пермь ул. Петропавловская 56");


                                string street1 = "Пермь ул. Петропавловская 56";
                                SqlConnection str1 = new SqlConnection(connecting);
                                str1.Open();
                                SqlCommand command1 = new SqlCommand($"update Клиенты set  ПунктВыдачи = '" + street1 + "' WHERE TelegramID = '" + infoID + "'", str1);
                                command1.ExecuteNonQuery();
                                str1.Close();
                                break;

                            case "Киров ул. Красноармейская 26":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Вы изменили пункт выдачи на: Киров ул. Красноармейская 26");


                                string street2 = "Киров ул. Красноармейская 26";
                                SqlConnection str2 = new SqlConnection(connecting);
                                str2.Open();
                                SqlCommand command2 = new SqlCommand($"update Клиенты set  ПунктВыдачи = '" + street2 + "' WHERE TelegramID = '" + infoID + "'", str2);
                                command2.ExecuteNonQuery();
                                str2.Close();
                                break;

                            case "Кастрома ул. Чайковского 9Б":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Вы изменили пункт выдачи на: Кастрома ул. Чайковского 9Б");


                                string street3 = "Кастрома ул. Чайковского 9Б";
                                SqlConnection str3 = new SqlConnection(connecting);
                                str3.Open();
                                SqlCommand command3 = new SqlCommand($"update Клиенты set  ПунктВыдачи = '" + street3 + "' WHERE TelegramID = '" + infoID + "'", str3);
                                command3.ExecuteNonQuery();
                                str3.Close();
                                break;

                            case "Ярославль ул. Свободы 19":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "Вы изменили пункт выдачи на: Ярославль ул. Свободы 19");


                                string street4 = "Ярославль ул. Свободы 19";
                                SqlConnection str4 = new SqlConnection(connecting);
                                str4.Open();
                                SqlCommand command4 = new SqlCommand($"update Клиенты set  ПунктВыдачи = '" + street4 + "' WHERE TelegramID = '" + infoID + "'", str4);
                                command4.ExecuteNonQuery();
                                str4.Close();
                                break;




    




                            case "Противо капканные Ботинки":
                                 IDTovar = 3;
                                                       
                                 Kategory = "";
                                 Photo = "";
                                 Diskription = "";
                                 price = "";
                                SqlConnection str119 = new SqlConnection(connecting);
                                str119.Open();
                                SqlCommand command119 = new SqlCommand("select Категории.Категория,Фото,Описание,Цена from Товары join Категории on Категории.ID_Категории = Товары.ID_Категории where Товары.ID_Категории = " + IDTovar+"", str119);
                                SqlDataReader reader119 = command119.ExecuteReader();
                                while (reader119.Read())
                                {

                                    Kategory = reader119[0].ToString();
                                    Photo = reader119[1].ToString();
                                    Diskription = reader119[2].ToString();
                                    price = reader119[3].ToString();



                                    var inlineKeyboard111 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                                {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Оплатить","Оплатить"),// 1 это назваание 2 это обращение или что то типа того
                                      InlineKeyboardButton.WithCallbackData("В корзину","В корзину"),
                                }

                                });
                                    await botClient.SendPhotoAsync(
                              chatId: mesage.From.Id,
                              photo: Photo,
                              caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nЦена: " + price + " ", replyMarkup: inlineKeyboard111);

                                    // await botClient.SendTextMessageAsync(mesage.From.Id, "описание товара", replyMarkup: inlineKeyboard111);

                                }
                                reader119.Close();
                                str119.Close();

                                break;







                            case "Крепления":
                                 IDTovar = 2;

                                 Kategory = "";
                                 Photo = "";
                                 Diskription = "";
                                 price = "";
                                SqlConnection str1199 = new SqlConnection(connecting);
                                str1199.Open();
                                SqlCommand command1199 = new SqlCommand("select Категории.Категория,Фото,Описание,Цена from Товары join Категории on Категории.ID_Категории = Товары.ID_Категории where Товары.ID_Категории = " + IDTovar + "", str1199);
                                SqlDataReader reader1199 = command1199.ExecuteReader();
                                while (reader1199.Read())
                                {

                                    Kategory = reader1199[0].ToString();
                                    Photo = reader1199[1].ToString();
                                    Diskription = reader1199[2].ToString();
                                    price = reader1199[3].ToString();



                                    var inlineKeyboard111 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                                {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Оплатить","Оплатить"),// 1 это назваание 2 это обращение или что то типа того
                                      InlineKeyboardButton.WithCallbackData("В корзину","В корзину"),
                                }

                                });
                                    await botClient.SendPhotoAsync(
                              chatId: mesage.From.Id,
                              photo: Photo,
                              caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nЦена: " + price + " ", replyMarkup: inlineKeyboard111);

                                    // await botClient.SendTextMessageAsync(mesage.From.Id, "описание товара", replyMarkup: inlineKeyboard111);

                                }
                                reader1199.Close();
                                str1199.Close();



                                break;








                            case "Камуфляж":
                                IDTovar = 1;

                                Kategory = "";
                                Photo = "";
                                Diskription = "";
                                price = "";
                                SqlConnection str11992 = new SqlConnection(connecting);
                                str11992.Open();
                                SqlCommand command11992 = new SqlCommand("select Категории.Категория,Фото,Описание,Цена from Товары join Категории on Категории.ID_Категории = Товары.ID_Категории where Товары.ID_Категории = " + IDTovar + "", str11992);
                                SqlDataReader reader11992 = command11992.ExecuteReader();
                                while (reader11992.Read())
                                {

                                    Kategory = reader11992[0].ToString();
                                    Photo = reader11992[1].ToString();
                                    Diskription = reader11992[2].ToString();
                                    price = reader11992[3].ToString();



                                    var inlineKeyboard111 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                                {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Оплатить","Оплатить"),// 1 это назваание 2 это обращение или что то типа того
                                      InlineKeyboardButton.WithCallbackData("В корзину","В корзину"),
                                }

                                });
                                    await botClient.SendPhotoAsync(
                              chatId: mesage.From.Id,
                              photo: Photo,
                              caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nЦена: " + price + " ", replyMarkup: inlineKeyboard111);

                                    // await botClient.SendTextMessageAsync(mesage.From.Id, "описание товара", replyMarkup: inlineKeyboard111);

                                }
                                reader11992.Close();
                                str11992.Close();



                                break;



                            case "Оружие":
                                IDTovar = 4;

                                Kategory = "";
                                Photo = "";
                                Diskription = "";
                                price = "";
                                SqlConnection str119928 = new SqlConnection(connecting);
                                str119928.Open();
                                SqlCommand command119928 = new SqlCommand("select Категории.Категория,Фото,Описание,Цена from Товары join Категории on Категории.ID_Категории = Товары.ID_Категории where Товары.ID_Категории = " + IDTovar + "", str119928);
                                SqlDataReader reader119928 = command119928.ExecuteReader();
                                while (reader119928.Read())
                                {

                                    Kategory = reader119928[0].ToString();
                                    Photo = reader119928[1].ToString();
                                    Diskription = reader119928[2].ToString();
                                    price = reader119928[3].ToString();



                                    var inlineKeyboard111 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                                {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Оплатить","Оплатить"),// 1 это назваание 2 это обращение или что то типа того
                                      InlineKeyboardButton.WithCallbackData("В корзину","В корзину"),
                                }

                                });
                                    await botClient.SendPhotoAsync(
                              chatId: mesage.From.Id,
                              photo: Photo,
                              caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nЦена: " + price + " ", replyMarkup: inlineKeyboard111);

                                   
                                }
                                reader119928.Close();
                                str119928.Close();



                                break;






                            case "В корзину":

                                await botClient.SendTextMessageAsync(mesage.From.Id, "  " + Diskription + ".\nТовар успешно добавлен в корзину!");


                                IDtovara = ""; 
                                SqlConnection str110 = new SqlConnection(connecting);
                                str110.Open();
                                SqlCommand command110 = new SqlCommand("select ID_Товара from Товары where Описание = '" + Diskription + "'", str110);
                                SqlDataReader reader110 = command110.ExecuteReader();
                                while (reader110.Read())
                                {
                                    IDtovara = reader110[0].ToString();          
                                }
                                reader110.Close();
                                str110.Close();

                          

                                IDklient = "";
                                SqlConnection str1101 = new SqlConnection(connecting);
                                str1101.Open();
                                SqlCommand command1101 = new SqlCommand("select ID_Клиента from Клиенты Where TelegramID =  '" + infoID + "'", str1101);
                                SqlDataReader reader1101 = command1101.ExecuteReader();
                                while (reader1101.Read())
                                {
                                    IDklient = reader1101[0].ToString();
                                }
                                reader1101.Close();
                                str1101.Close();



                                SqlConnection str66 = new SqlConnection(connecting);
                                str66.Open();
                                SqlCommand command66 = new SqlCommand($"insert into  Корзины(ID_Товара,ID_Клиента) values('" + IDtovara + "','" + IDklient + "')", str66);
                                command66.ExecuteNonQuery();
                                str66.Close();

                                break;


                            case "Оплатить":

                                await botClient.SendTextMessageAsync(mesage.From.Id, " Товар успешно оплачен! ");



                             
                                SqlConnection str1109 = new SqlConnection(connecting);
                                str1109.Open();
                                SqlCommand command1109 = new SqlCommand("select ID_Товара from Товары where Описание = '"+Diskription+"'", str1109);
                                SqlDataReader reader1109 = command1109.ExecuteReader();
                                while (reader1109.Read())
                                {
                                    IDtovara = reader1109[0].ToString();
                                }
                                reader1109.Close();
                                str1109.Close();

                         
                                SqlConnection str11015 = new SqlConnection(connecting);
                                str11015.Open();
                                SqlCommand command11015 = new SqlCommand("select ID_Клиента from Клиенты Where TelegramID =  '"+infoID+"'", str11015);
                                SqlDataReader reader11015 = command11015.ExecuteReader();
                                while (reader11015.Read())
                                {
                                    IDklient = reader11015[0].ToString();
                                }
                                reader11015.Close();
                                str11015.Close();




                                String punkt = "";
                                SqlConnection str110151 = new SqlConnection(connecting);
                                str110151.Open();
                                SqlCommand command110151 = new SqlCommand("select ПунктВыдачи from Клиенты where(ID_Клиента = '" + IDklient + "')", str110151);
                                SqlDataReader reader110151 = command110151.ExecuteReader();
                                while (reader110151.Read())
                                {
                                    punkt = reader110151[0].ToString();
                                }
                                reader110151.Close();
                                str110151.Close();




                                string treknomer = "";
                                int[] myArray = new int[5];
                                Random rand = new Random();

                                for (int x = 0; x < myArray.Length; x++)
                                {
                                    myArray[x] = rand.Next(20);
                                    treknomer += myArray[x];
                                }
                                Random random = new Random();
                                SqlConnection str665 = new SqlConnection(connecting);
                                str665.Open();
                                SqlCommand command665 = new SqlCommand($"insert into  Продажи(ID_Товара,ID_Клиента,Количество,Цена,Способ_оплаты,Дата_продажи, ТрекНомер, ID_Статуса_отпраки,Пункт_выдачи) values('" + IDtovara + "','" + IDklient + "', '1' , '"+price+"' , 'Списание с личного кабинета', '"+DateTime.Now +"', "+treknomer+" , '1' , '"+punkt+"')", str665);
                                command665.ExecuteNonQuery();
                                str665.Close();
                                break;







                            case "Возврат":

                                await botClient.SendTextMessageAsync(mesage.From.Id, " Для возврата отнесите товар в пункт выдачи и сообщите ваш трек номер! ");








                                break;





                            default:
                                // Обработка других кнопок
                                break;
                        }
                        await botClient.AnswerCallbackQueryAsync(query.Id);





                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }





                switch (mesage.Text) // считываем текст сообщения
                {



                    case "/start":
                        var replyKeyboard = new ReplyKeyboardMarkup(new[] // создаём массив кнопок в панели
                        {
                        new[]
                        {
                            new KeyboardButton("Каталог товаров"),
                              new KeyboardButton("Корзина")
                        },
                         new[]
                        {
                             new KeyboardButton("Личный кабинет"),
                              new KeyboardButton("История заказов")
                         },
                          new[]
                        {
                           new KeyboardButton("Связь с админом"),
                            new KeyboardButton("Правила магазина")
                         }
                    })
                        {
                            ResizeKeyboard = true // уменьшает размер кнопок
                        };
                        await botClient.SendTextMessageAsync(mesage.From.Id, "У вас есть доступ к магазину, удачных покупок!", replyMarkup: replyKeyboard);
                        break;





                    case "Связь с админом":
                        InlineKeyboardMarkup inlineKeyboard11 = new(new[]
                        {
                        InlineKeyboardButton.WithUrl(
                            text: "Ссылка ↗",
                            url: "https://t.me/jablazyabka")
                    });
                        Message sentMessage = await botClient.SendTextMessageAsync(
                            chatId: e.Message.Chat.Id,
                            text: "❗ Внимание! неуважительное общение с администратором, приведёт к блокировке аккаунта на 1 день! ❗",
                            replyMarkup: inlineKeyboard11);
                        break;





                    case "Правила магазина":
                        await botClient.SendPhotoAsync(
                       chatId: mesage.From.Id,
                       photo: "https://i.postimg.cc/gkkQxDV2/Rules-Gold-Letters-o-1.jpg",
                       caption: "1) Уважительное отношение к сотрудникам магазина и другим покупателям.\r\n 2) Соблюдение законов и правил, связанных с продажей товаров.\r\n 3) Оплата товаров и услуг в установленные сроки.\r\n 4) Использование товаров и услуг магазина в соответствии с их назначением.\r\n 5) Соблюдение правил безопасности при использовании товаров и услуг.\r\n 6) Запрет на распространение ложной информации о магазине, его сотрудниках и товарах.\r\n 7) При нарушении этих правил, покупатель может быть временно заблокирован на один день, без возможности восстановления доступа к аккаунту.\r\n 8) Решение о блокировке принимается администратором магазина на основе фактов нарушения правил.\r\n 9) Покупатель имеет право обжаловать решение о блокировке, предоставив доказательства своей невиновности.\r\n 10) В случае необоснованной блокировки, покупатель имеет право на компенсацию ущерба и возврат средств, потраченных на товары и услуги магазина. ");
                        break;





                    case "Личный кабинет":

                        var inlineKeyboard12 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                        {
                    new [] // первый массив кнопок 
                    {
                        InlineKeyboardButton.WithCallbackData("Изменить пункт выдачи","Изменить пункт выдачи"),// 1 это назваание 2 это обращение или что то типа того
                          InlineKeyboardButton.WithCallbackData("Пополнить счёт","Пополнить счёт"),

                    }

                    });
                        string adress = "";
                        SqlConnection str11 = new SqlConnection(connecting);
                        str11.Open();
                        SqlCommand command11 = new SqlCommand("select ПунктВыдачи  from Клиенты WHERE TelegramID = '" + infoID + "'", str11);
                        SqlDataReader reader11 = command11.ExecuteReader();
                        while (reader11.Read())
                        {

                            adress = reader11[0].ToString();

                        }
                        reader11.Close();
                        str11.Close();


                        await botClient.SendTextMessageAsync(mesage.From.Id, "Личный кабинет\r\n\r\nUSerName: " + name + "\r\n\r\nВаш ID: " + infoID + "\r\n\r\nБаланс: 0.0р\r\n\r\nПункт выдачи: " + adress + "", replyMarkup: inlineKeyboard12);
                        break;



                    case "Каталог товаров":




                        var inlineKeyboard111 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                        {
                        new [] // первый массив кнопок 
                        {
                            InlineKeyboardButton.WithCallbackData("Противо капканные Ботинки","Противо капканные Ботинки"),// 1 это назваание 2 это обращение или что то типа того
                            
                        },
                         new [] // первый массив кнопок 
                        {
                           
                              InlineKeyboardButton.WithCallbackData("Крепления","Крепления"),
                              
                        },
                           new [] // первый массив кнопок 
                        {

                             
                                InlineKeyboardButton.WithCallbackData("Камуфляж","Камуфляж"),
                        }
                           ,
                           new [] // первый массив кнопок 
                        {


                                InlineKeyboardButton.WithCallbackData("Оружие","Оружие"),
                        }

                        });
                        await botClient.SendTextMessageAsync(mesage.From.Id, "Выберите вид товара", replyMarkup: inlineKeyboard111); // выводим в переписку созданные кнопки

                    

                        break;



                    case "История заказов":

                      

                        IDklient = "";
                        SqlConnection str11015 = new SqlConnection(connecting);
                        str11015.Open();
                        SqlCommand command11015 = new SqlCommand("select ID_Клиента from Клиенты Where TelegramID =  '" + infoID + "'", str11015);
                        SqlDataReader reader11015 = command11015.ExecuteReader();
                        while (reader11015.Read())
                        {
                            IDklient = reader11015[0].ToString();
                        }
                        reader11015.Close();
                        str11015.Close();
                        string Count = "";
                        Photo = "";
                        Diskription = "";
                        price = "";
                        string sposoboplat = "";
                        string datasell = "";
                        string treksell = "";
                        SqlConnection str11992 = new SqlConnection(connecting);
                        str11992.Open();
                        SqlCommand command11992 = new SqlCommand("select Товары.Описание,Товары.Фото,ID_Клиента , Количество, Продажи.Цена, Способ_оплаты ,Дата_продажи,ТрекНомер from Продажи join Товары ON Товары.ID_Товара = Продажи.ID_Товара where ID_Клиента =  " + IDklient + "", str11992);
                        SqlDataReader reader11992 = command11992.ExecuteReader();
                        while (reader11992.Read())
                        {
                            Diskription = reader11992[0].ToString();
                            Photo = reader11992[1].ToString();
                            Count = reader11992[3].ToString();
                            price = reader11992[4].ToString();
                            sposoboplat = reader11992[5].ToString();
                            datasell = reader11992[6].ToString();
                            treksell = reader11992[7].ToString();



                            var inlineKeyboard1112 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                        {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Возврат","Возврат"),// 1 это назваание 2 это обращение или что то типа того
                                    
                                }

                                });
                            await botClient.SendPhotoAsync(
                      chatId: mesage.From.Id,
                      photo: Photo,
                      caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nОплачено: " + price + " \nСпособ оплаты: "+sposoboplat+" \nДата продажи: "+datasell+" \nТрек номер: "+treksell+" ", replyMarkup: inlineKeyboard1112);

                           

                        }
                        reader11992.Close();
                        str11992.Close();

                        break;
















                    case "Корзина":



                        SqlConnection str110154 = new SqlConnection(connecting);
                        str110154.Open();
                        SqlCommand command110154 = new SqlCommand("select ID_Клиента from Клиенты Where TelegramID =  '" + infoID + "'", str110154);
                        SqlDataReader reader110154 = command110154.ExecuteReader();
                        while (reader110154.Read())
                        {
                            IDklient = reader110154[0].ToString();
                        }
                        reader110154.Close();
                        str110154.Close();

                        string check = "";
                        SqlConnection str1015 = new SqlConnection(connecting);
                        str1015.Open();
                        SqlCommand command1015 = new SqlCommand("select ID_Товара from Корзины Where ID_Клиента =  '" + IDklient + "'", str1015);
                        SqlDataReader reader1015 = command1015.ExecuteReader();
                        while (reader1015.Read())
                        {
                            check = reader1015[0].ToString();





                            SqlConnection str110151 = new SqlConnection(connecting);
                            str110151.Open();
                            SqlCommand command110151 = new SqlCommand("select Товары.Описание,Товары.Фото, Цена,Категории.Категория from Товары join Категории on Категории.ID_Категории = Товары.ID_Категории join Корзины ON Корзины.ID_Товара = Товары.ID_Товара where Корзины.ID_Товара =  " + check + "", str110151);
                            SqlDataReader reader110151 = command110151.ExecuteReader();
                            while (reader110151.Read())
                            {
                                Diskription = reader110151[0].ToString();
                                Photo = reader110151[1].ToString();
                                price = reader110151[2].ToString();
                                Kategory = reader110151[3].ToString();




                                var inlineKeyboard1112 = new InlineKeyboardMarkup(new[]      // создаём клавиатуру инлайн
                                {
                                new [] // первый массив кнопок 
                                {
                                    InlineKeyboardButton.WithCallbackData("Оплатить","Оплатить"),// 1 это назваание 2 это обращение или что то типа того
                                    
                                }

                                });
                                await botClient.SendPhotoAsync(
                          chatId: mesage.From.Id,
                          photo: Photo,
                          caption: "Описание товара: \nКатегория: " + Kategory + " \nОписние: " + Diskription + " \nЦена: " + price + " ", replyMarkup: inlineKeyboard1112);
                            }
                            reader110151.Close();
                            str110151.Close();
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(mesage.From.Id, "Доступ заблокирован");
            }
           
        }
    }
}






    



















