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


            //em produção, não exibirá a saída de informações no console.
            if (Ambiente.isProduction())
            {
                try
                {
                    Console.Clear();
                    factory = new ConnectionFactory() { HostName = RMQHost };
                    connection = factory.CreateConnection();
                    channel = connection.CreateModel();
                    channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                    queueName = channel.QueueDeclare(RMQChannel).QueueName;
                    channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                    X9.ShowInfo(1, "");
                    EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                    consumidorEventos.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        String message = Encoding.UTF8.GetString(body);
                        dao.salva(message);
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumidorEventos);
                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(e, 1);
                }
            }



            //fora do ambiente de produção, ativar todas as mensagens de informação no console
            else
            {
                try
                {
                    Console.Clear();
                    factory = new ConnectionFactory() { HostName = RMQHost };
                    connection = factory.CreateConnection();
                    channel = connection.CreateModel();
                    channel.ExchangeDeclare(exchange: RMQTopic, type: "topic", durable: true);
                    queueName = channel.QueueDeclare(RMQChannel).QueueName;
                    channel.QueueBind(queue: queueName, exchange: RMQTopic, routingKey: RMQKey);
                    X9.ShowInfo(2, "");
                    EventingBasicConsumer consumidorEventos = new EventingBasicConsumer(channel);
                    consumidorEventos.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body;
                        String message = Encoding.UTF8.GetString(body);
                        X9.ShowInfo(3, message);
                        dao.salva(message);
                    };
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumidorEventos);
                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(e, 1);
                }
            }
        }
    }
}
