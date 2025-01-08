-- Création de la base de données
CREATE DATABASE GestionTachesDB
GO

-- Utilisation de la base de données
USE GestionTachesDB
GO

-- Création de la table Utilisateur
CREATE TABLE Utilisateur (
    Id_utilisateur INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(255),
    prenom VARCHAR(255),
    date_naissance DATE,
    email VARCHAR(255) UNIQUE,
    mot_de_passe VARCHAR(255)
)
GO

-- Création de la table Objectif
CREATE TABLE Objectif (
    Id_objectif INT IDENTITY(1,1) PRIMARY KEY,
    titre VARCHAR(255),
    description TEXT,
    date_debut DATE,
    date_fin DATE,
    statut VARCHAR(50) NOT NULL CHECK (statut IN ('Non commencé', 'En cours', 'Terminé')),
    Id_utilisateur INT,
    FOREIGN KEY (Id_utilisateur) REFERENCES Utilisateur(Id_utilisateur)
)
GO

-- Création de la table Tache
CREATE TABLE Tache (
    Id_tache INT IDENTITY(1,1) PRIMARY KEY,
    titre VARCHAR(255),
    description TEXT,
    date_debut DATE,
    date_limite DATE,
    statut VARCHAR(50) NOT NULL CHECK (statut IN ('Non commencé', 'En cours', 'Terminé', 'Abandonné')),
    Id_utilisateur INT,
    Id_objectif INT,
    FOREIGN KEY (Id_utilisateur) REFERENCES Utilisateur(Id_utilisateur),
    FOREIGN KEY (Id_objectif) REFERENCES Objectif(Id_objectif)
)
GO

-- Création de la table Notification
CREATE TABLE Notification (
    Id_notification INT IDENTITY(1,1) PRIMARY KEY,
    date_envoie DATETIME,
    type VARCHAR(50),
	message TEXT,
    Id_utilisateur INT,
    Id_objectif INT,
    Id_tache INT,
    FOREIGN KEY (Id_utilisateur) REFERENCES Utilisateur(Id_utilisateur),
    FOREIGN KEY (Id_objectif) REFERENCES Objectif(Id_objectif),
    FOREIGN KEY (Id_tache) REFERENCES Tache(Id_tache)
)
GO

INSERT INTO GestionTachesDB.dbo.Utilisateur (nom, prenom, date_naissance, email, mot_de_passe)
VALUES
('Dupont', 'Jean', '1985-03-12', 'jean.dupont@example.com', 'motdepasse1'),
('Martin', 'Alice', '1990-07-23', 'alice.martin@example.com', 'motdepasse2'),
('Durand', 'Paul', '1982-11-05', 'paul.durand@example.com', 'motdepasse3')
GO

INSERT INTO GestionTachesDB.dbo.Objectif (titre, description, date_debut, date_fin, statut, Id_utilisateur)
VALUES 
('Objectif 1', 'Terminer le projet X', '2025-01-01', '2025-06-30', 'Non commencé', 1),
('Objectif 2', 'Améliorer le processus Y', '2025-01-05', '2025-12-31', 'En cours', 2)
GO

INSERT INTO GestionTachesDB.dbo.Tache (titre, description, date_debut, date_limite, statut, Id_utilisateur, Id_objectif)
VALUES 
('Tâche 1', 'Préparer la présentation du projet X', '2025-01-01', '2025-01-10', 'Non commencé', 1, 1),
('Tâche 2', 'Analyser les données du projet X', '2025-01-02', '2025-01-15', 'En cours', 1, 1),
('Tâche 3', 'Organiser les réunions hebdomadaires', '2025-01-03', '2025-01-20', 'Terminé', 2, 2),
('Tâche 4', 'Réviser la documentation du projet Y', '2025-01-04', '2025-01-25', 'Abandonné', 2, 2)
GO

-- Insertion d'exemples de notifications
INSERT INTO Notification (date_envoie, type, message, Id_utilisateur, Id_objectif, Id_tache)
VALUES
('2025-01-06 10:16:00', 'Rappel Tâche', 'N’oubliez pas de terminer la tâche 1.', 1, NULL, 1),
('2025-01-06 15:00:00', 'Rappel Objectif', 'L’objectif 2 doit être complété aujourd’hui.', 1, 2, NULL),
('2025-01-06 16:00:00', 'Notification Générale', 'Il est temps de planifier vos prochaines tâches.', 1, NULL, NULL);
GO

