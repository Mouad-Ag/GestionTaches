using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GestionTaches
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtUserName.Text;
            string password = txtPassword.Text;

            // Connexion à la base de données
            string connectionString = "Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Integrated Security=True;";
            int userId = -1; // Variable pour stocker l'Id_utilisateur

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Requête pour récupérer l'Id_utilisateur correspondant
                    string query = "SELECT Id_utilisateur FROM Utilisateur WHERE email = @Email AND mot_de_passe = @Password";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Ajouter les paramètres pour éviter les injections SQL
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        // Exécuter la commande
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            // Convertir le résultat en entier
                            userId = Convert.ToInt32(result);

                            // Connexion réussie, afficher un message
                            MessageBox.Show($"Connexion réussie!");

                            // Naviguer vers une autre page (exemple SignUp)
                            FormAccueil obj = new FormAccueil(userId);
                            obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            // Si aucun utilisateur correspondant n'a été trouvé
                            MessageBox.Show("Email ou mot de passe incorrect!");
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Erreur SQL: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur: " + ex.Message);
                }
            }

            // Si nécessaire, utiliser userId pour d'autres opérations
            if (userId > 0)
            {
                // Exemple : Utilisation de userId
                Console.WriteLine($"Utilisateur connecté avec ID: {userId}");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SignUp signUpPage = new SignUp();
            signUpPage.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
