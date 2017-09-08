
CREATE TABLE [dbo].[Utilisateurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Courriel] [nvarchar](50) NOT NULL,
	[MDP] [nvarchar](70) NOT NULL,
	[NomFamille] [nvarchar](50) NOT NULL,
	[Prenom] [nvarchar](50) NOT NULL,
	[Langue] [nchar](5) NOT NULL,
 CONSTRAINT [PK_Utilisateurs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Utilisateurs_Courriel] UNIQUE NONCLUSTERED 
(
	[Courriel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Utilisateurs] ADD  CONSTRAINT [DF_Utilisateur_Langue]  DEFAULT ('fr-CA') FOR [Langue]
GO

INSERT INTO Utilisateurs (Courriel, MDP, NomFamille, Prenom, Langue) VALUES ('m@m.com', '1000:ikdrAPIDBhysTmLPhhqfeJC39rCS9tDe:f4e4oufG7NHLwlufnzzuBW10wxRjM4ZY', 'Tremblay', 'Mohand', 'fr-CA');
GO

CREATE TABLE [dbo].[Articles](
	[Titre] [nvarchar](100) NOT NULL,
	[Contenu] [nvarchar](max) NOT NULL,
	[Revision] [int] NOT NULL,
	[IdContributeur] [int] NOT NULL,
	[DateModification] [datetime] NOT NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[Titre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Articles] ADD CONSTRAINT [DF_Articles_Revision]  DEFAULT ((1)) FOR [Revision]
GO
ALTER TABLE [dbo].[Articles] ADD CONSTRAINT [DF_Articles_IdContributeur]  DEFAULT ((1)) FOR [IdContributeur]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Utilisateurs] FOREIGN KEY([IdContributeur])
REFERENCES [dbo].[Utilisateurs] ([Id])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Utilisateurs]
GO

CREATE PROCEDURE [dbo].[AddArticle]
	@Titre nvarchar(100),
	@Contenu nvarchar(MAX),
	@IdContributeur int

AS
BEGIN

	SET NOCOUNT OFF;
	insert into Articles (Titre, Contenu, IdContributeur, DateModification, Revision)
	values (@Titre,@Contenu,@IdContributeur, getdate(), 1);

END

GO


CREATE PROCEDURE [dbo].[AddUtilisateur]
	@Courriel Nvarchar(50),
	@Mdp Nvarchar(70),
	@Prenom NvarChar(50),
	@NomFamille Nvarchar(50),
	@Langue Nchar(5) = null

AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Utilisateurs (Mdp,Prenom,NomFamille,Courriel,Langue) 	
	VALUES (@Mdp,@Prenom,@NomFamille,@Courriel,@Langue) 

	SELECT scope_identity()

END
GO


create PROCEDURE [dbo].[DeleteArticle]
	@Titre nvarchar(100)
AS
BEGIN

	SET NOCOUNT OFF;

   delete from articles where Titre = @Titre
END

GO

CREATE PROCEDURE [dbo].[FindArticle] 
	@Titre nvarchar(100)
AS
BEGIN

	SET NOCOUNT ON;

   
	SELECT * from articles where titre = @Titre
END

GO


CREATE procedure [dbo].[FindUtilisateurByCourriel] 
	@Courriel nvarChar(50)
as
	Select * from Utilisateurs
	Where Courriel = @Courriel


GO


Create procedure [dbo].[FindUtilisateurById] 
	@Id int
as
	Select * from Utilisateurs
	Where Id = @Id


GO

Create PROCEDURE [dbo].[GetArticles]

AS
BEGIN

	SET NOCOUNT ON;

    Select * from articles
END

GO

create PROCEDURE [dbo].[GetTitresArticles]

AS
BEGIN

	SET NOCOUNT ON;

    Select titre from articles order by titre
END
GO

CREATE PROCEDURE [dbo].[GetArticlesByContributeur]
	@id int

AS
BEGIN

	SET NOCOUNT ON;

    Select * from articles
	Where idContributeur = @id
END
GO

CREATE PROCEDURE [dbo].[UpdateArticle] 
	@Titre nvarchar(100),
	@Contenu nvarchar (MAX),
	@IdContributeur int
AS
BEGIN

	SET NOCOUNT OFF;

    update Articles
	set Contenu = @Contenu,IdContributeur = @IdContributeur, DateModification=getdate(), revision = revision + 1
	where Titre = @Titre
END

GO

CREATE PROCEDURE [dbo].[UpdateMotDePasse]
	@id int, 
	@nouveauMDP nvarchar(70)
AS
	UPDATE	Utilisateurs
	SET		Mdp = @nouveauMDP
	WHERE	Id = @id


GO

CREATE PROCEDURE [dbo].[UpdateUtilisateur]
	
	@NomFamille		nvarchar(50),
	@Prenom			nvarchar(50),
	@Id             int,
	@Langue Nchar(5) = null
AS
UPDATE Utilisateurs
SET  NomFamille = @NomFamille, Prenom = @Prenom, Langue = @Langue 
WHERE Id = @Id


GO
