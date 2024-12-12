using System;
using System.Collections.Generic;

// Класс пользователя
public class User
{
    public string Name { get; set; }
    public ChatMediator Mediator { get; set; }
    public List<string> MessageHistory { get; set; }

    public User(string name, ChatMediator mediator)
    {
        Name = name;
        Mediator = mediator;
        MessageHistory = new List<string>();
    }

    // Метод отправки сообщения
    public void SendMessage(string message, string recipientName)
    {
        Mediator.SendMessage(message, Name, recipientName);
    }

    // Метод получения сообщения
    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"Пользователь {Name} получил сообщение: {message}");
        MessageHistory.Add(message);
    }

    // Метод вывода истории сообщений
    public void DisplayMessageHistory()
    {
        Console.WriteLine($"История сообщений пользователя {Name}:");
        foreach (var message in MessageHistory)
        {
            Console.WriteLine(message);
        }
    }
}

// Класс посредник
public class ChatMediator
{
    public List<User> Users { get; set; }

    public ChatMediator()
    {
        Users = new List<User>();
    }

    // Метод добавления пользователя в чат
    public void AddUser(User user)
    {
        Users.Add(user);
    }

    // Метод отправки сообщения от одного пользователя к другому
    public void SendMessage(string message, string senderName, string recipientName)
    {
        var recipient = Users.Find(u => u.Name == recipientName);
        if (recipient != null)
        {
            recipient.ReceiveMessage($"{senderName}: {message}");
        }
        else
        {
            Console.WriteLine($"Пользователь {recipientName} не найден в чате.");
        }
    }

    // Метод удаления пользователя из чата
    public void RemoveUser(User user)
    {
        Users.Remove(user);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание посредника
        var mediator = new ChatMediator();

        // Создание пользователей
        var user1 = new User("Пользователь1", mediator);
        var user2 = new User("Пользователь2", mediator);

        // Добавление пользователей в чат
        mediator.AddUser(user1);
        mediator.AddUser(user2);

        // Отправка сообщений
        user1.SendMessage("Привет!", "Пользователь2");
        user2.SendMessage("Привет!", "Пользователь1");

        // Вывод истории сообщений
        user1.DisplayMessageHistory();
        user2.DisplayMessageHistory();

        // Удаление пользователя из чата
        mediator.RemoveUser(user2);

        // Попытка отправки сообщения удаленному пользователю
        user1.SendMessage("Привет!", "Пользователь2");
    }
}
