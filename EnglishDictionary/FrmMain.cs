using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;


namespace EnglishDictionary
{
    
    public partial class FrmMain : Form
    {
        BindingSource worddata = new BindingSource();
        waystore ws = new waystore();
        string allword;

        public FrmMain()
        {
            InitializeComponent();
            //*********************************************//
            //Load the data . In through listwms.json file.//
            //*********************************************//
            timer1.Start();
            worddata.DataSource = wmlistword.WMlist;
            LbDictionaryData.DataSource = worddata;
            LbDictionaryData.DisplayMember = "WordAndMean";
            string fileName = "listwms.json";
            string jsonString = File.ReadAllText(fileName);
            List<WoMe> j = JsonSerializer.Deserialize<List<WoMe>>(jsonString);
            for (int i = j.Count - 1; i >= 0; i--)
            {
                wmlistword.WMlist.Add(j[i]);
               
            }
            worddata.ResetBindings(false);
        }
        public class seriANDdeseri
        {
            //**************************************************//
            //Words are serializea and save to listwms.json file//
            //**************************************************//
            public void serializelist()
            {
                
                    string fileName = "listwms.json";
                    string jsonString = JsonSerializer.Serialize(wmlistword.WMlist);
                    File.WriteAllText(fileName, jsonString);

            }
            //*****************************************************************************//
            //When seriaize words, meaning are deserializeing and save to datafile.dat.text//
            //*****************************************************************************//
            public void create()
            {

                string Jsonword= File.ReadAllText("listwms.json");
                
                List<WoMe> jw = JsonSerializer.Deserialize<List<WoMe>>(Jsonword);

                using (StreamWriter ws = new StreamWriter(@"datafile.dat.Text"))
                    foreach (WoMe o in jw)
                    {
                        string jsonString = JsonSerializer.Serialize(o);
                        ws.WriteLine(jsonString);
                    }
            }

            public void fileupdate()
            {

                
                string fileName = "listwms.json";
                string jsonString = File.ReadAllText(fileName);
                List<WoMe> j = JsonSerializer.Deserialize<List<WoMe>>(jsonString);

                serializelist();
                create();

                if (j != wmlistword.WMlist)
                {
                    create();
                }
            }
        }
        //*****************************************************************//
        //When click the add btn. words, meanings are save to list , files.//
        //*****************************************************************//
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            
            string word = TxtWord.Text;
            string mean = TxtMeaning.Text;
            ws.store(word, mean);
            worddata.ResetBindings(false);

      
            seriANDdeseri way = new seriANDdeseri();
            way.fileupdate();
            way.serializelist();
            way.create();

            worddata.DataSource = wmlistword.WMlist;
            LbDictionaryData.DataSource = worddata;
            LbDictionaryData.DisplayMember = "WordAndMean";
            worddata.ResetBindings(false);

            // After the click the  btn . text box auto clear.//
            TxtWord.Text = string.Empty;
            TxtMeaning.Text = string.Empty;

            // Check if the word already exists
            // If it exists, check if the meaning already exists
            // If the meaning already exists, replace it after confirmation
            // If the meaning doesn't exist, add it
            // If the word doesn't exist, add it
        }

        //Find the words in through this btn.//
        private void BtnFind_Click(object sender, EventArgs e)
        {
            // Search for the matching words
            // If there are results display them in the list box
            LbDictionaryData.DataSource = null;
            bool setset = true;
            if(textBox1.Text == "")
            {
                worddata.DataSource = wmlistword.WMlist;
                LbDictionaryData.DataSource = worddata;
                LbDictionaryData.DisplayMember = "WordAndMean";
                setset = false;
                string message = "Enter the word!";
                MessageBox.Show(message);

            }
            if (setset == true)
            {
                int i = wmlistword.WMlist.Count - 1;
                while (i >= 0)
                {
                    LbDictionaryData.DataSource = null;
                    
                    if (wmlistword.WMlist[i].wordset.Contains(textBox1.Text) == true)
                    {
                        LbDictionaryData.Items.Clear();
                        LbDictionaryData.Items.Add(wmlistword.WMlist[i].WordAndMean);
                        allword = wmlistword.WMlist[1].WordAndMean;
                        break;
                    }
                    
                    i--;
                }
               
            }
            textBox1.Text = string.Empty;
            worddata.ResetBindings(false);
        }

        private void BtnDeleteSelected_Click(object sender, EventArgs e)
        {
            // Ask for confirmation; Warn for unrecoverability
            // If confirmed delete the word
            //Conformation for the delete records//
            DialogResult DR = MessageBox.Show(" This will delete permently.Do you want to delete ? ", "!Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            {
                try
                {
                    wmlistword.WMlist.Remove((WoMe)LbDictionaryData.SelectedItem);
                    worddata.ResetBindings(false);
                }
                catch
                {
                    int i = wmlistword.WMlist.Count - 1;
                    while (i >= 0)
                    {

                        if (wmlistword.WMlist[i].WordAndMean.Contains(allword) == true)
                        {
                            wmlistword.WMlist.RemoveAt(i);
                            LbDictionaryData.Items.Clear();
                        }
                        i--;

                    }
                }
            }
            seriANDdeseri way = new seriANDdeseri();
        }

        private void BtnListAll_Click(object sender, EventArgs e)
        {
            // Iteratively populate the list box in the ascending order of words
            LbDictionaryData.DataSource = worddata;
            LbDictionaryData.DisplayMember = "WordAndMean";
            worddata.ResetBindings(false);
            wmlistword.WMlist.Sort();

        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            // Ask for confirmation TWICE; Warn for unrecoverability
            // If confirmed delete the word
            //First conformattion for this delete //
            DialogResult DR = MessageBox.Show(" ALL MEANING AND WORD WILL BE LOST DO YOU WANT TO COUNTINUCE ? ", "!Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DR == DialogResult.Yes)
            { 
                //***********************************//
                //Second conformation for this delete//
                DialogResult R = MessageBox.Show(" WANT TO DELEAT ALL ? ", "warrning!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (R == DialogResult.Yes)
                {
                  
                    int i = wmlistword.WMlist.Count - 1;
                    while (i >= 0)
                    {
                        wmlistword.WMlist.RemoveAt(i);
                        --i;
                    }
                    

                }
                else if (DR == DialogResult.No)
                {

                }

            }
            if (DR == DialogResult.No)
            {

            }
            worddata.ResetBindings(false);
            seriANDdeseri way = new seriANDdeseri();
        }

        private void TxtWord_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Add the timer 
            timer1.Start();
            DateTime datetime = DateTime.Now;
            this.lbl3.Text = datetime.ToString();
            seriANDdeseri way = new seriANDdeseri();
            way.fileupdate();
            string message = "Updated!!";
            MessageBox.Show(message);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
