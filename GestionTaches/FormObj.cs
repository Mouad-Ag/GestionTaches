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
        
        private int Id_utilisateur;
        public FormObj(int id_utilisateur)
        {
            Id_utilisateur = id_utilisateur;
            InitializeComponent();
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

            if (!dgvNonCommence.Columns.Contains("btnSupprimerNonCommence"))
            {
                DataGridViewButtonColumn btnSupprimerNonCommence = new DataGridViewButtonColumn
                {
                    Name = "btnSupprimerNonCommence",
                    HeaderText = "Supprimer",
                    Text = "Supprimer",
                    UseColumnTextForButtonValue = true
                };
                dgvNonCommence.Columns.Add(btnSupprimerNonCommence);
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

            if (!dgvEnCours.Columns.Contains("btnSupprimerEnCours"))
            {
                DataGridViewButtonColumn btnSupprimerEnCours = new DataGridViewButtonColumn
                {
                    Name = "btnSupprimerEnCours",
                    HeaderText = "Supprimer",
                    Text = "Supprimer",
                    UseColumnTextForButtonValue = true
                };
                dgvEnCours.Columns.Add(btnSupprimerEnCours);
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

            if (!dgvTermine.Columns.Contains("btnSupprimerTermine"))
            {
                DataGridViewButtonColumn btnSupprimerTermine = new DataGridViewButtonColumn
                {
                    Name = "btnSupprimerTermine",
                    HeaderText = "Supprimer",
                    Text = "Supprimer",
                    UseColumnTextForButtonValue = true
                };
                dgvTermine.Columns.Add(btnSupprimerTermine);
            }
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
            dgvNonCommence.DataSource = GetObjectifsParStatut("Non commencé", Id_utilisateur);
            dgvEnCours.DataSource = GetObjectifsParStatut("En cours", Id_utilisateur);
            dgvTermine.DataSource = GetObjectifsParStatut("Terminé", Id_utilisateur);
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

        

        private void SupprimerObjectifDansBase(int idObjectif)
        {
            string connectionString = "Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SupprimerObjectif", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Ajouter le paramètre de la procédure
                    command.Parameters.AddWithValue("@Id_objectif", idObjectif);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                string columnName = dgv.Columns[e.ColumnIndex].Name;

                if (columnName.Contains("btnSupprimer"))
                {
                    // Récupérer l'Id_objectif de la ligne sélectionnée
                    int idObjectif = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["Id_objectif"].Value);

                    // Appeler la méthode de suppression
                    SupprimerObjectifDansBase(idObjectif);

                    MessageBox.Show("Suppression réussie !");
                    ChargerObjectifs();
                }
                else if (columnName.Contains("btnAfficherTaches"))
                {

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
                            FormTaches formTaches = new FormTaches(idObjectif, Id_utilisateur);
                            formTaches.Show();
                            this.Hide();
                        }
                    }
                }
            }
        }



        private void btnAjouterTache_Click(object sender, EventArgs e)
        {
            AjoutObjectif obj =new AjoutObjectif(Id_utilisateur);
            obj.ShowDialog();
        }

        private void BtnActualiser_Click(object sender, EventArgs e)
        {
            ChargerObjectifs();
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            FormAccueil obj = new FormAccueil(Id_utilisateur);
            obj.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAccueil obj = new FormAccueil(Id_utilisateur);
            obj.Show();
            this.Hide();
        }
    }
}
