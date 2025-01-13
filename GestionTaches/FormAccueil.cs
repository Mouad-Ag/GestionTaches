using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionTaches
{
    public partial class FormAccueil : Form
    {
        private Timer timerNotifications;
        private int Id_utilisateur;

        public FormAccueil(int id_utilisateur)
        {
            InitializeComponent();
            Id_utilisateur = id_utilisateur;
            timerNotifications = new Timer();
            try
            {
                // Intervalle de 20s (en millisecondes)
                timerNotifications.Interval = 20000;
                // Activation du Timer
                timerNotifications.Enabled = true;
                // Liaison de l'événement Tick
                timerNotifications.Tick += TimerNotifications_Tick;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du démarrage du Timer : {ex.Message}");
            }
            ChargerObjectifsEtTachesEnCours();
        }

        private void VerifierNotifications()
        {
            string connectionString = "Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // Appeler la procédure stockée
                    string procedureQuery = "EXEC RecupererEtMarquerNotifications @IdUtilisateur";
                    SqlCommand cmd = new SqlCommand(procedureQuery, con);
                    cmd.Parameters.AddWithValue("@IdUtilisateur", Id_utilisateur);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Afficher toutes les notifications
                    while (reader.Read())
                    {
                        string type = reader["type"].ToString();
                        string message = reader["message"].ToString();

                        notifyIconApp.BalloonTipTitle = $"Notification - {type}";
                        notifyIconApp.BalloonTipText = message;
                        notifyIconApp.ShowBalloonTip(5000);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la vérification des notifications : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TimerNotifications_Tick(object sender, EventArgs e)
        {
            VerifierNotifications();
        }

        private DataTable GetObjectifsEnCours(int idUtilisateur)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Objectif WHERE statut = 'En cours' AND Id_utilisateur = @Id_utilisateur";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id_utilisateur", idUtilisateur);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
            return dt;
        }

        private DataTable GetTachesEnCours(int idUtilisateur)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Tache WHERE statut = 'En cours' AND Id_utilisateur = @Id_utilisateur";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id_utilisateur", idUtilisateur);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
            return dt;
        }

        private void ChargerObjectifsEtTachesEnCours()
        {
            // Utilisation de la variable d'instance Id_utilisateur pour charger les objectifs et tâches
            dgvObjectifsEnCours.DataSource = GetObjectifsEnCours(Id_utilisateur);
            dgvTachesEnCours.DataSource = GetTachesEnCours(Id_utilisateur);
        }

        private void btnObjectifs_Click_1(object sender, EventArgs e)
        {
            FormObj formObj = new FormObj(Id_utilisateur);  // FormObj est celle que vous avez déjà créée pour gérer les objectifs
            formObj.Show();  // Ouvrir la form des objectifs
            this.Hide();  // Fermer la Form d'Accueil
        }

        private void FormAccueil_Load(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            login obj = new login();
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
