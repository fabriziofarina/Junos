using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Resources;



namespace Junos
{
    public partial class clienti : Form
    {
        public string site_db_name = "admin_milliards";
        public string site_db_user = "root";
        public string site_db_pass = "in12LA7260179.,-";
        public string site_db_addr = "10.0.2.210";
        ResourceManager res_man;    // declare Resource manager to access to specific cultureinfo
        CultureInfo cul;            // declare culture info
        public double[] commission = new double[31];
        public string default_language { get; set; }
        void switch_language()
        {
            cul = CultureInfo.CreateSpecificCulture(((roots)MdiParent).default_language);    //create culture for vietnamese
        }

        private void search()
        {
            MySqlConnection conn = null;
            MySqlCommand myCommand = new MySqlCommand();
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            System.Data.DataTable myData2 = new System.Data.DataTable();

            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string SQL_ADD = "";
                if (search_licensee.Text != "")
                    SQL_ADD = "licensee LIKE '%" + search_licensee.Text + "%' OR ";
                if (search_skin.Text != "")
                    SQL_ADD += "skin LIKE '%" + search_skin.Text + "%' OR ";
                if (search_master.Text != "")
                    SQL_ADD += "master LIKE '%" + search_master.Text + "%' OR ";
                if (search_sagent.Text != "")
                    SQL_ADD += "sagent LIKE '%" + search_sagent.Text + "%' OR ";
                if (search_agent.Text != "")
                    SQL_ADD += "agent LIKE '%" + search_agent.Text + "%' OR ";
                if (search_agency.Text != "")
                    SQL_ADD += "agency LIKE '%" + search_agency.Text + "%' OR ";
                if (search_client.Text != "")
                    SQL_ADD += "login LIKE '%" + search_client.Text + "%' OR ";

                if (SQL_ADD.Length > 0)
                {
                    SQL_ADD = SQL_ADD.Remove(SQL_ADD.Length - 4);
                    SQL_ADD = "WHERE " + SQL_ADD;
                }
                cmd.CommandText = "SELECT licensee, skin, id, login, saldo, fido, nome, cognome, citta, provincia, email, subuser, agency, agent, sagent, master FROM login " +SQL_ADD + " ORDER BY id ASC LIMIT 0,100";



                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData2);
                client_list.DataSource = myData2;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Errore nella connessione al database: " + ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                // EventLog.WriteEntry("AdmiralPlatinumCheckNewReg", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        
        private void get_plan(int ids)
        {
            // CORREZIONE RISULTATI RAMO ELEMENTARE
            // SELEZIONARE la tabella lineschedina ed unirla con la tabella evento e con la tabella specialità in cui ci sarà la riga di algoritmo
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT eventi, percentuale FROM profili_provvigionali_linee WHERE id_profilo='" + ids.ToString() + "' ORDER BY eventi ASC";

                cmd.ExecuteNonQuery();


                MySqlDataReader myDataReader = cmd.ExecuteReader();

                while (myDataReader.Read() != false)
                {
                    int i = myDataReader.GetInt16("eventi");
                    double percent= myDataReader.GetDouble("percentuale");
                    commission[i] = percent;
                }
                myDataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            // CORREZIONE RISULTATI RAMO HANDICAP


        }
        private void get_ticket_from_shop(int ids)
        {
            // CORREZIONE RISULTATI RAMO ELEMENTARE
            // SELEZIONARE la tabella lineschedina ed unirla con la tabella evento e con la tabella specialità in cui ci sarà la riga di algoritmo
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT id, puntata, numero_segni FROM schedine WHERE id_agency='" + ids.ToString() + "' AND datagiocata BETWEEN '" + comm_from.Text  +" 00:00:00' AND '" + comm_to.Text   + " 23:59:59' AND status !='d' AND commissioni='0' ORDER BY id ASC";

                cmd.ExecuteNonQuery();


                MySqlDataReader myDataReader = cmd.ExecuteReader();

                while (myDataReader.Read() != false)
                {
                    int i = myDataReader.GetInt16("numero_segni");
                    double imp = myDataReader.GetDouble("puntata");
                    double comm = (commission[i] * imp)/100;
                    update_commission(myDataReader.GetInt32("id"), comm);
                   // MessageBox.Show(comm.ToString());
                }
                myDataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            // CORREZIONE RISULTATI RAMO HANDICAP


        }
        private void get_shop_commission(int ids)
        {
            // CORREZIONE RISULTATI RAMO ELEMENTARE
            // SELEZIONARE la tabella lineschedina ed unirla con la tabella evento e con la tabella specialità in cui ci sarà la riga di algoritmo
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT SUM(commissioni) AS COMM FROM schedine WHERE id_agency='" + ids.ToString() + "' AND datagiocata BETWEEN '" + comm_from.Text + " 00:00:00' AND '" + comm_to.Text + " 23:59:59' AND status !='d'";

                cmd.ExecuteNonQuery();


                MySqlDataReader myDataReader = cmd.ExecuteReader();

                while (myDataReader.Read() != false)
                {
                    carica_provvigioni_log(id.Text , username.Text , "1", comm_to.Text + " 23:59:59" , sett_prof_id.Text , sett_prof_name.Text , "2", myDataReader.GetDouble("COMM").ToString().Replace(",", "."), id_agency.Text , agency.Text, id_agent.Text , agent.Text , id_sagent.Text , sagent.Text , id_master.Text , master.Text , id_skin.Text , skin_bis.Text , id_licensee.Text , licensee_bis.Text );
                    operazione_conto(username.Text , "+", myDataReader.GetDouble("COMM").ToString().Replace(",", "."), "ACCREDITO PROVVIGIONALE " + sett_prof_name.Text  + " TURNOVER SETTIMANALE", "C");
                }
                myDataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            // CORREZIONE RISULTATI RAMO HANDICAP


        }

