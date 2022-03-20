using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ESTABELECE CONEXAO COM O RABBITMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                // CRIA UMA FILA PARA TRANSMITIR AS MENSAGENS
                channel.QueueDeclare(queue: "qteste",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                int count = 0;

                while (true)
                {
                    // DEFINE A MENSAGEM A SER TRANSMITIDA
                    string message = $"Mensagem teste {count++}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "qteste",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(message + " enfileirada ");

                    //TEMPORIZADOR PARA SIMULAR PROCESSAMENTO
                    System.Threading.Thread.Sleep(6000);
                }


            }

        }
    }
}
