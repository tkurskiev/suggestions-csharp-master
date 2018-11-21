using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using suggestionscsharp;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SuggestClient api;
        public Form1()
        {
            InitializeComponent();

            var token = "278acbb5adacb6e1c7ba3aad25e802c2c1e9952a"; // 10 000  suggestions per twenty-four hours
            //var token = "5ef98f5781a106962077fb18109095f9f11ebac1";
            var url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
            api = new SuggestClient(token, url);


            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;

            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;

            comboBox1.Text = "г Москва";
            comboBox1.DroppedDown = false;
            comboBox2.DroppedDown = false;
            

            

            //api.QueryBank



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            var query = textBox1.Text;
            var response = api.QueryAddress(query);
            //Console.WriteLine(string.Join("\n", response.suggestions));

            //textBox1.Text += response.suggestions;
            label1.Text = string.Join("\n", response.suggestions);

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (comboBox1.Text.Length > 9)
            {
                var query = comboBox1.Text;
                var response = api.QueryAddress(query);
                //AutoCompleteStringCollection strings = new AutoCompleteStringCollection();
                //foreach (var item in response.suggestions)
                //{
                //    strings.Add(item.ToString());
                //}

                //comboBox1.AutoCompleteCustomSource = strings;
                foreach(var item in response.suggestions)
                {
                    comboBox1.Items.Add(item);
                }
            }
            this.comboBox1.DroppedDown = true;
            comboBox1.Select(comboBox1.Text.Length, 0);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox2.SelectedIndex;
            if(index >= 0)
            {
                label2.Text = "Адрес";
                label3.Text = "БИК";
                label4.Text = "Полное наименование";
                label5.Text = "Swift";

                label6.Text = currentSuggestions[index].data.address?.ToString();
                label7.Text = currentSuggestions[index].data.bic?.ToString();
                label8.Text = currentSuggestions[index].data.name.full?.ToString();
                label9.Text = currentSuggestions[index].data.swift?.ToString();
                //label10.Text = currentSuggestions[index].data.name?.payment.ToString();
            }
            
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            //if(currentSuggestions != null)
                currentSuggestions?.Clear();
            if (comboBox2.Text.Length > 2)
            {
                var query = comboBox2.Text;
                var response = api.QueryBank(query);
                //AutoCompleteStringCollection strings = new AutoCompleteStringCollection();
                //foreach (var item in response.suggestions)
                //{
                //    strings.Add(item.ToString());
                //}

                //comboBox1.AutoCompleteCustomSource = strings;
                foreach (var item in response.suggestions)
                {
                    comboBox2.Items.Add(item);
                }

                currentSuggestions = response.suggestions;
                label10.Text = currentSuggestions.Count.ToString();
            }
            this.comboBox2.DroppedDown = true;
            comboBox2.Select(comboBox1.Text.Length, 0);
            
        }

        public List<SuggestBankResponse.Suggestions> currentSuggestions { get; set; }
    }
}
