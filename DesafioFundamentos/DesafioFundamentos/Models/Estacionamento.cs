namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private DateTime horaInicial;
        private List<string> veiculos = new List<string>();
        private List<DateTime> horarios = new List<DateTime>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            string placa = "";

            Console.WriteLine("Digite a placa do veículo para estacionar: ");

            placa = Console.ReadLine();

            int encontrou = veiculos.FindIndex(x => x.ToUpper() == placa.ToUpper());

            if(encontrou > -1)
            {
                Console.WriteLine($"Este veículo já está estacionado aqui desde às {horarios[encontrou].ToString("HH:mm:ss")}. Digite uma nova placa.\n\n ");
                AdicionarVeiculo();
                return;
            }

            veiculos.Add(placa);
            horarios.Add(DateTime.Now);
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            string placa = Console.ReadLine();

            int indice = veiculos.FindIndex(x => x.ToUpper() == placa.ToUpper());

            // Verifica se o veículo existe
            if (indice >= 0)
            {
                TimeSpan diferenca = DateTime.Now - horarios[indice];
                int horas = diferenca.Hours;

                if(horas <= 0)
                  horas = 1;
          
                decimal valorTotal = this.precoInicial + (horas * this.precoPorHora);

                Console.WriteLine($"\nO veículo foi estacionado às {horarios[indice].ToString("HH:mm:ss")}.\nTempo total: {horas} hora(s).\nValor total: R$ {valorTotal}");

                Console.WriteLine("Digite o valor pago pelo cliente: ");

                decimal valorPago = Convert.ToDecimal(Console.ReadLine());

                while (valorPago < valorTotal) {
                    Console.WriteLine($"Está faltando R$ {valorTotal - valorPago}. Digite o valor pago novamente:");
                    valorPago = Convert.ToDecimal(Console.ReadLine());
                }

                if (valorPago > valorTotal)
                {
                    Console.WriteLine($"Troco: R$ {valorPago - valorTotal}");
                }

                veiculos.RemoveAt(indice);
                horarios.RemoveAt(indice);

                Console.WriteLine("Veículo removido com sucesso!\n\nVeículos ainda estacionados: ");

                ListarVeiculos(false);
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Deseja digitar a placa novamente? Digite SIM ou NÃO ");

                if(Console.ReadLine() == "SIM")
                {
                    RemoverVeiculo();
                }
            }
        }

        public void ListarVeiculos(bool titulo = true)
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                if (titulo)
                {
                    Console.WriteLine("Os veículos estacionados são:");
                }
                
                for(int i = 0; i < veiculos.Count; i++)
                {
                    Console.Write($"Veículo {i+1}: {veiculos[i]}. Horário de Entrada: {horarios[i].ToString("HH:mm:ss")}\n");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}