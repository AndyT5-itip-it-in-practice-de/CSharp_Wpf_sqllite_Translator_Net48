using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Runtime.InteropServices;
using Nocksoft.IO.ConfigFiles;

namespace Wpf_sqllite_Translator_Net48
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FindWord { get; private set; }
        public int loopcount = 0;


        public MainWindow()
        {
            InitializeComponent();

            //string defaultBrowserPath = GetDefaultBrowserPath();
            string Appfolder = AppDomain.CurrentDomain.BaseDirectory;
            txtbx_Appath.Text = Appfolder;

            /*
            McCheckBox_InLanguage_Deutsch.IsChecked = true;
            McCheckBox_InLanguage_Englisch.IsChecked = true;
            McCheckBox_InLanguage_Chinesisch.IsChecked = true;
            McCheckBox_InLanguage_Franzoesisch.IsChecked = true;

            McCheckBox_OutLanguage_Deutsch.IsChecked = true;
            McCheckBox_OutLanguage_Englisch.IsChecked = true;
            McCheckBox_OutLanguage_Chinesisch.IsChecked = true;
            McCheckBox_OutLanguage_Franzoesisch.IsChecked = true;
            */

            //-------------springe zu *Prozess*----------------------
            string Sender = "";
            Load_Inifiles(Sender, new RoutedEventArgs());
        }

        private void Load_Inifiles(object sender, RoutedEventArgs e)
        {

            /*
            [inlanguage]
            Answer = Franzoesisch

            [outlanguage]
            Answer = Chinesisch
            */

            string appDirectory = System.AppContext.BaseDirectory;

            // lade von ini-file Individual_Compname-Wert
            string sys_ini_folder_file = (appDirectory + "Data/System.ini");
            INIFile inifile1 = new INIFile(sys_ini_folder_file, true);
            string value1 = inifile1.GetValue("inlanguage", "Answer");
            //txtbx_Myweb_adressbox.Text = value4;

            //MessageBox.Show(value1);

            if (value1 == "Deutsch")
            {
                McCheckBox_InLanguage_Deutsch.IsChecked = true;
                McCheckBox_InLanguage_Englisch.IsChecked = false;
                McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
            }
            if (value1 == "Englisch")
            {
                McCheckBox_InLanguage_Deutsch.IsChecked = false;
                McCheckBox_InLanguage_Englisch.IsChecked = true;
                McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
            }
            if (value1 == "Franzoesisch")
            {
                McCheckBox_InLanguage_Deutsch.IsChecked = false;
                McCheckBox_InLanguage_Englisch.IsChecked = false;
                McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                McCheckBox_InLanguage_Franzoesisch.IsChecked = true;
            }
            if (value1 == "Chinesisch")
            {
                McCheckBox_InLanguage_Deutsch.IsChecked = false;
                McCheckBox_InLanguage_Englisch.IsChecked = false;
                McCheckBox_InLanguage_Chinesisch.IsChecked = true;
                McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
            }

            //----------------------------------------------------------------------------------------

            INIFile inifile2 = new INIFile(sys_ini_folder_file, true);
            string value2 = inifile2.GetValue("outlanguage", "Answer");
            //txtbx_Myweb_adressbox.Text = value4;

           // MessageBox.Show(value2);

            if (value2 == "Deutsch")
            {
                McCheckBox_OutLanguage_Deutsch.IsChecked = true;
                McCheckBox_OutLanguage_Englisch.IsChecked = false;
                McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
            }
            if (value2 == "Englisch")
            {
                McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                McCheckBox_OutLanguage_Englisch.IsChecked = true;
                McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
            }
            if (value2 == "Franzoesisch")
            {
                McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                McCheckBox_OutLanguage_Englisch.IsChecked = false;
                McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                McCheckBox_OutLanguage_Franzoesisch.IsChecked = true;
            }
            if (value2 == "Chinesisch")
            {
                McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                McCheckBox_OutLanguage_Englisch.IsChecked = false;
                McCheckBox_OutLanguage_Chinesisch.IsChecked = true;
                McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
            }






        }
        private void btnCreateDatabase_Click(object sender, RoutedEventArgs e)
        {
            string Appfolder = AppDomain.CurrentDomain.BaseDirectory;
            string DBFile = txtbx_Datenbankname.Text;
            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;


            try
            {
                //-------------------------------------------------------------------------------------------------------                 
                // Check if file exists with its full path    
                if (File.Exists(System.IO.Path.Combine(Appfolder, DBFile)))
                {
                    // If file found....
                    if (MessageBox.Show("Delete Database-File?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no delete stuff
                        MessageBox.Show("File Not delete...");
                        return;
                    }
                    else
                    {
                        //do yes delet stuff
                        MessageBox.Show("File delete...");
                        File.Delete(databaseFile);
                    }



                    ///File.Delete(System.IO.Path.Combine(Appfolder, DBFile));
                    MessageBox.Show("File deleted.");
                }
                else
                {
                    //file not found...
                    MessageBox.Show("File not found");
                }

                MessageBox.Show("Now,Create File...");

                SQLiteConnection.CreateFile(databaseFile);

                if (File.Exists(databaseFile))   //nochmal überprüfen ob datenban da ist...
                {
                    MessageBox.Show("Leere Datenbank   " + databaseFile + "  wurde erstellt.");
                }


                //-------------------------------------------------------------------------------------------------------
            }
            catch (IOException ioExp)
            {
                MessageBox.Show(ioExp.Message);
            }






        }

        private void Datenbankname_Set_Click(object sender, RoutedEventArgs e)
        {
            //----
        }
        private void Click_Create_Table(object sender, RoutedEventArgs e)
        {

            string Appfolder = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            //AUTOINCREMENT
            string sql = "CREATE TABLE IF NOT EXISTS " + Tablename + " ( " + Columm_1 + "  INTEGER PRIMARY KEY,  " + Columm_2 + "  varchar(20),  " + Columm_3 + "  varchar(20),  " + Columm_4 + "  varchar(20),  " + Columm_5 + "  varchar(20))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            command.Dispose();

        }

        private void Click_Filling_Table(object sender, RoutedEventArgs e)
        {
            string Appfolder = AppDomain.CurrentDomain.BaseDirectory;
            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            //der
            string sql1 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('der', 'the', 'la', '这')";
            SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
            command1.ExecuteNonQuery();

            //die
            string sql2 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('die', 'the', 'la', '这')";
            SQLiteCommand command2 = new SQLiteCommand(sql2, m_dbConnection);
            command2.ExecuteNonQuery();

            //das
            string sql3 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('das', 'the', 'que', '那')";
            SQLiteCommand command3 = new SQLiteCommand(sql3, m_dbConnection);
            command3.ExecuteNonQuery();

            //an
            string sql4 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('an', 'at', 'à', '在')";
            SQLiteCommand command4 = new SQLiteCommand(sql4, m_dbConnection);
            command4.ExecuteNonQuery();

            //Maschine
            string sql5 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('Maschine', 'machine', 'Machine', '机器')";
            SQLiteCommand command5 = new SQLiteCommand(sql5, m_dbConnection);
            command5.ExecuteNonQuery();

            //Fehler
            string sql6 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('Fehler', 'error', 'Erreur', '错误')";
            SQLiteCommand command6 = new SQLiteCommand(sql6, m_dbConnection);
            command6.ExecuteNonQuery();

            //Störung
            string sql7 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('Störung', 'Disturbance', 'Perturbation', '骚乱')";
            SQLiteCommand command7 = new SQLiteCommand(sql7, m_dbConnection);
            command7.ExecuteNonQuery();

            //Einlauf
            string sql8 = "insert or replace into  " + Tablename + "  ( " + Columm_2 + " ,  " + Columm_3 + " ,  " + Columm_4 + " ,  " + Columm_5 + " ) values ('Einlauf', 'Infeed', 'Lavement', '灌肠')";
            SQLiteCommand command8 = new SQLiteCommand(sql8, m_dbConnection);
            command8.ExecuteNonQuery();

            //after all - load in List......
            //----------------------------------------------------------------------------
            string Sender = "";
            Insert_from_db_to_Listview(Sender, new RoutedEventArgs());
            //----------------------------------------------------------------------------
        }

        private void Listview_Clear(object sender, RoutedEventArgs e)
        {
            this.listViewSQLite.Items.Clear();
        }

        public class MyItem
        {
            public int Id { get; set; }
            public string Deutsch { get; set; }
            public string Englisch { get; set; }
            public string Französisch { get; set; }
            public string Chinesisch { get; set; }

            /*
            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";
            */
        }

        private void Insert_from_db_to_Listview(object sender, RoutedEventArgs e)
        {
            //first delete List...
            //----------------------------------------------------------------------------
            string Sender = "";
            Listview_Clear(Sender, new RoutedEventArgs());
            //----------------------------------------------------------------------------


            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //


            //-------------------------------------------------------------------------------------------------------                 
            // Check if file exists with its full path    
            if (File.Exists(databaseFile))
            {
                //existiert
                goto AnswerYes;

            }
            else
            {
                MessageBox.Show("No DatabaseFile");
                goto AnswerNo;
            }
        //-----------------------------------------------------------------------------------------------


        AnswerYes:

            try
            {
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
                m_dbConnection.Open();

                string Tablename = "LanguagePack";
                string Columm_1 = "ID";
                string Columm_2 = "Deutsch";
                string Columm_3 = "Englisch";
                string Columm_4 = "Französisch";
                string Columm_5 = "Chinesisch";


                string sql = "select * from  " + Tablename + "  order by  " + Columm_3 + "  desc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();

                loopcount = 0;

                while (reader.Read())
                {
                    loopcount = (loopcount + 1);
                    txtbx_loop_Listview_eintraege.Text = loopcount.ToString();


                    //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                    //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                    string _id = "" + reader[Columm_1];
                    string _Deutsch = "" + reader[Columm_2];
                    string _Englisch = "" + reader[Columm_3];
                    string _Französisch = "" + reader[Columm_4];
                    string _Chinesisch = "" + reader[Columm_5];

                    this.listViewSQLite.Items.Add(new MyItem
                    {
                        Id = Int32.Parse(_id),
                        Deutsch = _Deutsch,
                        Englisch = _Englisch,
                        Französisch = _Französisch,
                        Chinesisch = _Chinesisch,
                    });

                }
                m_dbConnection.Dispose();
            }
            catch (IOException ioExp)
            {
                MessageBox.Show(ioExp.Message);
            }


        AnswerNo:;

            //----------------------------------------------------------------------------
            //clear txtboxes
            // txtbx_Outbox_Listview_ID.Text = "";
            // txtbx_Outbox_Listview_Name.Text = "";
            // txtbx_Outbox_Listview_Score.Text = "";
            //----------------------------------------------------------------------------
            //----------------------------------------------------------------------------




        }


        private void Open_Folder_Click(object sender, RoutedEventArgs e)
        {
            //string defaultBrowserPath = GetDefaultBrowserPath();
            string Appfolder = AppDomain.CurrentDomain.BaseDirectory;

            txtbx_Appath.Text = Appfolder;


            try

            {
                // launch default browser
                // Process.Start(defaultBrowserPath, textBox1.Text);
                System.Diagnostics.Process.Start(Appfolder);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void Eingang_Text_Separate_Click(object sender, RoutedEventArgs e)
        {
            txtbx_Eingang_Text_Sep1.Clear();
            txtbx_Eingang_Text_Sep2.Clear();
            txtbx_Eingang_Text_Sep3.Clear();
            txtbx_Eingang_Text_Sep4.Clear();
            txtbx_Eingang_Text_Sep5.Clear();



            string phrase = txtbx_Eingang_Text.Text;
            string[] words = phrase.Split(' ');

            foreach (var word in words)
            {
                string Text1 = ($"<{word}>");

                string Text = string.Join("", Text1.Split('<', '>', '.', ';', '\'', ',', ' '));

                //------------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Eingang_Text_Sep1.Text))
                {
                    txtbx_Eingang_Text_Sep1.Text = Text;
                }
                if (String.IsNullOrEmpty(txtbx_Eingang_Text_Sep2.Text))
                {
                    txtbx_Eingang_Text_Sep2.Text = Text;
                }
                if (String.IsNullOrEmpty(txtbx_Eingang_Text_Sep3.Text))
                {
                    txtbx_Eingang_Text_Sep3.Text = Text;
                }
                if (String.IsNullOrEmpty(txtbx_Eingang_Text_Sep4.Text))
                {
                    txtbx_Eingang_Text_Sep4.Text = Text;
                }
                if (String.IsNullOrEmpty(txtbx_Eingang_Text_Sep5.Text))
                {
                    txtbx_Eingang_Text_Sep5.Text = Text;
                }
                //------------------------------------------------------------------------------------------

                if (txtbx_Eingang_Text_Sep2.Text.Contains(txtbx_Eingang_Text_Sep1.Text))
                {
                    txtbx_Eingang_Text_Sep2.Clear();
                    txtbx_Eingang_Text_Sep2.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep3.Text.Contains(txtbx_Eingang_Text_Sep1.Text))
                {
                    txtbx_Eingang_Text_Sep3.Clear();
                    txtbx_Eingang_Text_Sep3.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep4.Text.Contains(txtbx_Eingang_Text_Sep1.Text))
                {
                    txtbx_Eingang_Text_Sep4.Clear();
                    txtbx_Eingang_Text_Sep4.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep5.Text.Contains(txtbx_Eingang_Text_Sep1.Text))
                {
                    txtbx_Eingang_Text_Sep5.Clear();
                    txtbx_Eingang_Text_Sep5.Text = Text;
                }

                //------------------------------------------------------------------------------------------

                if (txtbx_Eingang_Text_Sep3.Text.Contains(txtbx_Eingang_Text_Sep2.Text))
                {
                    txtbx_Eingang_Text_Sep3.Clear();
                    txtbx_Eingang_Text_Sep3.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep4.Text.Contains(txtbx_Eingang_Text_Sep2.Text))
                {
                    txtbx_Eingang_Text_Sep4.Clear();
                    txtbx_Eingang_Text_Sep4.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep5.Text.Contains(txtbx_Eingang_Text_Sep2.Text))
                {
                    txtbx_Eingang_Text_Sep5.Clear();
                    txtbx_Eingang_Text_Sep5.Text = Text;
                }

                //------------------------------------------------------------------------------------------

                if (txtbx_Eingang_Text_Sep4.Text.Contains(txtbx_Eingang_Text_Sep3.Text))
                {
                    txtbx_Eingang_Text_Sep4.Clear();
                    txtbx_Eingang_Text_Sep4.Text = Text;
                }
                if (txtbx_Eingang_Text_Sep5.Text.Contains(txtbx_Eingang_Text_Sep3.Text))
                {
                    txtbx_Eingang_Text_Sep5.Clear();
                    txtbx_Eingang_Text_Sep5.Text = Text;
                }
                //------------------------------------------------------------------------------------------
                if (txtbx_Eingang_Text_Sep5.Text.Contains(txtbx_Eingang_Text_Sep4.Text))
                {
                    txtbx_Eingang_Text_Sep5.Clear();
                    txtbx_Eingang_Text_Sep5.Text = Text;
                }
                //------------------------------------------------------------------------------------------

            }
        }

        private void Checkup_Einzel_wörter_in_DB_bringto_Listview_and_outboxes_Click(object sender, RoutedEventArgs e)
        {

            //first delete List...
            //----------------------------------------------------------------------------
            string Sender = "";
            Listview_Clear(Sender, new RoutedEventArgs());
            //clear txtboxes
            txtbx_Ausgang_Text_Sep1.Text = "";
            txtbx_Ausgang_Text_Sep2.Text = "";
            txtbx_Ausgang_Text_Sep3.Text = "";
            txtbx_Ausgang_Text_Sep4.Text = "";
            txtbx_Ausgang_Text_Sep5.Text = "";
            txtbx_Ausgang_Text_zusammen.Text = "";
            //----------------------------------------------------------------------------

            loopcount = 0;

            ListView_Search1(Sender, new RoutedEventArgs());
         // Listview_getSelectedItem(Sender, new RoutedEventArgs());
         //   listViewSQLite.SelectionChanged += Listview_getSelectedItem;

            //listViewSQLite.Items.Clear();
            ListView_Search2(Sender, new RoutedEventArgs());
          // Listview_getSelectedItem(Sender, new RoutedEventArgs());
          //  listViewSQLite.SelectionChanged += Listview_getSelectedItem;

            //listViewSQLite.Items.Clear();
            ListView_Search3(Sender, new RoutedEventArgs());
           // Listview_getSelectedItem(Sender, new RoutedEventArgs());
           // listViewSQLite.SelectionChanged += Listview_getSelectedItem;

            //listViewSQLite.Items.Clear();
            ListView_Search4(Sender, new RoutedEventArgs());
           // Listview_getSelectedItem(Sender, new RoutedEventArgs());
           // listViewSQLite.SelectionChanged += Listview_getSelectedItem;

            // listViewSQLite.Items.Clear();
            ListView_Search5(Sender, new RoutedEventArgs());
            //Listview_getSelectedItem(Sender, new RoutedEventArgs());


          //  listViewSQLite.SelectionChanged += Listview_getSelectedItem;
         //   Listview_getSelectedItem(Sender, new RoutedEventArgs());


            //listViewSQLite.Items.Clear();



        }
        private void ListView_Search1(object sender, RoutedEventArgs e)
        {

            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            string Searchname = txtbx_Eingang_Text_Sep1.Text;


            string sql = "SELECT * FROM " + Tablename + " WHERE like('" + Searchname + "%', Deutsch)";
            // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                loopcount = (loopcount + 1);
                txtbx_loop_Listview_eintraege.Text = loopcount.ToString();


                //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                string _id = "" + reader[Columm_1];
                string _Deutsch = "" + reader[Columm_2];
                string _Englisch = "" + reader[Columm_3];
                string _Französisch = "" + reader[Columm_4];
                string _Chinesisch = "" + reader[Columm_5];


                this.listViewSQLite.Items.Add(new MyItem
                {
                    Id = Int32.Parse(_id),
                    Deutsch = _Deutsch,
                    Englisch = _Englisch,
                    Französisch = _Französisch,
                    Chinesisch = _Chinesisch,
                });

                //---------------------------------------------------------------------------------------
                // Englisch
                //---------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep1.Text))
                {
                    
                    txtbx_Ausgang_Text_Sep1.Text = _Englisch;
                    
                }
                //---------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------

                //listViewSQLite.Items[0].Selected = true;

                //  string Sender = "";
                //  Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }

            //------------------------------------------------------------------------------------------
        }
        private void ListView_Search2(object sender, RoutedEventArgs e)
        {

            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            string Searchname = txtbx_Eingang_Text_Sep2.Text;


            string sql = "SELECT * FROM " + Tablename + " WHERE like('" + Searchname + "%', Deutsch)";
            // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                loopcount = (loopcount + 1);
                txtbx_loop_Listview_eintraege.Text = loopcount.ToString();

                //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                string _id = "" + reader[Columm_1];
                string _Deutsch = "" + reader[Columm_2];
                string _Englisch = "" + reader[Columm_3];
                string _Französisch = "" + reader[Columm_4];
                string _Chinesisch = "" + reader[Columm_5];


                this.listViewSQLite.Items.Add(new MyItem
                {
                    Id = Int32.Parse(_id),
                    Deutsch = _Deutsch,
                    Englisch = _Englisch,
                    Französisch = _Französisch,
                    Chinesisch = _Chinesisch,
                });

                //---------------------------------------------------------------------------------------
                // Englisch
                //---------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep2.Text))
                {

                    txtbx_Ausgang_Text_Sep2.Text = _Englisch;

                }
                //---------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------

                //listViewSQLite.Items[0].Selected = true;

               // string Sender = "";
               // Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }

            //------------------------------------------------------------------------------------------
        }
        private void ListView_Search3(object sender, RoutedEventArgs e)
        {

            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            string Searchname = txtbx_Eingang_Text_Sep3.Text;


            string sql = "SELECT * FROM " + Tablename + " WHERE like('" + Searchname + "%', Deutsch)";
            // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                loopcount = (loopcount + 1);
                txtbx_loop_Listview_eintraege.Text = loopcount.ToString();

                //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                string _id = "" + reader[Columm_1];
                string _Deutsch = "" + reader[Columm_2];
                string _Englisch = "" + reader[Columm_3];
                string _Französisch = "" + reader[Columm_4];
                string _Chinesisch = "" + reader[Columm_5];


                this.listViewSQLite.Items.Add(new MyItem
                {
                    Id = Int32.Parse(_id),
                    Deutsch = _Deutsch,
                    Englisch = _Englisch,
                    Französisch = _Französisch,
                    Chinesisch = _Chinesisch,
                });

                //---------------------------------------------------------------------------------------
                // Englisch
                //---------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep3.Text))
                {

                    txtbx_Ausgang_Text_Sep3.Text = _Englisch;

                }
                //---------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------

                //listViewSQLite.Items[0].Selected = true;

                //string Sender = "";
                //Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }

            //------------------------------------------------------------------------------------------
        }
        private void ListView_Search4(object sender, RoutedEventArgs e)
        {

            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            string Searchname = txtbx_Eingang_Text_Sep4.Text;


            string sql = "SELECT * FROM " + Tablename + " WHERE like('" + Searchname + "%', Deutsch)";
            // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                loopcount = (loopcount + 1);
                txtbx_loop_Listview_eintraege.Text = loopcount.ToString();

                //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                string _id = "" + reader[Columm_1];
                string _Deutsch = "" + reader[Columm_2];
                string _Englisch = "" + reader[Columm_3];
                string _Französisch = "" + reader[Columm_4];
                string _Chinesisch = "" + reader[Columm_5];


                this.listViewSQLite.Items.Add(new MyItem
                {
                    Id = Int32.Parse(_id),
                    Deutsch = _Deutsch,
                    Englisch = _Englisch,
                    Französisch = _Französisch,
                    Chinesisch = _Chinesisch,
                });

                //---------------------------------------------------------------------------------------
                // Englisch
                //---------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep4.Text))
                {

                    txtbx_Ausgang_Text_Sep4.Text = _Englisch;

                }
                //---------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------

                //listViewSQLite.Items[0].Selected = true;

                //string Sender = "";
                //Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }

            //------------------------------------------------------------------------------------------
        }
        private void ListView_Search5(object sender, RoutedEventArgs e)
        {

            string databaseFile = AppDomain.CurrentDomain.BaseDirectory + txtbx_Datenbankname.Text;
            //
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=" + databaseFile + "; Version=3;");
            m_dbConnection.Open();

            string Tablename = "LanguagePack";
            string Columm_1 = "ID";
            string Columm_2 = "Deutsch";
            string Columm_3 = "Englisch";
            string Columm_4 = "Französisch";
            string Columm_5 = "Chinesisch";

            string Searchname = txtbx_Eingang_Text_Sep5.Text;


            string sql = "SELECT * FROM " + Tablename + " WHERE like('" + Searchname + "%', Deutsch)";
            // string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                loopcount = (loopcount + 1);
                txtbx_loop_Listview_eintraege.Text = loopcount.ToString();

                //Trace.WriteLine("ID: " + reader["id"] + "Name: " + reader["name"] + "\tScore: " + reader["score"]);

                //DB_Listbox_Output.Items.Add("ID: " + reader["id"] + "  \tName: " + reader["name"] + "  \tScore: " + reader["score"]);

                string _id = "" + reader[Columm_1];
                string _Deutsch = "" + reader[Columm_2];
                string _Englisch = "" + reader[Columm_3];
                string _Französisch = "" + reader[Columm_4];
                string _Chinesisch = "" + reader[Columm_5];


                this.listViewSQLite.Items.Add(new MyItem
                {
                    Id = Int32.Parse(_id),
                    Deutsch = _Deutsch,
                    Englisch = _Englisch,
                    Französisch = _Französisch,
                    Chinesisch = _Chinesisch,
                });

                //---------------------------------------------------------------------------------------
                // Englisch
                //---------------------------------------------------------------------------------------

                if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep5.Text))
                {

                    txtbx_Ausgang_Text_Sep5.Text = _Englisch;

                }
                //---------------------------------------------------------------------------------------
                //---------------------------------------------------------------------------------------

                //listViewSQLite.Items[0].Selected = true;

               // string Sender = "";
               // Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }

            //------------------------------------------------------------------------------------------
        }


        private void Listview_getSelectedItem(object sender, RoutedEventArgs e)
        {
            //Startpoint:
          /*
            try
            {
                
               // var item = sender as ListViewItem;
               // if (item != null && item.IsSelected)
               // {
              //      //Do your stuff
               // }
                


                if (listViewSQLite.SelectedItems.Count > 0)
                {
                    if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep1.Text))
                    {
                        //TEXT BOX IS EMPTY";
                        MyItem book = (MyItem)listViewSQLite.SelectedItems[0];
                        txtbx_Ausgang_Text_Sep1.Text = (book.Englisch.ToString());
                        listViewSQLite.Items.RemoveAt(listViewSQLite.SelectedIndex);
                    }

                    else if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep2.Text))
                    {
                        //TEXT BOX IS EMPTY";
                        MyItem book = (MyItem)listViewSQLite.SelectedItems[0];
                        txtbx_Ausgang_Text_Sep2.Text = (book.Englisch.ToString());
                        listViewSQLite.Items.RemoveAt(listViewSQLite.SelectedIndex);

                    }

                    else if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep3.Text))
                    {
                        //TEXT BOX IS EMPTY";
                        MyItem book = (MyItem)listViewSQLite.SelectedItems[0];
                        txtbx_Ausgang_Text_Sep3.Text = (book.Englisch.ToString());
                        listViewSQLite.Items.RemoveAt(listViewSQLite.SelectedIndex);
                    }

                    else if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep4.Text))
                    {
                        //TEXT BOX IS EMPTY";
                        MyItem book = (MyItem)listViewSQLite.SelectedItems[0];
                        txtbx_Ausgang_Text_Sep4.Text = (book.Englisch.ToString());
                        listViewSQLite.Items.RemoveAt(listViewSQLite.SelectedIndex);
                    }

                    else if (String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep5.Text))
                    {
                        //TEXT BOX IS EMPTY";
                        MyItem book = (MyItem)listViewSQLite.SelectedItems[0];
                        txtbx_Ausgang_Text_Sep5.Text = (book.Englisch.ToString());
                        listViewSQLite.Items.RemoveAt(listViewSQLite.SelectedIndex);
                    }




                }


                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                //  goto Startpoint;

            }
         */
        }

        private void del_all_txtboxes_Click(object sender, RoutedEventArgs e)
        {
            listViewSQLite.Items.Clear();

            txtbx_Eingang_Text_Sep1.Clear();
            txtbx_Eingang_Text_Sep2.Clear();
            txtbx_Eingang_Text_Sep3.Clear();
            txtbx_Eingang_Text_Sep4.Clear();
            txtbx_Eingang_Text_Sep5.Clear();

            txtbx_Ausgang_Text_Sep1.Clear();
            txtbx_Ausgang_Text_Sep2.Clear();
            txtbx_Ausgang_Text_Sep3.Clear();
            txtbx_Ausgang_Text_Sep4.Clear();
            txtbx_Ausgang_Text_Sep5.Clear();
            txtbx_Ausgang_Text_zusammen.Clear();
        }

        private void del_out_txtboxes_Click(object sender, RoutedEventArgs e)
        {

            txtbx_Ausgang_Text_Sep1.Clear();
            txtbx_Ausgang_Text_Sep2.Clear();
            txtbx_Ausgang_Text_Sep3.Clear();
            txtbx_Ausgang_Text_Sep4.Clear();
            txtbx_Ausgang_Text_Sep5.Clear();
            txtbx_Ausgang_Text_zusammen.Clear();
        }

        private void ListView_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ListView lb = sender as ListView;
            if (lb != null && lb.HasItems)
            {
                lb.SelectedIndex = 0;
                listViewSQLite.SelectedIndex = 0;

                string Sender = "";
                Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }
        }

        void ListView_TargetUpdated(object sender, RoutedEventArgs e)
        {
            ListView lb = sender as ListView;
            if (lb != null && lb.HasItems)
            {
                lb.SelectedIndex = 0;
                listViewSQLite.SelectedIndex = 0;

                string Sender = "";
                Listview_getSelectedItem(Sender, new RoutedEventArgs());
            }
        }

       

        
        private void Answers_Bring_Together_Click(object sender, RoutedEventArgs e)
        {

            //if (!String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep1.Text)) { txtbx_Ausgang_Text_Sep1.Text = "."; }
            //if (!String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep2.Text)) { txtbx_Ausgang_Text_Sep2.Text = "."; }
            //if (!String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep3.Text)) { txtbx_Ausgang_Text_Sep3.Text = "."; }
            //if (!String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep4.Text)) { txtbx_Ausgang_Text_Sep4.Text = "."; }
            //if (!String.IsNullOrEmpty(txtbx_Ausgang_Text_Sep5.Text)) { txtbx_Ausgang_Text_Sep5.Text = "."; }


            string stResult1 = txtbx_Ausgang_Text_Sep1.Text;
            string stResult2 = txtbx_Ausgang_Text_Sep2.Text;
            string stResult3 = txtbx_Ausgang_Text_Sep3.Text;
            string stResult4 = txtbx_Ausgang_Text_Sep4.Text;
            string stResult5 = txtbx_Ausgang_Text_Sep5.Text;

            if (String.IsNullOrEmpty(stResult1)) { stResult1 = "."; }
            if (String.IsNullOrEmpty(stResult2)) { stResult2 = "."; }
            if (String.IsNullOrEmpty(stResult3)) { stResult3 = "."; }
            if (String.IsNullOrEmpty(stResult4)) { stResult4 = "."; }
            if (String.IsNullOrEmpty(stResult5)) { stResult5 = "."; }


            string All_Results_Together = (stResult1 + " " + stResult2 + " " + stResult3 + " " + stResult4 + " " + stResult5);

            txtbx_Ausgang_Text_zusammen.Text = All_Results_Together;

        }

        private void All_Together_Click(object sender, RoutedEventArgs e)
        {
            //wenn Eingang-Textfeld nicht leer ist....
            //----------------------------------------------------------------------------------------------
            if (!String.IsNullOrEmpty(txtbx_Eingang_Text.Text))
            {
                string Sender = "";
                //1
                Eingang_Text_Separate_Click(Sender, new RoutedEventArgs());

                // System.Threading.Thread.Sleep(500);//1/2sec

                //2
                Checkup_Einzel_wörter_in_DB_bringto_Listview_and_outboxes_Click(Sender, new RoutedEventArgs());

                // System.Threading.Thread.Sleep(500);//1/2sec

                
                // System.Threading.Thread.Sleep(500);//1/2sec

                //4
                Answers_Bring_Together_Click(Sender, new RoutedEventArgs());
               // System.Threading.Thread.Sleep(100);//1/2sec
                //Answers_Bring_Together_Click(Sender, new RoutedEventArgs());
            }
        }

        private void ListView_TargetUpdated(object sender, SelectionChangedEventArgs e)
        {
            string Sender = "";
            Listview_getSelectedItem(Sender, new RoutedEventArgs());
        }

       
        private void Listview_Search(object sender, RoutedEventArgs e)
        {
            /*
            
            // Select the first ListViewItem of items, whose Text is item2
            ListViewItem item = listViewSQLite.Items
                                         .Cast<ListViewItem>()
                                         .FirstOrDefault(x => x. = "item2");

            MessageBox.Show(listViewSQLite.Items.IndexOf(item).ToString());
            */

        }

        private void Checkbox_check(object sender, RoutedEventArgs e)
        {
            McCheckBox_OutLanguage_Deutsch.IsChecked = true;

            McCheckBox_OutLanguage_Englisch.IsChecked = false;
        }


        private void McCheckBox_InLanguage_Deutsch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_InLanguage_Deutsch.IsChecked == true))
                {
                    //McCheckBox_InLanguage_Deutsch.IsChecked = false;
                    McCheckBox_InLanguage_Englisch.IsChecked = false;
                    McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Deutsch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("InLanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
        }

        private void McCheckBox_InLanguage_Englisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_InLanguage_Englisch.IsChecked == true))
                {
                    McCheckBox_InLanguage_Deutsch.IsChecked = false;
                    //McCheckBox_InLanguage_Englisch.IsChecked = false;
                    McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Englisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("InLanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
        }

        private void McCheckBox_InLanguage_Franzoesisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_InLanguage_Franzoesisch.IsChecked == true))
                {
                    McCheckBox_InLanguage_Deutsch.IsChecked = false;
                    McCheckBox_InLanguage_Englisch.IsChecked = false;
                    McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                    // McCheckBox_InLanguage_Französisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Franzoesisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("InLanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
        }

        private void McCheckBox_InLanguage_Chinesisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_InLanguage_Chinesisch.IsChecked == true))
                {
                    McCheckBox_InLanguage_Deutsch.IsChecked = false;
                    McCheckBox_InLanguage_Englisch.IsChecked = false;
                    //McCheckBox_InLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_InLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Chinesisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("InLanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
        }

        private void McCheckBox_OutLanguage_Deutsch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
               if ((McCheckBox_OutLanguage_Deutsch.IsChecked == true))
                {
                    //McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                    McCheckBox_OutLanguage_Englisch.IsChecked = false;
                    McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Deutsch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("outlanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
}

        private void McCheckBox_OutLanguage_Englisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_OutLanguage_Englisch.IsChecked == true))
                {
                    McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                    //McCheckBox_OutLanguage_Englisch.IsChecked = false;
                    McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Englisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("outlanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
}
       
        private void McCheckBox_OutLanguage_Franzoesisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_OutLanguage_Franzoesisch.IsChecked == true))
                {
                    McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                    McCheckBox_OutLanguage_Englisch.IsChecked = false;
                    McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                   // McCheckBox_OutLanguage_Französisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Franzoesisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("outlanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
}

        private void McCheckBox_OutLanguage_Chinesisch_Checkedchanched(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((McCheckBox_OutLanguage_Chinesisch.IsChecked == true))
                {
                    McCheckBox_OutLanguage_Deutsch.IsChecked = false;
                    McCheckBox_OutLanguage_Englisch.IsChecked = false;
                    //McCheckBox_OutLanguage_Chinesisch.IsChecked = false;
                    McCheckBox_OutLanguage_Franzoesisch.IsChecked = false;
                }

                // speichere in ini---------------------------------------------------------------------
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string Adjustments_ini_folder_file = (appDirectory + "Data/System.ini");

                string value999 = "Chinesisch";
                INIFile inifile999 = new INIFile(Adjustments_ini_folder_file, true);
                inifile999.SetValue("outlanguage", "Answer", value999);
            }
            catch (IOException ioExp)
            {
            }
        }

        private void App_Restart(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);
            this.Close();
            System.Windows.Application.Current.Shutdown();
            // System.Windows.Application.Current.Shutdown();
        }

        private void App_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
            //this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
