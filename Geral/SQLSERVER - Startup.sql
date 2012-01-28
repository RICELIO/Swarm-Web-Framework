/*
==============================================================
SCRIPTS DE CRIAÇÃO >> SQL SERVER <<
==============================================================
*/

BEGIN TRAN

/* ============================= SEGURANÇA ============================= */
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario')
BEGIN
    BEGIN TRAN
        CREATE TABLE [Usuario] (
          [IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
          [Tipo] [int] NOT NULL,
          [Login] [varchar](15) NOT NULL,
		  [Senha] [varchar](50) NOT NULL,
		  [Avatar] [varchar](30) NULL,
		  [Habilitado] [bit] NOT NULL,
		  [Bloqueado] [bit] NOT NULL,
		  [TrocarSenha] [bit] NOT NULL,
		  CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED (IdUsuario) ON [PRIMARY]
        )
        CREATE INDEX IX_Usuario_IdUsuario ON [Usuario](IdUsuario) ON [PRIMARY]
    COMMIT
END

GO

/* == SENHA PADRÃO "admin" == */
INSERT INTO Usuario VALUES (1,'administrador','ISMvKXpXpadDiUoOSoAfww==',NULL,1,0,0);

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Grupo')
BEGIN
    BEGIN TRAN
        CREATE TABLE [Grupo] (
          [IdGrupo] [bigint] IDENTITY(1,1) NOT NULL,
          [Descricao] [varchar](150) NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED (IdGrupo) ON [PRIMARY]
        )
        CREATE INDEX IX_Grupo_IdGrupo ON [Grupo](IdGrupo) ON [PRIMARY]
    COMMIT
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Grupo') AND
   EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Usuario')
BEGIN
    BEGIN TRAN
        CREATE TABLE [GrupoUsuario] (
          [IdGrupoUsuario] [bigint] IDENTITY(1,1) NOT NULL,
          [IdGrupo] [bigint] NOT NULL,
          [IdUsuario] [bigint] NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_GrupoUsuario] PRIMARY KEY CLUSTERED (IdGrupoUsuario) ON [PRIMARY],
		  CONSTRAINT [FK_GrupoUsuario_IdGrupo] FOREIGN KEY([IdGrupo]) REFERENCES [dbo].[Grupo] ([IdGrupo]) ON DELETE CASCADE ON UPDATE CASCADE,
		  CONSTRAINT [FK_GrupoUsuario_IdUsuario] FOREIGN KEY([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE CASCADE ON UPDATE CASCADE
        )
        CREATE INDEX IX_GrupoUsuario_IdGrupoUsuario ON [GrupoUsuario](IdGrupoUsuario) ON [PRIMARY]
    COMMIT
END
/* ============================= SEGURANÇA ============================= */

GO

/* ============================= CONTROLE DE ACESSO ============================= */
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AcessoAmbiente')
BEGIN
    BEGIN TRAN
        CREATE TABLE [AcessoAmbiente] (
          [IdAcessoAmbiente] [bigint] IDENTITY(1,1) NOT NULL,
          [GUID] [varchar](50) NOT NULL,
          [Titulo] [varchar](150) NOT NULL,
          [Habilitado] [bit] NOT NULL,
          [Restrito] [bit] NOT NULL,
          [CodigoInterno] [int] NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_AcessoAmbiente] PRIMARY KEY CLUSTERED (IdAcessoAmbiente) ON [PRIMARY]
        )
        CREATE INDEX IX_AcessoAmbiente_IdAcessoAmbiente ON [AcessoAmbiente](IdAcessoAmbiente) ON [PRIMARY]
    COMMIT
END

GO

INSERT INTO AcessoAmbiente VALUES ('1febb761-7e5c-4e8b-8916-f0db37724289','Portal Administrativo',1,1,null,'administrador',GETDATE(),null)

GO

INSERT INTO AcessoAmbiente VALUES ('5b7a4cbd-4566-4a21-9a37-7b8abaa96761','Anônimo',1,0,2,'administrador',GETDATE(),null)

GO

INSERT INTO AcessoAmbiente VALUES ('9714073c-3e5c-44ab-a056-c988167d4d89','Callback',1,0,1,'administrador',GETDATE(),null)

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AcessoSuperGrupo')
BEGIN
    BEGIN TRAN
        CREATE TABLE [AcessoSuperGrupo] (
          [IdAcessoSuperGrupo] [bigint] IDENTITY(1,1) NOT NULL,
          [GUID] [varchar](50) NOT NULL,
          [Titulo] [varchar](150) NOT NULL,
          [Habilitado] [bit] NOT NULL,
          [Exibir] [bit] NOT NULL,
          [IdAmbiente] [bigint] NOT NULL,
          [CodigoInterno] [int] NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_AcessoSuperGrupo] PRIMARY KEY CLUSTERED (IdAcessoSuperGrupo) ON [PRIMARY],
		  CONSTRAINT [FK_AcessoSuperGrupo_IdAmbiente] FOREIGN KEY([IdAmbiente]) REFERENCES [dbo].[AcessoAmbiente] ([IdAcessoAmbiente]) ON DELETE CASCADE ON UPDATE CASCADE
        )
        CREATE INDEX IX_AcessoSuperGrupo_IdAcessoSuperGrupo ON [AcessoSuperGrupo](IdAcessoSuperGrupo) ON [PRIMARY]
    COMMIT
