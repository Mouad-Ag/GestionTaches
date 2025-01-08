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

namespace GestionTaches
{
    public partial class FormTaches : Form
    {
        private int idObjectif;

        public FormTaches(int objectifId)
        {
            InitializeComponent();
            idObjectif = objectifId;
            ChargerTaches();
        }

        private DataTable GetTachesParStatut(string statut, int idUtilisateur, int idObjectif)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection con = new SqlConnection("Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();
                    string query = @"
                SELECT * 
                FROM GetTachesParStatutEtUtilisateur(@Statut, @Id_utilisateur, @Id_objectif)
                ORDER BY date_debut, date_limite";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Statut", statut);
                    cmd.Parameters.AddWithValue("@Id_utilisateur", idUtilisateur);
                    cmd.Parameters.AddWithValue("@Id_objectif", idObjectif);  // Ajout de l'ID de l'objectif

                    SqlDataReader reader = cmd.ExecuteReader();
                    dataTable.Load(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
            return dataTable;
        }


        private void ChargerTaches()
        {
            int idUtilisateur = 5; // L'utilisateur actuel
                                   // Assurez-vous que l'idObjectif est récupéré
            int id = idObjectif; // Cette variable contient l'ID de l'objectif pour lequel les tâches sont affichées

            dgvNonCommence.DataSource = GetTachesParStatut("Non commencé", idUtilisateur, id);
            dgvEnCours.DataSource = GetTachesParStatut("En cours", idUtilisateur, id);
            dgvTermine.DataSource = GetTachesParStatut("Terminé", idUtilisateur, id);
            dgvAbandonne.DataSource = GetTachesParStatut("Abandonné", idUtilisateur, id);
        }


        private void UpdateTachesFromGrid(SqlConnection con, DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Ignore les nouvelles lignes (non sauvegardées)
                if (row.IsNewRow) continue;
                SqlCommand cmd = new SqlCommand("UpdateTache", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_tache", row.Cells["Id_tache"].Value);
                cmd.Parameters.AddWithValue("@Titre", row.Cells["titre"].Value);
                cmd.Parameters.AddWithValue("@Description", row.Cells["description"].Value);
                cmd.Parameters.AddWithValue("@DateDebut", row.Cells["date_debut"].Value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DateLimite", row.Cells["date_limite"].Value ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Statut", row.Cells["statut"].Value);
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
                    UpdateTachesFromGrid(con, dgvNonCommence);
                    UpdateTachesFromGrid(con, dgvEnCours);
                    UpdateTachesFromGrid(con, dgvTermine);
                    UpdateTachesFromGrid(con, dgvAbandonne);
                    MessageBox.Show("Mise à jour réussie !");
                    ChargerTaches(); // Recharge les données pour afficher les mises à jour
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }
        }

        private void btnAjouterTache_Click(object sender, EventArgs e)
        {
            FormObj Form = new FormObj();
            Form.ShowDialog();
        }

    }
}