INSERT INTO Notification (date_envoie, type, message, Id_utilisateur, Id_objectif, Id_tache)
VALUES
('2025-01-07 12:48:00', 'Rappel Tâche', 'N’oubliez pas de terminer cette nouvelle tâche.', 1, NULL, (SELECT MAX(Id_tache) FROM Tache));

SELECT * FROM Notification WHERE date_envoie >= GETDATE();
DELETE FROM Utilisateur
GO

alter FUNCTION GetTachesParStatutEtUtilisateur (@Statut VARCHAR(50), @Id_utilisateur INT, @Id_objectif INT) RETURNS TABLE
AS
RETURN
(
    SELECT Id_tache, titre, description, date_debut, date_limite, statut
    FROM GestionTachesDB.dbo.Tache
    WHERE statut = @Statut 
    AND Id_utilisateur = @Id_utilisateur
    AND Id_objectif = @Id_objectif
)
GO


CREATE PROCEDURE UpdateTache (@Id_tache INT, @Titre VARCHAR(255), @Description TEXT, @DateDebut DATE, @DateLimite DATE, @Statut VARCHAR(50))
AS
UPDATE GestionTachesDB.dbo.Tache
SET 
    titre = @Titre,
    description = @Description,
    date_debut = @DateDebut,
    date_limite = @DateLimite,
    statut = @Statut
WHERE Id_tache = @Id_tache
GO

alter FUNCTION GetObjectifsParStatutEtUtilisateur (@Statut VARCHAR(50), @Id_utilisateur INT) RETURNS TABLE
AS
RETURN
(
SELECT Id_objectif, titre, description, date_debut, date_fin, statut
FROM GestionTachesDB.dbo.Objectif
WHERE statut = @Statut AND Id_utilisateur = @Id_utilisateur
)
GO

CREATE PROCEDURE UpdateObjectif (@Id_objectif INT, @Titre VARCHAR(255), @Description TEXT, @DateDebut DATE, @DateFin DATE, @Statut VARCHAR(50))
AS
BEGIN
UPDATE GestionTachesDB.dbo.Objectif
SET 
    titre = @Titre,
    description = @Description,
    date_debut = @DateDebut,
    date_fin = @DateFin,
    statut = @Statut
WHERE Id_objectif = @Id_objectif
END
GO

INSERT INTO Utilisateur (nom, prenom, date_naissance, email, mot_de_passe) 
VALUES 
('Dupont', 'Jean', '1990-01-15', 'jean.dupont@example.com', 'password123'),
('Martin', 'Sophie', '1985-06-20', 'sophie.martin@example.com', 'mypassword'),
('Doe', 'John', '1992-03-10', 'john.doe@example.com', 'john123');
SELECT * FROM Objectif;


-- Objectifs pour l'utilisateur avec Id_utilisateur = 1 (Jean Dupont)
INSERT INTO Objectif (titre, description, date_debut, date_fin, statut, Id_utilisateur) 
VALUES 
('Apprendre C#', 'Compléter le tutoriel C# en ligne', '2025-01-01', '2025-01-15', 'Non commencé', 5),
('Créer une application', 'Développer une application Windows Forms', '2025-01-05', '2025-01-20', 'En cours', 5),
('Préparer une présentation', 'Créer des diapositives pour le projet final', '2024-12-20', '2024-12-25', 'Terminé', 5);

-- Objectifs pour l'utilisateur avec Id_utilisateur = 2 (Sophie Martin)
INSERT INTO Objectif (titre, description, date_debut, date_fin, statut, Id_utilisateur) 
VALUES 
('Lecture technique', 'Lire le livre sur les bases de données avancées', '2025-01-10', '2025-01-30', 'Non commencé', 6),
('Rédiger un rapport', 'Préparer un rapport sur les nouvelles fonctionnalités', '2025-01-01', '2025-01-10', 'En cours', 6),
('Finaliser un projet', 'Compléter le projet de développement web', '2024-12-01', '2024-12-15', 'Terminé', 6);

INSERT INTO Tache (titre, description, date_debut, date_limite, statut, Id_utilisateur, Id_objectif)
VALUES 
('Configurer l’environnement', 'Installer Visual Studio et configurer le projet', '2025-01-01', '2025-01-05', 'Non commencé', 5, 7),
('Créer le formulaire principal', 'Développer le formulaire Windows Forms principal', '2025-01-05', '2025-01-10', 'En cours', 5, 7),
('Tester l’application', 'Exécuter des tests sur les fonctionnalités développées', '2025-01-15', '2025-01-20', 'En cours', 6, 8);

