using Grpc.Core;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.services
{
    internal class MessageServicesImpl : MessageServices.MessageServicesBase
    {
        public override async Task GetMostLengthString(
            IAsyncStreamReader<SingleWordMessage> requestStream,
            IServerStreamWriter<SingleWordMessage> responseStream,
            ServerCallContext context)
        {
            string result = "";

            await foreach (var request in requestStream.ReadAllAsync())
            {
                Console.WriteLine($"String received: {request.Word}");
                if (request.Word.Length > result.Length)
                {
                    result = request.Word;
                    await responseStream.WriteAsync(
                        new SingleWordMessage()
                        {
                            Word = result
                        });
                }
            }
        }

    }
}