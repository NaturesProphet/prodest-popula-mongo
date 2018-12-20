using System;

/**
Fiz esta classe para obter um código mais limpo no programa principal.
O objetivo é apenas printar erros e informações no terminal.
*/
namespace popMongo
{
    static class X9
    {

        public static void ShowInfo(int NumeroMagico, String msg)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[POP-MONGO]   Serviço inicializado em modo de produção!");
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[POP-MONGO]   Serviço inicializado em modo de desenvolvimento!");
                    Console.WriteLine("[POP-MONGO]   Aguardando a chegada dos dados...\n");
                    Console.ResetColor();
                    break;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[POP-MONGO]   Recebendo dados: " + msg);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.ResetColor();
                    break;
            }
        }


        public static void OQueRolouNaParada(Exception e, int NumeroMagico)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n##########################################################");
                    Console.WriteLine("[POP-MONGO]   Erro ao tentar se conectar com o RabbitMQ");
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[ ERROR:  ]   " + e.Message);
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("[POP-MONGO]   Dados da conexão listados a seguir:\n");
                    Console.WriteLine("[POP-MONGO]   Host: " + Ambiente.getRabbitHost());
                    Console.WriteLine("[POP-MONGO]   Topico: " + Ambiente.getRabbitTopic());
                    Console.WriteLine("[POP-MONGO]   Chave de Rota: " + Ambiente.getRabbitKey());
                    Console.WriteLine("[POP-MONGO]   Canal: " + Ambiente.getRabbitChannelName());
                    Console.WriteLine("##########################################################");
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n#############################################");
                    Console.WriteLine("Erro ao tentar salvar no MongoDB");
                    Console.WriteLine("#############################################");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(e.Message + "\n");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("ERRO NAO ESPECIFICADO");
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
