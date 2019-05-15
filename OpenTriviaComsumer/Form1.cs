using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTriviaConsumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("https://opentdb.com");
        }

        private static HttpClient client;

        private async void Form1_Load(object sender, EventArgs e)
        {
            //HttpClient client = new HttpClient();
            //Access the list of categories from the Web API
            HttpResponseMessage response = await client.GetAsync("api_category.php");

            if (response.IsSuccessStatusCode)
            {
                // category = cats
                string cats = await response.Content.ReadAsStringAsync();

                CategoryResponse catResponse = JsonConvert.DeserializeObject<CategoryResponse>(cats);

                List<TriviaCategory> entertainment = GetEntertainmentCategories(catResponse);
                PopluateCategoryComboBox(entertainment);
            }
        }
        private void PopluateCategoryComboBox(List<TriviaCategory> entertainment)
        {
            foreach (TriviaCategory category in entertainment)
            {
            //sorts by name
            //cboCategories.Items.Add(category.name);
                #region Comments for ComboBox
                //cboCategories.DataSource = catResponse.trivia_categories;
                //cboCategories.DisplayMember = nameof(TriviaCategory.name);
                //catResponse.trivia_categories.Sort();

                //MessageBox.Show(cats);
                //List<TriviaCategory> categories = catResponse.trivia_categories;
                #endregion
                cboCategories.Items.Add(category);
            }
        }

        private static List<TriviaCategory> GetEntertainmentCategories(CategoryResponse catResponse)
        {
            //LINQ to Objects
            //All entertainment categories sorted alphabetically
            return catResponse.trivia_categories
                .Where(c => c.name.StartsWith("Entertainment"))
                .OrderBy(c => c.name)
                .ToList();
        }
        private async void cboCategories_SelectedIndexChange(object sender, EventArgs e)
        {
            if (cboCategories.SelectedIndex < 0)
                return;

            //Get selected category id
            //TriviaCategory cat = (TriviaCategory)cboCategories.Select
            TriviaCategory cat = cboCategories.SelectedItem as TriviaCategory;
            int selectedId = cat.id;

            //Get number of questions in that category
            HttpResponseMessage msg = await client.GetAsync($"api_count.php?category={selectedId}");

            if (msg.IsSuccessStatusCode)
            {
                string response = await msg.Content.ReadAsStringAsync();
                MessageBox.Show(response);
            }
        }
    }
}
