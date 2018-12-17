using System;

namespace popMongo
{
    static class Ambiente
    {
        public static string getMongoHost()
        {
            string host = Environment.GetEnvironmentVariable("MONGO_SERVER_HOST");
            if (host is null) host = "localhost";
            return host;
        }

        public static string getMongoDatabase()
        {
            string schema = Environment.GetEnvironmentVariable("MONGO_SERVER_DATABASE_NAME");
            if (schema is null) schema = "realtime";
            return schema;
        }
        public static string getMongoCollection()
        {
            string collection = Environment.GetEnvironmentVariable("MONGO_SERVER_COLLECTION");
            if (collection is null) collection = "veiculos";
            return collection;
        }
        public static int getMongoPort()
        {
            int porta = 27017;
            string portaStr = Environment.GetEnvironmentVariable("MONGO_SERVER_PORT");
            if (portaStr is null) return porta;
            try
            {
                return Int32.Parse(portaStr);
            }
            catch (Exception e)
            {
                Console.WriteLine("A porta do Mongo lida do ambiente era inválida. usando o Default 27017");
                Console.WriteLine(e.Message);
                return porta;
            }
        }

        public static string getMongoUser()
        {
            String user = Environment.GetEnvironmentVariable("MONGO_SERVER_USER");
            if (user is null) user = "root";
            return user;
        }

        public static String getMongoPassword()
        {
            string pass = Environment.GetEnvironmentVariable("MONGO_SERVER_PASSWORD");
            if (pass is null) pass = "rootpass";
            return pass;
        }

        public static String getMongoAuthMethod()
        {
            string method = Environment.GetEnvironmentVariable("MONGO_SERVER_AUTH_METHOD");
            if (method is null) method = "?authenticationDatabase=admin";
            return method;
        }
        public static String getRabbitHost()
        {
            string host = Environment.GetEnvironmentVariable("RABBIT_HOST");
            if (host is null) host = "localhost";
            return host;
        }

        public static String getRabbitTopic()
        {
            string topico = Environment.GetEnvironmentVariable("RABBIT_TOPIC");
            if (topico is null) topico = "CETURB";
            return topico;
        }

        public static String getRabbitKey()
        {
            string key = Environment.GetEnvironmentVariable("RABBIT_KEY");
            if (key is null) key = "#";
            return key;
        }
        public static String getRabbitChannelName()
        {
            string canal = Environment.GetEnvironmentVariable("RABBIT_CHANNEL");
            if (canal is null) canal = "realtime";
            return canal;
        }

        public static bool isProduction()
        {
            string env = Environment.GetEnvironmentVariable("NODE_ENV");
            if (env is null) return false;
            if (env.Equals("production")) return true;
            return false;
        }
    }
}
