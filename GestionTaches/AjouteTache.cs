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
    public partial class AjouteTache : Form
    {
        public AjouteTache()
        {
            InitializeComponent();
        }

        
        private void AjouteTache_Load(object sender, EventArgs e)
        {
            // Ajouter les options au ComboBox
            Statut.Items.Add("Non commencé");
            Statut.Items.Add("En cours");
            Statut.Items.Add("Terminé");
            Statut.Items.Add("Abandonné");

            // Optionnel : Sélectionner une valeur par défaut
            Statut.SelectedIndex = 0; // 'Non commencé'


            // Ajouter les options au ComboBox de priorité
            Priorite.Items.AddRange(new string[] { "Haute", "Moyenne", "Basse" });

            // Sélectionner une valeur par défaut
            Priorite.SelectedIndex = 1;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Récupération des données depuis les champs
            string titre = TitreBox.Text; // Assurez-vous que TitreBox est le nom de votre TextBox
            string description = DescriptionBox.Text; // Assurez-vous que DescriptionBox existe
            DateTime debut = dateDebut.Value; // Assurez-vous que dateDebut est un DateTimePicker
            DateTime fin = dateFin.Value; // Assurez-vous que dateFin est un DateTimePicker
            string priorite = Priorite.SelectedItem?.ToString(); // Assurez-vous que Priorite est un ComboBox
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

            if (priorite == null || statut == null)
            {
                MessageBox.Show("Priorité et statut sont obligatoires.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Valeurs d'exemple pour les clés étrangères (à récupérer dynamiquement dans une vraie application)
            int idUtilisateur = 1; // Remplacez par l'ID d'utilisateur réel
            int idObjectif = 1; // Remplacez par l'ID d'objectif réel

            // Chaîne de connexion à la base de données
            string connectionString = "Server=localhost; Database=GestionTachesDB; Integrated Security=True;";

            // Requête SQL pour insérer les données
            string query = @"INSERT INTO Tache (titre, description, date_debut, date_limite, statut, Priorite, Id_utilisateur, Id_objectif)
                             VALUES (@Titre, @Description, @DateDebut, @DateFin, @Statut, @Priorite, @IdUtilisateur, @IdObjectif)";

            // Connexion et exécution de la requête
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
                        cmd.Parameters.AddWithValue("@Priorite", priorite);
                        cmd.Parameters.AddWithValue("@IdUtilisateur", idUtilisateur);
                        cmd.Parameters.AddWithValue("@IdObjectif", idObjectif);

                        // Exécution de la commande
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Vérification de l'insertion
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Tâche ajoutée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Réinitialiser les champs après l'insertion (optionnel)
                            TitreBox.Clear();
                            DescriptionBox.Clear();
                            dateDebut.Value = DateTime.Now;
                            dateFin.Value = DateTime.Now;
                            Priorite.SelectedIndex = -1;
                            Statut.SelectedIndex = -1;
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



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TitreBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateDebut_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Priorite_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Statut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
