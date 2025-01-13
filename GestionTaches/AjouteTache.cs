using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionTaches
{
    public partial class AjouteTache : Form
    {
        private int utilisateurId;
        private int objectifId;

        public AjouteTache(int objectifIdParam, int utilisateurIdParam)
        {
            InitializeComponent();

            utilisateurId = utilisateurIdParam;
            objectifId = objectifIdParam;
        }

        private void AjouteTache_Load(object sender, EventArgs e)
        {
            Statut.Items.Add("Non commencé");
            Statut.Items.Add("En cours");
            Statut.Items.Add("Terminé");
            Statut.Items.Add("Abandonné");
            Statut.SelectedIndex = 0;

            Priorite.Items.AddRange(new string[] { "Haute", "Moyenne", "Basse" });
            Priorite.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titre = TitreBox.Text;
            string description = DescriptionBox.Text;
            DateTime debut = dateDebut.Value;
            DateTime fin = dateFin.Value;
            string priorite = Priorite.SelectedItem?.ToString();
            string statut = Statut.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(titre))
            {
                MessageBox.Show("Le titre est obligatoire.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (fin < debut)
            {
                MessageBox.Show("La date de fin doit être postérieure à la date de début.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (priorite == null || statut == null)
            {
                MessageBox.Show("Priorité et statut sont obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Server=LAPULGA\\SQLEXPRESS; Database=GestionTachesDB; Integrated Security=True;";
            string query = @"INSERT INTO Tache (titre, description, date_debut, date_limite, statut, Priorite, Id_utilisateur, Id_objectif)
                     OUTPUT INSERTED.Id_tache
                     VALUES (@Titre, @Description, @DateDebut, @DateFin, @Statut, @Priorite, @IdUtilisateur, @IdObjectif)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Titre", titre);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@DateDebut", debut);
                        cmd.Parameters.AddWithValue("@DateFin", fin);
                        cmd.Parameters.AddWithValue("@Statut", statut);
                        cmd.Parameters.AddWithValue("@Priorite", priorite);
                        cmd.Parameters.AddWithValue("@IdUtilisateur", utilisateurId);
                        cmd.Parameters.AddWithValue("@IdObjectif", objectifId);

                        // Récupérer l'ID de la tâche insérée
                        int idTache = (int)cmd.ExecuteScalar();

                        if (idTache > 0)
                        {
                            MessageBox.Show("Tâche ajoutée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TitreBox.Clear();
                            DescriptionBox.Clear();
                            dateDebut.Value = DateTime.Now;
                            dateFin.Value = DateTime.Now;
                            Priorite.SelectedIndex = -1;
                            Statut.SelectedIndex = -1;

                            // Appeler la procédure stockée pour ajouter une notification
                            AjouterNotificationTache(conn, idTache, titre, statut, fin);
                        }
                        else
                        {
                            MessageBox.Show("Aucune donnée n'a été insérée.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Erreur SQL : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AjouterNotificationTache(SqlConnection conn, int idTache, string titre, string statut, DateTime dateLimite)
        {
            string procedureQuery = "EXEC AjouterNotificationTache @Id_utilisateur, @Id_tache, @Id_objectif, @Titre, @Statut, @Date_limite";
            using (SqlCommand cmd = new SqlCommand(procedureQuery, conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Id_utilisateur", utilisateurId);
                    cmd.Parameters.AddWithValue("@Id_tache", idTache);
                    cmd.Parameters.AddWithValue("@Id_objectif", objectifId);
                    cmd.Parameters.AddWithValue("@Titre", titre);
                    cmd.Parameters.AddWithValue("@Statut", statut);
                    cmd.Parameters.AddWithValue("@Date_limite", dateLimite);

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Erreur lors de l'ajout de la notification : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
