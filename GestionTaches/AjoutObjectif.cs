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
    public partial class AjoutObjectif : Form
    {
        int idUtilisateur;
        public AjoutObjectif(int  idUtilisateur)
        {
            InitializeComponent();
            this.idUtilisateur = idUtilisateur;
        }
        private void AjoutObjectif_Load(object sender, EventArgs e)
        {
            // Ajouter les options au ComboBox
            Statut.Items.Add("Non commencé");
            Statut.Items.Add("En cours");
            Statut.Items.Add("Terminé");
            Statut.Items.Add("Abandonné");
            Statut.SelectedIndex = 0; // 'Non commencé'
            // Ajouter les options au ComboBox de priorité
            // Sélectionner une valeur par défaut
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Récupération des données depuis les champs
            string titre = TitreBox.Text; // Assurez-vous que TitreBox est le nom de votre TextBox
            string description = DescriptionBox.Text; // Assurez-vous que DescriptionBox existe
            DateTime debut = dateDebut.Value; // Assurez-vous que dateDebut est un DateTimePicker
            DateTime fin = dateTimePicker1.Value; // Assurez-vous que dateFin est un DateTimePicker
            string statut = Statut.SelectedItem?.ToString(); // Assurez-vous que Statut est un ComboBox

            // Validation des données
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

            // Chaîne de connexion à la base de données
            string connectionString = "Server=LAPULGA\\SQLEXPRESS; Database=GestionTachesDB; Integrated Security=True;";

            // Requête SQL pour insérer les données
            string query = @"INSERT INTO Objectif (titre, description, date_debut, date_fin, statut, Id_utilisateur)
                     OUTPUT INSERTED.Id_objectif
                     VALUES (@Titre, @Description, @DateDebut, @DateFin, @Statut, @IdUtilisateur)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Ajout des paramètres
                        cmd.Parameters.AddWithValue("@Titre", titre);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@DateDebut", debut);
                        cmd.Parameters.AddWithValue("@DateFin", fin);
                        cmd.Parameters.AddWithValue("@Statut", statut);
                        cmd.Parameters.AddWithValue("@IdUtilisateur", idUtilisateur);

                        // Récupérer l'ID de l'objectif inséré
                        int idObjectif = (int)cmd.ExecuteScalar();

                        if (idObjectif > 0)
                        {
                            MessageBox.Show("Objectif ajouté avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Réinitialiser les champs après l'insertion (optionnel)
                            TitreBox.Clear();
                            DescriptionBox.Clear();
                            dateDebut.Value = DateTime.Now;
                            dateTimePicker1.Value = DateTime.Now;
                            Statut.SelectedIndex = -1;

                            // Appeler la procédure stockée pour ajouter une notification
                            AjouterNotificationObjectif(conn, idObjectif, titre, statut, fin);
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

        private void AjouterNotificationObjectif(SqlConnection conn, int idObjectif, string titre, string statut, DateTime dateFin)
        {
            string procedureQuery = "EXEC AjouterNotificationObjectif @Id_utilisateur, @Id_objectif, @Titre, @Statut, @Date_fin";
            using (SqlCommand cmd = new SqlCommand(procedureQuery, conn))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@Id_utilisateur", idUtilisateur);
                    cmd.Parameters.AddWithValue("@Id_objectif", idObjectif);
                    cmd.Parameters.AddWithValue("@Titre", titre);
                    cmd.Parameters.AddWithValue("@Statut", statut);
                    cmd.Parameters.AddWithValue("@Date_fin", dateFin);

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
