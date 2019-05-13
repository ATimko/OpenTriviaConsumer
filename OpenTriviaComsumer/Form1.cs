﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTriviaComsumer
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

            HttpResponseMessage response = await client.GetAsync("api_category.php");

            if (response.IsSuccessStatusCode)
            {
                // category = cats
                string cats = await response.Content.ReadAsStringAsync();
                MessageBox.Show(cats);
            }
        }
    }
}