END

GO

DECLARE @IdAcessoAmbiente [bigint];
SET @IdAcessoAmbiente = (SELECT TOP 1 IdAcessoAmbiente FROM AcessoAmbiente WHERE GUID = '1febb761-7e5c-4e8b-8916-f0db37724289');
INSERT INTO AcessoSuperGrupo VALUES ('5dc0f243-7aa7-42c6-b4d1-cbb19c06f1f0','Super-Grupo de páginas que não serão exibidas para o Usuário',1,0,@IdAcessoAmbiente,1,'administrador',GETDATE(),null)
INSERT INTO AcessoSuperGrupo VALUES ('10a15003-7cd0-4bb6-b093-b2565a75d901','Administrativo',1,1,@IdAcessoAmbiente,NULL,'administrador',GETDATE(),NULL)

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AcessoGrupo')
BEGIN
    BEGIN TRAN
        CREATE TABLE [AcessoGrupo] (
          [IdAcessoGrupo] [bigint] IDENTITY(1,1) NOT NULL,
          [GUID] [varchar](50) NOT NULL,
          [Titulo] [varchar](150) NOT NULL,
          [Habilitado] [bit] NOT NULL,
          [Exibir] [bit] NOT NULL,
          [IdSuperGrupo] [bigint] NOT NULL,
          [CodigoInterno] [int] NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_AcessoGrupo] PRIMARY KEY CLUSTERED (IdAcessoGrupo) ON [PRIMARY],
		  CONSTRAINT [FK_AcessoGrupo_IdSuperGrupo] FOREIGN KEY([IdSuperGrupo]) REFERENCES [dbo].[AcessoSuperGrupo] ([IdAcessoSuperGrupo]) ON DELETE CASCADE ON UPDATE CASCADE
        )
        CREATE INDEX IX_AcessoGrupo_IdAcessoGrupo ON [AcessoGrupo](IdAcessoGrupo) ON [PRIMARY]
    COMMIT
END

GO

DECLARE @IdAcessoSuperGrupo [bigint];
SET @IdAcessoSuperGrupo = (SELECT TOP 1 IdAcessoSuperGrupo FROM AcessoSuperGrupo WHERE GUID = '5dc0f243-7aa7-42c6-b4d1-cbb19c06f1f0');
INSERT INTO AcessoGrupo VALUES ('e34690cc-0fda-426b-82b6-97602f7e14b3','Grupo de páginas que não serão exibidas para o Usuário',1,0,@IdAcessoSuperGrupo,1,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoSuperGrupo [bigint];
SET @IdAcessoSuperGrupo = (SELECT TOP 1 IdAcessoSuperGrupo FROM AcessoSuperGrupo WHERE GUID = '10a15003-7cd0-4bb6-b093-b2565a75d901');
INSERT INTO AcessoGrupo VALUES ('017a5be9-509d-4ab0-a912-5ac79f4b6cf0','Sistema',1,1,@IdAcessoSuperGrupo,NULL,'administrador',GETDATE(),null)
INSERT INTO AcessoGrupo VALUES ('689498e3-a949-4296-88b4-8841ced27825','Segurança',1,1,@IdAcessoSuperGrupo,NULL,'administrador',GETDATE(),null)

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AcessoFuncionalidade')
BEGIN
    BEGIN TRAN
        CREATE TABLE [AcessoFuncionalidade] (
          [IdAcessoFuncionalidade] [bigint] IDENTITY(1,1) NOT NULL,
          [GUID] [varchar](50) NOT NULL,
          [Titulo] [varchar](150) NOT NULL,
          [Descricao] [varchar](300) NULL,
          [Habilitado] [bit] NOT NULL,
          [Exibir] [bit] NOT NULL,
          [IdGrupo] [bigint] NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_AcessoFuncionalidade] PRIMARY KEY CLUSTERED (IdAcessoFuncionalidade) ON [PRIMARY],
		  CONSTRAINT [FK_AcessoFuncionalidade_IdGrupo] FOREIGN KEY([IdGrupo]) REFERENCES [dbo].[AcessoGrupo] ([IdAcessoGrupo]) ON DELETE CASCADE ON UPDATE CASCADE
        )
        CREATE INDEX IX_AcessoFuncionalidade_IdAcessoFuncionalidade ON [AcessoFuncionalidade](IdAcessoFuncionalidade) ON [PRIMARY]
    COMMIT
