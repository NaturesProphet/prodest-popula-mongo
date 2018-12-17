using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace popMongo
{
    class Program
    {
        protected static DAO dao = new DAO();
        protected static string RMQHost = Ambiente.getRabbitHost();
        protected static string RMQTopic = Ambiente.getRabbitTopic();
        protected static string RMQKey = Ambiente.getRabbitKey();
        protected static string RMQChannel = Ambiente.getRabbitChannelName();
        static void Main(string[] args)
        {
            ConnectionFactory factory;
            IConnection connection;
            IModel channel;
            String queueName;
            DAO dao = new DAO();
            try
            {
                factory = new ConnectionFactory() { HostName = RMQHost };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                queueName = channel.QueueDeclare(RMQChannel).QueueName;
                channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[POP-MONGO]   Serviço inicializado!");
                Console.WriteLine("[POP-MONGO]   Aguardando a chegada dos dados...\n");
                Console.ResetColor();
                EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                consumidorEventos.Received += (model, ea) =>
                {
                    byte[] body = ea.Body;
                    String message = Encoding.UTF8.GetString(body);
                    if (!Ambiente.isProduction())
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("[POP-MONGO]   Recebendo dados: " + message);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.ResetColor();
                    }
                    dao.salva(message);
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumidorEventos);

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n##########################################################");
                Console.WriteLine("[POP-MONGO]   Erro ao tentar se conectar com o RabbitMQ");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("[ ERROR:  ]   " + e.Message);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("[POP-MONGO]   Dados da conexão listados a seguir:\n");
                Console.WriteLine("[POP-MONGO]   Host: " + RMQHost);
                Console.WriteLine("[POP-MONGO]   Topico: " + RMQTopic);
                Console.WriteLine("[POP-MONGO]   Chave de Rota: " + RMQKey);
                Console.WriteLine("[POP-MONGO]   Canal: " + RMQChannel);
                Console.WriteLine("##########################################################");
                Console.ResetColor();
            }
        }
    }
}
