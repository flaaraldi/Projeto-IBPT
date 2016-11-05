using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TesteIBPT
{
    public partial class frmIBPT : Form
    {
        public frmIBPT()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public void Get(string token, string cnpj, string codigo, string uf, string descricao, string unidadeMedida, decimal valor)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("http://iws.ibpt.org.br/api/Servicos?token={0}&cnpj={1}&codigo={2}&uf={3}&descricao={4}&unidadeMedida={5}&valor={6}", token, cnpj, codigo, uf, descricao, unidadeMedida, valor);
                var response = client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var servico = response.Content.ReadAsAsync<ServicoDTO>();
                    Console.WriteLine("Serviço: {0} - {1}", produto.Codigo, produto.Descricao);
                }
                else
                {
                    Console.WriteLine("Falha");
                }
            }
        }

        public void Post(string token, string localFilePath)
        {
            using (var httpClient = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    var fileContent = new ByteArrayContent(File.ReadAllBytes(localFilePath));
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "nfe" };
                    formData.Add(fileContent);

                    var response = httpClient.PostAsync(string.Concat("http://iws.ibpt.org.br/api/NFE?token=", token), formData);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var nfe = response.Content.ReadAsAsync<NFEDTO>();
                        Console.WriteLine("{0} - Quantidade de itens: {1}", nfe.Chave, nfe.Produtos.Count);
                    }
                    else
                    {
                        Console.WriteLine("Falha");
                    }
                }
            }
        }

        public void Get(string token, string cnpj, string codigo, string uf, int ex, string codigoInterno, string descricao, string unidadeMedida, decimal valor, string gtin)
        {
            using (var client = new HttpClient())
            {
                var url = string.Format("http://iws.ibpt.org.br/api/Produtos?token={0}&cnpj={1}&codigo={2}&uf={3}&ex={4}&codigoInterno={5}&descricao={6}&unidadeMedida={7}&valor={8}&gtin={9}", token, cnpj, codigo, uf, ex, codigoInterno, descricao, unidadeMedida, valor, gtin);
                var response = client.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var produto = response.Content.ReadAsAsync<ProdutoDTO>();
                    Console.WriteLine("Produto: {0} - {1}", produto.Codigo, produto.Descricao);
                }
                else
                {
                    Console.WriteLine("Falha");
                }
            }
        }
    }
}
