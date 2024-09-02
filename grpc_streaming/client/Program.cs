using Grpc.Core;
using Grpc.Net.Client;
using Messages;
using System;
using System.Threading.Channels;

namespace client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:50005");

            await BiDirectionalStreamingCallAsyncWord(channel);
            await BiDirectionalStreamingCallAsyncAgeCheck(channel);
        }

        static async Task BiDirectionalStreamingCallAsyncWord(GrpcChannel channel)
        {
            var client = new MessageServices.MessageServicesClient(channel);
            Random random = new Random();

            string[] stringArray =
            {
                "Lucas", "Izaltino", "Izal", "jubileudeazeu"
            };

            //Get request stream
            var request = client.GetMostLengthString();

            //Listen for responses with thread
            var serverListenerTask = Task.Run(async () =>
            {
                await foreach (var response in
                    request.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Server --> Client || Max so far: {response.Word}");
                }
            });

            //Prepare request
            foreach (var word in stringArray)
            {
                Console.WriteLine(
                    $"Client --> Server || Sending string: {word}");
                await request.RequestStream.WriteAsync(
                    new SingleWordMessage()
                    {
                        Word = word
                    });
                await Task.Delay(random.Next(1, 3) * 1000);
            }
            await request.RequestStream.CompleteAsync();
            //Complete request preparation

            await serverListenerTask;
            Console.WriteLine($"_________________________________");
        }

        static async Task BiDirectionalStreamingCallAsyncAgeCheck(GrpcChannel channel)
        {
            var client = new MessageServices.MessageServicesClient(channel);
            var request = client.CheckIfOverAge();
            Random random = new Random();

            var people = new[]
            {
                new SinglePersonMessage { Name = "Lucas", Idade = 22 },
                new SinglePersonMessage { Name = "Izaltino", Idade = 17 },
                new SinglePersonMessage { Name = "Izal", Idade = 20 },
                new SinglePersonMessage { Name = "Jubileudeazeu", Idade = 16 }
            };

            var serverListenerTask = Task.Run(async () =>
            {
                await foreach (var response in request.ResponseStream.ReadAllAsync())
                {
                    Console.WriteLine($"Server --> Client || {response.Message}");
                }
            });

            foreach (var person in people)
            {
                Console.WriteLine($"Client --> Server || Sending: {person.Name}, Age: {person.Idade}");
                await request.RequestStream.WriteAsync(person);
                await Task.Delay(random.Next(1, 3) * 1000);
            }


            await request.RequestStream.CompleteAsync();
            await serverListenerTask;

            Console.WriteLine($"_________________________________");
        }
    }
}