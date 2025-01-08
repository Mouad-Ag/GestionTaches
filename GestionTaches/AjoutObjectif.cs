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
        public AjoutObjectif()
        {
            InitializeComponent();
        }

        private void AjoutObjectif_Load(object sender, EventArgs e)
        {
            // Ajouter les options au ComboBox
            Statut.Items.Add("Non commencé");
            Statut.Items.Add("En cours");
            Statut.Items.Add("Terminé");
            Statut.Items.Add("Abandonné");

            // Optionnel : Sélectionner une valeur par défaut
            Statut.SelectedIndex = 0; // 'Non commencé'


            // Ajouter les options au ComboBox de priorité

            comboBox1.Items.AddRange(new string[] { "Haute", "Moyenne", "Basse" });

            // Sélectionner une valeur par défaut
            comboBox1.SelectedIndex = 1;

        }

        

        private void TitreBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DescriptionBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Statut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Récupération des données depuis les champs
            string titre = TitreBox.Text; // Assurez-vous que TitreBox est le nom de votre TextBox
            string description = DescriptionBox.Text; // Assurez-vous que DescriptionBox existe
            DateTime debut = dateDebut.Value; // Assurez-vous que dateDebut est un DateTimePicker
            DateTime fin = dateTimePicker1.Value; // Assurez-vous que dateFin est un DateTimePicker
            string priorite = comboBox1.SelectedItem?.ToString(); // Assurez-vous que Priorite est un ComboBox
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
            

            // Chaîne de connexion à la base de données
            string connectionString = "Server=localhost; Database=GestionTachesDB; Integrated Security=True;";

            // Requête SQL pour insérer les données
            string query = @"INSERT INTO Objectif (titre, description, date_debut, date_fin, statut, Priorite, Id_utilisateur)
                             VALUES (@Titre, @Description, @DateDebut, @DateFin, @Statut, @Priorite, @IdUtilisateur)";

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
                        
                        // Exécution de la commande
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Vérification de l'insertion
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Obejectif ajoutée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Réinitialiser les champs après l'insertion (optionnel)
                            TitreBox.Clear();
                            DescriptionBox.Clear();
                            dateDebut.Value = DateTime.Now;
                            dateTimePicker1.Value = DateTime.Now;
                            comboBox1.SelectedIndex = -1;
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




        


        private void dateDebut_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