        void operazione_conto(string login, string operazione, string importo, string causale, string tipo)
        {
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO new_contabilita (login, operazione, importo, causale,operatore, dataora, tipo) VALUES (@login, @operazione, @importo, @causale, @operatore, NOW(), @tipo)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@operazione", operazione);
                cmd.Parameters.AddWithValue("@importo", importo.Replace(",", "."));
                cmd.Parameters.AddWithValue("@causale", causale);
                cmd.Parameters.AddWithValue("@operatore", "admin");
                cmd.Parameters.AddWithValue("@tipo", tipo);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }




        void carica_provvigioni_log(string id_login, string login, string struttura, string data_contabile, string id_profilo, string profilo, string tipo_livello, string importo, string id_agency, string agency, string id_agent, string agent, string id_sagent, string sagent, string id_master, string master, string skinID, string skin, string licenseeID, string licensee)
        {
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO provvigioni_erogate (id_login, login, struttura, data_contabile,  id_profilo, profilo, tipo_livello, importo, id_agency, agency, id_agent, agent, id_sagent, sagent, id_master, master, skinID,skin, licenseeID, licensee) VALUES (@id_login, @login, @struttura, @data_contabile,  @id_profilo, @profilo, @tipo_livello,  @importo, @id_agency, @agency, @id_agent, @agent, @id_sagent, @sagent, @id_master, @master, @skinID, @skin, @licenseeID, @licensee)";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@id_login", id_login);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@struttura", struttura);
                cmd.Parameters.AddWithValue("@data_contabile", data_contabile);
                cmd.Parameters.AddWithValue("@id_profilo", id_profilo);
                cmd.Parameters.AddWithValue("@profilo", profilo);
                cmd.Parameters.AddWithValue("@tipo_livello", tipo_livello);
                cmd.Parameters.AddWithValue("@importo", importo.Replace(",", "."));
                cmd.Parameters.AddWithValue("@id_agency", id_agency);
                cmd.Parameters.AddWithValue("@agency", agency);
                cmd.Parameters.AddWithValue("@id_agent", id_agent);
                cmd.Parameters.AddWithValue("@agent", agent);
                cmd.Parameters.AddWithValue("@id_sagent", id_sagent);
                cmd.Parameters.AddWithValue("@sagent", sagent);
                cmd.Parameters.AddWithValue("@id_master", id_master);
                cmd.Parameters.AddWithValue("@master", master);
                cmd.Parameters.AddWithValue("@skinID", skinID);
                cmd.Parameters.AddWithValue("@skin", skin);
                cmd.Parameters.AddWithValue("@licenseeID", licenseeID);
                cmd.Parameters.AddWithValue("@licensee", licensee);

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        private void search(string ADD)
        {
            MySqlConnection conn = null;
            MySqlCommand myCommand = new MySqlCommand();
            MySqlDataAdapter myAdapter = new MySqlDataAdapter();
            System.Data.DataTable myData2 = new System.Data.DataTable();

            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                string SQL_ADD = "";
                if (search_licensee.Text != "")
                    SQL_ADD = "licensee LIKE '%" + search_licensee.Text + "%' OR login LIKE '%" + search_licensee.Text + "%' OR ";
                if (search_skin.Text != "")
                    SQL_ADD += "skin LIKE '%" + search_skin.Text + "%' OR login LIKE '%" + search_skin.Text + "%' OR ";
                if (search_master.Text != "")
                    SQL_ADD += "master LIKE '%" + search_master.Text + "%' OR login LIKE '%" + search_master.Text + "%' OR ";
                if (search_sagent.Text != "")
                    SQL_ADD += "sagent LIKE '%" + search_sagent.Text + "%' OR login LIKE '%" + search_sagent.Text + "%' OR ";
                if (search_agent.Text != "")
                    SQL_ADD += "agent LIKE '%" + search_agent.Text + "%' OR login LIKE '%" + search_agent.Text + "%' OR ";
                if (search_agency.Text != "")
                    SQL_ADD += "agency LIKE '%" + search_agency.Text + "%' OR login LIKE '%" + search_agency.Text + "%' OR ";
                if (search_client.Text != "")
                    SQL_ADD += "login LIKE '%" + search_client.Text + "%' OR login LIKE '%" + search_client.Text + "%' OR ";

                if (SQL_ADD.Length > 0)
                {
                    SQL_ADD = SQL_ADD.Remove(SQL_ADD.Length - 4);
                    SQL_ADD = "WHERE (" + SQL_ADD + ") " + ADD;
                }
                cmd.CommandText = "SELECT licensee, skin, id, login, saldo, fido, nome, cognome, citta, provincia, email, subuser, agency, agent, sagent, master FROM login " + SQL_ADD + " ORDER BY id ASC LIMIT 0,100";



                myAdapter.SelectCommand = cmd;
                myAdapter.Fill(myData2);
                client_list.DataSource = myData2;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Errore nella connessione al database: " + ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                // EventLog.WriteEntry("AdmiralPlatinumCheckNewReg", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public clienti()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void clienti_Load(object sender, EventArgs e)
        {
            res_man = new ResourceManager("MultiLanguageApp.Resource.Res", typeof(roots).Assembly);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            search();
        }

       
        void getall(int ids)
        {

            // CORREZIONE RISULTATI RAMO ELEMENTARE
            // SELEZIONARE la tabella lineschedina ed unirla con la tabella evento e con la tabella specialità in cui ci sarà la riga di algoritmo
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT * FROM login WHERE id='" + ids.ToString() + "'";

                cmd.ExecuteNonQuery();


                MySqlDataReader myDataReader = cmd.ExecuteReader();

                while (myDataReader.Read() != false)
                {
                    licensee.Text = myDataReader.GetString("licensee");
                    id_licensee.Text = myDataReader.GetInt32("licenseeID").ToString();
                    licensee_bis.Text = myDataReader.GetString("licensee");
                    id_skin.Text = myDataReader.GetInt32("skinID").ToString();
                    skin_bis.Text = myDataReader.GetString("skin");

                    skin.Text = myDataReader.GetString("skin");
                    id.Text  = myDataReader.GetInt32("id").ToString();
                    username.Text = myDataReader.GetString("login");
                    sett_prof_id.Text = myDataReader.GetInt32("profilo_id").ToString();
                    id_master.Text = myDataReader.GetInt32("id_master").ToString();
                    master.Text = myDataReader.GetString("master");
                    id_sagent.Text = myDataReader.GetInt32("id_sagent").ToString();
                    sagent.Text = myDataReader.GetString("sagent");
                    id_agent.Text = myDataReader.GetInt32("id_agent").ToString();
                    agent.Text = myDataReader.GetString("agent");
                    id_agency.Text = myDataReader.GetInt32("id_agency").ToString();
                    agency.Text = myDataReader.GetString("agency");
                    id_parent.Text = myDataReader.GetInt32("subuserID").ToString();
                    parent.Text = myDataReader.GetString("subuser");
                    get_profile(myDataReader.GetInt32("profilo_id"));   
                }


                myDataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            // CORREZIONE RISULTATI RAMO HANDICAP

        }
        void get_profile(int ids)
        {
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;


            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT nome FROM profili_provvigionali WHERE id='" + ids.ToString() + "'";

                cmd.ExecuteNonQuery();


                MySqlDataReader myDataReader = cmd.ExecuteReader();

                while (myDataReader.Read() != false)
                {
                    sett_prof_name.Text = myDataReader.GetString("nome");
                }
                myDataReader.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            get_plan(Convert.ToInt32(sett_prof_id.Text ));
            get_ticket_from_shop(Convert.ToInt32(id.Text));
        }
        private void update_commission(int ids, double importo)
        {
            string cs = @"server=" + site_db_addr + ";userid=" + site_db_user + ";password=" + site_db_pass + ";database=" + site_db_name;
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE schedine SET commissioni=@importo WHERE id=@id";
                cmd.Parameters.AddWithValue("@id", ids.ToString());
                cmd.Parameters.AddWithValue("@importo", importo.ToString().Replace(",","."));
            
                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                // Console.WriteLine("Error: {0}", ex.ToString);
                //EventLog.WriteEntry("AdmiralPlatinumRefertAndComm", "CONNESSIONE IMPOSSIBILE AL DATABASE REMOTO : " + ex.ToString());
            }
            finally
            {
                //MessageBox.Show("done");
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        private void client_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (client_list.SelectedCells.Count > 0)
            {
                int selectedRow = client_list.SelectedCells[0].RowIndex;
                DataGridViewRow selRow = client_list.Rows[selectedRow];
                int a = Convert.ToInt32 (selRow.Cells["id"].Value);
                getall(a);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            search();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            search();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            search();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            search();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            search();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            search();
        }



        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            search("AND active='7'");
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            search("AND active='5'");
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            search("AND active='4'");
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            search("AND active='3'");
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            search("AND active='2'");
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            search("AND active='1'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            get_shop_commission(Convert.ToInt32(id.Text));
        }
    }
}
