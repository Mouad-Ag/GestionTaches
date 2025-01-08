using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace GestionTaches
{
    public partial class FormObj : Form
    {
        private Timer timerNotifications;

        public FormObj()
        {
            InitializeComponent();
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
            AjouterColonnesBoutons();

            dgvNonCommence.CellContentClick += dgv_CellContentClick;
            dgvEnCours.CellContentClick += dgv_CellContentClick;
            dgvTermine.CellContentClick += dgv_CellContentClick;
            ChargerObjectifs();
            this.BackColor = Color.White;
        }

        private void AjouterColonnesBoutons()
        {
            // Pour dgvNonCommence
            if (!dgvNonCommence.Columns.Contains("btnAfficherTachesNonCommence"))
            {
                DataGridViewButtonColumn btnAfficherTachesNonCommence = new DataGridViewButtonColumn
                {
                    Name = "btnAfficherTachesNonCommence",
                    HeaderText = "Actions",
                    Text = "Voir les tâches",
                    UseColumnTextForButtonValue = true
                };
                dgvNonCommence.Columns.Add(btnAfficherTachesNonCommence);
            }

            // Pour dgvEnCours
            if (!dgvEnCours.Columns.Contains("btnAfficherTachesEnCours"))
            {
                DataGridViewButtonColumn btnAfficherTachesEnCours = new DataGridViewButtonColumn
                {
                    Name = "btnAfficherTachesEnCours",
                    HeaderText = "Actions",
                    Text = "Voir les tâches",
                    UseColumnTextForButtonValue = true
                };
                dgvEnCours.Columns.Add(btnAfficherTachesEnCours);
            }

            // Pour dgvTermine
            if (!dgvTermine.Columns.Contains("btnAfficherTachesTermine"))
            {
                DataGridViewButtonColumn btnAfficherTachesTermine = new DataGridViewButtonColumn
                {
                    Name = "btnAfficherTachesTermine",
                    HeaderText = "Actions",
                    Text = "Voir les tâches",
                    UseColumnTextForButtonValue = true
                };
                dgvTermine.Columns.Add(btnAfficherTachesTermine);
            }
        }

        private void VerifierNotifications()
        {
            string connectionString = "Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT message FROM Notification WHERE date_envoie = @DateActuelle";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DateActuelle", DateTime.Now);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        string message = reader["message"].ToString();
                        // Affichage de la notification
                        notifyIconApp.BalloonTipTitle = "Notification";
                        notifyIconApp.BalloonTipText = message;
                        notifyIconApp.ShowBalloonTip(5000);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la vérification des notifications : {ex.Message}");
                }
            }
        }

        private void TimerNotifications_Tick(object sender, EventArgs e)
        {
            // Débogage pour vérifier que cet événement est bien déclenché
            MessageBox.Show("Le TimerNotifications_Tick a été déclenché.");
            // Appel de la méthode pour vérifier les notifications
            VerifierNotifications();
        }

        private DataTable GetObjectifsParStatut(string statut, int idUtilisateur)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection("Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();
                    string query = @"
                SELECT * 
                FROM GetObjectifsParStatutEtUtilisateur(@Statut, @Id_utilisateur)
                ORDER BY date_debut, date_fin";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Statut", statut);
                    cmd.Parameters.AddWithValue("@Id_utilisateur", idUtilisateur);

                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
            return dataTable;
        }

        private void ChargerObjectifs()
        {
            int idUtilisateur = 5;
            dgvNonCommence.DataSource = GetObjectifsParStatut("Non commencé", idUtilisateur);
            dgvEnCours.DataSource = GetObjectifsParStatut("En cours", idUtilisateur);
            dgvTermine.DataSource = GetObjectifsParStatut("Terminé", idUtilisateur);
        }

        private void UpdateObjectifsFromGrid(SqlConnection con, DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Ignore les nouvelles lignes (non sauvegardées)
                if (row.IsNewRow) continue;

                // Prépare la commande SQL pour appeler la procédure stockée
                SqlCommand cmd = new SqlCommand("UpdateObjectif", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Ajout des paramètres pour la procédure stockée
                cmd.Parameters.AddWithValue("@Id_objectif", row.Cells["Id_objectif"].Value);
                cmd.Parameters.AddWithValue("@Titre", row.Cells["titre"].Value);
                cmd.Parameters.AddWithValue("@Description", row.Cells["description"].Value);
                cmd.Parameters.AddWithValue("@DateDebut", row.Cells["date_debut"].Value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateFin", row.Cells["date_fin"].Value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Statut", row.Cells["statut"].Value);

                // Exécute la procédure stockée
                cmd.ExecuteNonQuery();
            }
        }

        private void btnModifierTaches_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();

                    // Mise à jour des objectifs dans chaque DataGridView
                    UpdateObjectifsFromGrid(con, dgvNonCommence);
                    UpdateObjectifsFromGrid(con, dgvEnCours);
                    UpdateObjectifsFromGrid(con, dgvTermine);

                    MessageBox.Show("Mise à jour réussie !");
                    ChargerObjectifs(); // Recharge les données pour afficher les mises à jour
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Vérifiez si c'est une colonne bouton
            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Vérifiez quel DataGridView est concerné
                string idObjectifCol = "Id_objectif"; // Nom de la colonne contenant l'identifiant
                if (dgv.Rows[e.RowIndex].Cells[idObjectifCol].Value != null)
                {
                    int idObjectif = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[idObjectifCol].Value);

                    // Ouvrir FormTaches
                    FormTaches formTaches = new FormTaches(idObjectif);
                    formTaches.ShowDialog();
                }
            }
        }

        private void btnAjouterTache_Click(object sender, EventArgs e)
        {
            //...
        }

        private void FormObj_Load(object sender, EventArgs e)
        {

        }
    }
}
