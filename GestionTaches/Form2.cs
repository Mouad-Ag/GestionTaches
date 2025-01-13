using System.Windows.Forms;
using System;
using System.Data.SqlClient;



namespace GestionTaches
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string prenom = txtprenom.Text;
            string nom = txtNom.Text;
            DateTime dateNaissance = datenes.Value; // Assurez-vous que la date est bien formatée
            string email = txtEmail.Text;
            string motDePasse = txtmotdepasse1.Text;

            // Chaîne de connexion à la base de données
            string connectionString = "Server=LAPULGA\\SQLEXPRESS;Database=GestionTachesDB;Integrated Security=True;";

            // Requête SQL d'insertion
            string query = "INSERT INTO Utilisateur (nom, prenom, date_naissance, email, mot_de_passe) " +
                           "VALUES (@Nom, @Prenom, @DateNaissance, @Email, @MotDePasse)";

            // Connexion à la base de données
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Ouvrir la connexion
                    connection.Open();

                    // Créer une commande SQL
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Ajouter les paramètres à la commande pour éviter les injections SQL
                        cmd.Parameters.AddWithValue("@Nom", nom);
                        cmd.Parameters.AddWithValue("@Prenom", prenom);
                        cmd.Parameters.AddWithValue("@DateNaissance", dateNaissance);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@MotDePasse", motDePasse);

                        // Exécuter la commande d'insertion
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Vérifier si l'insertion a réussi
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Utilisateur enregistré avec succès !");
                            login obj = new login();
                            obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de l'enregistrement de l'utilisateur.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Afficher un message d'erreur en cas d'exception
                    MessageBox.Show("Erreur de connexion à la base de données: " + ex.Message);
                }
            }
        }
    }
}