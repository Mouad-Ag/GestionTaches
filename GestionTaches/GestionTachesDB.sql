-- Création de la base de données
CREATE DATABASE GestionTachesDB

-- Utilisation de la base de données
USE GestionTachesDB

-- Création de la table Utilisateur
CREATE TABLE Utilisateur (
    Id_utilisateur INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(255),
    prenom VARCHAR(255),
    date_naissance DATE,
    email VARCHAR(255) UNIQUE,
    mot_de_passe VARCHAR(255)
)

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

-- Création de la table Notification
CREATE TABLE Notification (
    Id_notification INT IDENTITY(1,1) PRIMARY KEY,
    date_envoie DATETIME,
    type VARCHAR(50),
    Id_utilisateur INT,
    Id_objectif INT,
    Id_tache INT,
    FOREIGN KEY (Id_utilisateur) REFERENCES Utilisateur(Id_utilisateur),
    FOREIGN KEY (Id_objectif) REFERENCES Objectif(Id_objectif),
    FOREIGN KEY (Id_tache) REFERENCES Tache(Id_tache)
)
