using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace AppEguraldiaAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string hiria = textBox1.Text.Trim(); 
                if (string.IsNullOrEmpty(hiria))
                {
                    MessageBox.Show("Mesedez, sartu hiriaren izena.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string apiKey = "a2fc01bcaea326e089b76dc7f2b4a58e"; 

                string url = $"https://api.openweathermap.org/data/2.5/weather?q={hiria}&appid={apiKey}&units=metric";

                HttpClient bezeroa = new HttpClient();

                using (HttpResponseMessage erantzuna = await bezeroa.GetAsync(url))
                {
                    if (erantzuna.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        // Obtener el contenido de la respuesta
                        string webEdukia = await erantzuna.Content.ReadAsStringAsync();

                        // Parsear el JSON
                        JObject eguraldia = JObject.Parse(webEdukia);

                        // Extraer datos de la respuesta JSON
                        string descripcion = eguraldia["weather"][0]["description"].ToString();
                        string temperatura = eguraldia["main"]["temp"].ToString();
                        string presion = eguraldia["main"]["pressure"].ToString();
                        string humedad = eguraldia["main"]["humidity"].ToString();

                        // Mostrar resultados en Labels y ListBox
                        label2.Text = $"Egoera: {descripcion}";
                        label3.Text = $"Tenperatura: {temperatura}°C";
                        listBox1.Items.Clear();
                        listBox1.Items.Add($"Presioa: {presion} hPa");
                        listBox1.Items.Add($"Hezetasuna: {humedad}%");
                    }
                    else
                    {
                        MessageBox.Show("Ez da hiria aurkitu edo errore bat gertatu da.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Errorea: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore orokorra: {ex.Message}", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}