using System;
using System.IO;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace FCM
{
    class Program
    {
        static void Main(string[] args)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Directory.GetCurrentDirectory() + "/serviceAccountKey.json"),
            });

            Start();
        }

        static void Start()
        {
            Console.Write("Please enter target Token: ");

            var messageInput = Console.ReadLine();
            if (String.IsNullOrEmpty(messageInput))
            {
                Console.WriteLine("Are u disco or cola?"); // Only those who watch "Cem Yilmaz" can understand. :)

                Start();
            }

            SendNotification(messageInput);

            Start();
        }

        static void SendNotification(string token)
        {
            var message = new Message
            {
                Token = token,
                Notification = new Notification
                {
                    Title = "Firebase Notification Test",
                    Body = "Hi, everyone! Time: " + DateTime.Now.ToString()
                }
            };

            var result = FirebaseMessaging.DefaultInstance.SendAsync(message)
                .ConfigureAwait(false)
                .GetAwaiter().GetResult();

            Console.WriteLine("Response: " + result);
        }
    }
}