END

GO

DECLARE @IdAcessoGrupo [bigint];
SET @IdAcessoGrupo = (SELECT TOP 1 IdAcessoGrupo FROM AcessoGrupo WHERE GUID = 'e34690cc-0fda-426b-82b6-97602f7e14b3');
INSERT INTO AcessoFuncionalidade VALUES ('ea0bd008-9b63-4c67-83ec-f321c4da28f8','Seleção de Funcionalidade','Esta página é responsável pela gestão dos itens a serem exibidos para o usuário selecionar.',1,0,@IdAcessoGrupo,'administrador',GETDATE(),null)
INSERT INTO AcessoFuncionalidade VALUES ('cd8415a8-3853-42f0-a375-983a5f45f795','Perfil','Esta página é responsável pela manutenção do perfil de acesso do usuário logado.',1,0,@IdAcessoGrupo,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoGrupo [bigint];
SET @IdAcessoGrupo = (SELECT TOP 1 IdAcessoGrupo FROM AcessoGrupo WHERE GUID = '017a5be9-509d-4ab0-a912-5ac79f4b6cf0');
INSERT INTO AcessoFuncionalidade VALUES ('f50df45f-2b04-443e-934f-1ae40fc33384','Controle de Navegação','Responsável pela manutenção das funcionalidades disponibilizadas no sistema. Sua utilização não é recomendada para usuários que não tenham pleno conhecimento referente a mecânica de funcionamento do sistema.',1,1,@IdAcessoGrupo,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoGrupo [bigint];
SET @IdAcessoGrupo = (SELECT TOP 1 IdAcessoGrupo FROM AcessoGrupo WHERE GUID = '689498e3-a949-4296-88b4-8841ced27825');
INSERT INTO AcessoFuncionalidade VALUES ('f8e67837-79e6-47f6-b23c-94bbf4b818de','Usuários','Funcionalide responsável pela manutenção dos usuários que utilizam o sistema.',1,1,@IdAcessoGrupo,'administrador',GETDATE(),null)

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AcessoMap')
BEGIN
    BEGIN TRAN
        CREATE TABLE [AcessoMap] (
          [IdAcessoMap] [bigint] IDENTITY(1,1) NOT NULL,
          [Tipo] [int] NOT NULL,
          [IdAcesso] [bigint] NOT NULL,
          [UrlMapID] [int] NOT NULL,
          [Principal] [bit] NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_AcessoMap] PRIMARY KEY CLUSTERED (IdAcessoMap) ON [PRIMARY]
        )
        CREATE INDEX IX_AcessoMap_IdAcessoMap ON [AcessoMap](IdAcessoMap) ON [PRIMARY]
    COMMIT
END

GO

DECLARE @IdAcessoAmbiente [bigint];
SET @IdAcessoAmbiente = (SELECT TOP 1 IdAcessoAmbiente FROM AcessoAmbiente WHERE GUID = '1febb761-7e5c-4e8b-8916-f0db37724289');
INSERT INTO AcessoMap VALUES (1,@IdAcessoAmbiente,6,1,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoFuncionalidade [bigint];
SET @IdAcessoFuncionalidade = (SELECT TOP 1 IdAcessoFuncionalidade FROM AcessoFuncionalidade WHERE GUID = 'ea0bd008-9b63-4c67-83ec-f321c4da28f8');
INSERT INTO AcessoMap VALUES (4,@IdAcessoFuncionalidade,7,1,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoFuncionalidade [bigint];
SET @IdAcessoFuncionalidade = (SELECT TOP 1 IdAcessoFuncionalidade FROM AcessoFuncionalidade WHERE GUID = 'f50df45f-2b04-443e-934f-1ae40fc33384');
INSERT INTO AcessoMap VALUES (4,@IdAcessoFuncionalidade,8,1,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoFuncionalidade [bigint];
SET @IdAcessoFuncionalidade = (SELECT TOP 1 IdAcessoFuncionalidade FROM AcessoFuncionalidade WHERE GUID = 'f8e67837-79e6-47f6-b23c-94bbf4b818de');
INSERT INTO AcessoMap VALUES (4,@IdAcessoFuncionalidade,9,1,'administrador',GETDATE(),null)

GO

DECLARE @IdAcessoFuncionalidade [bigint];
SET @IdAcessoFuncionalidade = (SELECT TOP 1 IdAcessoFuncionalidade FROM AcessoFuncionalidade WHERE GUID = 'cd8415a8-3853-42f0-a375-983a5f45f795');
INSERT INTO AcessoMap VALUES (4,@IdAcessoFuncionalidade,10,1,'administrador',GETDATE(),null)
/* ============================= CONTROLE DE ACESSO ============================= */

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Permissao')
BEGIN
    BEGIN TRAN
        CREATE TABLE [Permissao] (
          [IdPermissao] [bigint] IDENTITY(1,1) NOT NULL,
          [IdUsuario] [bigint] NULL,
          [IdGrupo] [bigint] NULL,
          [GUID] [varchar](50) NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_Permissao] PRIMARY KEY CLUSTERED (IdPermissao) ON [PRIMARY],
		  CONSTRAINT [FK_Permissao_IdUsuario] FOREIGN KEY([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE CASCADE ON UPDATE CASCADE,
		  CONSTRAINT [FK_Permissao_IdGrupo] FOREIGN KEY([IdGrupo]) REFERENCES [dbo].[GrupoUsuario] ([IdGrupoUsuario]) ON DELETE NO ACTION ON UPDATE NO ACTION
        )
        CREATE INDEX IX_Permissao_IdPermissao ON [Permissao](IdPermissao) ON [PRIMARY]
    COMMIT
END

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ControleVersao')
BEGIN
    BEGIN TRAN
        CREATE TABLE [ControleVersao] (
          [IdControleVersao] [bigint] IDENTITY(1,1) NOT NULL,
          [Versao] [varchar](10) NOT NULL,
          [Notas] [text] NOT NULL,
		  [Log] [datetime] NOT NULL,
		  [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_ControleVersao] PRIMARY KEY CLUSTERED (IdControleVersao) ON [PRIMARY]
        )
        CREATE INDEX IX_ControleVersao_IdControleVersao ON [ControleVersao](IdControleVersao) ON [PRIMARY]
    COMMIT
END

GO

INSERT INTO ControleVersao VALUES ('1.0.1','Implantação do protótipo.',GETDATE(),'administrador',GETDATE(),null);

GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'ConfiguracoesGerais')
BEGIN
    BEGIN TRAN
        CREATE TABLE [ConfiguracoesGerais] (
          [IdConfiguracoesGerais] [smallint] IDENTITY(1,1) NOT NULL,
          [IdUsuarioMaster] [bigint] NULL,
          [HabilitarDemonstracao] [bit] NOT NULL,
          [TituloDemonstracao] [varchar](150) NOT NULL,
          [MensagemDemonstracao] [text] NOT NULL,
          [TituloProduto] [varchar](150) NOT NULL,
          [TipoApresentacao] [int] NOT NULL,
          [TituloApresentacao] [varchar](150) NOT NULL,
          [MensagemApresentacao] [text] NULL,
          [ImagemApresentacao] [varchar](60) NULL,
          [TentativasAcesso] [int] NOT NULL,
          [Usuario] [varchar](50) NOT NULL,
          [DataCriacao] [datetime] NULL,
          [UltimaAtualizacao] [datetime] NULL,
		  CONSTRAINT [PK_ConfiguracoesGerais] PRIMARY KEY CLUSTERED (IdConfiguracoesGerais) ON [PRIMARY],
		  CONSTRAINT [FK_ConfiguracoesGerais_IdUsuario] FOREIGN KEY([IdUsuarioMaster]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE SET NULL ON UPDATE CASCADE,
        )
        CREATE INDEX IX_ConfiguracoesGerais_IdConfiguracoesGerais ON [ConfiguracoesGerais](IdConfiguracoesGerais) ON [PRIMARY]
    COMMIT
END

GO

INSERT INTO ConfiguracoesGerais VALUES
(
 null,1,
 'Sistema de Controle de Obras',
 'Voc&ecirc; est&aacute; acessando uma vers&atilde;o de demonstra&ccedil;&atilde;o do sistema de Controle de Obras que est&aacute; sendo desenvolvido pela Swarm Consultoria e Sistemas. Para adquirir acesso ao mesmo &ecirc; necess&aacute;rio que entre em contato com o nosso consultor Marcos Aur&eacute;lio Gon&ccedil;alves Guarnier atrav&eacute;s do e-mail: <a href="mailto:mguarnier@gmail.com">mguarnier@gmail.com</a>.',
 '&copy;&nbsp;2011 Swarm Consultoria e Sistemas - Controle de Obras',
 1,'Mensagem/Informa&ccedil;&atilde;o padr&atilde;o de apresenta&ccedil;&atilde;o do sistema',
 'Este bloco do sistema &eacute; configurado pelos usu&aacute;rios com acesso administrativo ao mesmo, podendo informar um texto de entrada ou disponibilizar a logomarca da empresa ou alguma outra imagem desej&aacute;vel. Para isto basta estar acessando as configura&ccedil;&otilde;es gerais do mesmo.',
 null,3,'administrador',GETDATE(),null
);

GO

UPDATE ConfiguracoesGerais SET IdUsuarioMaster = (SELECT TOP 1 IdUsuario FROM Usuario WHERE [Login] = 'administrador')

COMMIT